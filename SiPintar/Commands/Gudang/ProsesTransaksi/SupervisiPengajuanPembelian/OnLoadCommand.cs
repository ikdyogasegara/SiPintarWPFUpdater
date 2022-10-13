using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly IRestApiClientModel RestApi;
        public readonly bool IsTest;
        private readonly string ApiVersion = Application.Current?.Resources["api_version"]?.ToString();

        public OnLoadCommand(SupervisiPengajuanPembelianViewModel _vm, IRestApiClientModel _restApi, bool _isTest = false)
        {
            Vm = _vm;
            RestApi = _restApi;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsLoading = true;
            var Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-barang", null));
            if (!Response.IsError && Response.Data.Status)
            {
                AppDispatcherHelper.Run(IsTest, () =>
                {
                    Vm.DataBarang = Response.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                });
            }
            Response = await Task.Run(() => RestApi.GetAsync($"/api/{ApiVersion}/master-kategori-barang-masuk", null));
            if (!Response.IsError && Response.Data.Status)
            {
                AppDispatcherHelper.Run(IsTest, () =>
                {
                    Vm.DataKategoriBarangMasuk = Response.Data.Data.ToObject<ObservableCollection<MasterKategoriBarangMasukDto>>();
                });
            }
            AppDispatcherHelper.Run(IsTest, () =>
            {
                Vm.OnRefreshCommand.Execute(null);
            });
            _ = RestApi;
            await Task.FromResult(IsTest);
        }
    }
}
