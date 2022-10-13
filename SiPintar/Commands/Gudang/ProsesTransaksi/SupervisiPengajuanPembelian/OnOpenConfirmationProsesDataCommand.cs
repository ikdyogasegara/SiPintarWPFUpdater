using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnOpenConfirmationProsesDataCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnOpenConfirmationProsesDataCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.DataLpb == null || Vm.DataLpb?.Count == 0)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(IsTest, true, "GudangRootDialog",
                    "Proses Data ke Persediaan?", "Data LPB masih kosong!",
                    buttonText: "Oke", moduleName: "gudang");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(IsTest, true, "GudangRootDialog",
                    "Proses Data ke Persediaan?",
                    "Mohon pastikan data telah terverifikasi dengan benar",
                    "confirmation",
                    Vm.OnOpenProsesDataDialogCommand,
                    "Proses Data",
                    "Batal",
                    moduleName: "gudang");
            }
            await Task.FromResult(IsTest);
        }
    }
}
