using System;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Interop;
using Core.Hotkeys.Desktop;

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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            IntPtr current = new WindowInteropHelper(this).Handle;
            matrix.PinWindow(current);
        }
    }
}