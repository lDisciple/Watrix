using System;
using System.Runtime.InteropServices;

namespace Core.COM
{
	/// <summary>
	///     An array of unknown types that is often used by the COM Object interfaces.
	/// </summary>
	[ComImport]
    [Guid("92ca9dcd-5622-4bba-a805-5e9f541bd8c9")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectArray
    {
        uint GetCount();

        object GetAt(uint iIndex, ref Guid riid, [Out] [MarshalAs(UnmanagedType.Interface)]
            out object ppvObject);
    }
}