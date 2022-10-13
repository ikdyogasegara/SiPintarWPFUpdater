using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk
{
    public class OnSubmitDeleteFormCommand : AsyncCommandBase
    {
        private readonly KategoriBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitDeleteFormCommand(KategoriBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohong tunggu", "sedang menghapus data...");
            var param = new Dictionary<string, dynamic>
            {
                { "idKategoriBarangMasuk", _vm.SelectedData.IdKategoriBarangMasuk! }
            };
            var response = await Task.Run(() => _restApi.DeleteAsync($"/api/{_restApi.GetApiVersion()}/master-kategori-barang-masuk", param));
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", dg);
            if (!response.IsError)
            {
                var Result = response.Data;
                DialogHelper.ShowSnackbar(_isTest, Result.Status ? "success" : "danger", Result.Ui_msg!, "gudang");
            }
            else
            {
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "gudang");
            }
            _vm.OnRefreshCommand.Execute(null);
            await Task.FromResult(_isTest);
        }
    }
}
