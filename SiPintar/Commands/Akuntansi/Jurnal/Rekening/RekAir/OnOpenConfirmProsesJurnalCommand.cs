using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Jurnal.Rekening;

namespace SiPintar.Commands.Akuntansi.Jurnal.Rekening.RekAir
{
    public class OnOpenConfirmProsesJurnalCommand : AsyncCommandBase
    {
        private readonly RekAirViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly bool _isTrue;

        public OnOpenConfirmProsesJurnalCommand(RekAirViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, bool isTrue = false)
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
                ShowProgress();
            }
            else
            {
                await DialogHelper.ShowDialogGlobalYesCancelViewAsync(
                _isTest,
                false,
                "AkuntansiRootDialog",
                $"Proses Ulang Posting Data JRA?",
                $"Data JRA untuk bulan April 2021 sudah tersedia,\nApakah Anda ingin melakukan proses ulang?",
                "confirmation",
                _viewModel.OnProsesJurnalCommand,
                "Ya, proses Ulang",
                "Tidak",
                false,
                false,
                "akuntansi",
                _viewModel.OnCetakDataCommand);
            }

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowProgress()
        {
            if (!_isTest)
                _ = ((AsyncCommandBase)_viewModel.OnProsesJurnalCommand).ExecuteAsync(null);
        }
    }
}
