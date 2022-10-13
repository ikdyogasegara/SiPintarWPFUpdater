using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Ongkos;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos
{
    public class OnOpenEditPaketFormCommand : AsyncCommandBase
    {
        private readonly PaketOngkosViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditPaketFormCommand(PaketOngkosViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingPaket || _viewModel.IsLoadingRincian || _viewModel.SelectedPaket == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodePaketForm = _viewModel.SelectedPaket.KodePaketOngkos;
            _viewModel.NamaPaketForm = _viewModel.SelectedPaket.NamaPaketOngkos;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new PaketFormView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
