using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(ManajementParafViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
            {
                object dg = null;
                DialogHelper.CloseDialog(_isTest, false, "MainRootDialog");
                dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "MainRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

                var body = new Dictionary<string, dynamic> { { "IdParaf", _viewModel.SelectedData.IdParaf } };
                var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-paraf", body));

                if (!response.IsError)
                {
                    DialogHelper.CloseDialog(_isTest, false, "MainRootDialog", dg);
                    var result = response.Data;
                    DialogHelper.ShowSnackbar(_isTest, result.Status ? "success" : "danger", result.Ui_msg);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message);

                await RefreshAsync();

            }
            else
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false,
                    "MainRootDialog",
                    "Hapus Data Paraf",
                    "Silahkan pilih data",
                    "warning",
                    "Ok",
                    false,
                    "main");

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task RefreshAsync()
        {
            if (!_isTest)
                await Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
        }
    }
}
