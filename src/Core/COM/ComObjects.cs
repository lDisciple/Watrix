using Core.COM.Interfaces;

namespace Core.COM
{
	internal static class ComObjects
	{
		internal static IVirtualDesktopManager VirtualDesktopManager { get; private set; }
		internal static VirtualDesktopManagerInternal VirtualDesktopManagerInternal { get; private set; }
		internal static IVirtualDesktopNotificationService VirtualDesktopNotificationService { get; private set; }
		internal static IVirtualDesktopPinnedApps VirtualDesktopPinnedApps { get; private set; }
		internal static IApplicationViewCollection ApplicationViewCollection { get; private set; }

		internal static void Initialize()
		{
			VirtualDesktopManager = Utils.CreateInstance<IVirtualDesktopManager>(CLSID.VirtualDesktopManager);
			VirtualDesktopManagerInternal = VirtualDesktopManagerInternal.GetInstance();
			VirtualDesktopPinnedApps = Utils.FromShell<IVirtualDesktopPinnedApps>(CLSID.VirtualDesktopPinnedApps);
			ApplicationViewCollection = Utils.FromShell<IApplicationViewCollection>();
		}

	}
}
