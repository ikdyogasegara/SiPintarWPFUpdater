using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk
{
    public class OnDeleteBarangMasukCommand : AsyncCommandBase
    {
        private readonly DaftarBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnDeleteBarangMasukCommand(DaftarBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (_vm.SelectedDaftarBarangMasukDetail != null)
            {
                _ = await DialogHelper.ShowDialogGlobalYesCancelViewAsync(_isTest, true, "GudangRootDialog", "Konfirmasi Hapus Barang",
                    "Hapus Barang " + _vm.SelectedDaftarBarangMasukDetail.KodeBarang + " - " + _vm.SelectedDaftarBarangMasukDetail.NamaBarang + " pada Nomor Transaksi " + _vm.SelectedDaftarBarangMasukDetail.NomorTransaksi + " ?", "confirmation",
                    null, moduleName: "gudang", highlightText: _vm.SelectedDaftarBarangMasukDetail.KodeBarang + " - " + _vm.SelectedDaftarBarangMasukDetail.NamaBarang, isBackgroundProcessOnConfirm: true, yesAction: () =>
                    {
                        _ = Task.Run(async () =>
                        {
                            object loading = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohon Tunggu", "Sedang memproses tindakan ...");
                            var res = await _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/barang-masuk-hapus-barang", new Dictionary<string, dynamic>
                            {
                                { "IdBarangMasuk", _vm.SelectedDaftarBarangMasukDetail.IdBarangMasuk },
                                { "IdBarang", _vm.SelectedDaftarBarangMasukDetail.IdBarang },
                            });
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
