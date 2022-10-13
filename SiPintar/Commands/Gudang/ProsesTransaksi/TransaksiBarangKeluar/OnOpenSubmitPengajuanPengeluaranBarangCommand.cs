using System;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenSubmitPengajuanPengeluaranBarangCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenSubmitPengajuanPengeluaranBarangCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.Form.IdGudang = _vm.SelectedGudang.IdGudang;
            _vm.Form.IdKategoriBarangKeluar = _vm.SelectedKategori.IdKategoriBarangKeluar;
            _vm.Form.IdBagianMemintaBarang = _vm.SelectedBagianMemintaBarang.IdBagianMemintaBarang;
            _vm.Form.IdUser = Application.Current.Resources["IdUser"] as int?;
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog",
                "Proses Daftar Pengajuan Pengeluaran Barang (DPPB)?",
                $"Anda akan memproses DPPB dengan No. {_vm.Form.NomorPengajuanPengeluaranWpf} Anda dapat melihat DPPB yang telah diproses pada halaman Supervisi Pengeluaran Barang",
                type: "confirmation",
                yesButtonText: "Ya, Proses DPPB",
                highlightText: _vm.Form.NomorPengajuanPengeluaran,
                moduleName: "gudang",
                yesButtonCommand: _vm.OnSubmitPengajuanPengeluaranBarangCommand,
                isEnabledCetakLaporan: true);
            await Task.FromResult(_isTest);
        }
    }
}
