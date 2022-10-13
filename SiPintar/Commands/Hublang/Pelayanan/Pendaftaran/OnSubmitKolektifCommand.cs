using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Hublang.Pelayanan.Pendaftaran
{
    public class OnSubmitKolektifCommand : AsyncCommandBase
    {
        private readonly PendaftaranViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitKolektifCommand(PendaftaranViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.JenisPelangganForm == null || _viewModel.KolektifList == null || _viewModel.KolektifList.Count == 0)
                return;

            _ = _viewModel;
            _ = _restApi;

            ShowAlert();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowAlert()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate { AlertDialog(); });
        }

        [ExcludeFromCodeCoverage]
        private void AlertDialog()
        {
            _ = DialogHost.Show(new DialogGlobalView(
                    "Belum ada Aksi",
                    "API untuk fitur Kolektif belum tersedia. Harap tunggu update selanjutnya..",
                    "error",
                    "oke",
                    false,
                    "hublang"
                ), "HublangRootDialog");
        }
    }
}
