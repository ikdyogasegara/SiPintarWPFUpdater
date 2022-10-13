using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Billing;
using SiPintar.ViewModels.Billing.Factories;

namespace SiPintar.Commands.Billing.Navigator
{
    [ExcludeFromCodeCoverage]
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private readonly INavigator _navigator;
        private readonly IViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is BillingViewType viewType)
            {
                _navigator.BillingCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(BillingViewType viewType)
        {
            switch (viewType)
            {
                case BillingViewType.Supervisi:
                    ((SupervisiViewModel)_navigator.BillingCurrentViewModel).NavigationItems = ((SupervisiViewModel)_navigator.BillingCurrentViewModel).GetNavigationItems();
                    //((SupervisiViewModel)_navigator.BillingCurrentViewModel).UpdatePage("Periode");
                    break;
                case BillingViewType.Atribut:
                    ((AtributViewModel)_navigator.BillingCurrentViewModel).NavigationItems = ((AtributViewModel)_navigator.BillingCurrentViewModel).GetNavigationItems();
                    ((AtributViewModel)_navigator.BillingCurrentViewModel).UpdatePage("Tarif");
                    break;
                case BillingViewType.Produktivitas:
                    ((ProduktivitasViewModel)_navigator.BillingCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case BillingViewType.Bantuan:
                    ((BantuanViewModel)_navigator.BillingCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.BillingCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.BillingCurrentViewModel).UpdatePage("Saran & Pengaduan");
                    break;
                default:
                    break;
            }
        }
    }
}
