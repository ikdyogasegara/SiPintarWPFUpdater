using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly InteraksiJenisPersediaanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(InteraksiJenisPersediaanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.Parent.IsLoadingForm = true;

            var type = typeof(ParamMasterInPersediaanDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.InJenisPersediaanForm)!);
                }
            }

            RestApiResponse response;
            if (_viewModel.Parent.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-inpersediaan", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-inpersediaan", null, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    await (_viewModel.OnLoadCommand as AsyncCommandBase)!.ExecuteAsync(null!);

                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "akuntansi");

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            _viewModel.Parent.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
