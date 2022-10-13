using System.Linq;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Commands.Gudang.ProsesTransaksi.SupervisiPengajuanPembelian
{
    public class OnTambahBarangPengajuanCommand : AsyncCommandBase
    {
        private readonly SupervisiPengajuanPembelianViewModel Vm;
        public readonly bool IsTest;

        public OnTambahBarangPengajuanCommand(SupervisiPengajuanPembelianViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(IsTest, true, "PengajuanBeliBarangDialog", null);
            var Param = parameter as ParamBarangPengajuanPembelianWpf;
            if (Param != null)
            {
                var DataCurrent = Vm.DataBarangPengajuanPembelianDetail;
                var Data = DataCurrent.FirstOrDefault(x => x.IdBarang == Param.IdBarang);
                if (Data is null)
                {
                    DataCurrent.Add(Param);
                }
                else
                {
                    DataCurrent.FirstOrDefault(x => x.IdBarang == Param.IdBarang).Qty += Param.Qty;
                }
                Vm.DataBarangPengajuanPembelianDetail = null;
                Vm.DataBarangPengajuanPembelianDetail = DataCurrent;
                Vm.IsEmptyBarangPengajuanPembelianDetail = Vm.DataBarangPengajuanPembelianDetail.Count == 0;
            }
            await Task.FromResult(IsTest);
        }
    }
}
