using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Instalasi;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Akuntansi.Jurnal.Instalasi.DaftarHutang
{
    public class OnProsesJurnalCommand : AsyncCommandBase, System.IDisposable
    {
        private readonly DaftarHutangViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public OnProsesJurnalCommand(DaftarHutangViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += new DoWorkEventHandler(DoWork);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _restApi;

            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { CloseDialog(); });

            if (_backgroundWorker.IsBusy != true)
                _backgroundWorker.RunWorkerAsync();

            await DialogHelper.ShowDialogGlobalProgressBarViewAsync(
                    _isTest,
                    false,
                    "AkuntansiRootDialog",
                    $"Memproses data DHHD April 2021",
                    $"Perkiraan estimasi proses 3-5 menit, mohon tunggu sebentar ...",
                    "Batal",
                    false,
                    false,
                    "akuntansi",
                    _viewModel,
                    _backgroundWorker,
                    _viewModel.OnCetakDataCommand);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(500);
                    worker.ReportProgress(i * 10);
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
