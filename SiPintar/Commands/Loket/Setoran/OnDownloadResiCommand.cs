using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Win32;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Setoran
{
    [ExcludeFromCodeCoverage]
    public class OnDownloadResiCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly bool _isTest;

        public OnDownloadResiCommand(SetoranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            var dlg = new SaveFileDialog()
            {
                FileName = DateTime.Now.ToString("MMddyyyy"), // Default file name
                //DefaultExt = ".png", // Default file extension
                Filter = "JPEG File|*.jpg|Bitmap File|*.bmp|PNG File|*.png" // Filter files by extension
            };

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {

                dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadFileAsync(new Uri(_viewModel.SelectedData.Foto), dlg.FileName);
                }

                // Save document
                string filename = dlg.FileName;
                DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");

            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "LoketRootDialog", "Konfirmasi", "Gambar berhasil di unduh !",
                "success", "Tutup", false, "loket");
        }
    }
}
