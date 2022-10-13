using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenTambahBarangCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenTambahBarangCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.CariBarangForm = new MasterBarangWpf();
            if (_vm.SelectedData == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Tambah Barang",
                    "Data Pengajuan belum dipilih!",
                    "warning", moduleName: "gudang");
            }
            else
            {
                object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog",
                    "Mohon tunggu",
                    "sedang mempersiapkan data...");
                await UpdateListData.ProcessAsync(_isTest, new List<string>()
                {
                    "MasterGudang",
                });
                _vm.GudangList = MasterListGlobal.MasterGudang ?? new ObservableCollection<MasterGudangDto>();
                _vm.SelectedGudang = _vm.GudangList.FirstOrDefault(x => x.IdGudang == _vm.SelectedData.IdGudang);
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true,
                    "GudangRootDialog", GetInstance);
            }
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new DialogTambahBarangView(_vm);
    }
}
