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
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class GetFotoStanCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;
        private int section = 1; // 1: Thumbnail Foto Stan, 2: Thumbnail Foto Stan Baca Ulang, 3: File Master Foto Stan

        private readonly string FolderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public GetFotoStanCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var UrlFile = parameter as string;

            _viewModel.IsLoadingBukti = true;

            string pathDirectory = App.Current.Resources["IsDLokasiFotoMeter"]?.ToString();
            UrlFile = Path.Combine(pathDirectory, UrlFile);

            if (UrlFile.ToLower().Contains("thumbnail"))
            {
                if (UrlFile.ToLower().Contains("u.jpg"))
                    section = 2;
                else
                    section = 1;
            }
            else
                section = 3;

            await GetImageAsync(UrlFile);

            _viewModel.IsLoadingBukti = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetImageAsync(string UrlFile)
        {
            bool HasFoto = false;
            var result = new BitmapImage();

            if (!_isTest)
            {
                var ImageSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute);

                result.BeginInit();

                if (!string.IsNullOrEmpty(UrlFile))
                {
                    try
                    {
                        if (!Directory.Exists(FolderPath))
                            Directory.CreateDirectory(FolderPath);

                        var FileName = UrlFile.Contains('/') ? UrlFile.Split('/')[^1] : UrlFile.Split('\\')[^1];
                        var LocalPathFile = Path.Combine(FolderPath, FileName);
                        var Name = UrlFile.Replace(".thumbnails", string.Empty);
                        var FileExtension = Name.Split('.')[^1];

                        if (!File.Exists(LocalPathFile))
                        {
                            using var webClient = new WebClient();
                            byte[] data = webClient.DownloadData(UrlFile);

                            var ImgFormat = ImageFormat.Jpeg;
                            if (FileExtension.ToLower() == "png")
                                ImgFormat = ImageFormat.Png;
                            else if (FileExtension.ToLower() == "bmp")
                                ImgFormat = ImageFormat.Bmp;

                            using var mem = new MemoryStream(data);
                            using var dataImage = Image.FromStream(mem);
                            dataImage.Save(LocalPathFile, ImgFormat);
                            await mem.DisposeAsync();
                        }

                        result.UriSource = new Uri(LocalPathFile, UriKind.RelativeOrAbsolute);

                        HasFoto = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = ImageSource;

                        HasFoto = false;
                    }
                }
                else
                {
                    result.UriSource = ImageSource;

                    HasFoto = false;
                }

                result.EndInit();
            }

            if (section == 1)
            {
                _viewModel.ThumbnailFotoStan = result;
                _viewModel.HasFotoStan = HasFoto;
            }
            else if (section == 2)
            {
                _viewModel.ThumbnailFotoStanUlang = result;
                _viewModel.HasFotoStanUlang = HasFoto;
            }
            else
                _viewModel.FileFotoStan = result;
        }
    }
}
