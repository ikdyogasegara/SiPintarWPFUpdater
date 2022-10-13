using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    public class OnRemovePetugasCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;

        public OnRemovePetugasCommand(RotasiMeterViewModel viewModel)
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
