using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.JenisBangunan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly JenisBangunanViewModel _viewModel;

        public OnResetFilterCommand(JenisBangunanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNamaJenisBangunanChecked = false;
            _viewModel.FilterNamaJenisBangunan = null;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
