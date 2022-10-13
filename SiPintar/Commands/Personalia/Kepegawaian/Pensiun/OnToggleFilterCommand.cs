using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.Pensiun
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly PensiunViewModel _viewModel;

        public OnToggleFilterCommand(PensiunViewModel viewModel)
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
