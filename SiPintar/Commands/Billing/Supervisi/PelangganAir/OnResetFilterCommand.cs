using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganAir
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PelangganAirViewModel _viewModel;

        public OnResetFilterCommand(PelangganAirViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsFlagChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNoRekeningChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsKolektifChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsKecamatanChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsBlokChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsMerekMeterChecked = false;
            _viewModel.IsNoSeriMeterChecked = false;
            _viewModel.IsDiameterChecked = false;
            _viewModel.IsNoSegelChecked = false;
            _viewModel.IsKondisiMeterChecked = false;
            _viewModel.IsSumberAirChecked = false;

            _viewModel.PelangganFilter = new MasterPelangganAirDto();

            await Task.FromResult(true);
        }

    }
}
