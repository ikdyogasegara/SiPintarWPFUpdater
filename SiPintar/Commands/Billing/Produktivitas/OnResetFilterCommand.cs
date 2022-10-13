using System;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing;

namespace SiPintar.Commands.Billing.Produktivitas
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly ProduktivitasViewModel _viewModel;
        private readonly bool _isTest;

        public OnResetFilterCommand(ProduktivitasViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var BulanPeriode = _viewModel.SelectedPeriode != null ? Convert.ToInt32(_viewModel.SelectedPeriode.KodePeriode.ToString().Substring(4)) : DateTime.Now.Month;
            var TahunPeriode = _viewModel.SelectedPeriode != null ? (int)_viewModel.SelectedPeriode.Tahun : DateTime.Now.Year;

            _viewModel.IsTglBacaChecked = true;
            _viewModel.TglBacaAwalFilter = new DateTime(TahunPeriode, BulanPeriode, 1);
            _viewModel.TglBacaAkhirFilter = new DateTime(TahunPeriode, BulanPeriode, 28);

            _viewModel.IsTglKirimHasilBacaChecked = false;
            _viewModel.IsRayonChecked = false;
            _viewModel.IsWilayahChecked = false;
            _viewModel.IsKelurahanChecked = false;
            _viewModel.IsPembacaMeterChecked = false;

            if (!_isTest)
                _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
