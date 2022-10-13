using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.Pekerjaan
{
    public class OnResetFilterCommand : AsyncCommandBase
    {
        private readonly PekerjaanViewModel _viewModel;

        public OnResetFilterCommand(PekerjaanViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsNamaPekerjaanChecked = false;
            _viewModel.FilterNamaPekerjaan = null;

            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }

    }
}
