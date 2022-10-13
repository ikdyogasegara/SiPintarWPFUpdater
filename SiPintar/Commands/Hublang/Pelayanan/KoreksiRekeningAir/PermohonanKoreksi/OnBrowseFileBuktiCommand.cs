using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFileBuktiCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFileBuktiCommand(PermohonanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var param = parameter as string;

            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "JPEG File|*.jpg|Bitmap File|*.bmp|PNG File|*.png"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    if (param == "foto_bukti1")
                    {
                        _viewModel.FotoBukti1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti1 = true;
                    }

                    if (param == "foto_bukti2")
                    {
                        _viewModel.FotoBukti2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti2 = true;
                    }

                    if (param == "foto_bukti3")
                    {
                        _viewModel.FotoBukti3Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti3 = true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.ToString());
                Debug.WriteLine(e);
                ShowWarning();
            }

            await Task.FromResult(_isTest);
        }

        private void ShowWarning()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Data",
                    "Pastikan file anda dalam format yang benar dan mengandung data yang sesuai.",
                    "warning",
                    "Batal",
                    false,
                    "hublang"
                ), "SambunganSubDialog");
        }
    }
}
