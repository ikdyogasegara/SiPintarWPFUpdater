using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Atribut;
using SiPintar.Views.Hublang.Atribut.Peruntukan;

namespace SiPintar.Commands.Hublang.Atribut.Peruntukan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PeruntukanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PeruntukanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new MasterPeruntukanDto();

            await ShowFormDialogAsync();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowFormDialogAsync()
        {
            if (!_isTest) await DialogHost.Show(new DialogFormView(_viewModel), "HublangRootDialog");
        }
    }
}
