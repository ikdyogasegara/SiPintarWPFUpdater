using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.Pengolahan;
using SiPintar.Views.Gudang.Pengolahan.PenyesuaianStock;

namespace SiPintar.Commands.Gudang.Pengolahan.PenyesuaianStock
{
    public class OnOpenAddCommand : AsyncCommandBase
    {
        private readonly PenyesuaianStockViewModel _vm;
        private readonly bool _isTest;

        public OnOpenAddCommand(PenyesuaianStockViewModel vm, bool isTest = false)
        {
            _vm = vm;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "GudangRootDialog", GetInstance);
            _ = _vm;
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormView(_vm);
    }
}
