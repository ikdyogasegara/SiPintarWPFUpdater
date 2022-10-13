using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut.Tarif;

namespace SiPintar.Commands.Billing.Atribut.Tarif.GolonganLimbah
{
    [ExcludeFromCodeCoverage]
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly GolonganLimbahViewModel _viewModel;

        public OnRefreshCommand(GolonganLimbahViewModel viewModel)
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
