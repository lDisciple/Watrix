using System.Runtime.InteropServices;

namespace Core.Utils
{
    /// <summary>
    ///     A utility class used to determine the resolution of the screen.
    /// </summary>
    public class Screen
    {
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref Devmode devMode);

        /// <summary>
        ///     Get the resolution width of the primary monitor.
        /// </summary>
        /// <returns>A width in pixels.</returns>
        public static int GetResolutionWidth()
        {
            return GetDm().dmPelsWidth;
        }

        /// <summary>
        ///     Get the resolution height of the primary monitor.
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
            devMode.dmSize = (short) Marshal.SizeOf(devMode);
            EnumDisplaySettings(null, enumCurrentSettings, ref devMode);
            return devMode;
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct Devmode
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public readonly string dmDeviceName;

            public readonly short dmSpecVersion;
            public readonly short dmDriverVersion;
            public short dmSize;
            public readonly short dmDriverExtra;
            public readonly int dmFields;
            public readonly int dmPositionX;
            public readonly int dmPositionY;
            public readonly int dmDisplayOrientation;
            public readonly int dmDisplayFixedOutput;
            public readonly short dmColor;
            public readonly short dmDuplex;
            public readonly short dmYResolution;
            public readonly short dmTTOption;
            public readonly short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public readonly string dmFormName;

            public readonly short dmLogPixels;
            public readonly int dmBitsPerPel;
            public readonly int dmPelsWidth;
            public readonly int dmPelsHeight;
            public readonly int dmDisplayFlags;
            public readonly int dmDisplayFrequency;
            public readonly int dmICMMethod;
            public readonly int dmICMIntent;
            public readonly int dmMediaType;
            public readonly int dmDitherType;
            public readonly int dmReserved1;
            public readonly int dmReserved2;
            public readonly int dmPanningWidth;
            public readonly int dmPanningHeight;
        }
    }
}