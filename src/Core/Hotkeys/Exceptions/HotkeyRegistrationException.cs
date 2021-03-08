using System;

namespace Core.Hotkeys.Exceptions
{
    /// <summary>
    ///     An exception thrown when an attempt to bind a hotkey fails.
    ///     An example is when binding control+alt+delete which is used by the OS.
    /// </summary>
    public class HotkeyRegistrationException : Exception
    {
        public HotkeyRegistrationException(Hotkey hotkey) : base($"Failed to register hotkey: {hotkey}")
        {
            this.hotkey = hotkey;
        }

        public Hotkey hotkey { get; }
    }
}