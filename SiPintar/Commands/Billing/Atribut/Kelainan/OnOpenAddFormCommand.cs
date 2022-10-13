using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Kelainan;

namespace SiPintar.Commands.Billing.Atribut.Kelainan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly KelainanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(KelainanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsEdit = false;
            _viewModel.FormData = new MasterKelainanDto();

            _viewModel.JenisKelainanForm = null;
            _viewModel.StatusForm = _viewModel.StatusList[0];

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
