using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.Jurnal;
using SiPintar.Views.Akuntansi.Jurnal.Umum;

namespace SiPintar.Commands.Akuntansi.Jurnal.Umum
{
    public class OnOpenTransaksiEditFormCommand : AsyncCommandBase
    {
        private readonly UmumViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenTransaksiEditFormCommand(UmumViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = false;
            await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        private object GetInstance() => new DialogFormDataTransaksiView(_viewModel);
    }
}
