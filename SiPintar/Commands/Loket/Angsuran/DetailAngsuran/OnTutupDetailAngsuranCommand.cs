using System.Threading.Tasks;
using SiPintar.ViewModels.Loket.Angsuran;


namespace SiPintar.Commands.Loket.Angsuran.DetailAngsuran
{
    public class OnTutupDetailAngsuranCommand : AsyncCommandBase
    {
        private readonly DetailAngsuranViewModel _viewModel;
        private readonly bool _isTest;

        public OnTutupDetailAngsuranCommand(DetailAngsuranViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            _viewModel.IsDetailAngsuranVisible = false;
            await Task.FromResult(_isTest);
        }
    }
}
