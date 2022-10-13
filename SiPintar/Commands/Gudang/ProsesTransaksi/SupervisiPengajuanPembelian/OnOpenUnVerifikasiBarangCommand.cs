using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;


namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenUnVerifikasiBarangCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenUnVerifikasiBarangCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedData != null)
            {
                if (Vm.SelectedData.FlagVerifikasi ?? true)
                {
                    await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog",
                    "Batal Verifikasi Pengajuan",
                    $"ingin batalkan verifikasi pengajuan dengan nomor transaksi {Vm.SelectedData.NomorPengajuanPembelian} ?",
                    "confirmation",
                    Vm.OnUnVerifikasiBarangCommand,
                    "Oke",
                    "Batal",
                    moduleName: "gudang");
                }
                else
                {
                    await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Batal Verifikasi Pengajuan",
                    "Data Pengajuan belum diverifikasi!",
                    "error",
                    "Oke",
                    false,
                    "gudang");
                }
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Batal Verifikasi Pengajuan",
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
