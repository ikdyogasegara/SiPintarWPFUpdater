using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;
using SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerRayon;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public class OnOpenAddPetugasCommand : AsyncCommandBase
    {
        private readonly PerRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddPetugasCommand(PerRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm)
                return;

            ShowDialog();

            _viewModel.Parent.KodePetugasFilter = null;
            _viewModel.Parent.NamaPetugasFilter = null;

            _ = GetPetugasAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new ListPetugasFormView(_viewModel), "RuteBacaMeterSubDialog");
        }

        private async Task GetPetugasAsync()
        {
            if (!_isTest)
                await ((AsyncCommandBase)_viewModel.Parent.GetDataPetugasCommand).ExecuteAsync(null);
        }
    }
}
