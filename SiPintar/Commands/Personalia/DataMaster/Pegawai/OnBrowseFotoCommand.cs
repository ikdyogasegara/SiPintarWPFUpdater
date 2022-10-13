using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Personalia.DataMaster;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFotoCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFotoCommand(PegawaiViewModel viewModel, bool isTest = false)
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
                    if (param == "FotoPegawai")
                        _viewModel.FotoPegawaiUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    else if (param == "FotoKtp")
                        _viewModel.FotoKtpUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    else if (param == "FotoKk")
                        _viewModel.FotoKkUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
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
                    "personalia"
                ), "PersonaliaInnerDialog");
        }
    }
}
