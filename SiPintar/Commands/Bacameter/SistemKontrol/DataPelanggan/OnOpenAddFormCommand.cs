using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DataPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DataPelangganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new MasterPelangganAirDto();

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
