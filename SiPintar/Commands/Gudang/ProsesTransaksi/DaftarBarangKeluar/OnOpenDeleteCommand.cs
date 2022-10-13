using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnOpenDeleteCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public readonly bool _isTest;

        public OnOpenDeleteCommand(DaftarBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangKeluar is null)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus BPP",
                    "Data BPP belum dipilih!", "error", moduleName: "gudang");
                return;
            }
            if (_vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai != null && _vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai == 1)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus BPP",
                    "Data BPP sudah diverifikasi!", "error", moduleName: "gudang");
                return;
            }
            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Hapus BPP",
                "Data BPP " + _vm.SelectedDaftarBarangKeluar.NomorTransaksi + " akan di hapus. Lanjutkan ?",
                "confirmation", moduleName: "gudang", highlightText: _vm.SelectedDaftarBarangKeluar.NomorTransaksi, yesButtonCommand: _vm.OnDeleteCommand);
            await Task.FromResult(_isTest);
        }
    }
}
