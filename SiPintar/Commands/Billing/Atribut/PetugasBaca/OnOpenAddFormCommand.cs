using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.PetugasBaca;

namespace SiPintar.Commands.Billing.Atribut.PetugasBaca
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new ParamMasterPetugasBacaDto();

            _viewModel.JenisPembacaForm = _viewModel.JenisPembacaList[0];
            _viewModel.StatusForm = _viewModel.StatusList[0];
            _viewModel.FotoPetugasURI = null;
            _viewModel.PasswordForm = null;

            ShowDialog();

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
