using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnUpdateVerifikasiCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUpdateVerifikasiCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.SelectedDataPeriode.Status == false)
            {
                DialogHelper.ShowPeriodeIsClosed(_isTest);
                return;
            }
            #endregion

            var selectedData = _viewModel.SelectedData;
            if (selectedData != null)
            {
                selectedData.IsUpdatingVerifikasi = true;

                if (selectedData.FlagVerifikasiWpf.HasValue && selectedData.FlagVerifikasiWpf == true)
                {
                    if (selectedData.FlagKoreksiBilling == true)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", "Un-Verifikasi ditolak karena rekening sudah di koreksi di Billing !", "billing");
                    }
                    else if (selectedData.FlagPublishWpf == true)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", "Un-Verifikasi ditolak karena rekening sudah di publish di Billing !", "billing");
                    }
                    else
                    {
                        var param = new Dictionary<string, dynamic>
                        {
                            {"IdPeriode", selectedData.IdPeriode},
                            {"IdPelangganAir", new List<int?>() {selectedData.IdPelangganAir}}
                        };

                        var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-unverifikasi", null, param));
                        if (!response.IsError)
                        {
                            var result = response.Data;
                            if (result.Status)
                            {
                                selectedData.FlagVerifikasiWpf = false;
                            }
                            else
                            {
                                DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                            }
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
                        }
                    }
                }
                else
                {
                    var param = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode", selectedData.IdPeriode},
                        {"IdPelangganAir", new List<int?>() {selectedData.IdPelangganAir}},
                        {"WaktuVerifikasi", DateTime.Now}
                    };

                    var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-verifikasi", null, param));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            selectedData.FlagVerifikasiWpf = true;
                            selectedData.WaktuVerifikasiWpf = param["WaktuVerifikasi"];
                        }
                        else
                        {
                            DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                        }
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
                    }
                }

                selectedData.IsUpdatingVerifikasi = false;
            }
        }
    }
}
