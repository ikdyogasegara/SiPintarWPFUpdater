using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Main;
using SiPintar.Views.Main.DaftarPengguna;

namespace SiPintar.Commands.Main.DaftarPengguna
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly DaftarPenggunaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(DaftarPenggunaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.FormData = new ParamMasterUserDto()
            {
                IdUser = _viewModel.SelectedData.IdUser,
                Nama = _viewModel.SelectedData.Nama,
                NamaUser = _viewModel.SelectedData.NamaUser,
                Password = null,
                Aktif = _viewModel.SelectedData.Aktif,
                NoIdentitas = _viewModel.SelectedData.NoIdentitas,
                IdRole = _viewModel.SelectedData.IdRole,
                IdLoket = _viewModel.SelectedData.IdLoket
            };

            int IdStatus = _viewModel.FormData.Aktif == true ? 1 : 2;

            _viewModel.SelectedRole = _viewModel.UserRoleList?.FirstOrDefault(i => i.IdRole == _viewModel.FormData.IdRole);
            _viewModel.SelectedLoket = _viewModel.LoketList?.FirstOrDefault(i => i.IdLoket == _viewModel.FormData.IdLoket);
            _viewModel.SelectedStatus = _viewModel.StatusList?.FirstOrDefault(i => i.Key == IdStatus);

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
