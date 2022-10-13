using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;
using SiPintar.Views.Bacameter.SistemKontrol.TarifGolongan.Diameter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Diameter
{
    public class OnOpenDetailFormCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel _viewModel;
        private readonly bool _isTest;
        public OnOpenDetailFormCommand(DiameterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFromDetail = true;

            _viewModel.DetailKeterangan = new List<dynamic>{
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
                    Keys = "Air Limbah",
                    Values = _viewModel.SelectedData.AirLimbah,
                },
                new
                {
                    Keys = "Denda PK 0",
                    Values = _viewModel.SelectedData.DendaPakai0,
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
            if (!_isTest)
                _ = DialogHost.Show(new DetailDiameterView(_viewModel), "BacameterRootDialog");
        }
    }
}
