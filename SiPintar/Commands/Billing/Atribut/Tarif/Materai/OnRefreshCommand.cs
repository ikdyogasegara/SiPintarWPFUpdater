using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Atribut.Tarif;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Atribut.Tarif.Materai
{
    public class OnRefreshCommand : AsyncCommandBase
    {
        private readonly MateraiViewModel _viewModel;
        private readonly bool _isTest;

        public OnRefreshCommand(MateraiViewModel viewModel, bool isTest = false)
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
        private void OpenDialogHost()
        {
            if (!_isTest)
                _ = DialogHost.Show(new GlobalLoadingDialog("Mohon Tunggu", "Atribut tarif materai", "sedang diperbarui"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CLoseDialogHost()
        {
            if (!_isTest)
                DialogHost.Close("BillingRootDialog");
        }
    }
}
