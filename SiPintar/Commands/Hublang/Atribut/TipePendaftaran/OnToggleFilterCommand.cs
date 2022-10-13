using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TipePendaftaran
{
    public class OnToggleFilterCommand : AsyncCommandBase
    {
        private readonly TipePendaftaranViewModel _viewModel;

        public OnToggleFilterCommand(TipePendaftaranViewModel viewModel)
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
