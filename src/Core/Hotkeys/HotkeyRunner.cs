using System;
using System.Collections.Generic;
using System.Threading;
using WinApi.User32;

namespace Core.Hotkeys
{
    public class HotkeyRunner
    {
        private static int _currentId;
        private bool Running { get; set; }
        private readonly Dictionary<int, Hotkey> _hotkeyMap;
        private readonly Dictionary<Hotkey, Action> _callbacks;
        private readonly IntPtr _handle;

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

        public HotkeyRunner(Dictionary<Hotkey, Action> callbacks, IntPtr handle) : this(callbacks)
        {
            this._handle = handle;
        }

        public void Start()
        {
            if (!Running)
            {
                Running = true;
                new Thread(this.RunnerFunction).Start();
            }
        }

        public void Stop()
        {
            Running = false;
        }

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

        private void DeregisterHotkeys(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                User32Methods.UnregisterHotKey(this._handle, id);
            }
        }

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