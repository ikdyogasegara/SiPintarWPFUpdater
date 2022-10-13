using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Atribut.Peruntukan;

namespace SiPintar.Commands.Hublang.Atribut.Peruntukan
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PeruntukanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PeruntukanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsEdit = true;
            _viewModel.FormData = _viewModel.SelectedData;
            await ShowFormDialogAsync();
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormDialogAsync()
        {
            if (!_isTest)
            {
                if (_viewModel.SelectedData != null) await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog");
                else await DialogHost.Show(new DialogGlobalView("Koreksi Peruntukan", "Silahkan pilih data", "warning"), "HublangRootDialog");
            }
        }
    }
}
