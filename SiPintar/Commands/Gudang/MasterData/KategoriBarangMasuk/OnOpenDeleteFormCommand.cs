using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly KategoriBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnOpenDeleteFormCommand(KategoriBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Hapus Kategori Barang Masuk",
                $"Ingin menghapus kategori barang masuk {_vm.SelectedData.Kategori} ?",
                "confirmation", _vm.OnSubmitDeleteFormCommand, moduleName: "gudang");
            await Task.FromResult(_isTest);
        }
    }
}
