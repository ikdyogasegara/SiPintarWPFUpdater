using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.Peruntukan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PeruntukanViewModel _viewModel;

        public OnResetFilterCommand(PeruntukanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNamaPeruntukanChecked = false;
            _viewModel.FilterNamaPeruntukan = null;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
