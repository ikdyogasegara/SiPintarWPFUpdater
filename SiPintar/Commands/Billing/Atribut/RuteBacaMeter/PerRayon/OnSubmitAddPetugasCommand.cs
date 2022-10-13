using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public class OnSubmitAddPetugasCommand : AsyncCommandBase
    {
        private readonly PerRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnSubmitAddPetugasCommand(PerRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingForm || parameter == null)
                return;

            var selected = parameter as MasterPetugasBacaDto;

            _viewModel.SelectedPetugas = selected;

            CloseDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest)
                DialogHost.Close("RuteBacaMeterSubDialog");
        }
    }
}
