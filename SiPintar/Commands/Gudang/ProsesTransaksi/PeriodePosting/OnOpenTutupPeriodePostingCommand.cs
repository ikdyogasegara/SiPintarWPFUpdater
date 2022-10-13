using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnOpenTutupPeriodePostingCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly bool IsTest;
        public OnOpenTutupPeriodePostingCommand(PeriodePostingViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                IsTest,
                dispatcher: true,
                identfier: "GudangRootDialog",
                header: "Tutup Periode Posting Gudang",
                message: $"Dengan menutup periode posting, segala akses untuk proses input transaksi hingga posting laporan rekap stok bulanan akan tertutup.",
                yesButtonCommand: Vm.OnTutupPeriodePostingCommand,
                yesButtonText: "Proses Tutup Periode",
                cancelButtonText: "Batal",
                moduleName: "gudang",
                type: "warning"
            );
            await Task.FromResult(IsTest);
        }
    }
}
