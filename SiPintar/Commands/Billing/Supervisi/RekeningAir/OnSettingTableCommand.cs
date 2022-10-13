using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views.Billing.Supervisi.RekeningAir;

namespace SiPintar.Commands.Billing.Supervisi.RekeningAir
{
    public class OnSettingTableCommand : AsyncCommandBase
    {
        private readonly RekeningAirViewModel _viewModel;
        private readonly bool _isTest;
        public OnSettingTableCommand(RekeningAirViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            if (!_isTest) _ = DialogHost.Show(new SettingTableView(_viewModel), "BillingRootDialog");
            await Task.FromResult(true);
        }
    }
}
