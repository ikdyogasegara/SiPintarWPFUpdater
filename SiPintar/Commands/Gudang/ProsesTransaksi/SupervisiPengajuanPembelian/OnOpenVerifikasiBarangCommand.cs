using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenVerifikasiBarangCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenVerifikasiBarangCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedData != null)
            {
                if (!Vm.SelectedData.FlagVerifikasi ?? false)
                {
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog",
                    "Verifikasi Pengajuan",
                    $"ingin verifikasi pengajuan dengan nomor transaksi {Vm.SelectedData.NomorPengajuanPembelian} ?",
                    "confirmation",
                    Vm.OnVerifikasiBarangCommand,
                    "Oke",
                    "Batal",
                    moduleName: "gudang");
                }
                else
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Verifikasi Pengajuan",
                    "Data sudah diverifikasi!",
                    "error",
                    "Oke",
                    false,
                    "gudang");
                }
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Verifikasi Pengajuan",
                    "Data Pengajuan belum dipilih!",
                    "warning",
                    "Oke",
                    false,
                    "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
