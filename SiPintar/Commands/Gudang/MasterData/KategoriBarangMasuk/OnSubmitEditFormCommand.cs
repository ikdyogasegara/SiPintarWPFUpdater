using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.KategoriBarangMasuk
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly KategoriBarangMasukViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitEditFormCommand(KategoriBarangMasukViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            var param = parameter as ParamMasterKategoriBarangMasukDto;
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog");
            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "GudangRootDialog", "Mohong tunggu", "sedang menyimpan perubahan...");
            var paramSend = new Dictionary<string, dynamic>
            {
                { "idKategoriBarangMasuk", param?.IdKategoriBarangMasuk! },
                { "kategori", param?.Kategori! },
                { "ppn", param?.Ppn! },
                { "flag", param?.Flag! }
            };
            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/master-kategori-barang-masuk", null!, paramSend));
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", dg);
            if (!response.IsError)
            {
                var result = response.Data;
                DialogHelper.ShowSnackbar(_isTest, result.Status ? "success" : "danger", result.Ui_msg!, "gudang");
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
