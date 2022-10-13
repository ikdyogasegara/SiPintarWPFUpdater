using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Distribusi;
using SiPintar.ViewModels.Distribusi.Factories;

namespace SiPintar.Commands.Distribusi.Navigator
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
            if (parameter is DistribusiViewType viewType)
            {
                _navigator.DistribusiCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(DistribusiViewType viewType)
        {
            switch (viewType)
            {

                case DistribusiViewType.Distribusi:
                    ((DistribusiViewModel)_navigator.DistribusiCurrentViewModel).NavigationItems = ((DistribusiViewModel)_navigator.DistribusiCurrentViewModel).GetNavigationItems();
                    ((DistribusiViewModel)_navigator.DistribusiCurrentViewModel).UpdatePage("Usulan");
                    break;
                case DistribusiViewType.Notifikasi:
                    ((NotifikasiViewModel)_navigator.DistribusiCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case DistribusiViewType.Atribut:
                    ((AtributViewModel)_navigator.DistribusiCurrentViewModel).NavigationItems = ((AtributViewModel)_navigator.DistribusiCurrentViewModel).GetNavigationItems();
                    ((AtributViewModel)_navigator.DistribusiCurrentViewModel).UpdatePage("Kelainan Ganti Meter");
                    break;
                case DistribusiViewType.Bantuan:
                    ((BantuanViewModel)_navigator.DistribusiCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.DistribusiCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.DistribusiCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
