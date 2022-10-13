using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Hublang.Atribut.TipePendaftaran;

namespace SiPintar.Commands.Hublang.Atribut.TipePendaftaran
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly TipePendaftaranViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(TipePendaftaranViewModel viewModel, bool isTest = false)
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
                else await DialogHost.Show(new DialogGlobalView("Koreksi TipePendaftaran", "Silahkan pilih data", "warning"), "HublangRootDialog");
            }
        }
    }
}
