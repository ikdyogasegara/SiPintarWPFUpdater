using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangKeluar;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnOpenAddCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public readonly bool _isTest;

        public OnOpenAddCommand(DaftarBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangKeluar is null)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Tambah Barang",
                    "Data BPP belum dipilih!", "error", moduleName: "gudang");
                return;
            }
            if (_vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai != null && _vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai == 1)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Tambah Barang",
                    "Data BPP sudah diverifikasi!", "error", moduleName: "gudang");
                return;
            }
            _vm.AddBarang = null!;
            _vm.AddBarangWithStock = null!;
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogAddView(_vm);
    }
}
