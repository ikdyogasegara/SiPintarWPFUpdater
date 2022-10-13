using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.TipeBarang
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TipeBarangViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(TipeBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaTipeBarangChecked = false;
            Vm.FilterNama = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
