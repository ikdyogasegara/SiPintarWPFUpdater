using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.PelangganLimbah;

namespace SiPintar.Commands.Billing.Supervisi.PelangganLimbah
{
    public class OnOpenRiwayatPembayaranCommand : AsyncCommandBase
    {
        private readonly PelangganLimbahViewModel _viewModel;
        private readonly bool _isTest;

        public OnOpenRiwayatPembayaranCommand(PelangganLimbahViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsLoading || _viewModel.SelectedData == null)
                return;

            OpenDialog();

            _viewModel.TahunPembayaranForm = new KeyValuePair<string, string>();
            _ = ((AsyncCommandBase)_viewModel.OnSearchRiwayatPembayaranCommand).ExecuteAsync(null);

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialog()
        {
            if (!_isTest && !DialogHost.IsDialogOpen("BillingRootDialog"))
                _ = DialogHost.Show(new RiwayatPembayaranView(_viewModel), "BillingRootDialog");
        }
    }
}
