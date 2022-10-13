using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Barang
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly BarangViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(BarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsKodeBarangChecked = false;
            Vm.IsSeriBarangChecked = false;
            Vm.IsNamaBarangChecked = false;
            Vm.IsJenisBarangChecked = false;
            Vm.IsDiameterBarangChecked = false;
            Vm.IsSatuanBarangChecked = false;
            Vm.FilterKodeBarang = null;
            Vm.FilterSeriBarang = null;
            Vm.FilterNamaBarang = null;
            Vm.FilterJenisBarang = null;
            Vm.FilterDiameterBarang = null;
            Vm.FilterSatuanBarang = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
