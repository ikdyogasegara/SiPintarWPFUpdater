using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter.SistemKontrol;
using SiPintar.Views.Bacameter.SistemKontrol.DataPembacaan;

namespace SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan
{
    public class OnOpenAddFormCommand : AsyncCommandBase
    {
        private readonly DataPembacaanViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenAddFormCommand(DataPembacaanViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading)
                return;

            _viewModel.BulanForm = new KeyValuePair<string, string>();
            _viewModel.TahunForm = new KeyValuePair<string, string>();
            _viewModel.TglDenda1Form = null;
            _viewModel.TglDenda2Form = null;
            _viewModel.TglDenda3Form = null;
            _viewModel.TglDenda4Form = null;
            _viewModel.TglMulaiDendaForm = null;
            _viewModel.AgreementForm = false;

            _viewModel.IsLoadingForm = true;

            ShowDialog();

            _viewModel.IsLoadingForm = false;
            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogFormView(_viewModel), "BacameterRootDialog");
        }
    }
}
