using System;
using System.Collections.Generic;
using System.Threading;
using Core.Hotkeys.Exceptions;
using WinApi.User32;

namespace Core.Hotkeys
{
    /// <summary>
    /// Registers and starts the hotkey thread.
    /// </summary>
    public class HotkeyRunner
    {
        private static int _currentId;
        private bool Running { get; set; }
        private readonly Dictionary<int, Hotkey> _hotkeyMap;
        private readonly Dictionary<Hotkey, Action> _callbacks;
        private readonly IntPtr _handle;

        /// <summary>
        /// Creates a hotkey runner using a dictionary of hotkey-callback pairs.
        /// </summary>
        /// <param name="callbacks">The dictionary of hotkey-callback pairs.</param>
        public HotkeyRunner(Dictionary<Hotkey, Action> callbacks)
        {
            this._callbacks = callbacks;

            _hotkeyMap = new Dictionary<int, Hotkey>();
            foreach (Hotkey hotkey in callbacks.Keys)
            {
                _hotkeyMap.Add(_currentId++, hotkey);
            }

            Running = false;
        }

        /// <summary>
        /// Creates a hotkey runner using a dictionary of hotkey-callback pairs.
        /// </summary>
        /// <param name="callbacks">The dictionary of hotkey-callback pairs.</param>
        /// <param name="handle">A window to associate the hotkey registration with.</param>
        public HotkeyRunner(Dictionary<Hotkey, Action> callbacks, IntPtr handle) : this(callbacks)
        {
            this._handle = handle;
        }
        
        /// <summary>
        /// Starts the hotkey thread. 
        /// </summary>
        public void Start()
        {
            if (!Running)
            {
                Running = true;
                new Thread(this.RunnerFunction).Start();
            }
        }

        /// <summary>
        /// Stops the hotkey thread.
        /// </summary>
        public void Stop()
        {
            Running = false;
        }

        /// <summary>
        /// Registers a hotkey.
        /// </summary>
        /// <exception cref="HotkeyRegistrationException">Thrown if an invalid hotkey is registered.</exception>
        private void RegisterHotkeys()
        {
            IList<int> registeredIds = new List<int>();
            foreach (var entry in this._hotkeyMap)
            {
                if (User32Methods.RegisterHotKey(
                    this._handle,
                    entry.Key,
                    entry.Value.modifiers,
                    entry.Value.key))
                {
                    registeredIds.Add(entry.Key);
                }
                else
                {
                    DeregisterHotkeys(registeredIds);
                    throw new HotkeyRegistrationException(entry.Value);
                }
            }
        }

        /// <summary>
        /// De-registers the hotkeys provided.
        /// </summary>
        /// <param name="ids">A list of hotkey IDs that have been successfully registered.</param>
        private void DeregisterHotkeys(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                User32Methods.UnregisterHotKey(this._handle, id);
            }
        }

        /// <summary>
        /// The hotkey thread's target.
        /// Listens for messages related to hotkey presses.
        /// </summary>
        private void RunnerFunction()
        {
            RegisterHotkeys();
            while (Running &&
                   User32Methods.GetMessage(out var message, IntPtr.Zero, 0, 0) != 0)
            {
                if (message.Value == 0x0312)
                {
                    int key = message.WParam.ToInt32();
                    if (this._hotkeyMap.ContainsKey(key))
                    {
                        Hotkey hotkey = this._hotkeyMap[key];
                        Action callback = this._callbacks[hotkey];
                        callback();
                    }
                    else
                    {
                        Console.Error.WriteLine($"Invalid hotkey ID returned: {key}");
                    }
                }
            }
            DeregisterHotkeys(this._hotkeyMap.Keys);
        }
        
    }
}