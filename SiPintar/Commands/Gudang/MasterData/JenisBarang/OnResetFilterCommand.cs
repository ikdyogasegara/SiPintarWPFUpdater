using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;


namespace SiPintar.Commands.Gudang.MasterData.JenisBarang
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly JenisBarangViewModel Vm;
        public readonly bool IsTest;

        public OnResetFilterCommand(JenisBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsTipeBarangChecked = false;
            Vm.IsNamaJenisBarangChecked = false;
            Vm.FilterTipeBarang = null;
            Vm.FilterNamaJenisBarang = null;
            _ = Vm;
            await Task.FromResult(IsTest);
        }
    }
}
