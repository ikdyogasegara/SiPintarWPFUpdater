using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditFormCommand(ManajementParafViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string successMessage = null;
            string errorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "MainRootDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdParaf", _viewModel.FormData.IdParaf },
                { "NamaPdam", _viewModel.FormData.NamaPdam },
                { "Key", _viewModel.FormData.Key },

                { "Keterangan1", _viewModel.FormData.Keterangan1 },
                { "Jabatan1", _viewModel.FormData.Jabatan1 },
                { "NoId1", _viewModel.FormData.NoId1 },
                { "Nama1", _viewModel.FormData.Nama1 },

                { "Keterangan2", _viewModel.FormData.Keterangan2 },
                { "Jabatan2", _viewModel.FormData.Jabatan2 },
                { "NoId2", _viewModel.FormData.NoId2 },
                { "Nama2", _viewModel.FormData.Nama2 },

                { "Keterangan3", _viewModel.FormData.Keterangan3 },
                { "Jabatan3", _viewModel.FormData.Jabatan3 },
                { "NoId3", _viewModel.FormData.NoId3 },
                { "Nama3", _viewModel.FormData.Nama3 },

                { "Keterangan4", _viewModel.FormData.Keterangan4 },
                { "Jabatan4", _viewModel.FormData.Jabatan4 },
                { "NoId4", _viewModel.FormData.NoId4 },
                { "Nama4", _viewModel.FormData.Nama4 },

                { "Keterangan5", _viewModel.FormData.Keterangan5 },
                { "Jabatan5", _viewModel.FormData.Jabatan5 },
                { "NoId5", _viewModel.FormData.NoId5 },
                { "Nama5", _viewModel.FormData.Nama5 }
            };

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-paraf", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    successMessage = response.Data.Ui_msg;
                else
                    errorMessage = response.Data.Ui_msg;
            }
            else
                errorMessage = response.Error.Message;

            ShowDialog(errorMessage, successMessage);

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog(string errorMessage, string successMessage)
        {
            if (!_isTest && App.OpenedWindow.ContainsKey("main"))
                DialogHelper.ShowSuccessErrorDialog(errorMessage, successMessage, _isTest, "MainRootDialog",
                    App.OpenedWindow["main"], true, _viewModel.OnRefreshCommand, true);
        }
    }
}
