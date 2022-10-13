using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangKeluar;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnOpenVerifikasiSemuaBarangCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        public readonly bool _isTest;

        public OnOpenVerifikasiSemuaBarangCommand(DaftarBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangKeluar is null)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Verifikasi BPP",
                    "Data BPP belum dipilih!", "error", moduleName: "gudang");
                return;
            }
            if (_vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai != null && _vm.SelectedDaftarBarangKeluar.FlagVerifikasi_Selesai == 1)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Verifikasi BPP",
                    "Data BPP sudah diverifikasi!", "error", moduleName: "gudang");
                return;
            }
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogVerifikasiBarangKeluarView(_vm);
    }
}
