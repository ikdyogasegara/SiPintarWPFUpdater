using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Hublang;
using SiPintar.ViewModels.Hublang.Factories;

namespace SiPintar.Commands.Hublang.Navigator
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
            if (parameter is HublangViewType viewType)
            {
                _navigator.HublangCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(HublangViewType viewType)
        {
            switch (viewType)
            {
                case HublangViewType.Pelayanan:
                    ((PelayananViewModel)_navigator.HublangCurrentViewModel).NavigationItems = ((PelayananViewModel)_navigator.HublangCurrentViewModel).GetNavigationItems();
                    ((PelayananViewModel)_navigator.HublangCurrentViewModel).UpdatePage("Pengaduan Pelanggan Air");
                    break;
                case HublangViewType.Verifikasi:
                    ((VerifikasiViewModel)_navigator.HublangCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case HublangViewType.Atribut:
                    ((AtributViewModel)_navigator.HublangCurrentViewModel).NavigationItems = ((AtributViewModel)_navigator.HublangCurrentViewModel).GetNavigationItems();
                    ((AtributViewModel)_navigator.HublangCurrentViewModel).UpdatePage("Tipe Permohonan");
                    break;
                case HublangViewType.Bantuan:
                    ((BantuanViewModel)_navigator.HublangCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.HublangCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.HublangCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
