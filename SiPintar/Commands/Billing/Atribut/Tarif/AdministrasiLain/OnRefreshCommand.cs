using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly AdministrasiLainViewModel _viewModel;
        private readonly bool _isTest;

        public OnRefreshCommand(AdministrasiLainViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            OpenDialogHost();

            await Task.Run(() =>
            {
                _viewModel.OnLoadCommand.Execute(null);
            });

            CLoseDialogHost();

            await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        public void OpenDialogHost()
        {
            if (!_isTest)
                _ = DialogHost.Show(new GlobalLoadingDialog("Mohon Tunggu", "Atribut tarif pemeliharaan", "sedang diperbarui"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        public void CLoseDialogHost()
        {
            if (!_isTest)
                DialogHost.Close("BillingRootDialog");
        }
    }
}
