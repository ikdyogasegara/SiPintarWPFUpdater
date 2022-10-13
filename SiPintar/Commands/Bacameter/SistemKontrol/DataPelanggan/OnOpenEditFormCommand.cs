using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly DataPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(DataPelangganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.FormData = _viewModel.SelectedData;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            //if (!_isTest)
            //    _ = DialogHost.Show(new DialogFormView(_viewModel), "BacameterRootDialog");
        }
    }
}
