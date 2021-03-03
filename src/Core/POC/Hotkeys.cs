using System;
using System.Runtime.InteropServices;
using WinApi.User32;

namespace Core.POC
{
    public static class Hotkeys
    {
        const int HOTKEY_ALTB = 1;
        const int HOTKEY_ALTC = 2;
        
        public static void POC_Hotkeys()
        {
            
            if (User32Methods.RegisterHotKey(
                    IntPtr.Zero, 
                    HOTKEY_ALTB, 
                    KeyModifierFlags.MOD_ALT, 
                    VirtualKey.B) &&
                User32Methods.RegisterHotKey(
                    IntPtr.Zero, 
                    HOTKEY_ALTC, 
                    KeyModifierFlags.MOD_ALT, 
                    VirtualKey.C))
            {
                try
                {
                    Console.Out.WriteLine("Registered.");
                    bool running = true;
                    while (running &&
                           User32Methods.GetMessage(out var message, IntPtr.Zero, 0, 0) != 0)
                    {
                        if (message.Value == 0x0312)
                        {
                            int key = message.WParam.ToInt32();
                            if (key == HOTKEY_ALTB)
                            {
                                Console.Out.WriteLine("ALTB");
                            }
                            else
                            {
                                if (key == HOTKEY_ALTC)
                                {
                                    Console.Out.WriteLine("ALTC");
                                    running = false;
                                }
                                else
                                {
                                    Console.Out.WriteLine("OTHER");
                                }
                            }
                        }
                    }
                }
                finally
                {
                    User32Methods.UnregisterHotKey(IntPtr.Zero, HOTKEY_ALTB);
                    User32Methods.UnregisterHotKey(IntPtr.Zero, HOTKEY_ALTC);
                    Console.Out.WriteLine("Deregister");
                }
                
            }
        }
    }
}