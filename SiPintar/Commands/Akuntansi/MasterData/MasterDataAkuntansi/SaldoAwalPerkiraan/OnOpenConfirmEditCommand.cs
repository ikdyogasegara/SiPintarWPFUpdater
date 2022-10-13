using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnOpenConfirmEditCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenConfirmEditCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }


        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog");

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                true,
                "AkuntansiRootDialog",
                $"Konfirmasi Tambah Saldo Awal Perkiraan",
                $"Anda akan menambahkan saldo awal perkiraan untuk kode perkiraan {_viewModel.SelectedData.KodePerkiraanWpf} - {_viewModel.SelectedData.NamaPerkiraanWpf}.",
                "confirmation",
                _viewModel.OnSubmitEditCommand,
                "Proses",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
