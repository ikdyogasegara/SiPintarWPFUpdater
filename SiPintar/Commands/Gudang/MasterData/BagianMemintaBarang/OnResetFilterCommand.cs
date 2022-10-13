using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly BagianMemintaBarangViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(BagianMemintaBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsNamaBagianChecked = false;
            Vm.IsDivisiChecked = false;
            Vm.IsWilayahChecked = false;
            Vm.FilterNamaBagian = null;
            Vm.FilterDivisi = null;
            Vm.FilterWilayah = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
