using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Pelayanan.Permohonan;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnOpenImageCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;
        private bool _isFotoExist;

        public OnOpenImageCommand(PermohonanViewModel viewModel, bool isTest = false)
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

            try
            {
                var urlFile = param switch
                {
                    "foto_bukti1" => _viewModel.FotoBukti1Uri,
                    "foto_bukti2" => _viewModel.FotoBukti2Uri,
                    "foto_bukti3" => _viewModel.FotoBukti3Uri,
                    "foto_buktispk1" => _viewModel.FotoBuktiSpk1Uri,
                    "foto_buktispk2" => _viewModel.FotoBuktiSpk2Uri,
                    "foto_buktispk3" => _viewModel.FotoBuktiSpk3Uri,
                    "foto_denah1" => _viewModel.FotoDenah1Uri,
                    "foto_denah2" => _viewModel.FotoDenah2Uri,
                    "foto_buktirab1" => _viewModel.FotoBuktiRab1Uri,
                    "foto_buktirab2" => _viewModel.FotoBuktiRab2Uri,
                    "foto_buktirab3" => _viewModel.FotoBuktiRab3Uri,
                    "foto_buktiberitaacara1" => _viewModel.FotoBuktiBeritaAcara1Uri,
                    "foto_buktiberitaacara2" => _viewModel.FotoBuktiBeritaAcara2Uri,
                    "foto_buktiberitaacara3" => _viewModel.FotoBuktiBeritaAcara3Uri,
                    _ => null
                };

                _viewModel.PreviewFile = GetImage(urlFile);

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
                _ = DialogHost.Show(new FotoView(_viewModel), "SambunganSubDialog");
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
                    "OK"
                ), "SambunganSubDialog");
        }
    }
}
