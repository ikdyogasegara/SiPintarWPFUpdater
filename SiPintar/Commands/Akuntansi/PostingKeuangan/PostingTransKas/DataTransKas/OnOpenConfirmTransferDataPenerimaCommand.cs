using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan.PostingTransKas;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PostingTransKas.DataTransKas
{
    public class OnOpenConfirmTransferDataPenerimaCommand : AsyncCommandBase
    {
        private readonly DataTransKasViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenConfirmTransferDataPenerimaCommand(DataTransKasViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
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
                $"Proses Ulang Posting ?",
                $"Data penerimaan harian sudah tersedia, Apakah Anda ingin melakukan proses ulang?",
                "confirmation",
                _viewModel.OnSubmitTransferDataPenerimaCommand,
                "Ya, proses ulang",
                "Batal",
                false,
                false,
                "akuntansi");

            await Task.FromResult(_isTest);
        }
    }
}
