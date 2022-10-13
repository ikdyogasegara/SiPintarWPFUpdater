using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnLoadCommand(DaftarBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memuat data ...");
            await UpdateListData.ProcessAsync(_isTest, new List<string>
            {
                "MasterGudang",
                "MasterPeriodeGudang",
                "MasterKeperluan"
            });
            _vm.DaftarPeriodeFilter = MasterListGlobal.MasterPeriodeGudang;
            _vm.DaftarGudangFilter = MasterListGlobal.MasterGudang;
            _vm.DaftarKeperluan = MasterListGlobal.MasterKeperluan;
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);

            _ = ((AsyncCommandBase)_vm.OnRefreshCommand).ExecuteAsync(parameter: null);
            await Task.FromResult(_isTest);
        }
    }
}
