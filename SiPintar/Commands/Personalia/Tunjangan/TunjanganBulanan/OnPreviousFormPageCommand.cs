using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Tunjangan;

namespace SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan
{
    public class OnPreviousFormPageCommand : AsyncCommandBase
    {
        private readonly TunjanganBulananViewModel _viewModel;

        public OnPreviousFormPageCommand(TunjanganBulananViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FormCurrentPage == 2)
            {
                _viewModel.FormCurrentPage = 1;
            }

            await Task.FromResult(true);
        }


    }
}
