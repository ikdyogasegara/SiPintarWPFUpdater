using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(DataRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var current = parameter as string;

            if (current == "rayon" && _viewModel.CurrentPageRayon > 1)
            {
                _viewModel.CurrentPageRayon -= 1;

                _viewModel.GetDataListCommand.Execute(null);
            }
            else if (current == "jadwal" && _viewModel.CurrentPageJadwal > 1)
            {
                _viewModel.CurrentPageJadwal -= 1;

                _viewModel.GetJadwalListCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
