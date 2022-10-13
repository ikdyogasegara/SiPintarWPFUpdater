using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.TipeBarang
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly TipeBarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(TipeBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Tipe Barang",
                $"Ingin menghapus tipe barang {Vm.SelectedData.NamaTipeBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
