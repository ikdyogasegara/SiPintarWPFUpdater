using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Material;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Material
{
    public class OnOpenAddPaketFormCommand : AsyncCommandBase
    {
        private readonly PaketMaterialViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddPaketFormCommand(PaketMaterialViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingPaket || _viewModel.IsLoadingRincian)
                return;

            _viewModel.IsEdit = false;
            _viewModel.KodePaketForm = null;
            _viewModel.NamaPaketForm = null;

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
