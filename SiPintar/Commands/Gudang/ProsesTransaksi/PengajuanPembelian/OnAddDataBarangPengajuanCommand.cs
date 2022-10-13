using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian
{
    public class OnAddDataBarangPengajuanCommand : AsyncCommandBase
    {
        private readonly PengajuanPembelianViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnAddDataBarangPengajuanCommand(PengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "PengajuanPembelianDialog", GetInstance);
            _ = await Task.FromResult(_isTest);
        }

        public object GetInstance() => new DialogPilihBarangMaterialView(restApi: _restApi, setDataTask: CallbackAddBarangAsync);

        public async Task<bool> CallbackAddBarangAsync(MasterBarangDto? param)
        {
            if (param != null)
            {
                while (true)
                {
                    if (!DialogHost.IsDialogOpen("PengajuanPembelianDialog"))
                    {
                        break;
                    }
                    await Task.Delay(100);
                }
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "PengajuanPembelianDialog",
                    () => new DialogInputQtyView(
                            callbackDecimal: (q) =>
                            {
                                var data = _vm.DaftarBarangPengajuan ?? new();
                                data.Add(new MasterBarangPengajuan()
                                {
                                    IdBarang = param.IdBarang,
                                    NamaBarang = param.NamaBarang,
                                    SatuanBarang = param.SatuanBarang,
                                    TotalQty = q,
                                    Stock = param.Stock
                                });
                                _vm.DaftarBarangPengajuan = new ObservableCollection<MasterBarangPengajuan>(data);
                                DialogHelper.CloseDialog(_isTest, true, "PengajuanPembelianDialog");
                                return true;
                            },
                            callbackInt: null,
                            moduleName: "gudang",
                            title: "Kode Barang",
                            titleContent: param.KodeBarang,
                            subtitle: "Nama Barang",
                            subtitleContent: param.NamaBarang
                        ));
            }
            return true;
        }
    }
}
