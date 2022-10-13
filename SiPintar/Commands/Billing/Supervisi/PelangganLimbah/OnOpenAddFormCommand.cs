using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PelangganLimbahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            _viewModel.IsEdit = false;
            _viewModel.PelangganForm = new MasterPelangganLimbahDto();

            _viewModel.SelectedTarifLimbahForm = null;
            _viewModel.SelectedRayonForm = null;
            _viewModel.SelectedKelurahanForm = null;
            _viewModel.SelectedKolektifForm = null;
            _viewModel.SelectedStatusForm = null;
            _viewModel.SelectedFlagForm = null;
            _viewModel.SelectedPelangganAirForm = null;

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
