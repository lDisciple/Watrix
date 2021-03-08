using System;
using System.Collections.Generic;
using System.Linq;
using WinApi.User32;

namespace Core.Hotkeys
{
    /// <summary>
    /// A combination of keyboard key and modifiers (e.g. control+alt)
    /// </summary>
    public class Hotkey
    {
        public VirtualKey key { get; }
        public KeyModifierFlags modifiers { get; }

        /// <summary>
        /// Create a hotkey from a key and modifier bitset.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">A bitset of modifiers.</param>
        /// <seealso cref="KeyModifierFlags"/>
        /// <seealso cref="VirtualKey"/>
        public Hotkey(VirtualKey key, KeyModifierFlags modifiers)
        {
            this.key = key;
            this.modifiers = modifiers;
        }
        /// <summary>
        /// Create a hotkey from key and modifier strings. 
        /// </summary>
        /// <param name="key">A string matching a key value.</param>
        /// <param name="modifiers">A '+'-separated list of modifier values (without the 'MOD_' prefix).</param>
        public Hotkey(string key, string modifiers)
        {
            this.key = parseKey(key);
            this.modifiers = parseModifiers(modifiers);
        }

        protected bool Equals(Hotkey other)
        {
            return key == other.key && modifiers == other.modifiers;
        }
        
        

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Hotkey) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) key, (int) modifiers);
        }

        private VirtualKey parseKey(string keyString)
        {
            VirtualKey key;
            if (Enum.TryParse(keyString, true, out key)) {
                if (Enum.IsDefined(typeof(VirtualKey), key) | key.ToString().Contains(","))
                {
                    return key;
                }
            }
            throw new InvalidKeyException(keyString);
        }
        
        private KeyModifierFlags parseModifier(string modifierString)
        {
            modifierString = $"MOD_{modifierString}";
            KeyModifierFlags modifier;
            if (Enum.TryParse(modifierString, true, out modifier)) {
                if (Enum.IsDefined(typeof(KeyModifierFlags), modifier))
                {
                    return modifier;
                }
            }
            throw new InvalidKeyException(modifierString);
        }
        
        private KeyModifierFlags parseModifiers(string modifierString)
        {
            KeyModifierFlags value = 0;
            foreach (var s in modifierString.Split("+"))
            {
                value |= parseModifier(s);
            }

            return value;
        }

        public override string ToString()
        {
            IList<string> modifierList = new List<string>();
            var modifierEntries = Enum.GetNames<KeyModifierFlags>()
                .Zip(Enum.GetValues<KeyModifierFlags>());
            foreach (var (name, value) in modifierEntries)
            {
                if ((modifiers & value) > 0)
                {
                    modifierList.Add(name);
                }
            }

            string modifierString = string.Join("+",modifierList.ToArray());
            return $"{key}[{modifierString}]";
        }
    }
}