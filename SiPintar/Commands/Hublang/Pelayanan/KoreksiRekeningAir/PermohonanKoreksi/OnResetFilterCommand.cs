using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PermohonanKoreksiViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(PermohonanKoreksiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNomorRegisterChecked = false;
            _viewModel.IsNoSambunganChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsTanggalInputChecked = false;
            _viewModel.IsUserInputChecked = false;
            _viewModel.IsStatusPermohonanChecked = false;

            _viewModel.FilterNomorRegister = null;
            _viewModel.FilterNoSambungan = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterGolongan = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterRayon = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterKelurahan = null;
            _viewModel.FilterCabang = null;
            _viewModel.FilterTanggalInputAwal = null;
            _viewModel.FilterTanggalInputAkhir = null;
            _viewModel.FilterUserInput = null;
            _viewModel.FilterStatusPermohonan = null;

            await Task.FromResult(_isTest);
        }
    }
}
