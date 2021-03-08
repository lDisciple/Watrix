using System;
using System.Collections.Generic;

namespace Core.Hotkeys
{
    /// <summary>
    /// Builds and manages a set of hotkeys to register with callbacks.
    /// </summary>
    public class HotkeyManager: IDisposable
    {
        private Dictionary<Hotkey, Action> hotkeys;
        private HotkeyRunner Runner { get; set; }
        
        /// <summary>
        /// Creates an empty set of hotkeys for registration.
        /// </summary>
        public HotkeyManager()
        {
            this.hotkeys = new Dictionary<Hotkey, Action>();
        }

        /// <summary>
        /// Registers a hotkey.
        /// </summary>
        /// <param name="key">The string representation of a keyboard key.</param>
        /// <param name="modifiers">A '+'-separated list of modifier values (without the 'MOD_' prefix).</param>
        /// <param name="callback">The function to be called when the hotkey is pressed.</param>
        /// <remarks>Any callback registered will be run on a separate thread dedicated to hotkeys.</remarks>
        public void Register(string key, string modifiers, Action callback)
        {
            hotkeys.Add(new Hotkey(key, modifiers), callback);
        }

        /// <summary>
        /// De-registers a hotkey.
        /// </summary>
        /// <param name="key">The string representation of a keyboard key.</param>
        /// <param name="modifiers">A '+'-separated list of modifier values (without the 'MOD_' prefix).</param>
        public void Deregister(string key, string modifiers)
        {
            hotkeys.Remove(new Hotkey(key, modifiers));
        }

        /// <summary>
        /// Starts a new thread where the hotkeys are registered and monitored.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if a hotkey manager is already running.</exception>
        public void Start()
        {
            if (Runner == null)
            {
                Runner = new HotkeyRunner(this.hotkeys);
                Runner.Start();
            }
            else
            {
                throw new InvalidOperationException("This manager already has a running process. " +
                                                    "Please stop the current runner before starting a new one.");
            }
        }

        /// <summary>
        /// Stops the current running hotkey thread.
        /// </summary>
        public void Stop()
        {
            if (Runner != null)
            {
                Runner.Stop();
                Runner = null;
            }
        }

        public void Dispose()
        {
            this.Runner.Stop();
        }
    }
}