using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Status;

namespace SiPintar.Commands.Billing.Atribut.Status
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly StatusViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(StatusViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        [ExcludeFromCodeCoverage]
        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.NamaForm = _viewModel.SelectedData.NamaStatus;
            _viewModel.IncludeRekeningAirForm = _viewModel.SelectedData.Rekening_Air_Include != null && (bool)_viewModel.SelectedData.Rekening_Air_Include;
            _viewModel.IncludeRekeningLimbahForm = _viewModel.SelectedData.Rekening_Limbah_Include != null && (bool)_viewModel.SelectedData.Rekening_Limbah_Include;
            _viewModel.IncludeRekeningLLTTForm = _viewModel.SelectedData.Rekening_LLTT_Include != null && (bool)_viewModel.SelectedData.Rekening_LLTT_Include;
            _viewModel.TanpaBiayaAirForm = _viewModel.SelectedData.TanpaBiayaPemakaianAir != null && (bool)_viewModel.SelectedData.TanpaBiayaPemakaianAir;

            ShowDialog();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
