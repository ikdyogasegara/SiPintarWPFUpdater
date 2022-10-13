using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly BagianMemintaBarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(BagianMemintaBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Bagian Meminta Barang",
                $"Ingin menghapus bagian meminta barang {Vm.SelectedData.NamaBagianMemintaBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
