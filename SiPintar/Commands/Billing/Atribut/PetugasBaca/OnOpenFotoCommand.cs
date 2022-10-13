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
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.PetugasBaca;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    [ExcludeFromCodeCoverage]
    public class OnOpenFotoCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        private readonly string FolderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        public OnOpenFotoCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            _viewModel.FormData ??= new ParamMasterPetugasBacaDto();

            DialogHelper.ShowLoading(_isTest, "BillingRootDialog");

            var hasFoto = false;

            if (_viewModel.SelectedData.FotoPetugasBaca != null)
            {
                _viewModel.IsOnGrid = false;
                await ((AsyncCommandBase)_viewModel.GetFotoCommand).ExecuteAsync(_viewModel.SelectedData.IdPetugasBaca);

                if (_viewModel.FotoPetugasURI != null)
                    hasFoto = true;
            }

            if (hasFoto)
            {
                _viewModel.IsFotoExist = false;
                _viewModel.PreviewFile = null;

                try
                {
                    var urlFile = _viewModel.FormData.FotoPetugasBaca;
                    _viewModel.PreviewFile = await GetImageAsync(urlFile);

                    ShowResult();
                }
                catch (Exception e)
                {
                    Log.Logger.Error(e.ToString());
                    Debug.WriteLine(e);
                    ShowAlert();
                }
            }
            else
            {
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
                var imageSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute);

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

                        _viewModel.IsFotoExist = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = imageSource;

                        _viewModel.IsFotoExist = false;
                    }
                }
                else
                {
                    result.UriSource = imageSource;

                    _viewModel.IsFotoExist = false;
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
                if (_viewModel.IsFotoExist)
                    Application.Current.Dispatcher.Invoke(delegate { ResultDialog(); });
                else
                    ShowAlert();
            }
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            if (DialogHost.IsDialogOpen("BillingRootDialog"))
                DialogHost.Close("BillingRootDialog");

            _ = DialogHost.Show(new FotoView(_viewModel), "BillingRootDialog");
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
            if (DialogHost.IsDialogOpen("BillingRootDialog"))
                DialogHost.Close("BillingRootDialog");

            _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Foto",
                    "Foto tidak ditemukan.",
                    "error",
                    "oke",
                    false,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
