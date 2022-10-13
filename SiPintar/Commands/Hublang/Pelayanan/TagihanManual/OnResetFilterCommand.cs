using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.TagihanManual
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly TagihanManualViewModel _viewModel;

        public OnResetFilterCommand(TagihanManualViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsJenisTipePelangganChecked = false;
            _viewModel.FilterJenisTipePelanggan = null;

            _viewModel.IsJenisNonAirChecked = false;
            _viewModel.FilterJenisNonAir = null;

            _viewModel.IsNomorNonAirChecked = false;
            _viewModel.FilterNomorNonAir = null;

            _viewModel.IsNomorPelangganChecked = false;
            _viewModel.FilterNomorPelanggan = null;

            _viewModel.IsNamaChecked = false;
            _viewModel.FilterNama = null;

            _viewModel.IsAlamatChecked = false;
            _viewModel.FilterAlamat = null;

            _viewModel.IsRayonChecked = false;
            _viewModel.FilterRayon = null;

            _viewModel.IsWilayahChecked = false;
            _viewModel.FilterWilayah = null;

            _viewModel.IsKelurahanChecked = false;
            _viewModel.FilterKelurahan = null;

            _viewModel.IsGolonganChecked = false;
            _viewModel.FilterGolongan = null;

            _viewModel.IsTarifLimbahChecked = false;
            _viewModel.FilterTarifLimbah = null;

            _viewModel.IsTarifLlttChecked = false;
            _viewModel.FilterTarifLltt = null;

            _viewModel.IsStatusChecked = true;
            _viewModel.FilterStatus = "Belum Lunas";

            await Task.FromResult(true);
        }

    }
}
