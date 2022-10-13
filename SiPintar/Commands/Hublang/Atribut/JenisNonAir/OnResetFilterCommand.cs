using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.JenisNonAir
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly JenisNonAirViewModel _viewModel;

        public OnResetFilterCommand(JenisNonAirViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsKodeJenisNonAirChecked = false;
            _viewModel.FilterKodeJenisNonAir = null;
            _viewModel.IsNamaJenisNonAirChecked = false;
            _viewModel.FilterNamaJenisNonAir = null;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
