using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TarifGolonganViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(TarifGolonganViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            _viewModel.CurrentSection = "golongan";

            await Task.FromResult(_isTest);
        }
    }
}
