using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(PerPetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var current = parameter as string;

            if (current == "petugas" && _viewModel.CurrentPagePetugas < _viewModel.TotalPagePetugas)
            {
                _viewModel.CurrentPagePetugas += 1;

                _viewModel.GetDataListCommand.Execute(null);
            }
            else if (current == "jadwal" && _viewModel.CurrentPageJadwal < _viewModel.TotalPageJadwal)
            {
                _viewModel.CurrentPageJadwal += 1;

                _viewModel.GetJadwalListCommand.Execute(null);
            }

            await Task.FromResult(_isTest);
        }
    }
}
