using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Pegawai
{
    public class OnPreviousFormPageCommand : AsyncCommandBase
    {
        private readonly PegawaiViewModel _viewModel;

        public OnPreviousFormPageCommand(PegawaiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.FormCurrentPage > 1)
            {
                _viewModel.FormCurrentPage -= 1;
            }

            await Task.FromResult(true);
        }


    }
}
