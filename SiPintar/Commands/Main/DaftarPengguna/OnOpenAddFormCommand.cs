using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.DaftarPengguna;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DaftarPenggunaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new ParamMasterUserDto();
            _viewModel.SelectedRole = null;
            _viewModel.SelectedLoket = null;
            _viewModel.SelectedStatus = _viewModel.StatusList[0];

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
