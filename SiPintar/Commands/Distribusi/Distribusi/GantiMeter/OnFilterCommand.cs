using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.ViewModels.Distribusi.Distribusi;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;


namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly GantiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnFilterCommand(GantiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PageViewModel == null)
                return;

            if (_viewModel.TahunFilter is null)
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "DistribusiRootDialog",
                    "Ganti Meter Non Rutin",
                    "Tahun belum dipilih!",
                    "warning",
                    "OK",
                    moduleName: "distrubusi");
                return;
            }

            _viewModel.Filter = new ParamJadwalGantiMeterFilterDto();

            if (_viewModel.TahunFilter != null)
                _viewModel.Filter.Tahun = _viewModel.TahunFilter;
            if (_viewModel.KelainanFilter != null)
                _viewModel.Filter.Kelainan = _viewModel.KelainanFilter.Kelainan;
            if (_viewModel.NomorSpkFilter != null)
                _viewModel.Filter.NomorSpk = _viewModel.NomorSpkFilter;
            if (_viewModel.NomorBaFilter != null)
                _viewModel.Filter.NomorBa = _viewModel.NomorBaFilter;
            if (_viewModel.NamaFilter != null)
                _viewModel.Filter.Nama = _viewModel.NamaFilter;
            if (_viewModel.NoSambFilter != null)
                _viewModel.Filter.NoSamb = _viewModel.NoSambFilter;
            if (_viewModel.StatusDataFilter != null)
                _viewModel.Filter.StatusData = _viewModel.StatusDataFilter.Value.Value;
            if (_viewModel.WilayahFilter != null)
                _viewModel.Filter.IdWilayah = _viewModel.WilayahFilter.IdWilayah;
            if (_viewModel.GolonganFilter != null)
                _viewModel.Filter.IdGolongan = _viewModel.GolonganFilter.IdGolongan;
            if (_viewModel.RayonFilter != null)
                _viewModel.Filter.IdRayon = _viewModel.RayonFilter.IdRayon;
            if (_viewModel.AreaFilter != null)
                _viewModel.Filter.IdArea = _viewModel.AreaFilter.IdArea;

            if (_viewModel.PageViewModel is KelainanBacameterViewModel kelainan)
                kelainan.OnLoadCommand.Execute(_viewModel.Filter);
            else if (_viewModel.PageViewModel is RotasiMeterViewModel rotasiMeter)
                rotasiMeter.OnLoadCommand.Execute(_viewModel.Filter);

            await Task.FromResult(_isTest);
        }
    }
}
