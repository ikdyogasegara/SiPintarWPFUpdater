using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Voucher;
using SiPintar.Views.Akuntansi.Voucher.PembayaranPembatalan;

namespace SiPintar.Commands.Akuntansi.Voucher.PembayaranPembatalan
{
    public class OnOpenPembayaranFormCommand : AsyncCommandBase
    {
        private readonly PembayaranPembatalanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPembayaranFormCommand(PembayaranPembatalanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);

        }

        private object GetInstance() => new DialogPembayaranFormView(_viewModel);
    }
}
