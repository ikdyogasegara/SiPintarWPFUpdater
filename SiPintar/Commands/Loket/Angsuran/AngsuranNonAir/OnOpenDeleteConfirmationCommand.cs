﻿using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranNonAir
{
    public class OnOpenDeleteConfirmationCommand : AsyncCommandBase
    {
        private readonly AngsuranNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmationCommand(AngsuranNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData?.NoAngsuran != null)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "LoketRootDialog",
                $"Konfirmasi Hapus Angsuran",
                $"Hapus angsuran non air lainnya atas nama {_viewModel.SelectedData.Nama} sekarang ?",
                "warning",
                _viewModel.OnSubmitDeleteCommand,
                "Hapus",
                "Batal",
                false,
                false,
                "loket");
            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "LoketRootDialog",
                    "Delete Angsuran",
                    "Data belum dipilih",
                    "error",
                    "OK",
                    false,
                    "loket");
            }
            await Task.FromResult(_isTest);
        }
    }
}
