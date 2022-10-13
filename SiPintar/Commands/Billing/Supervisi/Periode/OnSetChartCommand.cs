using System;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnSetChartCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnSetChartCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedData == null)
                return;

            _viewModel.IsLoadingChart = true;

            var RekeningAir = _viewModel.SelectedData.JumlahRekeningAir != null ? _viewModel.SelectedData.JumlahRekeningAir : 0;
            var RekeningLimbah = _viewModel.SelectedData.JumlahRekeningLimbah != null ? _viewModel.SelectedData.JumlahRekeningLimbah : 0;
            var RekeningLLTT = _viewModel.SelectedData.JumlahRekeningLLTT != null ? _viewModel.SelectedData.JumlahRekeningLLTT : 0;

            var PiutangAir = _viewModel.SelectedData.JumlahRekeningAirPiutang != null ? _viewModel.SelectedData.JumlahRekeningAirPiutang : 0;
            var PiutangLimbah = _viewModel.SelectedData.JumlahRekeningLimbahPiutang != null ? _viewModel.SelectedData.JumlahRekeningLimbahPiutang : 0;
            var PiutangLLTT = _viewModel.SelectedData.JumlahRekeningLLTTPiutang != null ? _viewModel.SelectedData.JumlahRekeningLLTTPiutang : 0;

            var TerbayarAir = _viewModel.SelectedData.JumlahRekeningAirLunas != null ? _viewModel.SelectedData.JumlahRekeningAirLunas : 0;
            var TerbayarLimbah = _viewModel.SelectedData.JumlahRekeningLimbahLunas != null ? _viewModel.SelectedData.JumlahRekeningLimbahLunas : 0;
            var TerbayarLLTT = _viewModel.SelectedData.JumlahRekeningLLTTLunas != null ? _viewModel.SelectedData.JumlahRekeningLLTTLunas : 0;

            var JumlahTagihan = Convert.ToInt32(RekeningAir + RekeningLimbah + RekeningLLTT);
            var JumlahPiutang = Convert.ToInt32(PiutangAir + PiutangLimbah + PiutangLLTT);
            var JumlahTerbayar = Convert.ToInt32(TerbayarAir + TerbayarLimbah + TerbayarLLTT);

            _viewModel.JumlahTagihanChart = new LiveCharts.ChartValues<int> { JumlahTagihan };
            _viewModel.JumlahPiutangChart = new LiveCharts.ChartValues<int> { JumlahPiutang };
            _viewModel.JumlahTerbayarChart = new LiveCharts.ChartValues<int> { JumlahTerbayar };

            _viewModel.IsEmptyChart = false;

            _viewModel.Labels = Array.Empty<string>();

            _viewModel.IsLoadingChart = false;

            await Task.FromResult(_isTest);
        }
    }
}
