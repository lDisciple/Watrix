using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     A listener for virtual desktop events.
	/// </summary>
	/// <seealso cref="IVirtualDesktopNotificationService" />
	[ComImport]
    [Guid("c179334c-4295-40d3-bea1-c654d965605a")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVirtualDesktopNotification
    {
        void VirtualDesktopCreated(IVirtualDesktop pDesktop);

        void VirtualDesktopDestroyBegin(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void VirtualDesktopDestroyFailed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void VirtualDesktopDestroyed(IVirtualDesktop pDesktopDestroyed, IVirtualDesktop pDesktopFallback);

        void ViewVirtualDesktopChanged(IntPtr pView);

        void CurrentVirtualDesktopChanged(IVirtualDesktop pDesktopOld, IVirtualDesktop pDesktopNew);
    }
}