using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialDesignThemes.Wpf;
using Serilog;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.AppConfig
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseReportPathCommand : AsyncCommandBase
    {
        private readonly AppConfigViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseReportPathCommand(AppConfigViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                bool IsFileReportExist = false;
                string SelectedPath = null;

                bool IsCanceled = false;

                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string[] files = Directory.GetFiles(fbd.SelectedPath);

                        foreach (var item in files)
                        {
                            if (item.ToLower().Contains("sipintarreport.exe"))
                            {
                                IsFileReportExist = true;
                                SelectedPath = fbd.SelectedPath;
                                break;
                            }
                        }
                    }
                    else
                    {
                        IsCanceled = true;
                    }
                }

                if (IsCanceled)
                    return;

                if (!IsFileReportExist)
                    ShowWarning();
                else
                {
                    _viewModel.ReportAppPath = SelectedPath;
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
                    "Pastikan direktori yang dipilih merupakan folder aplikasi SiPintarReport.exe",
                    "warning",
                    "Oke",
                    false
                ), "ApplicationConfigViewDialog");
        }
    }
}
