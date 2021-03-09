using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     The high-level (documented) Windows Virtual Desktop API.
	///     See	https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-ivirtualdesktopmanager
	///     for more details.
	/// </summary>
	[ComImport]
    [Guid("a5cd92ff-29be-454c-8d04-d82879fb3f1b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IVirtualDesktopManager
    {
	    /// <summary>
	    ///     Indicates whether the provided window is on the currently active virtual desktop.
	    /// </summary>
	    /// <param name="topLevelWindow">The window of interest.</param>
	    /// <returns>
	    ///     True if the <paramref name="topLevelWindow" /> is on the currently active virtual desktop,
	    ///     otherwise false.
	    /// </returns>
	    bool IsWindowOnCurrentVirtualDesktop(IntPtr topLevelWindow);

	    /// <summary>
	    ///     Gets the identifier for the virtual desktop hosting the provided top-level window.
	    /// </summary>
	    /// <param name="topLevelWindow">The top level window for the virtual desktop you are interested in.</param>
	    /// <returns>The identifier for the virtual desktop hosting the <paramref name="topLevelWindow" />.</returns>
	    Guid GetWindowDesktopId(IntPtr topLevelWindow);

	    /// <summary>
	    ///     Moves a window to the specified virtual desktop.
	    /// </summary>
	    /// <param name="topLevelWindow">The window to move.</param>
	    /// <param name="desktopId">The identifier of the virtual desktop to move.</param>
	    /// <remarks>
	    ///     This method only allows the movement of windows owned by this application.
	    ///     Please see the internal virtual desktop managers for more flexibility.
	    /// </remarks>
	    /// <seealso cref="IVirtualDesktopManagerInternal10130" />
	    /// <seealso cref="IVirtualDesktopManagerInternal10240" />
	    /// <seealso cref="IVirtualDesktopManagerInternal14328" />
	    void MoveWindowToDesktop(IntPtr topLevelWindow, ref Guid desktopId);
    }
}