using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Commands.Hublang.Verifikasi
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly VerifikasiViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(VerifikasiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsCabangChecked = false;
            _viewModel.IsNomorRegisterChecked = false;
            _viewModel.IsNoSambunganChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsTanggalInputChecked = false;
            _viewModel.IsUserInputChecked = false;
            _viewModel.IsUserBeritaAcaraChecked = false;
            _viewModel.IsTarifLimbahChecked = false;
            _viewModel.IsTarifLlttChecked = false;
            _viewModel.IsStatusPermohonanChecked = false;

            _viewModel.FilterCabang = null;
            _viewModel.FilterNomorRegister = null;
            _viewModel.FilterNoSambungan = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterTanggalInputAwal = null;
            _viewModel.FilterTanggalInputAkhir = null;
            _viewModel.FilterUserInput = null;
            _viewModel.FilterUserBeritaAcara = null;
            _viewModel.FilterTarifLimbah = null;
            _viewModel.FilterTarifLltt = null;
            _viewModel.FilterStatusPermohonan = null;
            await Task.FromResult(_isTest);
        }
    }
}
