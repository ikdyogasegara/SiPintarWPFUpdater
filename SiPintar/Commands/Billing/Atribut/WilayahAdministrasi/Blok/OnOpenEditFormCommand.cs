using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi;
using SiPintar.Views.Billing.Atribut.WilayahAdministrasi.Blok;

namespace SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Blok
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly BlokViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(BlokViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            _viewModel.IsEdit = true;
            _viewModel.KodeBlokForm = _viewModel.SelectedData.KodeBlok;
            _viewModel.NamaBlokForm = _viewModel.SelectedData.NamaBlok;


            _viewModel.IsLoadingForm = true;

            ShowDialog();

            if (_viewModel.RayonList != null)
            {
                _viewModel.RayonForm = _viewModel.RayonList.FirstOrDefault(i => i.IdRayon == _viewModel.SelectedData.IdRayon);
            }

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
