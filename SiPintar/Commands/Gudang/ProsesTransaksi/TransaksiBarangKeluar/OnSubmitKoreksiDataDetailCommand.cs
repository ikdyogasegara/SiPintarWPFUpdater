using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnSubmitKoreksiDataDetailCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitKoreksiDataDetailCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            var loading = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "GudangRootDialog", "Mohon tunggu", "sedang memproses tindakan...");
            var param = parameter as Dictionary<string, dynamic>;
            var response = await Task.Run(() =>
                _restApi.PatchAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang-detail-koreksi-qty", null, param));
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "gudang");
                    _vm.OnGetDataDetailCommand.Execute(null);
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
            await Task.FromResult(_isTest);
        }
    }
}
