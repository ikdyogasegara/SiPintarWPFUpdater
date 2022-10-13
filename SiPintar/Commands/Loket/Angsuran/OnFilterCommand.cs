using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Loket;
using SiPintar.ViewModels.Loket.Angsuran;

namespace SiPintar.Commands.Loket.Angsuran
{
    public class OnFilterCommand : AsyncCommandBase
    {
        private readonly AngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnFilterCommand(AngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.PageViewModel == null)
                return;

            _viewModel.AngsuranFilter = new RekeningAirAngsuranDto();

            if (_viewModel.NoSambFilter != null)
                _viewModel.AngsuranFilter.NoSamb = _viewModel.NoSambFilter;
            if (_viewModel.NamaFilter != null)
                _viewModel.AngsuranFilter.Nama = _viewModel.NamaFilter;
            if (_viewModel.StatusPublishFilter != null)
                _viewModel.AngsuranFilter.FlagPublish = _viewModel.StatusPublishFilter.Value.Value == "Sudah Publish" ? true : false;
            if (_viewModel.StatusBayarFilter != null)
                _viewModel.AngsuranFilter.FlagLunas = _viewModel.StatusBayarFilter.Value.Value == "Lunas" ? true : false;
            if (_viewModel.PetugasFilter != null)
                _viewModel.AngsuranFilter.IdUser = _viewModel.PetugasFilter.IdUser;
            if (_viewModel.JenisNonAirFilter != null)
                _viewModel.AngsuranFilter.IdJenisNonAir = _viewModel.JenisNonAirFilter?.IdJenisNonAir;


            if (_viewModel.PageViewModel is AngsuranTunggakanViewModel tunggakan)
                tunggakan.OnLoadCommand.Execute(_viewModel.AngsuranFilter);
            else if (_viewModel.PageViewModel is AngsuranSambunganBaruViewModel sambungan)
                sambungan.OnLoadCommand.Execute(_viewModel.AngsuranFilter);
            else if (_viewModel.PageViewModel is AngsuranNonAirViewModel nonair)
                nonair.OnLoadCommand.Execute(_viewModel.AngsuranFilter);

            await Task.FromResult(_isTest);
        }
    }
}
