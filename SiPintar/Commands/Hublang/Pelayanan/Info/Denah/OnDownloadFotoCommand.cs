using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.Denah
{
    [ExcludeFromCodeCoverage]
    public class OnDownloadFotoCommand : AsyncCommandBase
    {
        private readonly DenahViewModel _viewModel;
        private readonly bool _isTest;

        public OnDownloadFotoCommand(DenahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null || parameter == null)
                return;

            var ImageData = parameter as Image;
            var ImgPath = (ImageData.Source as BitmapImage).UriSource;

            if (ImgPath == null || !ImgPath.IsAbsoluteUri)
                return;

            try
            {
                string OriginalPath = ImgPath.AbsolutePath.Replace("%20", " ");
                string FileName = OriginalPath.Split('/')[^1];
                string FileExtension = FileName.Split('.')[^1];

                // Directory
                string WindowsPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).ToString();
                string SystemFolder = WindowsPath.Split(':')[0];

                string DirPath = Path.Combine(SystemFolder, ":", "Users", Environment.UserName, "Downloads");

                // Confirmation Save File Dialog
                var SaveDlg = new SaveFileDialog
                {
                    Filter = $"Image File ({FileExtension})|*.{FileExtension}",
                    Title = "Download Foto",
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
                DialogHost.Close("HublangRootDialog");
                _ = DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan download data. Silahkan hubungi tim teknis terkait.",
                        "warning"
                    ), "HublangRootDialog"
                 );
            }
        }
    }
}
