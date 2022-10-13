using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnUpdatePublishListCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnUpdatePublishListCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog");

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "BillingRootDialog",
                "Mohon tunggu",
                "sedang memproses tindakan...");

            var idPelanggan = _viewModel.IdPelangganAirList;

            var body = new Dictionary<string, dynamic>()
            {
                {"IdPeriode",_viewModel.SelectedDataPeriode.IdPeriode },
                {"IdPelangganAir",idPelanggan.ToList() },
                {"PasswordUser",_viewModel.Password },
                {"WaktuPublish",DateTime.Now },
                {"LockVerifikasiBacameter", AppSetting.LockVerifikasiBacameter}
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-publish", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");

            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);

            if (!_isTest)
            {
                await ((AsyncCommandBase)_viewModel.OnFilterCommand).ExecuteAsync(null);
            }
        }
    }
}
