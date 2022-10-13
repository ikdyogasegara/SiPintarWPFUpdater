using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.JenisBarang
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly JenisBarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(JenisBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Jenis Barang",
                $"Ingin menghapus jenis barang {Vm.SelectedData.NamaJenisBarang} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
