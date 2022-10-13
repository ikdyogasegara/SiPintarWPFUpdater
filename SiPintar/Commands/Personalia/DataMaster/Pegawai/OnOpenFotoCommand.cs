using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Personalia.DataMaster.Pegawai;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnOpenFotoCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly bool _isTest;
        private bool IsFotoExist;

        private readonly string FolderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public OnOpenFotoCommand(PegawaiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var param = parameter as string;

            IsFotoExist = false;
            _viewModel.PreviewFile = null;

            try
            {
                string UrlFile = null;

                if (param == "FotoPegawai")
                    UrlFile = _viewModel.FormData.Foto;
                else if (param == "FotoKtp")
                    UrlFile = _viewModel.FormData.FotoKtp;
                else if (param == "FotoKk")
                    UrlFile = _viewModel.FormData.FotoKk;

                _viewModel.PreviewFile = await GetImageAsync(UrlFile);

                ShowResult();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.ToString());
                Debug.WriteLine(e);
                ShowAlert();
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<BitmapImage> GetImageAsync(string UrlFile)
        {
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

                        IsFotoExist = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = ImageSource;

                        IsFotoExist = false;
                    }
                }
                else
                {
                    result.UriSource = ImageSource;

                    IsFotoExist = false;
                }

                result.EndInit();
            }

            return result;
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult()
        {
            if (!_isTest)
            {
                if (IsFotoExist)
                    Application.Current.Dispatcher.Invoke(delegate { ResultDialog(); });
                else
                    ShowAlert();
            }
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            _ = DialogHost.Show(new FotoView(_viewModel), "PersonaliaInnerDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowAlert()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { AlertDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private void AlertDialog()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Foto",
                    "Foto tidak ditemukan.",
                    "error",
                    "oke"
                ), "PersonaliaInnerDialog");
        }
    }
}
