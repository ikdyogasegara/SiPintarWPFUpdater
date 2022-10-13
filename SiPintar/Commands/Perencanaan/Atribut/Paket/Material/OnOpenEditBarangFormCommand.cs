using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Material;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnOpenEditBarangFormCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditBarangFormCommand(PaketMaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingPaket || _viewModel.IsLoadingRincian || _viewModel.SelectedRincian == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KuantitasForm = _viewModel.SelectedRincian.Qty;
            _viewModel.SelectedBarang = new MasterMaterialDto()
            {
                NamaMaterial = _viewModel.SelectedRincian.NamaMaterial,
                Satuan = _viewModel.SelectedRincian.Satuan
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
                _ = DialogHost.Show(new KuantitasFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
