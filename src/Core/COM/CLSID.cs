using System;

namespace Core.COM
{
	/// <summary>
	///     Contains hard-coded GUIDs for Windows' interface implementations.
	/// </summary>
	/// <remarks>
	///     It is planned to automatically detect and configure these values in a future release.
	/// </remarks>
	// ReSharper disable once InconsistentNaming
    public static class CLSID
    {
        public static Guid ImmersiveShell { get; } = new("c2f03a33-21f5-47fa-b4bb-156362a2f239");

        public static Guid VirtualDesktopManager { get; } = new("aa509086-5ca9-4c25-8f95-589d3c07b48a");

        public static Guid VirtualDesktopApiUnknown { get; } = new("c5e0cdca-7b6e-41b2-9fc4-d93975cc467b");

        public static Guid VirtualDesktopPinnedApps { get; } = new("b5a399e7-1c87-46b8-88e9-fc5747b171bd");
    }
}