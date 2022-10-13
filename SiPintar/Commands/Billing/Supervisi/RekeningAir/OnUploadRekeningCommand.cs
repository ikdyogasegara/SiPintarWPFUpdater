using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnUploadRekeningCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUploadRekeningCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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

            var selectedData = _viewModel.SelectedData;
            if (selectedData != null)
            {
                if (selectedData.FlagUploadWpf == true)
                {
                    OpenDialogWarning();
                    return;
                }

                selectedData.IsUpdatingPublish = true;

                var param = new Dictionary<string, dynamic>
                {
                    {"IdPeriode", selectedData.IdPeriode},
                    {"IdPelangganAir", selectedData.IdPelangganAir},
                    {"WaktuUpload", DateTime.Now}
                };

                var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/upload-tagihan-air-per-rekening", param));
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        selectedData.FlagUploadWpf = true;
                        selectedData.WaktuUploadWpf = param["WaktuUpload"];

                        DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
                }

                selectedData.IsUpdatingPublish = false;
            }
        }

        [ExcludeFromCodeCoverage]
        private void OpenDialogWarning()
        {
            if (!_isTest)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "BillingRootDialog",
                    "Upload Rekening",
                    $"Rekening Sudah Diupload. Harap Lakukan Proses Unpublish terlebih Dahulu !",
                    "warning",
                    moduleName: "billing");
            }
        }
    }
}
