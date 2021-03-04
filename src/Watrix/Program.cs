using System;
using System.Threading;
using System.Windows;
using Core.Hotkeys;
using Core.Hotkeys.Desktop;
using Core.POC;

namespace Watrix
{
    class Program: Application
    {
        private HotkeyManager hotkeys;
        private DesktopMatrix matrix;
        
        static void Main(string[] args)
        {
            new Program().Run();
            // new DesktopMatrix(3, 3);
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
        }
    }
}