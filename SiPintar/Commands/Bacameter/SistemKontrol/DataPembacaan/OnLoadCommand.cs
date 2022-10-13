using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SiPintar.ViewModels.Bacameter.SistemKontrol;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    [ExcludeFromCodeCoverage]
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnLoadCommand(DataPembacaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest)
                _viewModel.GetDataListCommand.Execute(null);

            await Task.FromResult(_isTest);
        }
    }
}
