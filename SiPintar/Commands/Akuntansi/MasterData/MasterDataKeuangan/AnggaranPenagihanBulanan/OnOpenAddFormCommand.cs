using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataKeuangan;
using SiPintar.Views.Akuntansi.MasterData.MasterDataKeuangan.AnggaranPenagihanBulanan;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataKeuangan.AnggaranPenagihanBulanan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly AnggaranPenagihanBulananViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(AnggaranPenagihanBulananViewModel viewModel, bool isTest = false)
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
