using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnOpenDeleteBarangCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public readonly bool _isTest;

        public OnOpenDeleteBarangCommand(DaftarBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangKeluarDetail is null)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus Barang",
                    "Data Barang belum dipilih!", "error", moduleName: "gudang");
                return;
            }
            if (_vm.SelectedDaftarBarangKeluarDetail.FlagVerifikasi ?? false)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus Barang",
                    "Data Barang sudah diverifikasi!", "error", moduleName: "gudang");
                return;
            }
            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Hapus Barang",
                "Barang " + _vm.SelectedDaftarBarangKeluarDetail.NamaBarang + " akan di hapus. Lanjutkan ?",
                "confirmation", moduleName: "gudang", highlightText: _vm.SelectedDaftarBarangKeluarDetail.NamaBarang, yesButtonCommand: _vm.OnDeleteBarangCommand);
            await Task.FromResult(_isTest);
        }
    }
}
