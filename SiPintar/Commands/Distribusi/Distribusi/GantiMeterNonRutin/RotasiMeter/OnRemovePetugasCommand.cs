using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public class OnRemovePetugasCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;

        public OnRemovePetugasCommand(RotasiMeterNonRutinViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var data = parameter as MasterPegawaiDto;
            if (_viewModel.FormPetugasTambahan.Any())
                _viewModel.FormPetugasTambahan.Remove(data);

            await Task.FromResult(true);
        }

    }
}
