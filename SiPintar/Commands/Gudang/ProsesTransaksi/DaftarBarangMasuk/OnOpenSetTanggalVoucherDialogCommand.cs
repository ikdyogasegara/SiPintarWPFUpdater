using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.DaftarBarangMasuk;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnOpenSetTanggalVoucherDialogCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly bool _isTest;

        public OnOpenSetTanggalVoucherDialogCommand(DaftarBarangMasukViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangMasuk != null)
            {
                _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetObject);
            }
            await Task.FromResult(_isTest);
        }

        private object GetObject() => new DialogSetTanggalVoucher(_vm);
    }
}
