using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Gudang.ProsesTransaksi.PengajuanPengeluaranBarang;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar
{
    public class OnOpenPengajuanFormCommand : AsyncCommandBase
    {
        private readonly TransaksiBarangKeluarViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnOpenPengajuanFormCommand(TransaksiBarangKeluarViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            object loading = DialogHelper.ShowDialogGlobalLoadingAsync(
                _isTest, true, "GudangRootDialog", "Mohon tunggu",
                "sedang memuat data...");

            await UpdateListData.ProcessAsync(_isTest, new List<string>()
            {
                "MasterKategoriBarangKeluar",
                "MasterGudang",
                "MasterBagianMemintaBarang",
                "MasterBarang",
            });

            _vm.CariBarangForm = new MasterBarangWpf();
            _vm.SelectedKategori = null;
            _vm.SelectedGudang = null;
            _vm.SelectedBagianMemintaBarang = null;
            _vm.KategoriList = MasterListGlobal.MasterKategoriBarangKeluar;
            _vm.GudangList = MasterListGlobal.MasterGudang;
            _vm.BagianMemintaBarangList = MasterListGlobal.MasterBagianMemintaBarang;
            var dataTemp = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(MasterListGlobal.MasterBarang)) as JArray;
            _vm.BarangList = dataTemp?.ToObject<ObservableCollection<MasterBarangWpf>>();


            _vm.Form = new ParamPengajuanPengeluaranBarangWpf();
            _ = _restApi;
            await ((AsyncCommandBase)_vm.OnGenerateNoTransaksiCommand).ExecuteAsync(null);
            await ((AsyncCommandBase)_vm.OnGenerateNoRegistrasiCommand).ExecuteAsync(null);
            DialogHelper.CloseDialog(_isTest, true, "GudangRootDialog", loading);
            await DialogHelper.ShowCustomDialogViewAsync(
            _isTest,
            true,
            "GudangRootDialog",
            GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstance() => new PengajuanPengeluaranView(_vm);
    }
}
