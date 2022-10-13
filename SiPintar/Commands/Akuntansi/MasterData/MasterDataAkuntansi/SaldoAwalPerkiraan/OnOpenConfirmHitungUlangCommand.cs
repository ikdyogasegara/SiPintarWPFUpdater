using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnOpenConfirmHitungUlangCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenConfirmHitungUlangCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Konfirmasi Proses verifikasi Saldo Awal Perkiraan",
                $"Anda akan melakukan proses hitung ulang untuk saldo awal perkiraan.",
                "confirmation",
                _viewModel.OnSubmitHitungUlangCommand,
                "Proses",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
