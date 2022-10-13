using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar
{
    public class OnVerifikasiSemuaBarangCommand : AsyncCommandBase
    {
        private readonly DaftarBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnVerifikasiSemuaBarangCommand(DaftarBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            var selectedKeperluan = parameter as MasterKeperluanDto;
            if (selectedKeperluan != null && selectedKeperluan.IdKeperluan != null && _vm.SelectedDaftarBarangKeluar != null && _vm.SelectedDaftarBarangKeluar.IdBarangKeluar != null)
            {
                _ = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon tunggu", "Sedang memproses tindakan...");
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangKeluar" , _vm.SelectedDaftarBarangKeluar.IdBarangKeluar },
                    { "IdKeperluan" , selectedKeperluan.IdKeperluan },
                };
                var resDetail = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/barang-keluar-verifikasi", string.Empty, param);
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                if (!resDetail.IsError)
                {
                    if (resDetail.Data.Status)
                    {
                        DialogHelper.ShowSnackbar(_isTest, "success", resDetail.Data.Ui_msg, "gudang", true);
                        _vm.OnRefreshCommand.Execute(null);
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(_isTest, "danger", resDetail.Data.Ui_msg, "gudang", true);
                    }
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", resDetail.Error.Message, "gudang", true);
                }
            }
            await Task.FromResult(_isTest);
        }
    }
}
