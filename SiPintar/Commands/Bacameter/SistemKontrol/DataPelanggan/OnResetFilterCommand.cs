using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly DataPelangganViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(DataPelangganViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsTanggalDaftarChecked = false;
            _viewModel.IsGolonganChecked = false;
            _viewModel.IsDiameterChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsBlokChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsAlamatChecked = false;
            _viewModel.IsWilayahChecked = false;

            _viewModel.FormFilter = new MasterPelangganAirDto();

            if (!_isTest)
                _viewModel.OnRefreshCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
