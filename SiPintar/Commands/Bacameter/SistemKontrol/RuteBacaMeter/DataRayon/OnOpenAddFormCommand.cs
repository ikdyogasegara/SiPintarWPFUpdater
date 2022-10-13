using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;
using SiPintar.Views.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DataRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedRayon == null)
                return;

            _viewModel.SelectedPetugas = null;
            _viewModel.IsTglBacaChecked = false;
            _viewModel.ToleransiMinus = 0;
            _viewModel.ToleransiPlus = 0;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new AddFormView(_viewModel), "BacameterRootDialog");
        }
    }
}
