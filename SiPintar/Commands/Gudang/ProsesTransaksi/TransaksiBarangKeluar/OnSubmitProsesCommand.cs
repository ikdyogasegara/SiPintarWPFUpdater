using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;


namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnSubmitProsesCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitProsesCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
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
            var param = new Dictionary<string, dynamic>()
            {
                { "idPengajuanPengeluaran", _vm.FormProses.IdPengajuanPengeluaran },
                { "nomorBpp",  _vm. FormProses.NomorBpp },
                { "idGudang",  _vm.FormProses.IdGudang },
                { "idPeriode", _vm.FormProses.IdPeriode },
                { "waktuDikeluarkan", _vm.FormProses.WaktuDikeluarkan }
            };

            var response = await Task.Run(() =>
                _restApi.PostAsync(
                    $"/api/{_restApi.GetApiVersion()}/supervisi-pengajuan-pengeluaran-barang-detail-proses-barang-keluar", param));
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
            if (!response.IsError)
            {
                if (response.Data.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", response.Data.Ui_msg, "gudang");
                    _vm.OnLoadCommand.Execute(null);
                    var isCetakLaporan = parameter as bool?;
                    if (isCetakLaporan ?? false)
                    {
                        var bodyCetak = new Dictionary<string, dynamic>()
                        {
                            { "IdPengajuanPengeluaran", _vm.FormProses.IdPengajuanPengeluaran }
                        };
                        ((OnCetakCommand)_vm.OnCetakPengajuanCommand).IsCetak = true;
                        ((OnCetakCommand)_vm.OnCetakPengajuanCommand).IsPreview = true;
                        ((OnCetakCommand)_vm.OnCetakPengajuanCommand).TemplateName = "DaftarPengajuanPengeluaranBarang";
                        _ = ((AsyncCommandBase)_vm.OnCetakPengajuanCommand).ExecuteAsync(bodyCetak);
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
            await Task.FromResult(_isTest);
        }
    }
}
