using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public class OnPreviousPageCommand : AsyncCommandBase
    {
        private readonly PerRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnPreviousPageCommand(PerRayonViewModel viewModel, bool isTest = false)
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
