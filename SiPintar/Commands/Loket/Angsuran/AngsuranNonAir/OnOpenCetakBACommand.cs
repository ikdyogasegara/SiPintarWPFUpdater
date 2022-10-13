using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;
//using SiPintar.Views.Loket.Angsuran.Tunggakan;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranNonAir
{
    public class OnOpenCetakBACommand : AsyncCommandBase
    {
        private readonly AngsuranNonAirViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenCetakBACommand(AngsuranNonAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;

            await Task.FromResult(_isTest);
        }
    }
}
