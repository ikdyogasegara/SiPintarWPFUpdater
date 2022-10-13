using System.Threading.Tasks;
using SiPintar.ViewModels.Gudang.MasterData;

namespace SiPintar.Commands.Gudang.MasterData.Paket
{
    public class OnHapusBarangCommand : AsyncCommandBase
    {
        private readonly PaketViewModel Vm;
        public readonly bool IsTest;

        public OnHapusBarangCommand(PaketViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (Vm.SelectedData1 != null)
            {
                Vm.Data1?.Remove(Vm.SelectedData1);
                Vm.SelectedData1 = null;
            }
            await Task.FromResult(IsTest);
        }
    }
}
