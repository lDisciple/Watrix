using System.Net.NetworkInformation;
using System.Windows;

namespace Watrix
{
    public partial class Overlay : Window
    {
        public Overlay()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Program.Pin(this);
        }
    }
}