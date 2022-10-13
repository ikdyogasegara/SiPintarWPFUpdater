using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Ongkos;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    public class OnSubmitAddItemStep1Command : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitAddItemStep1Command(PaketOngkosViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm || !(parameter is int idOngkos))
                return;

            _viewModel.IsEdit = false;
            _viewModel.KuantitasForm = null;
            _viewModel.SelectedItem = _viewModel.SearchList.FirstOrDefault(i => i.IdOngkos == idOngkos);

            if (_viewModel.SelectedItem == null)
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
