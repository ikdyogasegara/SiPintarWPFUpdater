using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;

        public OnResetFilterCommand(PenghapusanRekeningViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsPeriodeChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsRekeningAirChecked = false;
            _viewModel.IsPakaiChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsTglHapusChecked = false;
            _viewModel.IsLainnyaChecked = false;

            _viewModel.Filter = new ParamRekeningAirHapusSecaraAkuntansiDto();
            await Task.FromResult(true);
        }

    }
}
