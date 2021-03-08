using System;

namespace Core.Hotkeys
{
    /// <summary>
    /// Thrown when an invalid key is provided for a hotkey.
    /// </summary>
    public class InvalidKeyException: Exception
    {
        public InvalidKeyException(string? key) : base(FormatMessage(key)) { }

        public static string FormatMessage(string key)
        {
            return $"'{key}' is not a valid key";
        }
    }
}