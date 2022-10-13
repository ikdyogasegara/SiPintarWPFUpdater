using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnTambahBarangCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnTambahBarangCommand(DaftarBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as Dictionary<string, dynamic>;
            if (param != null)
            {
                DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
                _ = await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Konfirmasi Tambah Barang",
                    "Tambah Barang pada Nomor Transaksi " + _vm.SelectedDaftarBarangMasuk.NomorTransaksi + " ?", "confirmation",
                    null, moduleName: "gudang", highlightText: _vm.SelectedDaftarBarangMasuk.NomorTransaksi, isBackgroundProcessOnConfirm: true, yesAction: () =>
                    {
                        _ = Task.Run(async () =>
                        {
                            object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memproses tindakan ...");
                            var res = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk-tambah-barang", param);
                            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
                            if (!res.IsError)
                            {
                                if (res.Data.Status)
                                {
                                    DialogHelper.ShowSnackbar(_isTest, "success", res.Data.Ui_msg, "gudang", true);
                                    await ((AsyncCommandBase)_vm.OnGetBarangDetailCommand).ExecuteAsync(null);
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
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Informasi", "Data belum dipilih!", "warning", moduleName: "gudang");
            }
            await Task.FromResult(_isTest);
        }
    }
}
