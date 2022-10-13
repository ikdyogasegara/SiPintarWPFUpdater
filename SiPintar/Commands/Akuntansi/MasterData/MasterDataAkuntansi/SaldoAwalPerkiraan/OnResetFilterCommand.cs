using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;
namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(SaldoAwalPerkiraanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            _viewModel.IsKodePerkiraanChecked = false;
            _viewModel.IsNamaPerkiraanChecked = false;

            _viewModel.FilterKodePerkiraan = null;
            _viewModel.FilterNamaPerkiraan = null;

            _viewModel.OnRefreshCommand.Execute(null);

            await Task.FromResult(_isTest);

        }
    }
}
