using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.MasterData;
using SiPintar.Views.Gudang.MasterData.JenisBarang;

namespace SiPintar.Commands.Gudang.MasterData.JenisBarang
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly JenisBarangViewModel Vm;
        public readonly bool IsTest;

        public OnOpenAddFormCommand(JenisBarangViewModel _vm, bool _isTest = false)
        {
            Vm = _vm;
            IsTest = _isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Vm.IsAdd = true;
            await DialogHelper.ShowCustomDialogViewAsync(IsTest, true, "GudangRootDialog", GetInstance);
            await Task.FromResult(IsTest);
        }

        private object GetInstance() => new DialogFormView(Vm);
    }
}
