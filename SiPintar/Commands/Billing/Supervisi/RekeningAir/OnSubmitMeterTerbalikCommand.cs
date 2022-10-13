using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSubmitMeterTerbalikCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitMeterTerbalikCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var PakaiSkrg = _viewModel.SelectedData.Pakai ?? 0;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "Pakai", Math.Abs(PakaiSkrg) },
                { "FlagKoreksi", true }
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-update", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    _viewModel.SelectedData.PakaiWpf = body["Pakai"];
                    selectedData.IsUpdatingKoreksi = false;
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message, "billing");

            selectedData.IsUpdatingKoreksi = false;
        }
    }
}
