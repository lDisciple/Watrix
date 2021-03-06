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

            #region Desktop switching
            hotkeys.Register("left", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.LEFT));
            });
            hotkeys.Register("right", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.RIGHT));
            });
            hotkeys.Register("up", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.UP));
            });
            hotkeys.Register("down", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.DOWN));
            });
            #endregion
            #region window moving
            hotkeys.Register("left", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.LEFT,
                    true));
            });
            hotkeys.Register("right", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.RIGHT,
                    true));
            });
            hotkeys.Register("up", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.UP,
                    true));
            });
            hotkeys.Register("down", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.DOWN,
                    true));
            });
            #endregion
            hotkeys.Register("escape", "shift", () =>
            {
                Messenger.Default.Send(new ExitMessage());
                hotkeys.Stop();
            });
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            matrix = new DesktopMatrix(3,3);

            Overlay overlay = new Overlay(matrix);
            SetUpHotkeys(matrix, overlay);
            hotkeys.Start();
            // overlay.Show();
        }
    }
}