using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganL2T2;

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PelangganL2T2ViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsLoadingForm = true;

            _viewModel.IsEdit = true;
            _viewModel.PelangganForm = (MasterPelangganLlttDto)_viewModel.SelectedData.Clone();

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new DialogFormView(_viewModel), "BillingRootDialog");
        }
    }
}
