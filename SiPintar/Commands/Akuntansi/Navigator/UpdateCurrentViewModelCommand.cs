using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using SiPintar.State.Navigators;
using SiPintar.ViewModels.Akuntansi;
using SiPintar.ViewModels.Akuntansi.Factories;

namespace SiPintar.Commands.Akuntansi.Navigator
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
            if (parameter is AkuntansiViewType viewType)
            {
                _navigator.AkuntansiCurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

                TriggerEventChange(viewType);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private void TriggerEventChange(AkuntansiViewType viewType)
        {
            switch (viewType)
            {
                case AkuntansiViewType.Voucher:
                    ((VoucherViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((VoucherViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((VoucherViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.PostingAkuntansi:
                    ((PostingAkuntansiViewModel)_navigator.AkuntansiCurrentViewModel).OnLoadCommand.Execute(null);
                    break;
                case AkuntansiViewType.PostingKeuangan:
                    ((PostingKeuanganViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((PostingKeuanganViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((PostingKeuanganViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.Jurnal:
                    ((JurnalViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((JurnalViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((JurnalViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.Penyusutan:
                    ((PenyusutanViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((PenyusutanViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((PenyusutanViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.MasterData:
                    ((MasterDataViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((MasterDataViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((MasterDataViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.Bantuan:
                    ((BantuanViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((BantuanViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((BantuanViewModel)_navigator.AkuntansiCurrentViewModel).PageViewModel = null!;
                    break;
                case AkuntansiViewType.Pengaturan:
                    ((PengaturanViewModel)_navigator.AkuntansiCurrentViewModel).NavigationItems = ((PengaturanViewModel)_navigator.AkuntansiCurrentViewModel).GetNavigationItems();
                    ((PengaturanViewModel)_navigator.AkuntansiCurrentViewModel).UpdatePage("Daftar Posting");
                    break;
                default:
                    break;
            }
        }
    }
}
