using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.ManajementParaf;

namespace SiPintar.Commands.Main.ManajementParaf
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly ManajementParafViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(ManajementParafViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new ParamMasterParafDto();

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "MainRootDialog");
        }
    }
}
