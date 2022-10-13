using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.RotasiMeter
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(RotasiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNoSambunganChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsTglJadwalChecked = false;
            _viewModel.IsTglMeterChecked = false;
            _viewModel.IsUserInputChecked = false;
            _viewModel.IsUserBeritaAcaraChecked = false;

            _viewModel.FilterNoSambungan = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterGolongan = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterRayon = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterKelurahan = null;
            _viewModel.FilterCabang = null;
            _viewModel.FilterTglJadwalAwal = null;
            _viewModel.FilterTglJadwalAkhir = null;
            _viewModel.FilterTglMeterAwal = null;
            _viewModel.FilterTglMeterAkhir = null;
            _viewModel.FilterUserInput = null;
            _viewModel.FilterUserBeritaAcara = null;

            await Task.FromResult(_isTest);
        }
    }
}
