using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    internal class OnCariBarangCommand : AsyncCommandBase
    {
        public readonly SupervisiPengajuanPembelianViewModel _vm;
        public readonly IRestApiClientModel _restApi;
        public readonly bool _isTest;

        public OnCariBarangCommand(SupervisiPengajuanPembelianViewModel vm, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = vm;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "TambahBarangSatuanDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        public object GetInstance() => new DialogPilihBarangMaterialView(restApi: _restApi, setDataTask: CallbackAddBarangAsync);

        public Task<bool> CallbackAddBarangAsync(MasterBarangDto? param)
        {
            if (param != null)
            {
                var data = new ParamPengajuanPembelianBarangTambahBarangWpf()
                {
                    IdPengajuanPembelian = _vm.BarangTambahSatuan.IdPengajuanPembelian,
                    IdBarang = param.IdBarang,
                    NamaBarang = param.NamaBarang,
                    KodeBarang = param.KodeBarang,
                    Qty = 1
                };
                _vm.BarangTambahSatuan = data;
            }
            return Task.FromResult(true);
        }
    }
}
