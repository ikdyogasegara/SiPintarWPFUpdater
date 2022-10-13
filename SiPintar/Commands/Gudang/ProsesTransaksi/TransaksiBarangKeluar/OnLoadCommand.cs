using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnLoadCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsEmpty = false;
            _vm.IsLoading = true;
            _vm.DataDetailList = null;
            var param = new Dictionary<string, dynamic>() { { "FlagSelesai", false } };
            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    _vm.DataList = response.Data.Data.ToObject<ObservableCollection<PengajuanPengeluaranBarangDto>>();
                    _vm.DataList ??= new ObservableCollection<PengajuanPengeluaranBarangDto>();
                    _vm.IsEmpty = _vm.DataList.Count == 0;
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
            _vm.IsLoading = false;
            await Task.FromResult(_isTest);
        }
    }
}
