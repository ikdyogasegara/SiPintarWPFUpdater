using System.Threading.Tasks;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.Commands.Gudang.Pengolahan.PenyesuaianStock
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PenyesuaianStockViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnLoadCommand(PenyesuaianStockViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..

            _ = _vm;
            _ = _restApi;

            await Task.FromResult(_isTest);
        }
    }
}
