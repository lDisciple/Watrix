using System;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using WindowsDesktop;
using Core.Hotkeys;
using Core.Hotkeys.Desktop;
using Core.POC;
using Core.COM;

namespace Watrix
{
    class Program: Application
    {
        private HotkeyManager hotkeys;
        private DesktopMatrix matrix;
        
        [STAThread]
        static void Main(string[] args)
        {
            new Program().Run();
        }

        public void SetUpHotkeys(DesktopMatrix matrix)
        {
            hotkeys = new HotkeyManager();
            hotkeys.Register("left", "control+alt", matrix.MoveLeft);
            hotkeys.Register("right", "control+alt", matrix.MoveRight);
            hotkeys.Register("up", "control+alt", matrix.MoveUp);
            hotkeys.Register("down", "control+alt", matrix.MoveDown);
            hotkeys.Register("escape", "shift", () => Application.Current.Shutdown());
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            matrix = new DesktopMatrix(3,3);
            Console.WriteLine(matrix.GetCurrentPosition().ToString());
            SetUpHotkeys(matrix);
            hotkeys.Start();
            Console.Out.WriteLine(123);
            Overlay overlay = new Overlay();
            overlay.Show();
            
        }

        public static void Pin(Window w)
        {
            IntPtr top = WinApi.User32.User32Methods.GetForegroundWindow();
            IntPtr current = new WindowInteropHelper(w).Handle;
            ComObjects.Initialize();
            Guid desktop = ComObjects.VirtualDesktopManagerInternal.GetCurrentDesktop().GetID();
            ComObjects.ApplicationViewCollection.GetViewInFocus(out var view);
            Console.WriteLine(view);
            ComObjects.ApplicationViewCollection.GetViews(out var views);
            Console.WriteLine(views.GetCount());
            ComObjects.VirtualDesktopPinnedApps.PinView(view);
            Console.WriteLine(Application.Current.Properties.Keys.Count);
            foreach (var key in Application.Current.Properties.Keys)
            {
                Console.WriteLine(key);
            }
        }
    }
}