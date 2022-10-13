using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.Langganan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly LanggananViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(LanggananViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNomorSambunganChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsAreaChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsKecamatanChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsStatusChecked = false;
            _viewModel.IsFlagChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsTarifLimbahChecked = false;
            _viewModel.IsTarifLlttChecked = false;
            _viewModel.IsDiameterChecked = false;
            _viewModel.IsKolektifChecked = false;

            _viewModel.FilterNomorSambungan = null;
            _viewModel.FilterNama = null;
            _viewModel.FilterAlamat = null;
            _viewModel.FilterRayon = null;
            _viewModel.FilterArea = null;
            _viewModel.FilterWilayah = null;
            _viewModel.FilterKelurahan = null;
            _viewModel.FilterKecamatan = null;
            _viewModel.FilterCabang = null;
            _viewModel.FilterGolongan = null;
            _viewModel.FilterTarifLimbah = null;
            _viewModel.FilterTarifLltt = null;
            _viewModel.FilterDiameter = null;
            _viewModel.FilterStatus = null;
            _viewModel.FilterFlag = null;
            _viewModel.FilterKolektif = null;

            await Task.FromResult(_isTest);
        }
    }
}
