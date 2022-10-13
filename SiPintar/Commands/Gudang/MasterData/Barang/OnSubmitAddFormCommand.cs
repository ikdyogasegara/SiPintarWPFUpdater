using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnSubmitAddFormCommand(BarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var _param = parameter as ParamMasterBarangDto;
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohong tunggu", "sedang menambahkan barang...");
            var Param = new Dictionary<string, dynamic>();

            Param.Add("kodeBarang", _param.KodeBarang);
            Param.Add("namaBarang", _param.NamaBarang);
            Param.Add("seriBarang", _param.SeriBarang);
            Param.Add("idJenisBarang", _param.IdJenisBarang);
            Param.Add("idDiameterBarang", _param.IdDiameterBarang);
            Param.Add("idSatuanBarang", _param.IdSatuanBarang);
            Param.Add("idKodeAkun", _param.IdKodeAkun);
            Param.Add("minQty", _param.MinQty);
            Param.Add("maxQty", _param.MaxQty);
            Param.Add("hargaBeli", _param.HargaBeli);
            Param.Add("hargaJual", _param.HargaJual);
            Param.Add("loker", _param.Loker);


            var Response = await Task.Run(() => RestApi.PostAsync($"/api/{ApiVersion}/master-barang", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    JArray Data = Result.Data.ToObject<JArray>();
                    int? IdBarang = Convert.ToInt32(((JObject)Data[0]).GetValue("idBarang"));
                    await UploadFotoAsync(IdBarang);
                    DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", dg);
                }
                DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Vm.OnRefreshCommand.Execute(null);
            await Task.FromResult(IsTest);
        }

        private async Task UploadFotoAsync(int? IdBarang)
        {
            if (IdBarang.HasValue)
            {
                var body = new Dictionary<string, dynamic>
                        {
                            { "IdBarang", IdBarang.Value },
                        };
                if (Vm.FotoBarangFormUri != null && !Vm.FotoBarangFormUri.OriginalString.ToLower().Contains("http"))
                    body.Add("file_Foto", Vm.FotoBarangFormUri.OriginalString);
                var ResponseImage = await Task.Run(() => RestApi.UploadAsync($"/api/{ApiVersion}/master-barang-upload-foto", body));
                if (ResponseImage.IsError)
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", ResponseImage.Error.Message, "gudang");
                }
                if (!ResponseImage.IsError && !ResponseImage.Data.Status)
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", ResponseImage.Data.Ui_msg, "gudang");
                }
            }
        }
    }
}
