using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Personalia.Kepegawaian;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKPegawaiTetap
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFotoCommand : AsyncCommandBase
    {
        private readonly SKPegawaiTetapViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFotoCommand(SKPegawaiTetapViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "JPEG File|*.jpg|Bitmap File|*.bmp|PNG File|*.png"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    _viewModel.FotoSkUri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                    await ((AsyncCommandBase)_viewModel.OnUploadFotoCommand).ExecuteAsync(null);
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
                ), "PersonaliaRootDialog");
        }
    }
}
