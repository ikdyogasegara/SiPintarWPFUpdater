using System;
using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Loket.Tagihan;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket.Tagihan;

namespace SiPintar.ViewModels.Loket
{
    public class TagihanViewModel : ViewModelBase
    {
        private readonly TagihanKolektifAirViewModel _kolektifAir;
        public TagihanViewModel(IRestApiClientModel restApi)
        {
            _kolektifAir = new TagihanKolektifAirViewModel(this, restApi);
            OnLoadCommand = new OnLoadCommand(this);
        }

        public ICommand OnLoadCommand { get; }

        private bool _isLoadingMainPage;
        public bool IsLoadingMainPage
        {
            get { return _isLoadingMainPage; }
            set { _isLoadingMainPage = value; OnPropertyChanged(); }
        }

        private bool _isLoketTutupToday;
        public bool IsLoketTutupToday
        {
            get => _isLoketTutupToday;
            set { _isLoketTutupToday = value; OnPropertyChanged(); }
        }

        private bool _isFullPage;
        public bool IsFullPage
        {
            get { return _isFullPage; }
            set { _isFullPage = value; OnPropertyChanged(); }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private string _currentPageName;
        public string CurrentPageName
        {
            get { return _currentPageName; }
            set { _currentPageName = value; OnPropertyChanged(); }
        }

        private DateTime _tanggalTransaksi;
        public DateTime TanggalTransaksi
        {
            get { return _tanggalTransaksi; }
            set
            {
                _tanggalTransaksi = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _waktuTransaksi;
        public DateTimeOffset WaktuTransaksi
        {
            get { return _waktuTransaksi; }
            set
            {
                _waktuTransaksi = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaRoleAdmin;
        public bool IsNamaRoleAdmin
        {
            get => _isNamaRoleAdmin;
            set { _isNamaRoleAdmin = value; OnPropertyChanged(); }
        }

        private string _pageSebelumRiwayat;
        public string PageSebelumRiwayat
        {
            get { return _pageSebelumRiwayat; }
            set { _pageSebelumRiwayat = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Kolektif Air" => _kolektifAir,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            CurrentPageName = pageName;

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            switch (pageName)
            {
                case "Kolektif Air":
                    ((TagihanKolektifAirViewModel)PageViewModel).OnLoadCommand.Execute(null);
                    break;
                default:
                    break;
            }
        }
    }
}
