using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     Managed applications that are pinned in terms of virtual desktops.
	/// </summary>
	[ComImport]
    [Guid("4ce81583-1e4c-4632-a621-07a53543148f")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IVirtualDesktopPinnedApps
    {
	    /// <summary>
	    ///     Test is the application by the given AppID is pinned.
	    ///     For more information on AppID, see https://docs.microsoft.com/en-us/windows/win32/com/appid-key.
	    /// </summary>
	    /// <param name="appId">The AppID of the application.</param>
	    /// <returns>True if the application is pinned, false otherwise.</returns>
	    bool IsAppIdPinned(string appId);

	    /// <summary>
	    ///     Pin the the application given by the provided AppID.
	    ///     For more information on AppID, see https://docs.microsoft.com/en-us/windows/win32/com/appid-key.
	    /// </summary>
	    /// <param name="appId">The AppID of the application.</param>
	    void PinAppID(string appId);

	    /// <summary>
	    ///     Unpin the the application given by the provided AppID.
	    ///     For more information on AppID, see https://docs.microsoft.com/en-us/windows/win32/com/appid-key.
	    /// </summary>
	    /// <param name="appId">The AppID of the application.</param>
	    void UnpinAppID(string appId);

	    /// <summary>
	    ///     Test is the given view is pinned.
	    /// </summary>
	    /// <param name="applicationView">The pointer to the view.</param>
	    /// <returns>True if the application is pinned, false otherwise.</returns>
	    /// <seealso cref="IApplicationViewCollection" />
	    bool IsViewPinned(IntPtr applicationView);

	    /// <summary>
	    ///     Pin the the given view.
	    /// </summary>
	    /// <param name="applicationView">The pointer to the view.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    void PinView(IntPtr applicationView);

	    /// <summary>
	    ///     Unpin the the given view.
	    /// </summary>
	    /// <param name="applicationView">The pointer to the view.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    void UnpinView(IntPtr applicationView);
    }
}