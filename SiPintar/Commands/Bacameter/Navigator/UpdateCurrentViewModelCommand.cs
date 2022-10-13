using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Bacameter;
using SiPintar.ViewModels.Bacameter.Factories;

namespace SiPintar.Commands.Bacameter.Navigator
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
            if (parameter is BacameterViewType viewType)
            {
                _navigator.BacameterCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(BacameterViewType viewType)
        {
            switch (viewType)
            {
                case BacameterViewType.Supervisi:
                    ((SupervisiViewModel)_navigator.BacameterCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case BacameterViewType.Produktivitas:
                    ((ProduktivitasViewModel)_navigator.BacameterCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case BacameterViewType.PemetaanPelanggan:
                    ((PemetaanPelangganViewModel)_navigator.BacameterCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case BacameterViewType.SistemKontrol:
                    ((SistemKontrolViewModel)_navigator.BacameterCurrentViewModel).NavigationItems = ((SistemKontrolViewModel)_navigator.BacameterCurrentViewModel).GetNavigationItems();
                    ((SistemKontrolViewModel)_navigator.BacameterCurrentViewModel).UpdatePage("Rute Baca Meter");
                    break;
                case BacameterViewType.Bantuan:
                    ((BantuanViewModel)_navigator.BacameterCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.BacameterCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.BacameterCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
