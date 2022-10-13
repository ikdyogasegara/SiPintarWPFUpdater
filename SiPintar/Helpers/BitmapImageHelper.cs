using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class BitmapImageHelper
    {
        private static readonly Uri DefaultImage = new Uri($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Assets\\Images\\no_image.png", UriKind.RelativeOrAbsolute);
        public static BitmapImage BytesToImage(byte[] bytes)
        {
            try
            {
                using (var ms = new System.IO.MemoryStream(bytes))
                {
                    var imageSource = new BitmapImage();
                    imageSource.BeginInit();
                    imageSource.StreamSource = ms;
                    imageSource.CacheOption = BitmapCacheOption.OnLoad;
                    imageSource.EndInit();

                    return imageSource;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error generating image : " + ex);
                return new BitmapImage(DefaultImage);
            }
        }

        public static byte[] ImageToBytes(BitmapImage img)
        {
            try
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img));

                using (var ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error saving image : " + ex);
                return Array.Empty<byte>();
            }
        }
    }
}
