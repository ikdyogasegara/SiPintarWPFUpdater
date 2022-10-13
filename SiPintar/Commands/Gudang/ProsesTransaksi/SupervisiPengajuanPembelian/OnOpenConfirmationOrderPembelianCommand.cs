using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenConfirmationOrderPembelianCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenConfirmationOrderPembelianCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter != null)
            {
                DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
                Vm.OrderPembelian = parameter as ParamPengajuanPembelianBarangOPDto;
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog",
                    "Konfirmasi Proses Laporan Barang Masuk",
                    "Dengan mengkonfirmasi laporan barang masuk, berarti Anda sudah resmi melakukan pemesanan ke suplier yang telah dipilih",
                    "confirmation",
                    Vm.OnSubmitOrderPembelianCommand,
                    "Proses Laporan",
                    "Batal",
                    moduleName: "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
