using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.WilayahAdministrasi
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly WilayahAdministrasiViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(WilayahAdministrasiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.CurrentSection = "rayon";

            await Task.FromResult(_isTest);
        }
    }
}
