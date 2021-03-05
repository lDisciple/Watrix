using System;
using System.Windows;
using Core.Hotkeys;
using Core.Hotkeys.Desktop;
using MVVMLight.Messaging;
using Watrix.Messages;

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

        public void SetUpHotkeys(DesktopMatrix matrix, Overlay overlay)
        {
            hotkeys = new HotkeyManager();
            hotkeys.Register("left", "control+alt", () =>
            {
                matrix.MoveLeft();
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.LEFT));
            });
            hotkeys.Register("right", "control+alt", () =>
            {
                matrix.MoveRight();
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.RIGHT));
            });
            hotkeys.Register("up", "control+alt", () =>
            {
                matrix.MoveUp();
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.UP));
            });
            hotkeys.Register("down", "control+alt", () =>
            {
                matrix.MoveDown();
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.DOWN));
            });
            hotkeys.Register("escape", "shift", () =>
            {
                hotkeys.Stop();
                Messenger.Default.Send(new ExitMessage());
            });
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            matrix = new DesktopMatrix(3,3);
            Overlay overlay = new Overlay(matrix);
            SetUpHotkeys(matrix, overlay);
            hotkeys.Start();
            // overlay.RaiseEvent();
            overlay.Show();
        }
    }
}