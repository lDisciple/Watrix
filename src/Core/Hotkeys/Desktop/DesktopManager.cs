using System.Linq;
using WindowsDesktop;

namespace Core.Hotkeys.Desktop
{
    public static class DesktopManager
    {
        internal static void GoTo(int i)
        {
            VirtualDesktop.GetDesktops()[i].Switch();
        }
        
        internal static void Create()
        {
            VirtualDesktop.Create();
        }
        
        internal static int CountDesktops()
        {
            return VirtualDesktop.GetDesktops().Length;
        }
        
        internal static void Remove()
        {
            VirtualDesktop.GetDesktops()[CountDesktops()-1].Remove();
        }
        
        internal static void Remove(int i)
        {
            VirtualDesktop.GetDesktops()[i].Remove();
        }

        internal static int CurrentDesktop()
        {
            VirtualDesktop[] desktops = VirtualDesktop.GetDesktops();
            VirtualDesktop current = VirtualDesktop.Current;
            return desktops.Select((value, index) => new {Value = value, Index = index})
                .Single(p => p.Value.Id.Equals(current.Id)).Index;
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
    }
}