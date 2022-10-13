using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Laporan;
using SiPintar.ViewModels.Laporan.Factories;

namespace SiPintar.Commands.Laporan.Navigator
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
            if (parameter is LaporanViewType viewType)
            {
                _navigator.LaporanCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(LaporanViewType viewType)
        {
            switch (viewType)
            {
                case LaporanViewType.Laporan:
                    ((LaporanModuleViewModel)_navigator.LaporanCurrentViewModel).NavigationItems = ((LaporanModuleViewModel)_navigator.LaporanCurrentViewModel).GetNavigationItems();
                    break;
                case LaporanViewType.Bantuan:
                    break;
                default:
                    break;
            }
        }
    }
}
