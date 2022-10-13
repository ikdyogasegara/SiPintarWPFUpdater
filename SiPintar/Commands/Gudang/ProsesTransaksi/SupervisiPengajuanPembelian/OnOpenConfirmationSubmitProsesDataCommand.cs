using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenConfirmationSubmitProsesDataCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenConfirmationSubmitProsesDataCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter != null)
            {
                Vm.ProsesData = parameter as ParamPengajuanPembelianBarangProsesKePersediaanDto;
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "ProsesDataDialog",
                    "Konfirmasi Penyimpanan Transaksi Barang Masuk?",
                    $"Anda akan memproses transaksi barang masuk no. DPB: {Vm.ProsesData.NomorLpb} pada periode {Vm.DataPeriode.FirstOrDefault(x => x.IdPeriode == Vm.ProsesData.IdPeriode).NamaPeriode}",
                    "confirmation",
                    Vm.OnSubmitProsesDataCommand,
                    "Ya, Proses Data",
                    "Batal",
                    moduleName: "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
