using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.SaldoAwalPerkiraan
{
    public class OnSubmitHitungUlangCommand : AsyncCommandBase
    {
        private readonly SaldoAwalPerkiraanViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitHitungUlangCommand(SaldoAwalPerkiraanViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var body = new Dictionary<string, dynamic>
            {
                { "Tahun", _viewModel.SelectedTahun.Value },
            };

            await ProgressDialogHelper.ShowProgressDialogWorkerAsync(
                 _restApi,
                 "PATCH",
                 "hitung-ulang-saldo-awal-perkiraan",
                 body,
                 _isTest,
                 false,
                 "AkuntansiRootDialog",
                 $"Memproses Verifikasi Saldo Awal Perkiraan",
                 $"Perkiraan estimasi proses 3-5 menit, mohon tunggu sebentar ...",
                 "Batal",
                 false,
                 false,
                 "akuntansi",
                 _viewModel,
                 _viewModel.OnRefreshCommand
            );

            await Task.FromResult(_isTest);
        }
    }
}
