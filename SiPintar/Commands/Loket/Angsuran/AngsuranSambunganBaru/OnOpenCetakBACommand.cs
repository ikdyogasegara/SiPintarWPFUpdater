using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;
//using SiPintar.Views.Loket.Angsuran.Tunggakan;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranSambunganBaru
{
    public class OnOpenCetakBACommand : AsyncCommandBase
    {
        private readonly AngsuranSambunganBaruViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenCetakBACommand(AngsuranSambunganBaruViewModel viewModel, bool isTest = false)
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
