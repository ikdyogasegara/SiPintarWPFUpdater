using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnGenerateNoBppCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGenerateNoBppCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            _vm.IsGeneratingNoBpp = true;
            var param = new Dictionary<string, dynamic>() {
                { "waktuDikeluarkan" , _vm.FormProses.WaktuDikeluarkan.Value.ToString("yyyy-MM-dd") }
            };
            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang-generate-nomor-bpp", param));
            if (!response.IsError)
            {
                if (response.Data.Status && response.Data.Data != null)
                {
                    var temp = response.Data.Data.ToObject<JArray>();
                    if (temp?.Count > 0)
                    {
                        var o = temp[0] as JObject;
                        _vm.FormProses.NomorBppWpf = o.GetValue("NomorBpp", System.StringComparison.OrdinalIgnoreCase).ToString();
                    }
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
            _vm.IsGeneratingNoBpp = false;
            await Task.FromResult(_isTest);
        }
    }
}
