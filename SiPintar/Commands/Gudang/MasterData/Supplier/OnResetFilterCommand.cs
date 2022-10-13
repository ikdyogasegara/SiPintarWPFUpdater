using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Supplier
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly SupplierViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(SupplierViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaCpChecked = false;
            Vm.IsNamaPerusahaanChecked = false;
            Vm.IsJabatanChecked = false;
            Vm.IsAlamatChecked = false;
            Vm.FilterNamaCp = null;
            Vm.FilterNamaPerusahaan = null;
            Vm.FilterJabatan = null;
            Vm.FilterAlamat = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
