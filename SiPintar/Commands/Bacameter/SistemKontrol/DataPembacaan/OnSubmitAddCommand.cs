using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnSubmitAddCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitAddCommand(DataPembacaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog", true, true, 80);

            var body = new Dictionary<string, dynamic>
            {
                { "KodePeriode", _viewModel.TahunForm.Key + _viewModel.BulanForm.Key },
                { "TglMulaiDenda1", ((DateTime)_viewModel.TglDenda1Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda2", ((DateTime)_viewModel.TglDenda2Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda3", ((DateTime)_viewModel.TglDenda3Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda4", ((DateTime)_viewModel.TglDenda4Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDendaPerBulan", ((DateTime)_viewModel.TglMulaiDendaForm).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "Status", true }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-periode-rekening", null, body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("bacameter"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BacameterRootDialog",
                    App.OpenedWindow["bacameter"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;
        }
    }
}
