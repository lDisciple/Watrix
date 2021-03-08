using System;
using System.Windows;
using Core.Desktop;
using Core.Hotkeys;
using MVVMLight.Messaging;
using Watrix.Messages;

namespace Watrix
{
    internal class Program : Application
    {
        private HotkeyManager _hotkeys;
        private DesktopMatrix _matrix;

        [STAThread]
        private static void Main(string[] args)
        {
            new Program().Run();
        }

        public void SetUpHotkeys(DesktopMatrix matrix, Overlay overlay)
        {
            _hotkeys = new HotkeyManager();

            #region Desktop switching

            _hotkeys.Register("left", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.LEFT));
            });
            _hotkeys.Register("right", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.RIGHT));
            });
            _hotkeys.Register("up", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.UP));
            });
            _hotkeys.Register("down", "control+alt", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.DOWN));
            });

            #endregion

            #region window moving

            _hotkeys.Register("left", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.LEFT,
                    true));
            });
            _hotkeys.Register("right", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.RIGHT,
                    true));
            });
            _hotkeys.Register("up", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.UP,
                    true));
            });
            _hotkeys.Register("down", "control+alt+shift", () =>
            {
                Messenger.Default.Send(new DesktopUpdateMessage(
                    matrix.GetCurrentPosition(),
                    Direction.DOWN,
                    true));
            });

            #endregion

            _hotkeys.Register("escape", "shift", () =>
            {
                Messenger.Default.Send(new ExitMessage());
                _hotkeys.Stop();
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _matrix = new DesktopMatrix(3, 3);

            var overlay = new Overlay(_matrix);
            SetUpHotkeys(_matrix, overlay);
            _hotkeys.Start();
        }
    }
}