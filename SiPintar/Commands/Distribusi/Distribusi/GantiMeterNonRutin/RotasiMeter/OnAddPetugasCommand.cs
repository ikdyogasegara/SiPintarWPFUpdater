using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.ViewModels.Distribusi.Distribusi.GantiMeterNonRutin;

namespace SiPintar.Commands.Distribusi.Distribusi.GantiMeterNonRutin.RotasiMeter
{
    public class OnAddPetugasCommand : AsyncCommandBase
    {
        private readonly RotasiMeterNonRutinViewModel _viewModel;

        public OnAddPetugasCommand(RotasiMeterNonRutinViewModel viewModel)
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
