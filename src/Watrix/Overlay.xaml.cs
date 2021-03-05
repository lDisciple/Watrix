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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            IntPtr current = new WindowInteropHelper(this).Handle;
            matrix.PinWindow(current);
            
            using (Bitmap bmpScreenCapture = new Bitmap(Screen.GetResolutionWidth(), 
                Screen.GetResolutionHeight()))
            using (Graphics g = Graphics.FromImage(bmpScreenCapture))
            {
                g.CopyFromScreen(0,0,0,0,
                    bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
                // bmpScreenCapture.Save("test.png");
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = BmpImageFromBmp(bmpScreenCapture);
                BackgroundPanel.Background = brush;
            }

        }
        
        private BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}