using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Core.Utils;

namespace Watrix
{
    public static class Screenshot
    {
        private static BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Png);
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

        public static BitmapImage TakeScreenshot()
        {
            using var bmpScreenCapture = new Bitmap(Screen.GetResolutionWidth(),
                Screen.GetResolutionHeight());
            using var g = Graphics.FromImage(bmpScreenCapture);
            g.CopyFromScreen(0, 0, 0, 0,
                bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
            return BmpImageFromBmp(bmpScreenCapture);
        }
    }
}