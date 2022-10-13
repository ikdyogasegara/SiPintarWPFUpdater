using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;
using SiPintar.Views.Billing.Atribut.RuteBacaMeter.PerPetugasBaca;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    public class OnOpenEditFormCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenEditFormCommand(PerPetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoadingJadwal || _viewModel.SelectedJadwal == null)
                return;

            ShowDialog();

            KeyValuePair<int, string>? BatasTanggalBaca = null;
            if (_viewModel.SelectedJadwal.TanggalMulaiBaca != null)
                BatasTanggalBaca = _viewModel.Parent.TanggalList.FirstOrDefault(i => i.Key == _viewModel.SelectedJadwal.TanggalMulaiBaca);

            _viewModel.IsTglBacaChecked = BatasTanggalBaca != null;
            _viewModel.TanggalBacaForm = BatasTanggalBaca;

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new EditFormView(_viewModel), "BillingRootDialog");
        }
    }
}
