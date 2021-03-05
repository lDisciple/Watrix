using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	[ComImport]
	[Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IApplicationViewCollection
	{
		int GetViews(out IObjectArray array);

		int GetViewsByZOrder(out IObjectArray array);

		int GetViewsByAppUserModelId(string id, out IObjectArray array);

		int GetViewForHwnd(IntPtr hwnd, out IntPtr view);

		int GetViewForApplication(object application, out IntPtr view);

		int GetViewForAppUserModelId(string id, out IntPtr view);

		int GetViewInFocus(out IntPtr view);

		void outreshCollection();

		int RegisterForApplicationViewChanges(object listener, out int cookie);

		int RegisterForApplicationViewPositionChanges(object listener, out int cookie);

		int UnregisterForApplicationViewChanges(int cookie);
	}
}
