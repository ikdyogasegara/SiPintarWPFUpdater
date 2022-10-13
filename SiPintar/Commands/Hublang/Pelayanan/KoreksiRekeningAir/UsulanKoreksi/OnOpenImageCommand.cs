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
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi
{
    public class OnOpenImageCommand : AsyncCommandBase
    {
        private readonly UsulanKoreksiViewModel _viewModel;
        private readonly bool _isTest;
        private bool _isFotoExist;

        public OnOpenImageCommand(UsulanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var param = parameter as string;

            _isFotoExist = false;
            _viewModel.PreviewFile = null;

            var urlFile = param switch
            {
                "foto_bukti1permohonan" => _viewModel.FotoBukti1PermohonanUri,
                "foto_bukti2permohonan" => _viewModel.FotoBukti2PermohonanUri,
                "foto_bukti3permohonan" => _viewModel.FotoBukti3PermohonanUri,
                "foto_bukti1" => _viewModel.FotoBukti1Uri,
                "foto_bukti2" => _viewModel.FotoBukti2Uri,
                "foto_bukti3" => _viewModel.FotoBukti3Uri,
                _ => null
            };

            _viewModel.PreviewFile = GetImage(urlFile);

            ShowResult();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private BitmapImage GetImage(Uri urlFile)
        {
            var result = new BitmapImage();

            if (!_isTest)
            {
                var imageSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute);
                result.BeginInit();

                if (!string.IsNullOrEmpty(urlFile.ToString()))
                {
                    try
                    {
                        result.UriSource = urlFile;
                        _isFotoExist = true;
                    }
                    catch (Exception errorImage)
                    {
                        Debug.WriteLine("error get image " + errorImage);
                        Log.Logger.Error("error get image " + errorImage);

                        result.UriSource = imageSource;

                        _isFotoExist = false;
                    }
                }
                else
                {
                    result.UriSource = imageSource;

                    _isFotoExist = false;
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
                if (_isFotoExist)
                    Application.Current.Dispatcher.Invoke(delegate { ResultDialog(); });
                else
                    ShowAlert();
            }
        }

        [ExcludeFromCodeCoverage]
        private void ResultDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new FotoView(_viewModel), "KoreksiRekeningSubDialog");
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
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Foto",
                    "Foto tidak ditemukan.",
                    "error",
                    "oke"
                ), "SambunganSubDialog");
        }
    }
}
