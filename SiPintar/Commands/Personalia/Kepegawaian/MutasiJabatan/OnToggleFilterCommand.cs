using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.Kepegawaian;

namespace SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly MutasiJabatanViewModel _viewModel;

        public OnToggleFilterCommand(MutasiJabatanViewModel viewModel)
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
