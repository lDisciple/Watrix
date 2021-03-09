using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     The low-level internal implementation of the Window Virtual Desktop Manager API.
	/// </summary>
	/// <remarks>
	///     This is an undocumented Windows interface and so the given documentation may be inaccurate or incomplete.
	///     This interface is linked to a GUID that works for Windows 10 build 10130.
	///     This GUID is planned to be detected automatically in future releases.
	/// </remarks>
	[ComImport]
    [Guid("ef9f1a6c-d3cc-4358-b712-f84b635bebe7")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IVirtualDesktopManagerInternal10130
    {
	    /// <summary>
	    ///     Get the number of existing virtual desktops.
	    /// </summary>
	    /// <returns>The number of virtual desktops.</returns>
	    public int GetCount();

	    /// <summary>
	    ///     Move a view to a specific virtual desktop.
	    /// </summary>
	    /// <param name="pView">The reference to a view.</param>
	    /// <param name="desktop">The target desktop.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    void MoveViewToDesktop(IntPtr pView, IVirtualDesktop desktop);

	    /// <summary>
	    ///     Test if a view can be moved between virtual desktops.
	    /// </summary>
	    /// <param name="pView">The reference to the view to test.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    bool CanViewMoveDesktops(IntPtr pView);

	    /// <summary>
	    ///     Get the current active virtual desktop.
	    /// </summary>
	    /// <returns>A virtual desktop.</returns>
	    IVirtualDesktop GetCurrentDesktop();

	    /// <summary>
	    ///     Get all existing virtual desktops.
	    /// </summary>
	    /// <returns>A collection of virtual desktops.</returns>
	    IObjectArray GetDesktops();

	    /// <summary>
	    ///     Get the virtual desktop adjacent to the provided virtual desktop.
	    /// </summary>
	    /// <param name="pDesktopReference">The given virtual desktop.</param>
	    /// <param name="uDirection">The direction to look for the adjacent desktop.</param>
	    /// <returns>A virtual desktop that is adjacent to the given virtual desktop.</returns>
	    /// <remarks>
	    ///     Windows virtual desktops are (at the time of writing) presented in a linear format and
	    ///     so adjacent desktops can only be to the left and right of another desktop.
	    /// </remarks>
	    /// <see cref="AdjacentDesktop" />
	    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection);

	    /// <summary>
	    ///     Switch to the given virtual desktop.
	    /// </summary>
	    /// <param name="desktop">The desktop to switch to.</param>
	    void SwitchDesktop(IVirtualDesktop desktop);

	    /// <summary>
	    ///     Create a new virtual desktop.
	    /// </summary>
	    /// <returns>The new virtual desktop.</returns>
	    IVirtualDesktop CreateDesktopW();

	    /// <summary>
	    ///     Remove/destroy a given virtual desktop.
	    /// </summary>
	    /// <param name="pRemove">The desktop to remove.</param>
	    /// <param name="pFallbackDesktop">
	    ///     (Assumption) The virtual desktop to switch to if the user is currently on the
	    ///     destroyed virtual desktop.
	    /// </param>
	    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

	    /// <summary>
	    ///     Find a specific virtual desktop based on its GUID.
	    /// </summary>
	    /// <param name="desktopId">The GUID of the requested virtual desktop.</param>
	    /// <returns>The virtual desktop with a matching GUID.</returns>
	    IVirtualDesktop FindDesktop(ref Guid desktopId);
    }

	/// <summary>
	///     The low-level internal implementation of the Window Virtual Desktop Manager API.
	/// </summary>
	/// <remarks>
	///     This is an undocumented Windows interface and so the given documentation may be inaccurate or incomplete.
	///     This interface is linked to a GUID that works for Windows 10 build 10240.
	///     This GUID is planned to be detected automatically in future releases.
	/// </remarks>
	[ComImport]
    [Guid("af8da486-95bb-4460-b3b7-6e7a6b2962b5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IVirtualDesktopManagerInternal10240
    {
	    /// <summary>
	    ///     Get the number of existing virtual desktops.
	    /// </summary>
	    /// <returns>The number of virtual desktops.</returns>
	    public int GetCount();

	    /// <summary>
	    ///     Move a view to a specific virtual desktop.
	    /// </summary>
	    /// <param name="pView">The reference to a view.</param>
	    /// <param name="desktop">The target desktop.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    void MoveViewToDesktop(IntPtr pView, IVirtualDesktop desktop);

	    /// <summary>
	    ///     Test if a view can be moved between virtual desktops.
	    /// </summary>
	    /// <param name="pView">The reference to the view to test.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    bool CanViewMoveDesktops(IntPtr pView);

	    /// <summary>
	    ///     Get the current active virtual desktop.
	    /// </summary>
	    /// <returns>A virtual desktop.</returns>
	    IVirtualDesktop GetCurrentDesktop();

	    /// <summary>
	    ///     Get all existing virtual desktops.
	    /// </summary>
	    /// <returns>A collection of virtual desktops.</returns>
	    IObjectArray GetDesktops();

	    /// <summary>
	    ///     Get the virtual desktop adjacent to the provided virtual desktop.
	    /// </summary>
	    /// <param name="pDesktopReference">The given virtual desktop.</param>
	    /// <param name="uDirection">The direction to look for the adjacent desktop.</param>
	    /// <returns>A virtual desktop that is adjacent to the given virtual desktop.</returns>
	    /// <remarks>
	    ///     Windows virtual desktops are (at the time of writing) presented in a linear format and
	    ///     so adjacent desktops can only be to the left and right of another desktop.
	    /// </remarks>
	    /// <see cref="AdjacentDesktop" />
	    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection);

	    /// <summary>
	    ///     Switch to the given virtual desktop.
	    /// </summary>
	    /// <param name="desktop">The desktop to switch to.</param>
	    void SwitchDesktop(IVirtualDesktop desktop);

	    /// <summary>
	    ///     Create a new virtual desktop.
	    /// </summary>
	    /// <returns>The new virtual desktop.</returns>
	    IVirtualDesktop CreateDesktopW();

	    /// <summary>
	    ///     Remove/destroy a given virtual desktop.
	    /// </summary>
	    /// <param name="pRemove">The desktop to remove.</param>
	    /// <param name="pFallbackDesktop">
	    ///     (Assumption) The virtual desktop to switch to if the user is currently on the
	    ///     destroyed virtual desktop.
	    /// </param>
	    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

	    /// <summary>
	    ///     Find a specific virtual desktop based on its GUID.
	    /// </summary>
	    /// <param name="desktopId">The GUID of the requested virtual desktop.</param>
	    /// <returns>The virtual desktop with a matching GUID.</returns>
	    IVirtualDesktop FindDesktop(ref Guid desktopId);
    }

	/// <summary>
	///     The low-level internal implementation of the Window Virtual Desktop Manager API.
	/// </summary>
	/// <remarks>
	///     This is an undocumented Windows interface and so the given documentation may be inaccurate or incomplete.
	///     This interface is linked to a GUID that works for Windows 10 build 14328.
	///     This GUID is planned to be detected automatically in future releases.
	/// </remarks>
	[ComImport]
    [Guid("f31574d6-b682-4cdc-bd56-1827860abec6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IVirtualDesktopManagerInternal14328
    {
	    /// <summary>
	    ///     Get the number of existing virtual desktops.
	    /// </summary>
	    /// <returns>The number of virtual desktops.</returns>
	    public int GetCount();

	    /// <summary>
	    ///     Move a view to a specific virtual desktop.
	    /// </summary>
	    /// <param name="pView">The reference to a view.</param>
	    /// <param name="desktop">The target desktop.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    void MoveViewToDesktop(IntPtr pView, IVirtualDesktop desktop);

	    /// <summary>
	    ///     Test if a view can be moved between virtual desktops.
	    /// </summary>
	    /// <param name="pView">The reference to the view to test.</param>
	    /// <seealso cref="IApplicationViewCollection" />
	    bool CanViewMoveDesktops(IntPtr pView);

	    /// <summary>
	    ///     Get the current active virtual desktop.
	    /// </summary>
	    /// <returns>A virtual desktop.</returns>
	    IVirtualDesktop GetCurrentDesktop();

	    /// <summary>
	    ///     Get all existing virtual desktops.
	    /// </summary>
	    /// <returns>A collection of virtual desktops.</returns>
	    IObjectArray GetDesktops();

	    /// <summary>
	    ///     Get the virtual desktop adjacent to the provided virtual desktop.
	    /// </summary>
	    /// <param name="pDesktopReference">The given virtual desktop.</param>
	    /// <param name="uDirection">The direction to look for the adjacent desktop.</param>
	    /// <returns>A virtual desktop that is adjacent to the given virtual desktop.</returns>
	    /// <remarks>
	    ///     Windows virtual desktops are (at the time of writing) presented in a linear format and
	    ///     so adjacent desktops can only be to the left and right of another desktop.
	    /// </remarks>
	    /// <see cref="AdjacentDesktop" />
	    IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection);

	    /// <summary>
	    ///     Switch to the given virtual desktop.
	    /// </summary>
	    /// <param name="desktop">The desktop to switch to.</param>
	    void SwitchDesktop(IVirtualDesktop desktop);

	    /// <summary>
	    ///     Create a new virtual desktop.
	    /// </summary>
	    /// <returns>The new virtual desktop.</returns>
	    IVirtualDesktop CreateDesktopW();

	    /// <summary>
	    ///     Remove/destroy a given virtual desktop.
	    /// </summary>
	    /// <param name="pRemove">The desktop to remove.</param>
	    /// <param name="pFallbackDesktop">
	    ///     (Assumption) The virtual desktop to switch to if the user is currently on the
	    ///     destroyed virtual desktop.
	    /// </param>
	    void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop);

	    /// <summary>
	    ///     Find a specific virtual desktop based on its GUID.
	    /// </summary>
	    /// <param name="desktopId">The GUID of the requested virtual desktop.</param>
	    /// <returns>The virtual desktop with a matching GUID.</returns>
	    IVirtualDesktop FindDesktop(ref Guid desktopId);
    }

	/// <summary>
	///     An enum for value specifying directions with regards to virtual desktop positioning.
	/// </summary>
	public enum AdjacentDesktop
    {
        LeftDirection = 3,
        RightDirection = 4
    }
}