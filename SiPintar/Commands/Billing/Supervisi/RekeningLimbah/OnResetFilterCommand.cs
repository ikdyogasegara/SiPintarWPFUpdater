using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;

        public OnResetFilterCommand(RekeningLimbahViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNoLimbahChecked = false;
            _viewModel.IsTarifChecked = false;
            _viewModel.IsKolektifChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsCabangChecked = false;
            _viewModel.IsTglBayarChecked = false;
            _viewModel.IsTglPublishChecked = false;
            _viewModel.IsTglUploadChecked = false;
            _viewModel.IsBiayaChecked = false;
            _viewModel.IsKasirChecked = false;
            _viewModel.IsLoketBayarChecked = false;

            _viewModel.RekeningFilter = new RekeningLimbahDto { FlagHapus = null };
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
