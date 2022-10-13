using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenHapusPengajuanCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenHapusPengajuanCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedData == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog",
                    "Hapus Pengajuan", "Data belum dipilih!", "error", "Ok",
                    moduleName: "gudang");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog",
                    "Anda Akan Menghapus Pengajuan Pengeluaran Barang",
                    $"Apakah Anda ingin melanjutkan hapus DPPB Nomor {_vm.SelectedData.NomorPengajuanPengeluaran} ?",
                    "warning", yesButtonCommand: _vm.OnSubmitHapusPengajuanCommand, yesButtonText: "Ya, Hapus", moduleName: "gudang",
                    highlightText: $"{_vm.SelectedData.NomorPengajuanPengeluaran}", isEnabledCetakLaporan: false);
            }
            _ = await Task.FromResult(_isTest);
        }
    }
}
