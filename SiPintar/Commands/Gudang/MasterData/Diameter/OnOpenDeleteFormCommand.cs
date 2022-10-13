using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Diameter
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(DiameterViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Diameter Barang",
                $"Ingin menghapus diameter barang {Vm.SelectedData.DiameterBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
