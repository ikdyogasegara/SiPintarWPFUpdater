using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan1
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraan1ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(KelompokKodePerkiraan1ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...",
                null, true, true, 20);


            var data = parameter as ParamMasterPerkiraan1Dto;
            Type type = typeof(ParamMasterPerkiraan1Dto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(data));
                }
            }

            RestApiResponse Response = null;
            if (_viewModel.Parent.IsAdd)
            {
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-perkiraan1", body));
            }
            else
            {
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-perkiraan1", null, body));
            }
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    if (!_viewModel.Parent.IsAdd)
                        UpdateRecord(_viewModel.SelectedData, _viewModel.FormPerkiraan);
                    else
                        _viewModel.OnLoadCommand.Execute(null);

                    DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Result.Ui_msg, "akuntansi");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error?.Message, "akuntansi");
            }
            //End - Send Data


            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog", dg);

        }

        [ExcludeFromCodeCoverage]
        private void UpdateRecord(MasterPerkiraan1Wpf selectedData, ParamMasterPerkiraan1Dto resultData)
        {
            if (!_isTest)
            {
                selectedData.IdPerkiraan1 = resultData.IdPerkiraan1;
                selectedData.KodePerkiraan1Wpf = resultData.KodePerkiraan1;
                selectedData.NamaPerkiraan1Wpf = resultData.NamaPerkiraan1;
            }
        }
    }
}
