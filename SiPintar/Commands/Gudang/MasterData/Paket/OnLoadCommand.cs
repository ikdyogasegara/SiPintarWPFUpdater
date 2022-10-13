using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        private readonly IRestApiClientModel RestApi;
        private readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnLoadCommand(PaketViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-satuan-barang", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DataSatuan = Result.Data.ToObject<ObservableCollection<MasterSatuanBarangDto>>();
                }
                if (!Result.Status)
                {
                    DialogHelper.ShowSnackbar(IsTest, "danger", Result.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Vm.OnRefreshCommand.Execute(null);
            _ = RestApi;
            await Task.FromResult(IsTest);
        }
    }
}
