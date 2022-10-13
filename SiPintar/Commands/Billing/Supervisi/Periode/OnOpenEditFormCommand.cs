using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using NPOI.SS.Formula.Functions;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.Periode;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            #region akses
            if (_viewModel.AksesKoreksiTanggalDenda == false)
            {
                DialogHelper.ShowInvalidAkses(_isTest);
                return;
            }
            #endregion

            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsCheckedRayon = false;
            _viewModel.IsCheckedWilayah = false;
            _viewModel.SelectedRayon = null;
            _viewModel.SelectedWilayah = null;

            var year = _viewModel.SelectedData.KodePeriode.ToString().Substring(0, 4);
            var month = _viewModel.SelectedData.KodePeriode.ToString().Substring(4);
            _viewModel.IsEdit = true;
            _viewModel.BulanForm = _viewModel.BulanList.FirstOrDefault(i => i.Key == month);
            _viewModel.TahunForm = _viewModel.TahunList.FirstOrDefault(i => i.Key == year);
            _viewModel.TglMulaiTagihForm = _viewModel.SelectedData.TglMulaiTagih;
            _viewModel.TglDenda1Form = _viewModel.SelectedData.TglMulaiDenda1Wpf;
            _viewModel.TglDenda2Form = _viewModel.SelectedData.TglMulaiDenda2Wpf;
            _viewModel.TglDenda3Form = _viewModel.SelectedData.TglMulaiDenda3Wpf;
            _viewModel.TglDenda4Form = _viewModel.SelectedData.TglMulaiDenda4Wpf;
            _viewModel.TglMulaiDendaForm = _viewModel.SelectedData.TglMulaiDendaPerBulanWpf;

            _viewModel.AgreementForm = true;
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
