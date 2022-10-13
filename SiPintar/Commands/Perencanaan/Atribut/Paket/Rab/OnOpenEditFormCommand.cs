using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Rab;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PaketRabViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeForm = _viewModel.SelectedData.KodePaket;
            _viewModel.NamaForm = _viewModel.SelectedData.NamaPaket;
            _viewModel.PaketMaterialForm = _viewModel.PaketMaterialList.FirstOrDefault(i => i.IdPaketMaterial == _viewModel.SelectedData.IdPaketMaterial);
            _viewModel.PaketOngkosForm = _viewModel.PaketOngkosList.FirstOrDefault(i => i.IdPaketOngkos == _viewModel.SelectedData.IdPaketOngkos);
            _viewModel.HargaPaketForm = _viewModel.SelectedData.HargaNetPaket;
            _viewModel.PersentaseKeuntunganForm = _viewModel.SelectedData.PersentaseKeuntungan;
            _viewModel.PersentaseJasaDariBahanForm = _viewModel.SelectedData.PersentaseJasaDariBahan;
            _viewModel.IsHargaMaterialChecked = _viewModel.SelectedData.HargaNetMaterial > 0;
            _viewModel.IsHargaOngkosChecked = _viewModel.SelectedData.HargaNetOngkos > 0;
            _viewModel.HargaNetMaterialForm = _viewModel.SelectedData.HargaNetMaterial;
            _viewModel.HargaNetOngkosForm = _viewModel.SelectedData.HargaNetOngkos;

            _viewModel.WithPpnForm = _viewModel.SelectedData.PpnMaterial > 0 || _viewModel.SelectedData.PpnMaterialTambahan > 0 ||
                _viewModel.SelectedData.PpnOngkos > 0 || _viewModel.SelectedData.PpnOngkosTambahan > 0;
            _viewModel.NoPpnForm = !_viewModel.WithPpnForm;

            _viewModel.PpnMaterialForm = _viewModel.SelectedData.PpnMaterial;
            _viewModel.PpnMaterialTambahanForm = _viewModel.SelectedData.PpnMaterialTambahan;
            _viewModel.PpnOngkosForm = _viewModel.SelectedData.PpnOngkos;
            _viewModel.PpnOngkosTambahanForm = _viewModel.SelectedData.PpnOngkosTambahan;

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
