using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     Registers and unregisters virtual desktop event listenenrs.
	/// </summary>
	/// <seealso cref="IVirtualDesktopNotification" />
	[ComImport]
    [Guid("0cd45e71-d927-4f15-8b0a-8fef525337bf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IVirtualDesktopNotificationService
    {
        uint Register(IVirtualDesktopNotification pNotification);

        void Unregister(uint dwCookie);
    }
}