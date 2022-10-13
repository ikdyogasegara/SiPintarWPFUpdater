using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Supplier
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly SupplierViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(SupplierViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Suplier",
                $"Ingin menghapus suplier {Vm.SelectedData.NamaContactPerson} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
