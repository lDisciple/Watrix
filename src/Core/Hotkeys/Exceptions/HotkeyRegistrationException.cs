using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Hotkeys
{
    public class HotkeyRegistrationException: Exception
    {
        public Hotkey hotkey { get; }
        public HotkeyRegistrationException(Hotkey hotkey) : base("Failed to register hotkey.")
        {
            this.hotkey = hotkey;
        }
    }
}