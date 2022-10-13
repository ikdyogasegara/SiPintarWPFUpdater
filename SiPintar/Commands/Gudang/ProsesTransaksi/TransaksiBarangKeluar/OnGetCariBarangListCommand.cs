using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnGetCariBarangListCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGetCariBarangListCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedGudang is null)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "PengajuanPengeluaranDialog",
                    "Pengajuan Pengeluaran Barang", "Gudang belum dipilih!", "warning", moduleName: "gudang");
                return;
            }

            _vm.IsLoadingCari = true;
            var param = new Dictionary<string, dynamic>()
            {
                { "IdGudang", _vm.SelectedGudang.IdGudang },
                { "IncludeStock", true },
                { "PageSize", 20 },
            };

            if (!string.IsNullOrWhiteSpace(_vm.BarangPilihCari))
            {
                param.Add("NamaBarang", _vm.BarangPilihCari);
            }

            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/distribusi-barang-daftar-barang", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    _vm.BarangPilihList = response.Data.Data.ToObject<ObservableCollection<ListBarangDistribusiBarangDto>>();
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
            _vm.IsLoadingCari = false;
            await Task.FromResult(_isTest);
        }
    }
}
