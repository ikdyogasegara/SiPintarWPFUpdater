using System.Threading.Tasks;
using SiPintar.ViewModels.Hublang.Atribut;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnNextPageCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnNextPageCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.CurrentPage++;
            _viewModel.OnLoadCommand.Execute(null);
            await Task.FromResult(_isTest);
        }
    }
}
