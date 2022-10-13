using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut;
using SiPintar.Views.Billing.Atribut.Kolektif;

namespace SiPintar.Commands.Billing.Atribut.Kolektif
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KolektifViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KolektifViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeKolektifForm = _viewModel.SelectedData.KodeKolektif;
            _viewModel.NamaKolektifForm = _viewModel.SelectedData.NamaKolektif;
            _viewModel.KeteranganForm = _viewModel.SelectedData.Keterangan;

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
