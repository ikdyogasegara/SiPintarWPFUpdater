using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnOpenDeletePengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly bool _isTest;

        public OnOpenDeletePengajuanCommand(PengajuanPembelianViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedData != null)
            {
                if (_vm.SelectedData.FlagSelesai ?? true)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus Pengajuan Pembelian", "Tidak bisa menghapus pengajuan yang telah selesai!.",
                        "error", moduleName: "gudang");
                }
                else
                {
                    _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                        _isTest,
                        true,
                        "GudangRootDialog",
                        "Hapus Pengajuan Pembelian",
                        $"Apakah anda yakin ingin menghapus Pengajuan {_vm.SelectedData.NomorPengajuanPembelian} ?",
                        "confirmation",
                        _vm.OnDeletePengajuanCommand,
                        "Lanjutkan",
                        "Batal",
                        false, false, "gudang",
                        highlightText: _vm.SelectedData.NomorPengajuanPembelian
                    );
                }
            }
            await Task.FromResult(_isTest);
        }
    }
}
