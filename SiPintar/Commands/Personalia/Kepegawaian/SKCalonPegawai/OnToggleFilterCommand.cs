using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly SKCalonPegawaiViewModel _viewModel;

        public OnToggleFilterCommand(SKCalonPegawaiViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsFilterVisible = !_viewModel.IsFilterVisible;

            await Task.FromResult(true);
        }
    }
}
