using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Core.Desktop;
using MVVMLight.Messaging;
using Watrix.Messages;
using Point = Core.Desktop.Point;

namespace Watrix
{
    public partial class Overlay
    {
        private readonly DesktopMatrix _matrix;

        private readonly Panel[,] _tiles;
        private DispatcherTimer _timer;
        private int _dispatchCounter = 0;

        public Overlay(DesktopMatrix matrix)
        {
            InitializeComponent();
            _matrix = matrix;
            
            Messenger.Default.Register<DesktopUpdateMessage>(this, OnDesktopUpdate);
            Messenger.Default.Register<ExitMessage>(this, OnExitMessage);

            _tiles = new Panel[,]
            {
                {Holder00, Holder01, Holder02},
                {Holder10, Holder11, Holder12},
                {Holder20, Holder21, Holder22}
            };
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            var current = new WindowInteropHelper(this).Handle;
            _matrix.PinWindow(current);
            _matrix.SetOverlayWindow(current);

            var screenGridBrush = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
            ScreenGrid.Background = screenGridBrush;

            var pos = _matrix.GetCurrentPosition();
            UpdateTiles(pos.Y, pos.X);
        }

        private void OnExitMessage(ExitMessage obj)
        {
            Dispatcher.BeginInvoke((Action) Close);
        }

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ExitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnDesktopUpdate(DesktopUpdateMessage msg)
        {
            _timer?.Stop();
            int currentDispatch = ++_dispatchCounter;
            Dispatcher.BeginInvoke((Action) delegate
            {
                if (msg.WithWindow) _matrix.CaptureForegroundWindow();
                Show();
                Topmost = true;
                var current = new WindowInteropHelper(this).Handle;
                _matrix.PinWindow(current);

                var p = new Point(msg.X, msg.Y);
                AddDirectionToPoint(p, msg.Direction);
                UpdateTiles(p.Y, p.X);
                if (msg.WithWindow)
                    MoveWindow(msg.Direction);
                else
                    MoveDesktop(msg.Direction);

                _timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(0.3)
                };
                _timer.Start();
                _timer.Tick += (sender, _) =>
                {
                    (sender as DispatcherTimer)?.Stop();
                    if (currentDispatch == _dispatchCounter)
                    {
                        Hide();
                        if (msg.WithWindow) _matrix.PutCapturedWindowInFocus();
                    }
                };

                Debug.WriteLine($"DesktopUpdateMessage: {msg}");
            });
        }

        private void AddDirectionToPoint(Point p, Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    p.Y = Math.Max(0, p.Y - 1);
                    break;
                case Direction.DOWN:
                    p.Y = Math.Min(_matrix.Rows - 1, p.Y + 1);
                    break;
                case Direction.LEFT:
                    p.X = Math.Max(0, p.X - 1);
                    break;
                case Direction.RIGHT:
                    p.X = Math.Min(_matrix.Columns - 1, p.X + 1);
                    break;
            }
        }

        private void UpdateTiles(int r, int c)
        {
            for (var ri = 0; ri < 3; ri++)
            for (var ci = 0; ci < 3; ci++)
                _tiles[ri, ci].Opacity = ri == r && ci == c ? 1 : 0.5;
        }

        private void MoveDesktop(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    _matrix.MoveUp();
                    break;
                case Direction.DOWN:
                    _matrix.MoveDown();
                    break;
                case Direction.LEFT:
                    _matrix.MoveLeft();
                    break;
                case Direction.RIGHT:
                    _matrix.MoveRight();
                    break;
            }
        }

        private void MoveWindow(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    _matrix.MoveCapturedWindowUp();
                    break;
                case Direction.DOWN:
                    _matrix.MoveCapturedWindowDown();
                    break;
                case Direction.LEFT:
                    _matrix.MoveCapturedWindowLeft();
                    break;
                case Direction.RIGHT:
                    _matrix.MoveCapturedWindowRight();
                    break;
            }
        }
    }
}