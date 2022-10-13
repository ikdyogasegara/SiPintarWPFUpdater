using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
//using SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi.RekapAnggaranPerputaranUang;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.RekapAnggaranPerputaranUang
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly RekapAnggaranPerputaranUangViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(RekapAnggaranPerputaranUangViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsAdd = false;
            //await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "AkuntansiRootDialog", GetInstance);
            await Task.FromResult(_isTest);
        }

        //private object GetInstance() => new DialogFormView(_viewModel);
    }
}
