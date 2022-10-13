using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Penyusutan;

namespace SiPintar.Commands.Akuntansi.Penyusutan.DataAktiva
{
    public class OnOpenPenyusutanConfirmCommand : AsyncCommandBase
    {
        private readonly DataAktivaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenPenyusutanConfirmCommand(DataAktivaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            _ = _restApi;

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Konfirmasi Proses Penyusutan",
                $"Anda akan melakukan proses penyusutan untuk bulan September 2020. Data yang mengalami proses sebanyak : 693 item.",
                "confirmation",
                null,
                "Proses",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
