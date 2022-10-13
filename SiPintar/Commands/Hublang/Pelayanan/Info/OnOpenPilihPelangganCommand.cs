using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Info
{
    public class OnOpenPilihPelangganCommand : AsyncCommandBase
    {
        private readonly InfoViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPilihPelangganCommand(InfoViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public bool SetToDataLangganan()
        {
            _viewModel.NavigationItems = _viewModel.GetNavigationItems("Data Langganan");
            _ = _viewModel.UpdatePageAsync("Data Langganan");
            return true;
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new PilihPelangganDialog(_restApi, SetSelectedPelanggan, SetToDataLangganan);

        [ExcludeFromCodeCoverage]
        private bool SetSelectedPelanggan(dynamic param)
        {
            _viewModel.SelectedPelanggan = param;
            return true;
        }
    }
}
