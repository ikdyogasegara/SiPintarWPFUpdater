using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.Status
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly StatusViewModel _viewModel;

        public OnRefreshCommand(StatusViewModel viewModel)
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
