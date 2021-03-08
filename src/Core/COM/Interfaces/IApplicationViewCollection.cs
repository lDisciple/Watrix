using System;
using System.Runtime.InteropServices;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     The Application View Collection manages desktop windows of owned and other windows on the user's desktops.
	/// </summary>
	/// <remarks>
	///     This is an undocumented Windows interface and so the given documentation may be inaccurate or incomplete.
	/// </remarks>
	[ComImport]
    [Guid("1841C6D7-4F9D-42C0-AF41-8747538F10E5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IApplicationViewCollection
    {
	    /// <summary>
	    ///     Retrieve all views.
	    /// </summary>
	    /// <param name="array">The array to put the views into.</param>
	    int GetViews(out IObjectArray array);

	    /// <summary>
	    ///     Retrieve all views sorted by their Z position.
	    /// </summary>
	    /// <param name="array">The array to put the views into.</param>
	    int GetViewsByZOrder(out IObjectArray array);

	    /// <summary>
	    ///     Retrieve all views based on their Application User Model IDs.
	    ///     See https://docs.microsoft.com/en-us/windows/win32/shell/appids for
	    ///     more details on Application User Model IDs.
	    /// </summary>
	    /// <param name="id">The Application User Model ID for the requested views.</param>
	    /// <param name="array">The array to put the views into.</param>
	    int GetViewsByAppUserModelId(string id, out IObjectArray array);

	    /// <summary>
	    ///     Get a view with a specific window handle.
	    /// </summary>
	    /// <param name="hwnd">The handle for the requested view's window.</param>
	    /// <param name="view">The output destination for the retrieved view.</param>
	    int GetViewForHwnd(IntPtr hwnd, out IntPtr view);

	    /// <summary>
	    ///     Get the view of a specific application.
	    /// </summary>
	    /// <param name="application">The application of the requested view.</param>
	    /// <param name="view">The output destination for the retrieved view.</param>
	    int GetViewForApplication(object application, out IntPtr view);

	    /// <summary>
	    ///     Retrieve a view based on its Application User Model ID.
	    ///     See https://docs.microsoft.com/en-us/windows/win32/shell/appids for
	    ///     more details on Application User Model IDs.
	    /// </summary>
	    /// <param name="id">The Application User Model ID of the requested view.</param>
	    /// <param name="view">The output destination for the retrieved view.</param>
	    /// <remarks>
	    ///     It is unclear how the single view is selected.
	    ///     A method exists to fetch multiple views for one AUM ID which hints that there is a many-to-one mapping.
	    /// </remarks>
	    int GetViewForAppUserModelId(string id, out IntPtr view);

	    /// <summary>
	    ///     Get the view that is currently in focus.
	    /// </summary>
	    /// <param name="view">The output destination for the retrieved view.</param>
	    int GetViewInFocus(out IntPtr view);

        void outreshCollection();

        int RegisterForApplicationViewChanges(object listener, out int cookie);

        int RegisterForApplicationViewPositionChanges(object listener, out int cookie);

        int UnregisterForApplicationViewChanges(int cookie);
    }
}