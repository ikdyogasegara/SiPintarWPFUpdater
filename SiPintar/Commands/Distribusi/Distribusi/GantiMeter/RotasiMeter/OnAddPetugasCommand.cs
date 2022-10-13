using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeter;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeter.RotasiMeter
{
    public class OnAddPetugasCommand : AsyncCommandBase
    {
        private readonly RotasiMeterViewModel _viewModel;

        public OnAddPetugasCommand(RotasiMeterViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.FormPetugasTambahan.Add(new MasterPegawaiDto());

            await Task.FromResult(true);
        }

    }
}
