using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFileCommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFileCommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
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
                    Title = "Buka file .ini",
                    DefaultExt = "ini",
                    Filter = "Configuration files (*.ini)|*.ini|Text files (*.txt)|*.txt",
                    InitialDirectory = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    _viewModel.FileName = openFileDialog.FileName;
                    _viewModel.Content = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8).ToString();
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
                    "bacameter"
                ), "BacameterRootDialog");
        }
    }
}
