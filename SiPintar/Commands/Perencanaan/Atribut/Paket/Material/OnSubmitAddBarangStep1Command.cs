using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Material;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnSubmitAddBarangStep1Command : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitAddBarangStep1Command(PaketMaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm || !(parameter is int idMaterial))
                return;

            _viewModel.IsEdit = false;
            _viewModel.KuantitasForm = null;
            _viewModel.SelectedBarang = _viewModel.SearchList.FirstOrDefault(i => i.IdMaterial == idMaterial);

            if (_viewModel.SelectedBarang == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
            {
                DialogHost.Close("PerencanaanRootDialog");
                _ = DialogHost.Show(new KuantitasFormView(_viewModel), "PerencanaanRootDialog");
            }
        }
    }
}
