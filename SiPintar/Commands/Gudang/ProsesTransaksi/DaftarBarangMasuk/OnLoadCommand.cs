using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnLoadCommand(DaftarBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memuat data ...");
            await UpdateListData.ProcessAsync(_isTest, new List<string>
            {
                "MasterPeriodeGudang",
            });
            _vm.DaftarPeriodeFilter = MasterListGlobal.MasterPeriodeGudang;
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);

            _ = ((AsyncCommandBase)_vm.OnRefreshCommand).ExecuteAsync(null);
            await Task.FromResult(_isTest);
        }
    }
}
