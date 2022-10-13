using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnOpenBukaPeriodePostingCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly bool IsTest;
        public OnOpenBukaPeriodePostingCommand(PeriodePostingViewModel _vm, bool _isTest = false)
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
                header: "Buka Periode Posting?",
                message: $"Anda akan membuka kembali periode posting bulan {Vm.SelectedData.NamaPeriode}",
                yesButtonCommand: Vm.OnBukaPeriodePostingCommand,
                yesButtonText: "Proses Buka Periode",
                cancelButtonText: "Batal",
                moduleName: "gudang",
                type: "confirmation"
            );
            await Task.FromResult(IsTest);
        }
    }
}
