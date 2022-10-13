using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public class OnOpenAddRayonCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddRayonCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm)
                return;

            ShowDialog();

            _viewModel.Parent.KodeRayonFilter = null;
            _viewModel.Parent.NamaRayonFilter = null;

            _ = GetRayonAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new ListRayonFormView(_viewModel), "RuteBacaMeterSubDialog");
        }

        private async Task GetRayonAsync()
        {
            if (!_isTest)
                await ((AsyncCommandBase)_viewModel.Parent.GetDataRayonCommand).ExecuteAsync(null);
        }
    }
}
