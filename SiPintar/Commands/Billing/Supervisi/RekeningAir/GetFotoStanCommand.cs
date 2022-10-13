using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Serilog;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class GetFotoStanCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        private int _section = 1; // 1: Thumbnail Foto Stan, 2: Thumbnail Foto Stan Baca Ulang, 3: File Master Foto Stan

        private readonly string _folderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public GetFotoStanCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var urlFile = parameter as string;

            _viewModel.IsLoadingBukti = true;

            var pathDirectory = AppSetting.LokasiFotoMeter;

            urlFile = Path.Combine(pathDirectory, urlFile);

            if (urlFile.ToLower().Contains("thumbnail"))
            {
                _section = urlFile.ToLower().Contains("u.jpg") ? 2 : 1;
            }
            else
                _section = 3;

            await GetImageAsync(urlFile);

            _viewModel.IsLoadingBukti = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetImageAsync(string urlFile)
        {
            var hasFoto = false;
            var result = new BitmapImage();

            if (!_isTest)
            {
                var imageSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute);

                result.BeginInit();

                if (!string.IsNullOrEmpty(urlFile))
                {
                    try
                    {
                        if (!Directory.Exists(_folderPath))
                            Directory.CreateDirectory(_folderPath);

                        var fileName = urlFile.Contains('/') ? urlFile.Split('/')[^1] : urlFile.Split('\\')[^1];
                        var localPathFile = Path.Combine(_folderPath, fileName);
                        var name = urlFile.Replace(".thumbnails", string.Empty);
                        var fileExtension = name.Split('.')[^1];

                        if (!File.Exists(localPathFile))
                        {
                            using var webClient = new WebClient();
                            var data = webClient.DownloadData(urlFile);

                            var imgFormat = ImageFormat.Jpeg;
                            if (fileExtension.ToLower() == "png")
                                imgFormat = ImageFormat.Png;
                            else if (fileExtension.ToLower() == "bmp")
                                imgFormat = ImageFormat.Bmp;

                            using var mem = new MemoryStream(data);
                            using var dataImage = Image.FromStream(mem);
                            dataImage.Save(localPathFile, imgFormat);
                            await mem.DisposeAsync();
                        }

                        result.UriSource = new Uri(localPathFile, UriKind.RelativeOrAbsolute);

                        hasFoto = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = imageSource;

                        hasFoto = false;
                    }
                }
                else
                {
                    result.UriSource = imageSource;

                    hasFoto = false;
                }

                result.EndInit();
            }

            if (_section == 1)
            {
                _viewModel.ThumbnailFotoStan = result;
                _viewModel.HasFotoStan = hasFoto;
            }
            else if (_section == 2)
            {
                _viewModel.ThumbnailFotoStanUlang = result;
                _viewModel.HasFotoStanUlang = hasFoto;
            }
            else
                _viewModel.FileFotoStan = result;
        }
    }
}
