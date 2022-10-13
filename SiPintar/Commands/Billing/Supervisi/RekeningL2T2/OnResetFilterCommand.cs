using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningL2T2
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly RekeningL2T2ViewModel _viewModel;

        public OnResetFilterCommand(RekeningL2T2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsStatusChecked = false;
            _viewModel.IsNamaChecked = false;
            _viewModel.IsNoSambChecked = false;
            _viewModel.IsNoL2T2Checked = false;
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

            _viewModel.RekeningFilter = new RekeningLlttDto { FlagHapus = null };
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
