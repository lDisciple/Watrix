using System;
using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using Core.Hotkeys.Desktop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Display;

namespace Watrix
{
    
    public partial class Overlay : Window
    {
        
        private DesktopMatrix matrix;
        public Overlay(DesktopMatrix matrix)
        {
            InitializeComponent();
            this.matrix = matrix;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            IntPtr current = new WindowInteropHelper(this).Handle;
            matrix.PinWindow(current);
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = Screenshot.screenshot();
            BackgroundPanel.Background = brush;
        }

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void ExitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}