using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    [ExcludeFromCodeCoverage]
    public class OnDownloadCsvSampleCommand : AsyncCommandBase
    {
        private readonly bool _isTest;

        public OnDownloadCsvSampleCommand(bool isTest = false)
        {
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var directoryFile = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template\\Samples";
            var FileName = "Calon_Pelanggan_Sambungan_Baru_01.csv";

            var filePath = Path.Combine(directoryFile, FileName);

            if (!File.Exists(filePath))
            {
                return;
            }

            try
            {
                var fileExtension = FileName.Split('.')[^1];

                // Directory
                var windowsPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).ToString();
                var systemFolder = windowsPath.Split(':')[0];

                var dirPath = Path.Combine(systemFolder, ":", "Users", Environment.UserName, "Downloads");

                // Confirmation Save File Dialog
                var saveDlg = new SaveFileDialog
                {
                    Filter = $"CSV File (*.{fileExtension})|*.{fileExtension}",
                    Title = "Download File Sample",
                    InitialDirectory = null
                };
                saveDlg.InitialDirectory = dirPath;
                saveDlg.FileName = FileName;

                if (saveDlg.ShowDialog() == true)
                {
                    var saveAsLocation = saveDlg.FileName;

                    File.Copy(filePath, saveAsLocation, true);

                    // Open Directory after success
                    var directory = saveAsLocation.Replace(FileName, string.Empty);

                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(directory)
                        {
                            UseShellExecute = true
                        }
                    };
                    p.Start();
                }
            }
            catch (Exception errorInfo)
            {
                ShowError();
                Debug.WriteLine(errorInfo);
                Log.Logger.Error(errorInfo.ToString());
            }

            await Task.FromResult(_isTest);
        }

        private void ShowError()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    "Terjadi Kesalahan",
                    "Tidak dapat melakukan download data. Silahkan hubungi tim teknis terkait.",
                    "warning",
                    "Oke",
                    false,
                    "hublang"
                ), "SambunganSubDialog");
        }
    }
}
