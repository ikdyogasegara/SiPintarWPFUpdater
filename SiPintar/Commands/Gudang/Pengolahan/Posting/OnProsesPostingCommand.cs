using System.Threading;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Gudang.Pengolahan;

namespace SiPintar.Commands.Gudang.Pengolahan.Posting
{
    public class OnProsesPostingCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnProsesPostingCommand(PostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.PostingStatus = decimal.Zero;
            _vm.IsPosting = true;

            _vm.CancelToken = new CancellationTokenSource();
            var tes = Task.Run(async () =>
            {
                try
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        _vm.PostingStatus = i;
                        await Task.Delay(200);
                        _vm.CancelToken.Token.ThrowIfCancellationRequested();
                    }
                    _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "GudangRootDialog", "Posting Rekap Stock Barang Berhasil",
                        "Data laporan terkait telah diperbarui", "success", moduleName: "gudang");
                }
                finally
                {
                    _vm.IsPosting = false;
                }
            }, cancellationToken: _vm.CancelToken.Token);

            _ = _vm;
            _ = _restApi;
            await Task.FromResult(_isTest);
        }
    }
}
