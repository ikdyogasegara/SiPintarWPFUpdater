using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Hublang.Pelayanan.BAPengembalian;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    public class OnOpenDetailFormCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDetailFormCommand(BaPengembalianViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData != null)
                await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogView(_viewModel);
    }
}
