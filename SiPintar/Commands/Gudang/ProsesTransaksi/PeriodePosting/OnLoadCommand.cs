using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnLoadCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        public OnLoadCommand(PeriodePostingViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            Vm.OnRefreshCommand.Execute(null);
            _ = RestApi;
            await Task.FromResult(IsTest);
        }
    }
}
