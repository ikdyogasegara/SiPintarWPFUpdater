using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnOpenDeleteFormCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;
        private readonly bool _isTrue;

        public OnOpenDeleteFormCommand(UmumViewModel viewModel, bool isTest = false, bool isTrue = true)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _isTrue = isTrue;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            if (_isTrue)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Hapus Data Jurnal Umum?",
                    $"Data 001/JU/XII/2021 yang dihapus tidak dapat dibatalkan.",
                    "warning",
                    null,
                    "Ya",
                    "Batal",
                    false,
                    false,
                    "akuntansi");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    "Hapus Data Jurnal Umum",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "akuntansi");
            }
            await Task.FromResult(_isTest);
        }
    }
}
