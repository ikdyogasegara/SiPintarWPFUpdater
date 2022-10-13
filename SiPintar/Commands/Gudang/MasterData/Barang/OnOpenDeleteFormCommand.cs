using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(BarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Barang",
                $"Ingin menghapus barang {Vm.SelectedData.NamaBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
