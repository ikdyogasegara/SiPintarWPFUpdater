using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesPiutangBulanan
{
    public class OnOpenConfirmProsesPiutangCommand : AsyncCommandBase
    {
        private readonly ProsesPiutangBulananViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly bool _isTrue;

        public OnOpenConfirmProsesPiutangCommand(ProsesPiutangBulananViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, bool isTrue = true)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _isTrue = isTrue;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            if (_isTrue)
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Konfirmasi Proses Data Saldo Piuatang",
                $"Anda akan melakukan posting data saldo piutang untuk bulan Maret 2021",
                "confirmation",
                _viewModel.OnSubmitConfirmProsesPiutangCommand,
                "Proses",
                "Batal",
                false,
                false,
                "akuntansi");

            }
            else
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Piutang Sudah Pernah Diproses",
                    $"Bulan periode yang Anda pilih telah melalui proses piutang pada: " +
                    $"\nTanggal: 03 Mei 2021" +
                    $"\nPukul: 12:33:21",
                    "warning",
                    "Tutup",
                    false,
                    "akuntansi");
            }

            await Task.FromResult(_isTest);
        }
    }
}
