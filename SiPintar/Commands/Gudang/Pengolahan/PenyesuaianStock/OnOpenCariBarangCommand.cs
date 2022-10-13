using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Gudang.Pengolahan.PenyesuaianStock
{
    public class OnOpenCariBarangCommand : AsyncCommandBase
    {
        private readonly PenyesuaianStockViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenCariBarangCommand(PenyesuaianStockViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "DialogPenyesuaianStock", GetInstance);
            _ = _vm;
            _ = _restApi;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogPilihBarangMaterialView((MasterBarangDto param) =>
        {
            //use data here
            return true;
        }, _restApi);
    }
}
