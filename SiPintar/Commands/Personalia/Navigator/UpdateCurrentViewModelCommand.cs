using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Personalia;
using SiPintar.ViewModels.Personalia.Factories;

namespace SiPintar.Commands.Personalia.Navigator
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
            if (parameter is PersonaliaViewType viewType)
            {
                _navigator.PersonaliaCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(PersonaliaViewType viewType)
        {
            switch (viewType)
            {
                case PersonaliaViewType.DataMaster:
                    ((DataMasterViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((DataMasterViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((DataMasterViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Pegawai");
                    break;
                case PersonaliaViewType.Kepegawaian:
                    ((KepegawaianViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((KepegawaianViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((KepegawaianViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("SK Calon Pegawai");
                    break;
                case PersonaliaViewType.GajiPokok:
                    ((GajiPokokViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((GajiPokokViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((GajiPokokViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Pegawai Tetap");
                    break;
                case PersonaliaViewType.Tunjangan:
                    ((TunjanganViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((TunjanganViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((TunjanganViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Master Tunjangan");
                    break;
                case PersonaliaViewType.Potongan:
                    ((PotonganViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((PotonganViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((PotonganViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Master Potongan");
                    break;
                case PersonaliaViewType.Lainnya:
                    ((LainnyaViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((LainnyaViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((LainnyaViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Status Keluarga Pegawai");
                    break;
                case PersonaliaViewType.SupervisiGaji:
                    ((SupervisiGajiViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((SupervisiGajiViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((SupervisiGajiViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Rekap Absensi");
                    break;
                case PersonaliaViewType.Bantuan:
                    ((BantuanViewModel)_navigator.PersonaliaCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.PersonaliaCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.PersonaliaCurrentViewModel).UpdatePage("Cara Penggunaan");
                    break;
                default:
                    break;
            }
        }
    }
}
