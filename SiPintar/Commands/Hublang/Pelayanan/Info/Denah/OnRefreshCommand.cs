using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.Denah
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly DenahViewModel _viewModel;

        public OnRefreshCommand(DenahViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = Task.Run(() =>
            {
                _viewModel.OnLoadCommand.Execute(null);
            });

            await Task.FromResult(true);
        }
    }
}
