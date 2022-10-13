using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;


namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenConfirmationProsesCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenConfirmationProsesCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog",
                $"Proses Daftar Pengajuan Pengeluaran Barang(DPPB) ? ",
                $"Anda akan memproses DPPB dengan No. {_vm.SelectedData.NomorPengajuanPengeluaran} Anda dapat melihat DPPB yang telah diproses pada halaman Supervisi Pengeluaran Barang",
                "confirmation",
                yesButtonText: "Ya, Proses DPPB",
                highlightText: _vm.SelectedData.NomorPengajuanPengeluaran,
                moduleName: "gudang",
                yesButtonCommand: _vm.OnSubmitProsesCommand,
                isEnabledCetakLaporan: true);
            _ = await Task.FromResult(_isTest);
        }
    }
}
