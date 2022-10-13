using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;
//using SiPintar.Views.Loket.Angsuran.Tunggakan;

namespace SiPintar.Commands.Loket.Angsuran.AngsuranTunggakan
{
    public class OnOpenCetakBACommand : AsyncCommandBase
    {
        private readonly AngsuranTunggakanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenCetakBACommand(AngsuranTunggakanViewModel viewModel, bool isTest = false)
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
