using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Core.Hotkeys.Desktop;
using MVVMLight.Messaging;
using Watrix.Messages;
using Color = System.Windows.Media.Color;

namespace Watrix
{

    public partial class Overlay : Window
    {

        private DesktopMatrix matrix;

        private Panel[,] tiles;
        
        public Overlay(DesktopMatrix matrix)
        {
            InitializeComponent();
            this.matrix = matrix;
            Messenger.Default.Register<DesktopUpdateMessage>(this, OnDesktopUpdate);
            Messenger.Default.Register<ExitMessage>(this, OnExitMessage);
            
            tiles = new Panel[,]{
                { Holder00, Holder01, Holder02 },
                { Holder10, Holder11, Holder12 },
                { Holder20, Holder21, Holder22 },
            };
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            IntPtr current = new WindowInteropHelper(this).Handle;
            matrix.PinWindow(current);

            SolidColorBrush screenGridBrush = new SolidColorBrush(Color.FromArgb(100,0,0,0));
            ScreenGrid.Background = screenGridBrush;

            var pos = matrix.GetCurrentPosition();
            UpdateTiles(pos.Y, pos.X);
        }
        
        private void OnExitMessage(ExitMessage obj)
        {
            Dispatcher.BeginInvoke((Action)delegate()
            {
                this.Close(); 
            });
        }


        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void OnDesktopUpdate(DesktopUpdateMessage msg)
        {
            Dispatcher.BeginInvoke((Action)delegate()
            {
                Debug.WriteLine($"DesktopUpdateMessage: {msg}");
                UpdateTiles(msg.Y, msg.X);
            });
           
        }

        private void UpdateTiles(int r, int c)
        {
            for (int ri = 0; ri < 3; ri++)
            {
                for (int ci = 0; ci < 3; ci++)
                {
                    tiles[ri,ci].Opacity = ri == r && ci == c ? 1 : 0.5;
                }
            }            
        }
    }
}