using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(PaketViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaPaketChecked = false;
            Vm.IsUnitChecked = false;
            Vm.IsKodeBarangChecked = false;
            Vm.IsDeskripsiChecked = false;
            Vm.FilterNamaPaket = null;
            Vm.FilterUnit = null;
            Vm.FilterKodeBarang = null;
            Vm.FilterDeskripsi = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
