using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PeriodePosting
{
    public class OnOpenConfirmationAddCommand : AsyncCommandBase
    {
        public readonly PeriodePostingViewModel Vm;
        public readonly bool IsTest;
        public OnOpenConfirmationAddCommand(PeriodePostingViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                IsTest,
                dispatcher: true,
                identfier: "GudangRootDialog",
                header: "Pemberitahuan",
                message: $"Periode posting akan otomatis tertutup setelah posting rekap stok barang dilakukan.",
                yesButtonCommand: Vm.OnSubmitAddFormCommand,
                yesButtonText: "Oke",
                cancelButtonText: "Batal",
                moduleName: "gudang",
                type: "warning"
            );
            await Task.FromResult(IsTest);
        }
    }
}
