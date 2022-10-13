using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangKeluar
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly KategoriBarangKeluarViewModel Vm;
        private readonly IRestApiClientModel RestApi;
        private readonly bool IsTest;

        public OnLoadCommand(KategoriBarangKeluarViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _ = Vm;
            _ = RestApi;
            await Task.FromResult(IsTest);
        }
    }
}
