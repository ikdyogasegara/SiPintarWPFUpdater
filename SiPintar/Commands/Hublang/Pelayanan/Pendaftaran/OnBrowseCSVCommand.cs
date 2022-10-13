using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Serilog;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    [ExcludeFromCodeCoverage]
    public class OnBrowseCsvCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnBrowseCsvCommand(PendaftaranViewModel viewModel, bool isTest = false)
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
                    Filter = "CSV File (*.csv)|*.csv"
                };

                string[] data = null;

                if (openFileDialog.ShowDialog() == true)
                {
                    data = await File.ReadAllLinesAsync(openFileDialog.FileName);
                    data = data.Skip(1).ToArray();

                    _viewModel.FilenameCsvForm = openFileDialog.SafeFileName;
                }

                _viewModel.IsLoadingForm = true;

                _viewModel.KolektifList = new ObservableCollection<CalonPelangganCsvObject>();

                if (data != null && data.Length > 0)
                {
                    _viewModel.KolektifList = new ObservableCollection<CalonPelangganCsvObject>(
                        data.Select(line =>
                        {
                            var data = line.Split(';');
                            var result = new CalonPelangganCsvObject()
                            {
                                Nama = data[0],
                                Alamat = data[1],
                                KodeRayon = data[2],
                                KodeKelurahan = data[3],
                                KodeDiameter = data[4],
                                MerkMeter = data[5],
                                KodeGol = data[6],
                                NoHp = data[7],
                                SeriMeter = data[8],
                                TglDaftar = data[9],
                                NoKtp = data[10],
                                NoKk = data[11],
                                NamaKolektif = data[12]
                            };
                            // data[0], data[1], Convert.ToInt32(data[2]), data[3]
                            return result;
                        })
                    );
                }
                else
                {
                    ShowWarning();
                }

                _viewModel.IsLoadingForm = false;
                _viewModel.IsEmptyDialog = _viewModel.KolektifList.Count == 0;
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.ToString());
                Debug.WriteLine(e.Message);
                ShowWarning();
            }

            await Task.FromResult(_isTest);
        }

        private void ShowWarning()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView(
                    "Tidak Ada Data",
                    "Pastikan file CSV dalam format yang benar dan mengandung data yang sesuai.",
                    "warning",
                    "Batal",
                    false,
                    "hublang"
                ), "SambunganSubDialog");
        }
    }
}
