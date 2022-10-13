using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnGetBarangTambahDetailCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetBarangTambahDetailCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsLoadingForm = true;
            var kodeBarang = parameter as string;
            kodeBarang = kodeBarang?.Trim();

            if (!string.IsNullOrWhiteSpace(kodeBarang))
            {
                var barangList = await GetBarangAsync(kodeBarang);
                if (barangList?.FirstOrDefault(x => x.KodeBarang == kodeBarang) is MasterBarangDto br)
                {
                    _vm.CariBarangForm = new MasterBarangWpf()
                    {
                        IdBarang = br.IdBarang,
                        KodeBarang = br.KodeBarang,
                        NamaBarang = br.NamaBarang,
                        SatuanBarang = br.SatuanBarang,
                        Qty = 1
                    };
                }
                else
                {
                    _vm.IsErrorCariKodeBarang = true;
                }
            }
            _vm.IsLoadingForm = false;
            _vm.InvokeEventOnBarangTambahEnter();
            await Task.FromResult(_isTest);
        }

        private async Task<ObservableCollection<MasterBarangWpf>> GetBarangAsync(string kodebarang)
        {
            var param = new Dictionary<string, dynamic>()
            {
                {"IdPdam", Application.Current.Resources["IdPdam"]?.ToString()},
                {"KodeBarang", kodebarang }
            };

            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/master-barang", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    return response.Data.Data.ToObject<ObservableCollection<MasterBarangWpf>>();

                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "gudang");
                }
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "gudang");
            }
            return null;
        }
    }
}
