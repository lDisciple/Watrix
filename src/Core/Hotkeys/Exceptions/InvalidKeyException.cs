using System;

namespace Core.Hotkeys
{
    public class InvalidKeyException: Exception
    {
        public InvalidKeyException(string? key) : base(FormatMessage(key)) { }

        public static string FormatMessage(string key)
        {
            return $"'{key}' is not a valid key";
        }
    }
}