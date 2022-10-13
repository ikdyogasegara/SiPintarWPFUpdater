using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.Periode;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PeriodeViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            #region akses
            if (_viewModel.AksesTambah == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            _viewModel.IsEdit = false;
            _viewModel.IsBuatPeriodeAirForm = true;
            _viewModel.BulanForm = new KeyValuePair<string, string>();
            _viewModel.TahunForm = new KeyValuePair<string, string>();
            _viewModel.TglDenda1Form = null;
            _viewModel.TglDenda2Form = null;
            _viewModel.TglDenda3Form = null;
            _viewModel.TglDenda4Form = null;
            _viewModel.TglMulaiDendaForm = null;
            _viewModel.AgreementForm = false;
            _viewModel.IsProcessAsBackgroundForm = false;
            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;
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
