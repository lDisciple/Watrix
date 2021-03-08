using System;
using Core.COM;

namespace Core.Desktop
{
    internal static class DesktopManager
    {

        static DesktopManager()
        {
            ComObjects.Initialize();
        }
        
        internal static void GoTo(int i)
        {
            ComObjects.VirtualDesktopManagerInternal.SwitchDesktop(GetDesktop(i));
        }

        internal static IVirtualDesktop GetDesktop(int i)
        {
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            return desktops.Get<IVirtualDesktop>(i);
        }
        
        internal static void Create()
        {
            ComObjects.VirtualDesktopManagerInternal.CreateDesktopW();
        }
        
        internal static int CountDesktops()
        {
            return ComObjects.VirtualDesktopManagerInternal.GetCount();
        }
        
        internal static void Remove()
        {
            Remove(CountDesktops() - 1);
        }
        
        internal static void Remove(int i)
        {
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            var desktop = desktops.Get<IVirtualDesktop>(i);
            var fallback = desktops.Get<IVirtualDesktop>(i == 0 ? 1 : 0);
            ComObjects.VirtualDesktopManagerInternal.RemoveDesktop(desktop, fallback);
        }

        internal static int CurrentDesktop()
        {
            var current = ComObjects.VirtualDesktopManagerInternal.GetCurrentDesktop();
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            for (int i = 0; i < desktops.GetCount(); i++)
            {
                if (desktops.Get<IVirtualDesktop>(i).GetID().Equals(current.GetID()))
                {
                    return i;
                }
            }
            throw new InvalidOperationException($"No desktop exists with GUID {current.GetID()}");
        }

        internal static void PinWindow(IntPtr handle)
        {
            ComObjects.ApplicationViewCollection.GetViewForHwnd(handle, out var view);
            ComObjects.VirtualDesktopPinnedApps.PinView(view);
        }

        internal static void UnpinWindow(IntPtr handle)
        {
            ComObjects.ApplicationViewCollection.GetViewForHwnd(handle, out var view);
            ComObjects.VirtualDesktopPinnedApps.UnpinView(view);
        }

        internal static void MatchCount(int count)
        {
            while (CountDesktops() > count)
            {
                Remove();
            }

            while (CountDesktops() < count)
            {
                Create();
            }
        }

        internal static IntPtr GetForegroundView()
        {
            ComObjects.ApplicationViewCollection.GetViewInFocus(out var ptr);
            return ptr;
        }

        internal static void MoveViewToDesktop(IntPtr view, int desktop)
        {
            ComObjects.VirtualDesktopManagerInternal.MoveViewToDesktop(view, GetDesktop(desktop));
        }

        internal static void MoveForegroundWindowToDesktop(int i)
        {
            MoveViewToDesktop(GetForegroundView(), i);
        }
    }
}