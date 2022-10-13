using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Atribut.TarifAirTangki;

namespace SiPintar.Commands.Hublang.Atribut.TarifAirTangki
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TarifAirTangkiViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TarifAirTangkiViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _ = _viewModel;
            _viewModel.IsEdit = true;
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null) { await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog"); }
                else
                { await DialogHost.Show(new DialogGlobalView("Koreksi Tarif Tangki Air", "Silahkan pilih data", "warning"), "HublangRootDialog"); }
            }
            await Task.FromResult(_isTest);
        }
    }
}
