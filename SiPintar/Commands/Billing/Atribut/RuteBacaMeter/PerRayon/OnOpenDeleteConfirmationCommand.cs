using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon
{
    public class OnOpenDeleteConfirmationCommand : AsyncCommandBase
    {
        private readonly PerRayonViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenDeleteConfirmationCommand(PerRayonViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedJadwal == null)
                return;

            ShowDialog();

            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private void ShowDialog()
        {
            if (!_isTest)
                _ = DialogHost.Show(new DialogGlobalYesCancelView(
                    "Hapus Data?",
                    $"Dengan melakukan penghapusan ini, jadwal rute untuk petugas '{_viewModel.SelectedJadwal.PetugasBaca}' pada rayon '{_viewModel.SelectedRayon.NamaRayon}' tidak akan dapat dikembalikan.",
                    "warning",
                    _viewModel.OnSubmitDeleteCommand,
                    "Hapus",
                    "Batal",
                    false,
                    true,
                    "billing"
                ), "BillingRootDialog");
        }
    }
}
