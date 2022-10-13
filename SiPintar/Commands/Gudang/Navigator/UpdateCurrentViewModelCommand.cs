using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Gudang;
using SiPintar.ViewModels.Gudang.Factories;

namespace SiPintar.Commands.Gudang.Navigator
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
            if (parameter is GudangViewType viewType)
            {
                _navigator.GudangCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(GudangViewType viewType)
        {
            switch (viewType)
            {
                case GudangViewType.MasterData:
                    ((MasterDataViewModel)_navigator.GudangCurrentViewModel).NavigationItems = ((MasterDataViewModel)_navigator.GudangCurrentViewModel).GetNavigationItems();
                    ((MasterDataViewModel)_navigator.GudangCurrentViewModel).UpdatePage("Barang");
                    break;
                case GudangViewType.ProsesTransaksi:
                    ((ProsesTransaksiViewModel)_navigator.GudangCurrentViewModel).NavigationItems = ((ProsesTransaksiViewModel)_navigator.GudangCurrentViewModel).GetNavigationItems();
                    ((ProsesTransaksiViewModel)_navigator.GudangCurrentViewModel).PageViewModel = null!;
                    break;
                case GudangViewType.Pengolahan:
                    ((PengolahanViewModel)_navigator.GudangCurrentViewModel).NavigationItems = PengolahanViewModel.GetNavigationItems();
                    ((PengolahanViewModel)_navigator.GudangCurrentViewModel).PageViewModel = null!;
                    break;
                case GudangViewType.Bantuan:
                    ((BantuanViewModel)_navigator.GudangCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.GudangCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.GudangCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
