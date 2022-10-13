using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Perencanaan;
using SiPintar.ViewModels.Perencanaan.Factories;

namespace SiPintar.Commands.Perencanaan.Navigator
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
            if (parameter is PerencanaanViewType viewType)
            {
                _navigator.PerencanaanCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(PerencanaanViewType viewType)
        {
            switch (viewType)
            {
                case PerencanaanViewType.Perencanaan:
                    ((PerencanaanViewModel)_navigator.PerencanaanCurrentViewModel).NavigationItems = ((PerencanaanViewModel)_navigator.PerencanaanCurrentViewModel).GetNavigationItems();
                    ((PerencanaanViewModel)_navigator.PerencanaanCurrentViewModel).UpdatePage("Surat Perintah Kerja (SPK) Pelanggan Air");
                    break;
                case PerencanaanViewType.Atribut:
                    ((AtributViewModel)_navigator.PerencanaanCurrentViewModel).NavigationItems = ((AtributViewModel)_navigator.PerencanaanCurrentViewModel).GetNavigationItems();
                    ((AtributViewModel)_navigator.PerencanaanCurrentViewModel).UpdatePage("Material");
                    break;
                case PerencanaanViewType.Bantuan:
                    ((BantuanViewModel)_navigator.PerencanaanCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.PerencanaanCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.PerencanaanCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
