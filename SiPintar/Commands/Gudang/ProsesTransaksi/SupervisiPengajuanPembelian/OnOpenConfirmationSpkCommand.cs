using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenConfirmationSpkCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenConfirmationSpkCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter != null)
            {
                DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
                Vm.Spk = parameter as ParamPengajuanPembelianBarangSuratPerjanjianDto;
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog",
                    "Konfirmasi Proses Laporan Barang Masuk",
                    "Dengan mengkonfirmasi laporan barang masuk, berarti Anda sudah resmi melakukan pemesanan ke suplier yang telah dipilih",
                    "confirmation",
                    Vm.OnSubmitSpkCommand,
                    "Proses Laporan",
                    "Batal",
                    moduleName: "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
