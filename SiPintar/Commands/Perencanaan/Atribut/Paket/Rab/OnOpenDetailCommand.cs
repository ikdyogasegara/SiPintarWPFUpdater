using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Perencanaan.Atribut.Paket;
using SiPintar.Views.Perencanaan.Atribut.Paket.Rab;

namespace SiPintar.Commands.Perencanaan.Atribut.Paket.Rab
{
    public class OnOpenDetailCommand : AsyncCommandBase
    {
        private readonly PaketRabViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailCommand(PaketRabViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DetailView(_viewModel), "PerencanaanRootDialog");
        }
    }
}
