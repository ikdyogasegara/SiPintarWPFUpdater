using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SiPintar.Helpers
{
    public static class ImageUriHelper
    {
        public static async Task<Uri> SetUriAsync(string urlFile)
        {
            if (!string.IsNullOrEmpty(urlFile))
            {
                try
                {
                    var folderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var fileName = string.Concat(DateTime.Now.ToString("ddyyyyMMhhmmss"), urlFile.Contains('/') ? urlFile.Split('/')[^1] : urlFile.Split('\\')[^1]);
                    var localPathFile = Path.Combine(folderPath, fileName);
                    var name = urlFile.Replace(".thumbnails", string.Empty);
                    var fileExtension = name.Split('.')[^1];

                    using var webClient = new WebClient();
                    var data1 = webClient.DownloadData(urlFile);

                    var imgFormat = ImageFormat.Jpeg;
                    if (fileExtension.ToLower() == "png")
                        imgFormat = ImageFormat.Png;
                    else if (fileExtension.ToLower() == "bmp")
                        imgFormat = ImageFormat.Bmp;

                    await using var mem = new MemoryStream(data1, true);
                    using var dataImage = Image.FromStream(mem);

                    dataImage.Save(localPathFile, imgFormat);
                    await mem.DisposeAsync();
                    await mem.FlushAsync();

                    return new Uri(localPathFile, UriKind.RelativeOrAbsolute);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return null;
        }
    }
}
