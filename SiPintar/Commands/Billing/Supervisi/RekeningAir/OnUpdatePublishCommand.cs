using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using NPOI.HSSF.Record;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnUpdatePublishCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUpdatePublishCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesPublishUnpublish == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

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
                selectedData.IsUpdatingPublish = true;

                if (selectedData.FlagPublishWpf is true)
                {
                    var param = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode", selectedData.IdPeriode},
                        {"IdPelangganAir", new List<int?>() {selectedData.IdPelangganAir}},
                    };

                    var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-unpublish", null, param));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            selectedData.FlagPublishWpf = false;
                            selectedData.FlagUploadWpf = false;
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
                else
                {
                    var param = new Dictionary<string, dynamic>
                    {
                        {"IdPeriode", selectedData.IdPeriode},
                        {"IdPelangganAir", new List<int?>() {selectedData.IdPelangganAir}},
                        {"WaktuPublish", DateTime.Now},
                        {"LockVerifikasiBacameter", AppSetting.LockVerifikasiBacameter}
                    };

                    var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-publish", null, param));
                    if (!response.IsError)
                    {
                        var result = response.Data;
                        if (result.Status)
                        {
                            selectedData.FlagPublishWpf = true;
                            selectedData.WaktuPublishWpf = param["WaktuPublish"];

                            if (AppSetting.LockVerifikasiBacameter == false)
                            {
                                selectedData.FlagVerifikasiWpf = true;
                                selectedData.WaktuVerifikasiWpf = param["WaktuPublish"];
                            }
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

                selectedData.IsUpdatingPublish = false;
            }
        }
    }
}
