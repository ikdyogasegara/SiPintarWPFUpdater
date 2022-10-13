using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly BagianMemintaBarangViewModel Vm;
        private readonly IRestApiClientModel RestApi;
        private readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnLoadCommand(BagianMemintaBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-divisi", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarDivisi = Result.Data.ToObject<ObservableCollection<MasterDivisiDto>>();
                }
                DialogHelper.ShowSnackbar(IsTest, Result.Status ? "success" : "danger", Result.Ui_msg, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(IsTest, "danger", Response.Error.Message, "gudang");
            }
            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-wilayah", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarWilayah = Result.Data.ToObject<ObservableCollection<MasterWilayahDto>>();
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
    }
}
