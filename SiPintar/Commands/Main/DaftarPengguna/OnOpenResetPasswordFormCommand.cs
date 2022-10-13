using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.DaftarPengguna;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnOpenResetPasswordFormCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordFormCommand(DaftarPenggunaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.FormData = new ParamMasterUserDto()
            {
                IdUser = _viewModel.SelectedData.IdUser,
                Nama = _viewModel.SelectedData.Nama,
                NamaUser = _viewModel.SelectedData.NamaUser,
                IdRole = _viewModel.SelectedData.IdRole,
                Password = null
            };

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                DialogHost.Close("MainRootDialog");
                _ = DialogHost.Show(new PasswordFormView(_viewModel), "MainRootDialog");
            }
        }
    }
}
