using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;

        public OnRefreshCommand(PelangganLimbahViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.OnLoadCommand.Execute(null);

            await Task.FromResult(true);
        }
    }
}
