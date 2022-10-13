using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Hublang.Atribut.TarifAirTangki;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            _viewModel.IsEdit = false;
            if (!_isTest) { await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog"); }
            await Task.FromResult(_isTest);
        }
    }
}
