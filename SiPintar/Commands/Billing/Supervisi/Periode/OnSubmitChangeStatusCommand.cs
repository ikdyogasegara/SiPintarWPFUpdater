using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnSubmitChangeStatusCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitChangeStatusCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var selectedData = _viewModel.SelectedData;
            selectedData.IsUpdatingKoreksi = true;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "Status", _viewModel.StatusSection == "open" }
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-periode-rekening-update-status", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    selectedData.StatusWpf = body["Status"];
                    selectedData.IsUpdatingKoreksi = false;

                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "billing");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "billing");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "billing");
            }

            selectedData.IsUpdatingKoreksi = false;
        }
    }
}
