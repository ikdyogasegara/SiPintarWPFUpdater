using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Kas;

namespace SiPintar.Commands.Akuntansi.Jurnal.Kas.PembayaranKas
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PembayaranKasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PembayaranKasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            _ = _viewModel;
            _ = _restApi;

            await Task.FromResult(_isTest);
        }
    }
}
