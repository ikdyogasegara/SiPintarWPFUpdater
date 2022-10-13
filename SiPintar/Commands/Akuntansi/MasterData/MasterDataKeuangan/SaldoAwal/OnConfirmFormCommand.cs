using System.Globalization;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.SaldoAwal
{
    public class OnConfirmFormCommand : AsyncCommandBase
    {
        private readonly SaldoAwalViewModel _viewModel;
        private readonly bool _isTest;

        public OnConfirmFormCommand(SaldoAwalViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
            _isTest,
            false,
            "KoreksiSaldoAwalDialog",
            $"Konfirmasi Ubah Saldo Awal Kas & Bank",
            $"Proses ini akan mengubah nilai saldo awal kas & bank pada laporan keuangan PDAM Anda." +
            $"\nTanggal : {_viewModel.FilterTglRekonsiliasi!.Value.ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}.",
            "confirmation",
            _viewModel.OnSubmitEditFormCommand,
            "Konfirmasi",
            "Batal",
            false,
            false,
            "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
