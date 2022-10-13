using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DistribusiBarang
{
    public class OnLoadCommand : AsyncCommandBase
    {
        public readonly DistribusiBarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;

        public OnLoadCommand(DistribusiBarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            object Ld = DialogHelper.ShowDialogGlobalLoadingAsync(IsTest, true, "GudangRootDialog", "Mohon tunggu", "sedang mempersiapkan data...");
            await GetDaftarCabangAsync();
            await ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            DialogHelper.CloseDialog(IsTest, true, "GudangRootDialog", Ld);
            _ = Vm;
            _ = RestApi;
            await Task.FromResult(IsTest);
        }

        private async Task GetDaftarCabangAsync()
        {
            var Param = new Dictionary<string, dynamic>();
            Param.Add("CurrentPage", 1);
            Param.Add("PageSize", 10000);
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{RestApi.GetApiVersion()}/master-gudang", Param));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status && Result.Data != null)
                {
                    Vm.DaftarGudang = Result.Data.ToObject<ObservableCollection<MasterGudangDto>>();
                }
            }
        }
    }
}
