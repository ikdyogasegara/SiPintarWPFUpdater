using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.TipeKeluarga
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly TipeKeluargaViewModel _viewModel;

        public OnToggleFilterCommand(TipeKeluargaViewModel viewModel)
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
