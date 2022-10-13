using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Perencanaan.Atribut.Paket.Ongkos;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    public class OnOpenAddItemFormCommand : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddItemFormCommand(PaketOngkosViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingRincian)
                return;

            if (_viewModel.SelectedPaket == null)
            {
                ShowWarningPaket();
                return;
            }

            _viewModel.IsEdit = false;
            _viewModel.NamaItemForm = null;
            _viewModel.SelectedItem = null;
            _viewModel.KuantitasForm = null;

            // get list
            _viewModel.OnSearchItemCommand.Execute(null);

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new ItemFormView(_viewModel), "PerencanaanRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowWarningPaket()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", "Silakan pilih Paket terlebih dahulu", "error", "Ok", false, "perencanaan"), "PerencanaanRootDialog");
        }
    }
}
