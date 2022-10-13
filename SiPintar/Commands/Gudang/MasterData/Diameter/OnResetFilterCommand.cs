using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Diameter
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly DiameterViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(DiameterViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaDiameterBarangChecked = false;
            Vm.FilterNamaDiameterBarang = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
