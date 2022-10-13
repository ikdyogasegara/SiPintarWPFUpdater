using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class OnOpenAddPetugasCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddPetugasCommand(DataRayonViewModel viewModel, bool isTest = false)
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
