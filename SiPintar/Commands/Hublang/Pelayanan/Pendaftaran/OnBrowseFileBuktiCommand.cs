using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFileBuktiCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFileBuktiCommand(PendaftaranViewModel viewModel, bool isTest = false)
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
                    if (param == "surat_pernyataan")
                    {
                        _viewModel.FotoSuratPernyataanUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoSuratPernyataan = true;
                    }
                    else if (param == "kk")
                    {
                        _viewModel.FotoKkUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoKk = true;

                    }
                    else if (param == "ktp")
                    {
                        _viewModel.FotoKtpUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoKtp = true;
                    }
                    else if (param == "imb")
                    {
                        _viewModel.FotoImbUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoImb = true;
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
