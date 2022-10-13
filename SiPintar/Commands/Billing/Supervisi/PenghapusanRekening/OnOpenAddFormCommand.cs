using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PenghapusanRekening;

namespace SiPintar.Commands.Billing.Supervisi.PenghapusanRekening
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly PenghapusanRekeningViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(PenghapusanRekeningViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesTambah == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading)
                return;

            _viewModel.IsLoadingForm = true;

            _viewModel.NoSambPiutang = null;
            _viewModel.PiutangList = new ObservableCollection<RekeningAirPiutangWpf>();
            _viewModel.IsSelectAllPiutang = false;
            _viewModel.FirstPiutang = new RekeningAirPiutangWpf();
            _viewModel.TotalPiutang = 0;

            OpenDialog();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest) _ = DialogHost.Show(new TambahHapusRekeningView(_viewModel), "BillingRootDialog");
        }

    }
}
