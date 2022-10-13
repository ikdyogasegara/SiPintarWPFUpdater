using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFotoCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFotoCommand(RotasiMeterNonRutinViewModel viewModel, bool isTest = false)
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
                    if (param == "FotoMeterLama")
                        _viewModel.FotoMeterLamaUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    else if (param == "FotoMeterBaru")
                        _viewModel.FotoMeterBaruUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
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
                    "distribusi"
                ), "DistribusiInnerDialog");
        }
    }
}
