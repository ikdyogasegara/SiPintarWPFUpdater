using System.Threading.Tasks;
using SiPintar.ViewModels.Personalia.DataMaster;

namespace SiPintar.Commands.Personalia.DataMaster.Keluarga
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly KeluargaViewModel _viewModel;

        public OnToggleFilterCommand(KeluargaViewModel viewModel)
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
