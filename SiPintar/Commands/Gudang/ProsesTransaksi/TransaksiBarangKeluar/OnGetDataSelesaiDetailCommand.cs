using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnGetDataSelesaiDetailCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnGetDataSelesaiDetailCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsLoading1 = true;
            _vm.DataSelesaiDetailList = null;
            var param = new Dictionary<string, dynamic>() { { "IdPengajuanPengeluaran", _vm.SelectedDataSelesai.IdPengajuanPengeluaran } };
            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang-detail", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    _vm.DataSelesaiDetailList = response.Data.Data.ToObject<ObservableCollection<PengajuanPengeluaranBarangDetailDto>>();
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
            _vm.IsLoading1 = false;
            await Task.FromResult(_isTest);
        }
    }
}
