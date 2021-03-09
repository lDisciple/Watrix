using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Core.COM;
using Core.COM.Interfaces;
using WinApi.User32;

namespace Core.Desktop
{
    /// <summary>
    ///     A wrapper for virtual desktop COM Objects that manages virtual desktops.
    /// </summary>
    internal static class DesktopManager
    {
        static DesktopManager()
        {
            ComObjects.Initialize();
        }

        /// <summary>
        ///     Go to the given desktop.
        /// </summary>
        /// <param name="i">The index of the desktop.</param>
        internal static void GoTo(int i)
        {
            ComObjects.VirtualDesktopManagerInternal.SwitchDesktop(GetDesktop(i));
        }

        /// <summary>
        ///     Gets a virtual desktop at a given index.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        internal static IVirtualDesktop GetDesktop(int i)
        {
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            return desktops.Get<IVirtualDesktop>(i);
        }

        /// <summary>
        ///     Creates a new virtual desktop.
        /// </summary>
        internal static void Create()
        {
            ComObjects.VirtualDesktopManagerInternal.CreateDesktopW();
        }

        /// <summary>
        ///     Counts the number of current virtual desktops.
        /// </summary>
        /// <returns>The number of desktops.</returns>
        internal static int CountDesktops()
        {
            return ComObjects.VirtualDesktopManagerInternal.GetCount();
        }

        /// <summary>
        ///     Removes the last virtual desktop.
        /// </summary>
        internal static void Remove()
        {
            Remove(CountDesktops() - 1);
        }

        /// <summary>
        ///     Removes the virtual desktop at index <paramref name="i" />.
        ///     Uses the first or second desktops as fallback.
        /// </summary>
        /// <param name="i">The desktop-to-remove's index</param>
        internal static void Remove(int i)
        {
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            var desktop = desktops.Get<IVirtualDesktop>(i);
            var fallback = desktops.Get<IVirtualDesktop>(i == 0 ? 1 : 0);
            ComObjects.VirtualDesktopManagerInternal.RemoveDesktop(desktop, fallback);
        }

        /// <summary>
        ///     Gets the currently active desktop.
        /// </summary>
        /// <returns>The current desktop.</returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown only if a desktop is not available during the processing of this function.
        ///     This should only be possible during multi-threading.
        /// </exception>
        internal static int CurrentDesktop()
        {
            var current = ComObjects.VirtualDesktopManagerInternal.GetCurrentDesktop();
            var desktops = ComObjects.VirtualDesktopManagerInternal.GetDesktops();
            for (var i = 0; i < desktops.GetCount(); i++)
                if (desktops.Get<IVirtualDesktop>(i).GetID().Equals(current.GetID()))
                    return i;
            throw new InvalidOperationException($"No desktop exists with GUID {current.GetID()}");
        }

        /// <summary>
        ///     Pins the given window.
        /// </summary>
        /// <param name="handle">The handle of the window to pin.</param>
        internal static void PinWindow(IntPtr handle)
        {
            try
            {
                ComObjects.ApplicationViewCollection.GetViewForHwnd(handle, out var view);
                ComObjects.VirtualDesktopPinnedApps.PinView(view);
            } catch (COMException e) when (e.HResult == -2147319765)
            {
                Console.Error.WriteLine("Could not pin this window. Please make sure it is not hidden.");
            }
        }

        /// <summary>
        ///     Unpins the given window.
        /// </summary>
        /// <param name="handle">The handle of the window to unpin.</param>
        internal static void UnpinWindow(IntPtr handle)
        {
            try
            {
                ComObjects.ApplicationViewCollection.GetViewForHwnd(handle, out var view);
                ComObjects.VirtualDesktopPinnedApps.UnpinView(view);
            }
            catch (COMException e) when (e.HResult == -2147319765)
            {
                Console.Error.WriteLine("Could not pin this window. Please make sure it is not hidden.");
            }
        }

