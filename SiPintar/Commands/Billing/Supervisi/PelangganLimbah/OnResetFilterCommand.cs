using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;

        public OnResetFilterCommand(PelangganLimbahViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsFlagChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNoLimbahChecked = false;
            _viewModel.IsTarifChecked = false;
            _viewModel.IsKolektifChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsKecamatanChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsAreaChecked = false;
            _viewModel.IsCabangChecked = false;

            _viewModel.PelangganFilter = new MasterPelangganLimbahDto { FlagHapus = null };
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
