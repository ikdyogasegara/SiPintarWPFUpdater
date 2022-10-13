using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.PetugasBaca;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.IsOnGrid = false;
            _viewModel.FormData = new ParamMasterPetugasBacaDto()
            {
                IdPetugasBaca = _viewModel.SelectedData.IdPetugasBaca,
                NamaUser = _viewModel.SelectedData.NamaUser,
                KodePetugasBaca = _viewModel.SelectedData.KodePetugasBaca,
                PetugasBaca = _viewModel.SelectedData.PetugasBaca,
                JenisPembaca = _viewModel.SelectedData.JenisPembaca,
                Alamat = _viewModel.SelectedData.Alamat,
                TglLahir = _viewModel.SelectedData.TglLahir,
                TglMulaiKerja = _viewModel.SelectedData.TglMulaiKerja,
                NoHp = _viewModel.SelectedData.NoHp,
                FotoPetugasBaca = _viewModel.SelectedData.FotoPetugasBaca,
                Keterangan = _viewModel.SelectedData.Keterangan,
                Status = _viewModel.SelectedData.Status
            };
            _viewModel.PasswordForm = null;

            var Status = _viewModel.FormData.Status == true ? 1 : 0;

            _viewModel.JenisPembacaForm = _viewModel.JenisPembacaList.FirstOrDefault(i => i.Value == _viewModel.FormData.JenisPembaca);
            _viewModel.StatusForm = _viewModel.StatusList.FirstOrDefault(i => i.Key == Status);

            ShowDialog();

            await ((AsyncCommandBase)_viewModel.GetFotoCommand).ExecuteAsync(_viewModel.FormData.IdPetugasBaca);

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
