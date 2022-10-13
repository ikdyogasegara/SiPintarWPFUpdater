using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenKoreksiDataPengajuanCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenKoreksiDataPengajuanCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedData == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Koreksi Data Pengajuan", "Data belum dipilih!", "warning", moduleName: "gudang");
            }
            else
            {
                object loading = DialogHelper.ShowDialogGlobalLoadingAsync(
                _isTest, true, "GudangRootDialog", "Mohon tunggu",
                "sedang memuat data...");

                await UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterKategoriBarangKeluar",
                    "MasterGudang",
                    "MasterBagianMemintaBarang"
                });

                _vm.KategoriList = MasterListGlobal.MasterKategoriBarangKeluar ?? _vm.KategoriList;
                _vm.GudangList = MasterListGlobal.MasterGudang ?? _vm.GudangList;
                _vm.BagianMemintaBarangList = MasterListGlobal.MasterBagianMemintaBarang ?? _vm.BagianMemintaBarangList;
                _vm.SelectedKategori = _vm.KategoriList.FirstOrDefault(x => x.IdKategoriBarangKeluar == _vm.SelectedData.IdKategoriBarangKeluar);
                _vm.SelectedGudang = _vm.GudangList.FirstOrDefault(x => x.IdGudang == _vm.SelectedData.IdGudang);
                _vm.SelectedBagianMemintaBarang = _vm.BagianMemintaBarangList.FirstOrDefault(x => x.IdBagianMemintaBarang == _vm.SelectedData.IdBagianMemintaBarang);
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true,
                    "GudangRootDialog", GetInstance);
            }
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogKoreksiDataView(_vm);
    }
}
