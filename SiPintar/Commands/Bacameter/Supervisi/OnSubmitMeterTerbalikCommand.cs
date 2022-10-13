using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitMeterTerbalikCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitMeterTerbalikCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

            var PakaiSkrg = _viewModel.SelectedData.Pakai != null ? (decimal)_viewModel.SelectedData.Pakai : 0;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "Pakai", Math.Abs(PakaiSkrg) },
                { "FlagKoreksi", true }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-air-update", null, body));
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
