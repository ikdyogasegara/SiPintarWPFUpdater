using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Rab;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PaketRabViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;

            _viewModel.IsHargaMaterialChecked = false;
            _viewModel.IsHargaOngkosChecked = false;

            _viewModel.KodeForm = null;
            _viewModel.NamaForm = null;
            _viewModel.WithPpnForm = false;
            _viewModel.NoPpnForm = true;
            _viewModel.PaketMaterialForm = null;
            _viewModel.PaketOngkosForm = null;
            _viewModel.HargaPaketForm = null;
            _viewModel.HargaNetMaterialForm = 0;
            _viewModel.HargaNetOngkosForm = 0;
            _viewModel.PersentaseKeuntunganForm = 0;
            _viewModel.PersentaseJasaDariBahanForm = 0;
            _viewModel.PpnMaterialForm = null;
            _viewModel.PpnOngkosForm = null;
            _viewModel.PpnMaterialTambahanForm = null;
            _viewModel.PpnOngkosTambahanForm = null;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
