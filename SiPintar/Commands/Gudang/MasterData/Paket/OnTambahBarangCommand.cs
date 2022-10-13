using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnTambahBarangCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly bool IsTest;

        public OnTambahBarangCommand(PaketViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedBarang != null)
            {
                //check current qty
                var CurData = Vm.Data1?.FirstOrDefault(x => x.IdBarang == Vm.SelectedBarang.IdBarang);
                MasterBarangPaketDetailDto newData = null;
                if (CurData == null)
                {
                    newData = new MasterBarangPaketDetailDto()
                    {
                        IdBarang = Vm.SelectedBarang.IdBarang,
                        KodeBarang = Vm.SelectedBarang.KodeBarang,
                        NamaBarang = Vm.SelectedBarang.NamaBarang,
                        Qty = DecimalValidationHelper.FormatStringIdToDecimal(Vm.TotalBarang)
                    };
                }
                else
                {
                    newData = new MasterBarangPaketDetailDto()
                    {
                        IdBarang = Vm.SelectedBarang.IdBarang,
                        KodeBarang = Vm.SelectedBarang.KodeBarang,
                        NamaBarang = Vm.SelectedBarang.NamaBarang,
                        Qty = DecimalValidationHelper.FormatStringIdToDecimal(Vm.TotalBarang) + CurData.Qty
                    };
                    Vm.Data1.Remove(CurData);
                }
                var Temp = Vm.Data1;
                if (Temp == null)
                {
                    Vm.Data1 = new ObservableCollection<MasterBarangPaketDetailDto>()
                    {
                        newData
                    };
                }
                else
                {
                    Temp.Add(newData);
                    Vm.Data1 = Temp;
                }
            }
            await Task.FromResult(IsTest);
        }
    }
}
