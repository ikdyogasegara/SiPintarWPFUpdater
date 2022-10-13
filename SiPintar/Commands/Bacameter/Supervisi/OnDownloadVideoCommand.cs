using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Bacameter;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    [ExcludeFromCodeCoverage]
    public class OnDownloadVideoCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly bool _isTest;

        public OnDownloadVideoCommand(SupervisiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null || parameter == null)
                return;

            var FileData = parameter as MediaElement;
            var FilePath = FileData.Source as Uri;

            if (FilePath == null || !FilePath.IsAbsoluteUri)
                return;

            try
            {
                string OriginalPath = FilePath.AbsolutePath.Replace("%20", " ");
                string FileName = OriginalPath.Split('/')[^1];
                string FileExtension = FileName.Split('.')[^1];

                // Directory
                string WindowsPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).ToString();
                string SystemFolder = WindowsPath.Split(':')[0];

                string DirPath = Path.Combine(SystemFolder, ":", "Users", Environment.UserName, "Downloads");

                // Confirmation Save File Dialog
                var SaveDlg = new SaveFileDialog
                {
                    Filter = $"Video File ({FileExtension})|*.{FileExtension}",
                    Title = "Download Video",
                    InitialDirectory = null
                };
                SaveDlg.InitialDirectory = DirPath;
                SaveDlg.FileName = FileName;

                if (SaveDlg.ShowDialog() == true)
                {
                    string saveAsLocation = SaveDlg.FileName;

                    File.Copy(OriginalPath, saveAsLocation, true);

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
            {
                DialogHost.Close("BacameterRootDialog");
                _ = DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan download data. Silahkan hubungi tim teknis terkait.",
                        "warning"
                    ), "BacameterRootDialog"
                 );
            }
        }
    }
}
