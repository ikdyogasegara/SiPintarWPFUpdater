using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;

        public OnResetFilterCommand(PelangganL2T2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsFlagChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNomorLlttChecked = false;
            _viewModel.IsTarifLlttChecked = false;
            _viewModel.IsKolektifChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsKecamatanChecked = false;
            _viewModel.IsRayonChecked = false;

            _viewModel.PelangganFilter = new MasterPelangganLlttDto();
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
