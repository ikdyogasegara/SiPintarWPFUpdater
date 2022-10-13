using System.Threading.Tasks;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly KelompokKodePerkiraanViewModel _viewModel;

        public OnResetFilterCommand(KelompokKodePerkiraanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            _viewModel.IsKodePerkiraanChecked = false;
            _viewModel.IsNamaPerkiraanChecked = false;

            _viewModel.FilterKodePerkiraan = null;
            _viewModel.FilterNamaPerkiraan = null;

            _viewModel.OnRefreshCommand.Execute(null);

            await Task.FromResult(true);

        }
    }
}
