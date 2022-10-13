using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnFilterDataSelesaiCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnFilterDataSelesaiCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsEmpty = false;
            _vm.IsLoading = true;
            _vm.DataSelesaiDetailList = null;
            _vm.DataDetailList = null;
            var param = new Dictionary<string, dynamic>() { { "FlagSelesai", true } };
            if (_vm.IsNoTransaksiChecked && !string.IsNullOrWhiteSpace(_vm.FilterNomorTransaksi))
            {
                param.Add("nomorPengajuanPengeluaran", _vm.FilterNomorTransaksi);
            }
            if (_vm.IsNoRegistrasiChecked && !string.IsNullOrWhiteSpace(_vm.FilterNomorRegistrasi))
            {
                param.Add("nomorRegistrasi", _vm.FilterNomorRegistrasi);
            }
            if (_vm.IsTanggalPengajuanChecked)
            {
                param.Add("tglPengajuanMulai", _vm.FilterTanggalPengajuanMulai.ToString("yyyy-MM-dd"));
                param.Add("tglPengajuanSampaiDengan", _vm.FilterTanggalPengajuanSelesai.ToString("yyyy-MM-dd"));
            }

            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    _vm.DataSelesaiList = response.Data.Data.ToObject<ObservableCollection<PengajuanPengeluaranBarangDto>>();
                    _vm.DataSelesaiList ??= new ObservableCollection<PengajuanPengeluaranBarangDto>();
                    _vm.IsEmpty = _vm.DataSelesaiList.Count == 0;
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
