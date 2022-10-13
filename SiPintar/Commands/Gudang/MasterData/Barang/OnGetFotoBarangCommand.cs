using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnGetFotoBarangCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnGetFotoBarangCommand(BarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var Param = new Dictionary<string, dynamic>();
            Param.Add("IdBarang", Vm.SelectedData.IdBarang);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang-link-foto", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    JArray Data = Result.Data.ToObject<JArray>();
                    string LinkFoto = ((JObject)Data[0]).GetValue("foto")?.ToString();
                    Vm.FotoBarangUri = !string.IsNullOrWhiteSpace(LinkFoto) ? new Uri(LinkFoto, UriKind.Absolute) : null;
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
