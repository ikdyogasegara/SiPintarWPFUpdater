using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PerRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PerRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            Process();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void Process()
        {
            if (!_isTest)
                _viewModel.GetDataListCommand.Execute(null);
        }
    }
}
