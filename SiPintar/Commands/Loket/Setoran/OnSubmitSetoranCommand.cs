using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;

namespace SiPintar.Commands.Loket.Setoran
{
    public class OnSubmitSetoranCommand : AsyncCommandBase
    {
        private readonly SetoranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitSetoranCommand(SetoranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog");
            var msg = "sedang mengunggah resi...";
            var dg = DialogHelper.ShowDialogGlobalLoadingAsync(
                _isTest,
                true,
                "LoketRootDialog",
                "Mohon tunggu",
                msg);

            var body = new Dictionary<string, dynamic>
            {
                { "IdPdam", _viewModel.FormSetoran.IdPdam },
                { "IdSetoranLoket", _viewModel.FormSetoran.IdSetoranLoket },
            };

            if (_viewModel.ResiFormPath != null)
                body.Add("file_Foto", _viewModel.ResiFormPath);

            var Response = await Task.Run(() => _restApi.UploadAsync($"/api/{_restApi.GetApiVersion()}/setoran-loket-upload-foto", body));

            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "loket");
                    await SubmitDataAsync();
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");
            }

            DialogHelper.CloseDialog(_isTest, false, "LoketRootDialog", dg);

            if (!_isTest)
                _ = ((AsyncCommandBase)_viewModel.GetSetoranCommand).ExecuteAsync(null);


            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        public async Task SubmitDataAsync()
        {
            if (!_isTest)
            {
                _viewModel.FormSetoran.IdBank = _viewModel.BankForm.IdBank;
                Type type = typeof(ParamSetoranLoketDto);
                PropertyInfo[] properties = type.GetProperties();
                var body = new Dictionary<string, dynamic>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                    {
                        body.Add(property.Name, property.GetValue(_viewModel.FormSetoran));
                    }
                }

                RestApiResponse Response = null;
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/setoran-loket", null, body));

                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "loket");
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "loket");
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "loket");
                }
            }
        }
    }
}
