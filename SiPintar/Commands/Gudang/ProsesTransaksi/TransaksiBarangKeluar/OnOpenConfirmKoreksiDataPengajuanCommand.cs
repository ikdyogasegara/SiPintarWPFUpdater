using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenConfirmKoreksiDataPengajuanCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly bool _isTest;

        public OnOpenConfirmKoreksiDataPengajuanCommand(TransaksiBarangKeluarViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog",
                $"Koreksi Data Pengajuan",
                $"Simpan Perubahan Data Pengajuan Pengeluaran Barang ?",
                "confirmation",
                yesButtonText: "Ya",
                cancelButtonText: "Batal",
                moduleName: "gudang",
                yesButtonCommand: _vm.OnSubmitKoreksiDataPengajuanCommand);
            _ = await Task.FromResult(_isTest);
        }
    }
}
