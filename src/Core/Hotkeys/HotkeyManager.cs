using System;
using System.Collections.Generic;
using WinApi.User32;

namespace Core.Hotkeys
{
    public class HotkeyManager: IDisposable
    {
        private Dictionary<Hotkey, Action> hotkeys;
        private HotkeyRunner Runner { get; set; }
        public HotkeyManager()
        {
            this.hotkeys = new Dictionary<Hotkey, Action>();
        }

        public void Register(string key, string modifiers, Action callback)
        {
            hotkeys.Add(new Hotkey(key, modifiers), callback);
        }

        public void Deregister(string key, string modifiers)
        {
            hotkeys.Remove(new Hotkey(key, modifiers));
        }

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