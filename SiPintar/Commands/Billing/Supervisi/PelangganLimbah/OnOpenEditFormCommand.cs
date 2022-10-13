using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PelangganLimbahViewModel viewModel, bool isTest = false)
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
            _viewModel.PelangganForm = _viewModel.SelectedData;

            _viewModel.SelectedTarifLimbahForm = _viewModel.TarifLimbahList?.FirstOrDefault(i => i.IdTarifLimbah == _viewModel.SelectedData.IdTarifLimbah);
            _viewModel.SelectedRayonForm = _viewModel.RayonList?.FirstOrDefault(i => i.IdRayon == _viewModel.SelectedData.IdRayon);
            _viewModel.SelectedKelurahanForm = _viewModel.KelurahanList?.FirstOrDefault(i => i.IdKelurahan == _viewModel.SelectedData.IdKelurahan);
            _viewModel.SelectedKolektifForm = _viewModel.KolektifList?.FirstOrDefault(i => i.IdKolektif == _viewModel.SelectedData.IdKolektif);
            _viewModel.SelectedStatusForm = _viewModel.StatusList?.FirstOrDefault(i => i.IdStatus == _viewModel.SelectedData.IdStatus);
            _viewModel.SelectedFlagForm = _viewModel.FlagList?.FirstOrDefault(i => i.IdFlag == _viewModel.SelectedData.IdFlag);
            _viewModel.SelectedPelangganAirForm = _viewModel.PelangganAirList?.FirstOrDefault(i => i.IdPelangganAir == _viewModel.SelectedData.IdPelangganAir);

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
