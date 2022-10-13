using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;

namespace SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.RiwayatTransaksi
{
    public class OnSubmitBatalkanCommand : AsyncCommandBase
    {
        private readonly RiwayatTransaksiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBatalkanCommand(RiwayatTransaksiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as Dictionary<string, dynamic>;

            string successMessage = null;
            string errorMessage = null;

            DialogHelper.ShowLoading(_isTest, "LoketRootDialog");

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/payment-batal", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    successMessage = response.Data.Ui_msg;
                }
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
                errorMessage = response.Error.Message;

            if (App.OpenedWindow.ContainsKey("loket"))
                DialogHelper.ShowSuccessErrorDialog(errorMessage, successMessage, _isTest, "LoketRootDialog",
                    App.OpenedWindow["loket"], true, _viewModel.OnRefreshCommand, true);
        }
    }
}
