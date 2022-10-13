using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnDeletePengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnDeletePengajuanCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            var load = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon tunggu", "sedang memproses tindakan...");
            try
            {
                var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pembelian-barang-hapus-pengajuan", new Dictionary<string, dynamic>
                {
                    { "IdPengajuanPembelian", _vm.SelectedData?.IdPengajuanPembelian ?? 0 }
                }));
                if (!response.IsError && response.Data.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg ?? "", moduleName: "gudang");
                    _vm.OnRefreshCommand.Execute(null);
                }
                else
                {

                    DialogHelper.ShowSnackbar(_isTest, "danger", response.IsError switch
                    {
                        false => response.Data.Ui_msg ?? "",
                        true => response.Error.Message ?? ""

                    }, moduleName: "gudang");
                }
            }
            finally
            {
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", load);
            }
            _ = _isTest;
        }
    }
}
