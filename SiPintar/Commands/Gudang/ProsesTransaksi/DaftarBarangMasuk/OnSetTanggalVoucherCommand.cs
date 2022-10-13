using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;


namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnSetTanggalVoucherCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSetTanggalVoucherCommand(DaftarBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var tgl = parameter as DateTime?;
            if (tgl != null && tgl.HasValue)
            {
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                _ = await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Konfirmasi Set Tanggal Voucher",
                    "Set Tanggal Vocuher " + _vm.SelectedDaftarBarangMasuk.NomorTransaksi + " ke tanggal " + tgl.Value.ToString("dd/MM/yyyy"), "confirmation",
                    null, moduleName: "gudang", isBackgroundProcessOnConfirm: true, yesAction: () =>
                    {
                        _ = Task.Run(async () =>
                        {
                            object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memproses tindakan ...");
                            var res = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk-set-tgl-voucher", null, new Dictionary<string, dynamic>
                            {
                                { "IdBarangMasuk", _vm.SelectedDaftarBarangMasuk.IdBarangMasuk },
                                { "TglVoucher", tgl.Value }
                            });
                            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
                            if (!res.IsError)
                            {
                                if (res.Data.Status)
                                {
                                    DialogHelper.ShowSnackbar(_isTest, "success", res.Data.Ui_msg, "gudang", true);
                                }
                                else
                                {
                                    DialogHelper.ShowSnackbar(_isTest, "danger", res.Data.Ui_msg, "gudang", true);
                                }
                            }
                            else
                            {
                                DialogHelper.ShowSnackbar(_isTest, "danger", res.Error.Message, "gudang", true);
                            }
                        });
                    }
                );
            }
            await Task.FromResult(_isTest);
        }
    }
}
