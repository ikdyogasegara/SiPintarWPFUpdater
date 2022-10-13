using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi;

namespace SiPintar.Commands.Akuntansi.PostingAkuntansi
{
    public class OnOpenConfirmProsesPostingCommand : AsyncCommandBase
    {
        private readonly PostingAkuntansiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenConfirmProsesPostingCommand(PostingAkuntansiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Konfirmasi Proses Data Saldo Piuatang",
                $"Anda akan melakukan posting data saldo piutang untuk bulan Maret 2021",
                "confirmation",
                _viewModel.OnSubmitConfirmProsesPostingCommand,
                "Proses",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
