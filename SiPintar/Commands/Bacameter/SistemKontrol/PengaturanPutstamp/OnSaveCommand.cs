using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PengaturanPutstamp
{
    [ExcludeFromCodeCoverage]
    public class OnSaveCommand : AsyncCommandBase
    {
        private readonly PengaturanPutstampViewModel _viewModel;
        private readonly bool _isTest;

        public OnSaveCommand(PengaturanPutstampViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.Content == null)
                return;

            bool IsSuccess = false;
            string SaveAsLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}";
            string FileName = "customize.ini";

            try
            {
                // Confirmation Save File Dialog
                var SaveDlg = new SaveFileDialog
                {
                    Filter = "Configuration files (*.ini)|*.ini|Text files (*.txt)|*.txt",
                    Title = "Simpan file .ini",
                    InitialDirectory = null
                };
                SaveDlg.InitialDirectory = SaveAsLocation;
                SaveDlg.FileName = FileName;

                if (SaveDlg.ShowDialog() == true)
                {
                    SaveAsLocation = SaveDlg.FileName;
                    FileName = SaveDlg.SafeFileName;

                    File.WriteAllText(SaveAsLocation, _viewModel.Content);
                    IsSuccess = true;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("write file error: " + error);
            }

            if (IsSuccess)
                _ = OpenResultAsync(SaveAsLocation, FileName);

            await Task.FromResult(_isTest);
        }

        private async Task OpenResultAsync(string saveAsLocation, string FileName)
        {
            if (!_isTest)
            {
                await DialogHost.Show(
                    new DialogGlobalView(
                        "Simpan File",
                        "Data berhasil disimpan.",
                        "success",
                        "Buka Lokasi File"
                        ), "BacameterRootDialog"
                );

                // Open Directory after success
                string Directory = saveAsLocation.Replace(FileName, string.Empty);

                var p = new Process
                {
                    StartInfo = new ProcessStartInfo(Directory)
                    {
                        UseShellExecute = true
                    }
                };
                p.Start();

                _viewModel.OnResetCommand.Execute(null);
            }
        }
    }
}
