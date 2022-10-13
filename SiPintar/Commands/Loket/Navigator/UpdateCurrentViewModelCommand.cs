using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Loket;
using SiPintar.ViewModels.Loket.Factories;

namespace SiPintar.Commands.Loket.Navigator
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
            if (parameter is LoketViewType viewType)
            {
                _navigator.LoketCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(LoketViewType viewType)
        {
            switch (viewType)
            {
                case LoketViewType.Tagihan:
                    ((TagihanViewModel)_navigator.LoketCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case LoketViewType.Angsuran:
                    ((AngsuranViewModel)_navigator.LoketCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case LoketViewType.Laporan:
                    ((LaporanViewModel)_navigator.LoketCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case LoketViewType.TutupLoket:
                    ((TutupLoketViewModel)_navigator.LoketCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case LoketViewType.Setoran:
                    ((SetoranViewModel)_navigator.LoketCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case LoketViewType.Bantuan:
                    ((BantuanViewModel)_navigator.LoketCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.LoketCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.LoketCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
