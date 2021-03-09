using System;

namespace Core.COM.Interfaces
{
	/// <summary>
	///     A workaround for varying GUID values for the IVirtualDesktopManagerInternal interfaces.
	///     Manages 3 different services in hope that one is not null.
	///     Code from: https://github.com/Grabacr07/VirtualDesktop
	/// </summary>
	/// <remarks>
	///     This class is marked for removal in a future release when GUIDs are detected instead of hardcoded.
	/// </remarks>
    internal class VirtualDesktopManagerInternal
        : IVirtualDesktopManagerInternal10130
            , IVirtualDesktopManagerInternal10240
            , IVirtualDesktopManagerInternal14328
    {
        private IVirtualDesktopManagerInternal10130 _manager10130;
        private IVirtualDesktopManagerInternal10240 _manager10240;
        private IVirtualDesktopManagerInternal14328 _manager14328;

        public int GetCount()
        {
            if (_manager14328 != null) return _manager14328.GetCount();

            if (_manager10240 != null) return _manager10240.GetCount();

            if (_manager10130 != null) return _manager10130.GetCount();

            throw new NotSupportedException();
        }


        public void MoveViewToDesktop(IntPtr pView, IVirtualDesktop desktop)
        {
            if (_manager14328 != null)
            {
                _manager14328?.MoveViewToDesktop(pView, desktop);
                return;
            }

            if (_manager10240 != null)
            {
                _manager10240?.MoveViewToDesktop(pView, desktop);
                return;
            }

            if (_manager10130 != null)
            {
                _manager10130.MoveViewToDesktop(pView, desktop);
                return;
            }

            throw new NotSupportedException();
        }

        public bool CanViewMoveDesktops(IntPtr pView)
        {
            if (_manager14328 != null) return _manager14328.CanViewMoveDesktops(pView);

            if (_manager10240 != null) return _manager10240.CanViewMoveDesktops(pView);

            if (_manager10130 != null) return _manager10130.CanViewMoveDesktops(pView);

            throw new NotSupportedException();
        }

        public IVirtualDesktop GetCurrentDesktop()
        {
            if (_manager14328 != null) return _manager14328.GetCurrentDesktop();

            if (_manager10240 != null) return _manager10240.GetCurrentDesktop();

            if (_manager10130 != null) return _manager10130.GetCurrentDesktop();

            throw new NotSupportedException();
        }

        public IObjectArray GetDesktops()
        {
            if (_manager14328 != null) return _manager14328.GetDesktops();

            if (_manager10240 != null) return _manager10240.GetDesktops();

            if (_manager10130 != null) return _manager10130.GetDesktops();

            throw new NotSupportedException();
        }

        public IVirtualDesktop GetAdjacentDesktop(IVirtualDesktop pDesktopReference, AdjacentDesktop uDirection)
        {
            if (_manager14328 != null) return _manager14328.GetAdjacentDesktop(pDesktopReference, uDirection);

            if (_manager10240 != null) return _manager10240.GetAdjacentDesktop(pDesktopReference, uDirection);

            if (_manager10130 != null) return _manager10130.GetAdjacentDesktop(pDesktopReference, uDirection);

            throw new NotSupportedException();
        }

        public void SwitchDesktop(IVirtualDesktop desktop)
        {
            if (_manager14328 != null)
            {
                _manager14328?.SwitchDesktop(desktop);
                return;
            }

            if (_manager10240 != null)
            {
                _manager10240?.SwitchDesktop(desktop);
                return;
            }

            if (_manager10130 != null)
            {
                _manager10130.SwitchDesktop(desktop);
                return;
            }

            throw new NotSupportedException();
        }

        public IVirtualDesktop CreateDesktopW()
        {
            if (_manager14328 != null) return _manager14328.CreateDesktopW();

            if (_manager10240 != null) return _manager10240.CreateDesktopW();

            if (_manager10130 != null) return _manager10130.CreateDesktopW();

            throw new NotSupportedException();
        }

        public void RemoveDesktop(IVirtualDesktop pRemove, IVirtualDesktop pFallbackDesktop)
        {
            if (_manager14328 != null)
            {
                _manager14328.RemoveDesktop(pRemove, pFallbackDesktop);
                return;
            }

            if (_manager10240 != null)
            {
                _manager10240.RemoveDesktop(pRemove, pFallbackDesktop);
                return;
            }

            if (_manager10130 != null)
            {
                _manager10130.RemoveDesktop(pRemove, pFallbackDesktop);
                return;
            }

            throw new NotSupportedException();
        }

        public IVirtualDesktop FindDesktop(ref Guid desktopId)
        {
            if (_manager14328 != null) return _manager14328.FindDesktop(ref desktopId);

            if (_manager10240 != null) return _manager10240.FindDesktop(ref desktopId);

            if (_manager10130 != null) return _manager10130.FindDesktop(ref desktopId);

            throw new NotSupportedException();
        }

        public static VirtualDesktopManagerInternal GetInstance()
        {
            var v14328 = Utils.FromShell<IVirtualDesktopManagerInternal14328>(CLSID.VirtualDesktopApiUnknown);
            if (v14328 != null) return new VirtualDesktopManagerInternal {_manager14328 = v14328};

            var v10240 = Utils.FromShell<IVirtualDesktopManagerInternal10240>(CLSID.VirtualDesktopApiUnknown);
            if (v10240 != null) return new VirtualDesktopManagerInternal {_manager10240 = v10240};

            var v10130 = Utils.FromShell<IVirtualDesktopManagerInternal10130>(CLSID.VirtualDesktopApiUnknown);
            if (v10130 != null) return new VirtualDesktopManagerInternal {_manager10130 = v10130};

            throw new NotSupportedException();
        }
    }
}