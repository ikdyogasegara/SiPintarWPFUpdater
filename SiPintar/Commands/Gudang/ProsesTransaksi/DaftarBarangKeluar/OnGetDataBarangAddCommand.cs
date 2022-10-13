using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnGetDataBarangAddCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetDataBarangAddCommand(DaftarBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var param = new Dictionary<string, dynamic>
            {
                { "KodeBarang", (parameter as string) ?? "" }
            };
            var resDetail = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/master-barang", param ?? new Dictionary<string, dynamic>());
            if (!resDetail.IsError && resDetail.Data.Status && resDetail.Data.Data != null)
            {
                var dataGet = resDetail.Data.Data.ToObject<ObservableCollection<MasterBarangDto>>();
                if (dataGet?.Count > 0)
                {
                    _vm.AddBarang = dataGet[0];
                }
            }

            if (_vm.AddBarang != null)
            {
                var param1 = new Dictionary<string, dynamic>
                {
                    { "IdBarang", _vm.AddBarang.IdBarang ?? 0 },
                    { "IdGudang", _vm.SelectedDaftarBarangKeluar.IdGudang ?? 0 }
                };

                //get stock by id
                var response = await _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/distribusi-barang-stock-barang", param1);
                if (!response.IsError && response.Data.Status && response.Data.Data != null)
                {
                    var dataStock = response.Data.Data.ToObject<ObservableCollection<DistribusiBarangStockDetailDto>>();
                    if (dataStock?.Count > 0)
                    {
                        _vm.AddBarangWithStock = dataStock[0];
                    }
                    else
                    {
                        _vm.AddBarangWithStock = new DistribusiBarangStockDetailDto()
                        {
                            IdBarang = _vm.AddBarang.IdBarang,
                            NamaBarang = _vm.AddBarang.NamaBarang,
                            Qty_Stock = 0
                        };
                    }
                }
            }
            else
            {
                _vm.AddBarangWithStock = null!;
            }

            await Task.FromResult(_isTest);
        }
    }
}
