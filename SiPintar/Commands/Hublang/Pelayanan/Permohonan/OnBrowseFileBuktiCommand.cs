using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseFileBuktiCommand : AsyncCommandBase
    {
        private readonly PermohonanViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseFileBuktiCommand(PermohonanViewModel viewModel, bool isTest = false)
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
                    Filter = "JPG File|*.jpg|JPEG File|*.jpeg|PNG File|*.png|Bitmap File|*.bmp"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    if (param == "foto_bukti1")
                    {
                        _viewModel.FotoBukti1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti1 = true;
                    }

                    if (param == "foto_bukti2")
                    {
                        _viewModel.FotoBukti2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti2 = true;
                    }

                    if (param == "foto_bukti3")
                    {
                        _viewModel.FotoBukti3Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBukti3 = true;
                    }

                    if (param == "foto_buktispk1")
                    {
                        _viewModel.FotoBuktiSpk1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiSpk1 = true;
                    }

                    if (param == "foto_buktispk2")
                    {
                        _viewModel.FotoBuktiSpk2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiSpk2 = true;
                    }

                    if (param == "foto_buktispk3")
                    {
                        _viewModel.FotoBuktiSpk3Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiSpk3 = true;
                    }

                    if (param == "foto_denah1")
                    {
                        _viewModel.FotoDenah1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoDenah1 = true;
                    }

                    if (param == "foto_denah2")
                    {
                        _viewModel.FotoDenah2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoDenah2 = true;
                    }

                    if (param == "foto_buktirab1")
                    {
                        _viewModel.FotoBuktiRab1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiRab1 = true;
                    }

                    if (param == "foto_buktirab2")
                    {
                        _viewModel.FotoBuktiRab2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiRab2 = true;
                    }

                    if (param == "foto_buktirab3")
                    {
                        _viewModel.FotoBuktiRab3Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiRab3 = true;
                    }

                    if (param == "foto_buktiberitaacara1")
                    {
                        _viewModel.FotoBuktiBeritaAcara1Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiBeritaAcara1 = true;
                    }

                    if (param == "foto_buktiberitaacara2")
                    {
                        _viewModel.FotoBuktiBeritaAcara2Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiBeritaAcara2 = true;
                    }

                    if (param == "foto_buktiberitaacara3")
                    {
                        _viewModel.FotoBuktiBeritaAcara3Uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                        _viewModel.IsNewFotoBuktiBeritaAcara3 = true;
                    }
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
            {
                string moduleName;
                if (_viewModel.FiturName == "SPK")
                {
                    moduleName = "perencanaan";
                }
                else if (_viewModel.FiturName == "RAB")
                {
                    moduleName = "perencanaan";
                }
                else if (_viewModel.FiturName == "Berita Acara")
                {
                    moduleName = "distribusi";
                }
                else
                {
                    moduleName = "hublang";
                }

                _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Data",
                    "Pastikan file anda dalam format yang benar dan mengandung data yang sesuai.",
                    "warning",
                    "Batal",
                    false,
                    moduleName
                ), "SambunganSubDialog");
            }
        }
    }
}
