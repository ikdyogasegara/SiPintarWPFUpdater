using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.ProfilSaya;

namespace SiPintar.Commands.Main.ProfilSaya
{
    public class OnOpenResetPasswordFormCommand : AsyncCommandBase
    {
        private readonly ProfilSayaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordFormCommand(ProfilSayaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.DetailData == null)
                return;

            _viewModel.FormData = new ParamMasterUserDto()
            {
                IdUser = _viewModel.DetailData.IdUser,
                Nama = _viewModel.DetailData.Nama,
                NamaUser = _viewModel.DetailData.NamaUser,
                IdRole = _viewModel.DetailData.IdRole,
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