        /// <summary>
        ///     Ensures that the given number of desktops are available exactly.
        ///     Removes or creates desktops to achieve this.
        /// </summary>
        /// <param name="count">The number of desktops to have.</param>
        internal static void MatchCount(int count)
        {
            while (CountDesktops() > count) Remove();

            while (CountDesktops() < count) Create();
        }

        /// <summary>
        ///     Gets the current foreground view.
        /// </summary>
        /// <returns>A reference to a view</returns>
        internal static IntPtr GetForegroundWindow()
        {
            Debug.WriteLine("Begin top window search:", "TWSearch");
            IntPtr result = IntPtr.Zero;
            User32Methods.EnumWindows((whnd, _) =>
            {
                if (whnd.Equals(IntPtr.Zero)) return false;
                if (User32Methods.IsWindowVisible(whnd))
                {
                    try
                    {
                        ComObjects.ApplicationViewCollection.GetViewForHwnd(whnd, out var view);
                        var onDesktop = ComObjects.VirtualDesktopManager.IsWindowOnCurrentVirtualDesktop(whnd);
                        var pinnable = ComObjects.VirtualDesktopManagerInternal.CanViewMoveDesktops(view);

                        StringBuilder sb = new StringBuilder(40);
                        User32Methods.GetWindowText(whnd, sb, 40);
                        var s = sb.ToString();
                        if (s.Length > 0)
                        {
                            Debug.WriteLine($"{s}: {pinnable} {onDesktop}", "TWSearch");
                        }

                        if (onDesktop && pinnable)
                        {
                            result = whnd;
                            return false;
                        }
                    }
                    catch (COMException e) when (e.HResult == -2147319765)
                    {
                    }
                }

                return true;
            }, IntPtr.Zero);
            return result;
        }
        /// <summary>
        ///     Gets the current foreground view.
        /// </summary>
        /// <returns>A reference to a view</returns>
        internal static IntPtr GetForegroundWindow2()
        {
            IntPtr handle = User32Methods.FindWindow(null, null);
            Debug.WriteLine("Begin top window search:", "TWSearch");
            while (!handle.Equals(IntPtr.Zero))
            {
                if (User32Methods.IsWindowVisible(handle))
                {
                    try
                    {
                        ComObjects.ApplicationViewCollection.GetViewForHwnd(handle, out var view);
                        var onDesktop = ComObjects.VirtualDesktopManager.IsWindowOnCurrentVirtualDesktop(handle);
                        var pinnable = ComObjects.VirtualDesktopManagerInternal.CanViewMoveDesktops(view);


                        StringBuilder sb = new StringBuilder(40);
                        User32Methods.GetWindowText(handle, sb, 40);
                        var s = sb.ToString();
                        if (s.Length > 0)
                        {
                            Debug.WriteLine($"{s}: {pinnable} {onDesktop}", "TWSearch");
                        }

                        if (onDesktop && pinnable)
                        {
                            return handle;
                        }
                    }
                    catch (COMException e) when (e.HResult == -2147319765)
                    {
                    }
                }
                handle = User32Methods.GetWindow(handle, 2);
            }
            return IntPtr.Zero;
        }

        /// <summary>
        ///     Moves a given view/window to the given desktop.
        /// </summary>
        /// <param name="whnd">A reference to a window.</param>
        /// <param name="desktop">The destination desktop.</param>
        internal static void MoveWindowToDesktop(IntPtr whnd, int desktop)
        {
            if (whnd.Equals(IntPtr.Zero)) return;
            ComObjects.ApplicationViewCollection.GetViewForHwnd(whnd, out var view);
            ComObjects.VirtualDesktopManagerInternal.MoveViewToDesktop(view, GetDesktop(desktop));
        }

        /// <summary>
        ///     Moves the foreground window to a given desktop.
        /// </summary>
        /// <param name="i">The index of the destination desktop.</param>
        internal static void MoveForegroundWindowToDesktop(int i)
        {
            var window = GetForegroundWindow();
            MoveWindowToDesktop(window, i);
        }

        internal static void FocusWindow(IntPtr hwnd)
        {
            User32Methods.SetForegroundWindow(hwnd);
        }
    }
}