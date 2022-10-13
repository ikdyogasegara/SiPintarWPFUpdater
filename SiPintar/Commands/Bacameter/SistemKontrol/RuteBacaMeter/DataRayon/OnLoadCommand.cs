using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(DataRayonViewModel viewModel, bool isTest = false)
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
