using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;
using SiPintar.Views.Gudang.MasterData.BagianMemintaBarang;

namespace SiPintar.Commands.Gudang.MasterData.BagianMemintaBarang
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly BagianMemintaBarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenEditFormCommand(BagianMemintaBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsAdd = false;
            await DialogHelper.ShowCustomDialogViewAsync(IsTest, true, "GudangRootDialog", GetInstance);
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogFormView(Vm);
    }
}
