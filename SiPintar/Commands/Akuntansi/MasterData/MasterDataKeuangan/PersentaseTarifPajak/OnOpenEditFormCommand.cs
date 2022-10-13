using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.PersentaseTarifPajak
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PersentaseTarifPajakViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PersentaseTarifPajakViewModel viewModel, bool isTest = false)
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

        private object GetInstance() => new DialogFormView(_viewModel);
    }
}
