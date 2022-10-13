using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly DiklatPelatihanViewModel _viewModel;

        public OnToggleFilterCommand(DiklatPelatihanViewModel viewModel)
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
