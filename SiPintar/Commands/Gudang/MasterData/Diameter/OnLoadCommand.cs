using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Diameter
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;

        public OnLoadCommand(DiameterViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
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
