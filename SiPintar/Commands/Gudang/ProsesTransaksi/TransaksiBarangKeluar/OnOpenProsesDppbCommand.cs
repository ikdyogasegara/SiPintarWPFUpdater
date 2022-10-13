using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenProsesDppbCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenProsesDppbCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedData == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Proses Daftar Barang Keluar",
                    "Data belum dipilih!",
                    "warning", moduleName: "gudang");
            }
            else
            {
                await GetPeriodeAsync();
                _vm.FormProses = new ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarWpf()
                {
                    IdPengajuanPengeluaran = _vm.SelectedData.IdPengajuanPengeluaran,
                    IdGudang = _vm.SelectedData.IdGudang,
                    WaktuDikeluarkan = DateTime.Now
                };
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);
            }
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogProsesDataView(_vm);

        private async Task GetPeriodeAsync()
        {
            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterPeriodeGudang"
            });
            _vm.PeriodeList = MasterListGlobal.MasterPeriodeGudang;
        }
    }
}
