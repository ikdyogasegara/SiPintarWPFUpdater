using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangKeluar
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly KategoriBarangKeluarViewModel Vm;
        public readonly bool IsTest;

        public OnOpenDeleteFormCommand(KategoriBarangKeluarViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog", "Hapus Kategori Barang Keluar",
                $"Ingin menghapus kategori barang keluar {Vm.SelectedData.Kategori} ?",
                "confirmation", Vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(IsTest);
        }
    }
}
