using System.Runtime.InteropServices;

namespace Core.Display
{
    /// <summary>
    /// A utility class used to determine the resolution of the screen.
    /// </summary>
    public class Screen
    {
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref Devmode devMode);

        /// <summary>
        /// Get the resolution width of the primary monitor.
        /// </summary>
        /// <returns>A width in pixels.</returns>
        public static int GetResolutionWidth()
        {
            return GetDm().dmPelsWidth;
        }
        /// <summary>
        /// Get the resolution height of the primary monitor.
        /// </summary>
        /// <returns>A height in pixels.</returns>
        public static int GetResolutionHeight()
        {
            return GetDm().dmPelsHeight;
        }

        private static Devmode GetDm()
        {
            const int enumCurrentSettings = -1;

            Devmode devMode = default;
            devMode.dmSize = (short)Marshal.SizeOf(devMode);
            EnumDisplaySettings(null, enumCurrentSettings, ref devMode);
            return devMode;
        }
        
        
        
        [StructLayout(LayoutKind.Sequential)]
        struct Devmode
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }
    }
}