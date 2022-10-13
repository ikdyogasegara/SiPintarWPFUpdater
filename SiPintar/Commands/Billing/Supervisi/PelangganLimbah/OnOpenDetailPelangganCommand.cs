using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenDetailPelangganCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenDetailPelangganCommand(PelangganLimbahViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            if (_viewModel.IsLoading)
                return;

            _viewModel.DetailData = _viewModel.SelectedData;

            _viewModel.IsLoadingForm = true;

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DetailPelangganView(_viewModel), "BillingRootDialog");
        }
    }
}
