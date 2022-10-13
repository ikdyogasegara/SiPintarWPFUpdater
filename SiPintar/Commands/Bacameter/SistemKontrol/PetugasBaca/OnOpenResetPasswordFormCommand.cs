using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Bacameter.SistemKontrol.PetugasBaca;

namespace SiPintar.Commands.Bacameter.SistemKontrol.PetugasBaca
{
    public class OnOpenResetPasswordFormCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenResetPasswordFormCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.FormData = new ParamMasterPetugasBacaDto()
            {
                IdPetugasBaca = _viewModel.SelectedData.IdPetugasBaca,
                KodePetugasBaca = _viewModel.SelectedData.KodePetugasBaca,
                PetugasBaca = _viewModel.SelectedData.PetugasBaca,
                JenisPembaca = _viewModel.SelectedData.JenisPembaca,
                NamaUser = _viewModel.SelectedData.NamaUser,
                Alamat = _viewModel.SelectedData.Alamat,
                TglLahir = _viewModel.SelectedData.TglLahir,
                TglMulaiKerja = _viewModel.SelectedData.TglMulaiKerja,
                Status = _viewModel.SelectedData.Status,
            };

            _viewModel.PasswordForm = null;

            OpenDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest)
            {
                DialogHost.Close("BacameterRootDialog");
                _ = DialogHost.Show(new PasswordFormView(_viewModel), "BacameterRootDialog");
            }
        }
    }
}
