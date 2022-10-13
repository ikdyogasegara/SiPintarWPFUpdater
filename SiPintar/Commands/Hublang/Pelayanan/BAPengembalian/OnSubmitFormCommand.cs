using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(BaPengembalianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            // kalo ada action load page pertama kali letakkan disini ya... disini sudah otomatis dijalankan lho ketika memasuki halaman bersangkutan..
            object dg = null;
            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog");
            dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, false, "HublangRootDialog", "Mohon tunggu", "sedang memproses tindakan...", "");

            var Data = _viewModel.ParamSubmit;

            Type type = typeof(ParamRekeningAirPengembalianDto);
            PropertyInfo[] properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(Data));
                }
            }
            RestApiResponse Response = null;
            if (_viewModel.IsAdd)
                Response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-pengembalian", body));
            else
                Response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-pengembalian", null, body));

            DialogHelper.CloseDialog(_isTest, false, "HublangRootDialog", dg);
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    if (_viewModel.IsAdd)
                    {
                        ObservableCollection<JObject> DataResponse = Result.Data.ToObject<ObservableCollection<JObject>>();
                        var NomorBA = DataResponse[0].GetValue("NomorBa", StringComparison.OrdinalIgnoreCase) ?? "-";
                        await DialogHelper.ShowDialogGlobalViewAsync(_isTest, false, "HublangRootDialog", Result.Ui_msg, $"No Berita Acara : {NomorBA}", "success", "OK", false, "hublang");
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "success", Result.Ui_msg);

                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", Response.Data.Ui_msg);
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", Response.Error.Message);

            await Task.Run(() => ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null));
        }
    }
}
