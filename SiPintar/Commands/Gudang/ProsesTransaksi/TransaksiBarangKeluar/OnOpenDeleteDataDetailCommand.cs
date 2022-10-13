using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenDeleteDataDetailCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenDeleteDataDetailCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDataDetail == null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Hapus Kode Barang",
                    "Data belum dipilih!",
                    "warning", moduleName: "gudang");
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog",
                    "Anda Akan Menghapus Kode Barang",
                    $"Apakah Anda ingin melanjutkan hapus kode barang \"{_vm.SelectedDataDetail.KodeBarang}\"?",
                    "confirmation",
                    yesButtonText: "Ya, Hapus", yesButtonCommand: _vm.OnSubmitDeleteDataDetailCommand,
                    moduleName: "gudang");
            }
            _ = await Task.FromResult(_isTest);
        }
    }
}
