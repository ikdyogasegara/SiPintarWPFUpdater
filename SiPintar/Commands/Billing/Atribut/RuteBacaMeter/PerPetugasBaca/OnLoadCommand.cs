using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(PerPetugasBacaViewModel viewModel, bool isTest = false)
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
