using System;
using Core.COM.Interfaces;
using IVirtualDesktopManager = Core.COM.Interfaces.IVirtualDesktopManager;

namespace Core.COM
{
	public static class ComObjects
	{
		internal static IVirtualDesktopManager VirtualDesktopManager { get; private set; }
		public static VirtualDesktopManagerInternal VirtualDesktopManagerInternal { get; private set; }
		internal static IVirtualDesktopNotificationService VirtualDesktopNotificationService { get; private set; }
		public static IVirtualDesktopPinnedApps VirtualDesktopPinnedApps { get; private set; }
		public static IApplicationViewCollection ApplicationViewCollection { get; private set; }

		public static void Initialize()
		{
			VirtualDesktopManager = Utils.CreateInstance<IVirtualDesktopManager>(CLSID.VirtualDesktopManager);
			VirtualDesktopManagerInternal = VirtualDesktopManagerInternal.GetInstance();
			VirtualDesktopPinnedApps = Utils.FromShell<IVirtualDesktopPinnedApps>(CLSID.VirtualDesktopPinnedApps);
			ApplicationViewCollection = Utils.FromShell<IApplicationViewCollection>();
		}

	}
}
