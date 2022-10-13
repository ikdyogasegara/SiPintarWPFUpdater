using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FastReport.Data;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnGenerateNoTransaksiCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnGenerateNoTransaksiCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var param = new Dictionary<string, dynamic>()
            {
                { "TglPengajuan", _vm.Form.TanggalPengajuanWpf.Value.ToString("yyyy-MM-dd HH:mm:ss") }
            };
            var response = await Task.Run(() =>
                _restApi.GetAsync(
                    $"/api/{_restApi.GetApiVersion()}/pengajuan-pengeluaran-barang-generate-nomor-pengeluaran", param));
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    var res = response.Data.Data.ToObject<JArray>();
                    if (res.Count > 0)
                    {
                        var obj = res[0] as JObject;
                        _vm.Form.NomorPengajuanPengeluaranWpf = obj.GetValue("NomorPengajuanPengeluaran", StringComparison.OrdinalIgnoreCase).ToString();
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
            _ = await Task.FromResult(_isTest);
        }
    }
}
