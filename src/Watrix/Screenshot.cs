using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Display;

namespace Watrix
{
    public static class Screenshot
    {
        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
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

        public static BitmapImage screenshot()
        {
            using Bitmap bmpScreenCapture = new Bitmap(Screen.GetResolutionWidth(), 
                Screen.GetResolutionHeight());
            using Graphics g = Graphics.FromImage(bmpScreenCapture);
            g.CopyFromScreen(0,0,0,0,
                bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
            return BmpImageFromBmp(bmpScreenCapture);
        }
        
    }
}