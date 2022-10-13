using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Golongan;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    public class OnOpenDetailFormCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenDetailFormCommand(GolonganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFromDetail = true;

            _viewModel.DetailData = new List<dynamic>
            {
                new
                {
                    BB = _viewModel.SelectedData.Bb1,
                    BA = _viewModel.SelectedData.Ba1,
                    Tarif = _viewModel.SelectedData.T1,
                    Tetap = _viewModel.SelectedData.T1Tetap,
                },
                new
                {
                    BB = _viewModel.SelectedData.Bb2,
                    BA = _viewModel.SelectedData.Ba2,
                    Tarif = _viewModel.SelectedData.T2,
                    Tetap = _viewModel.SelectedData.T2Tetap,
                },
                new
                {
                    BB = _viewModel.SelectedData.Bb3,
                    BA = _viewModel.SelectedData.Ba3,
                    Tarif = _viewModel.SelectedData.T3,
                    Tetap = _viewModel.SelectedData.T3Tetap,
                },
                new
                {
                    BB = _viewModel.SelectedData.Bb4,
                    BA = _viewModel.SelectedData.Ba4,
                    Tarif = _viewModel.SelectedData.T4,
                    Tetap = _viewModel.SelectedData.T4Tetap,
                },
                new
                {
                    BB = _viewModel.SelectedData.Bb5,
                    BA = _viewModel.SelectedData.Ba5,
                    Tarif = _viewModel.SelectedData.T5,
                    Tetap = _viewModel.SelectedData.T5Tetap,
                },

            };

            _viewModel.DetailDenda = new List<dynamic>
            {
                new
                {
                    Keys = "Denda Tunggakan 1",
                    Values = _viewModel.SelectedData.DendaTunggakan1,
                },
                new
                {
                    Keys = "Denda Tunggakan 2",
                    Values = _viewModel.SelectedData.DendaTunggakan2,
                },
                new
                {
                    Keys = "Denda Tunggakan 3",
                    Values = _viewModel.SelectedData.DendaTunggakan3,
                },
                new
                {
                    Keys = "Denda Tunggakan 4",
                    Values = _viewModel.SelectedData.DendaTunggakan4,
                },
                new
                {
                    Keys = "Denda Tunggakan per Bulan",
                    Values = _viewModel.SelectedData.DendaTunggakanPerBulan,
                }
            };


            _viewModel.DetailKeterangan = new List<dynamic>
            {
                new
                {
                    Keys = "Administrasi",
                    Values = _viewModel.SelectedData.Administrasi,
                },
                new
                {
                    Keys = "Pemeliharaan",
                    Values = _viewModel.SelectedData.Pemeliharaan,
                },
                new
                {
                    Keys = "Pelayanan",
                    Values = _viewModel.SelectedData.Pelayanan,
                },
                new
                {
                    Keys = "Retribusi",
                    Values = _viewModel.SelectedData.Retribusi,
                },
                new
                {
                    Keys = "Minimal Denda",
                    Values = _viewModel.SelectedData.MinDenda,
                },
                new
                {
                    Keys = "Air Limbah",
                    Values = _viewModel.SelectedData.AirLimbah,
                },
                new
                {
                    Keys = "Denda PK 0",
                    Values = _viewModel.SelectedData.DendaPakai0,
                },
                new
                {
                    Keys = "Retribusi Pakai",
                    Values = _viewModel.SelectedData.RetribusiPakai,
                },
                new
                {
                    Keys = "Ppn %",
                    Values = _viewModel.SelectedData.Ppn,
                },
                new
                {
                    Keys = "Tarif Denda %",
                    Values = _viewModel.SelectedData.TrfDendaBerdasarkanPersen == true ? "Ya":"Tidak",
                }
            };

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DetailGolonganView(_viewModel), "BacameterRootDialog");
        }
    }
}
