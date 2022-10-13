using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;
using SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerPetugasBaca;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    public class OnOpenAddRayonCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddRayonCommand(PerPetugasBacaViewModel viewModel, bool isTest = false)
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
