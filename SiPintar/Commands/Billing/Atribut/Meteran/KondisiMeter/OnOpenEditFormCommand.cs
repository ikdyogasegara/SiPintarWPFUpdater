using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Meteran;
using SiPintar.Views.Billing.Atribut.Meteran.KondisiMeter;

namespace SiPintar.Commands.Billing.Atribut.Meteran.KondisiMeter
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly KondisiMeterViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(KondisiMeterViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeForm = _viewModel.SelectedData.KodeKondisiMeter;
            _viewModel.NamaForm = _viewModel.SelectedData.NamaKondisiMeter;

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
