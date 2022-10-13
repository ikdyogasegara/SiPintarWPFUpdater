using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DistribusiBarang
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        public readonly DistribusiBarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        public OnRefreshCommand(DistribusiBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await Task.Delay(1000);
            _ = Vm;
            _ = RestApi;
            await Task.FromResult(IsTest);
        }
    }
}
