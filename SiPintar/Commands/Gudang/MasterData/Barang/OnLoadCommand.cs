using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnLoadCommand(BarangViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            await GetFilterAsync();
            Vm.OnRefreshCommand.Execute(null);
            _ = RestApi;
            await Task.FromResult(IsTest);
        }

        public async Task GetFilterAsync()
        {
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-jenis-barang", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarJenisBarang = Result.Data.ToObject<ObservableCollection<MasterJenisBarangDto>>();
                }
            }

            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-diameter-barang", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarDiameterBarang = Result.Data.ToObject<ObservableCollection<MasterDiameterBarangDto>>();
                }
            }

            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-satuan-barang", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarSatuanBarang = Result.Data.ToObject<ObservableCollection<MasterSatuanBarangDto>>();
                }
            }

            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-kode-akun", null));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Data != null)
                {
                    Vm.DaftarKodeAkun = Result.Data.ToObject<ObservableCollection<MasterKodeAkunDto>>();
                }
            }
        }
    }
}
