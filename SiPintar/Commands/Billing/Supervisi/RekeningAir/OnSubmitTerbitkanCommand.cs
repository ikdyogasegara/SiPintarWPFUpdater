using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.ViewModels.Loket.Tagihan;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSubmitTerbitkanCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitTerbitkanCommand(RekeningAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "BillingRootDialog",
                "Mohon tunggu",
                "sedang memproses tindakan...");

            var listIdPelanggan = new List<int>();
            if (_viewModel.PenerbitanPelangganList != null)
            {
                foreach (var item in _viewModel.PenerbitanPelangganList)
                {
                    if (item.IsSelected == true && item.IdPelangganAir != null)
                    {
                        listIdPelanggan.Add(Convert.ToInt32(item.IdPelangganAir));
                    }
                }
            }

            var param = new Dictionary<string, dynamic>
                {
                    {"KodePeriode" , _viewModel.SelectedDataPeriode.KodePeriode},
                    {"IdPelangganAir" , listIdPelanggan},
                };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-terbitkan-pelanggan", null, param));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            DialogHelper.CloseDialog(_isTest, true, "BillingRootDialog", dg);
        }

    }
}
