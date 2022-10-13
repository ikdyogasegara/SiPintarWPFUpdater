using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Voucher;
using SiPintar.Views.Akuntansi.Voucher.IsiEdit;

namespace SiPintar.Commands.Akuntansi.Voucher.IsiEdit
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly IsiEditViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(IsiEditViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = true;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }
        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
