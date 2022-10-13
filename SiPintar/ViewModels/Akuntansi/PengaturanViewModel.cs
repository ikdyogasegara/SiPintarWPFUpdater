using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.Pengaturan;

namespace SiPintar.ViewModels.Akuntansi
{
    public class PengaturanViewModel : ViewModelBase
    {
        private readonly DaftarPostingViewModel _daftarPosting;
        private readonly SettingTtdViewModel _settingTtd;
        private readonly LogAksesViewModel _logAkses;
        private readonly SinkronasiBsbsViewModel _sinkronasiBsbs;
        private readonly BackupDatabaseViewModel _backupDatabase;
        private readonly RepairDatabaseViewModel _repairDatabase;

        public PengaturanViewModel(IRestApiClientModel restApi)
        {
            _daftarPosting = new DaftarPostingViewModel(restApi);
            _settingTtd = new SettingTtdViewModel(restApi);
            _logAkses = new LogAksesViewModel(restApi);
            _sinkronasiBsbs = new SinkronasiBsbsViewModel(restApi);
            _backupDatabase = new BackupDatabaseViewModel(restApi);
            _repairDatabase = new RepairDatabaseViewModel(restApi);
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

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Daftar Posting" => _daftarPosting,
                "Setting TTD" => _settingTtd,
                "Log Akses" => _logAkses,
                "Sinkronasi BSBS" => _sinkronasiBsbs,
                "Backup Database" => _backupDatabase,
                "Repair Database" => _repairDatabase,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", "pageName")
            };

            CurrentPageName = pageName;

            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Daftar Posting":
                        ((DaftarPostingViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Setting TTD":
                        ((SettingTtdViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Log Akses":
                        ((LogAksesViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Sinkronasi BSBS":
                        ((SinkronasiBsbsViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Backup Database":
                        ((BackupDatabaseViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    case "Repair Database":
                        ((RepairDatabaseViewModel)PageViewModel).OnLoadCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            });
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public List<INavigationItem> GetNavigationItems()
        {
            var Nav = new List<INavigationItem>
            {
                new SubheaderNavigationItem() { Subheader = "Menu Pengaturan" },
                new ColoredFirstLevelNavigationItem() { Label = "Daftar Posting", Icon = PackIconKind.FileExport, IconForeground = "#C30C0C", IsSelected = true },
                new ColoredFirstLevelNavigationItem() { Label = "Setting TTD", Icon = PackIconKind.DrawPen, IconForeground = "#028DDB", IsSelected = false },
                new ColoredFirstLevelNavigationItem() { Label = "Log Akses", Icon = PackIconKind.FormatListBulletedSquare, IconForeground = "#A66335", IsSelected = false },
                new ColoredFirstLevelNavigationItem() { Label = "Sinkronasi BSBS", Icon = PackIconKind.BookSync, IconForeground = "#D78813", IsSelected = false },
                new ColoredFirstLevelNavigationItem() { Label = "Backup Database", Icon = PackIconKind.DatabaseSync, IconForeground = "#9D68B7", IsSelected = false },
                new ColoredFirstLevelNavigationItem() { Label = "Repair Database", Icon = PackIconKind.DatabaseCog, IconForeground = "#FF4A6D", IsSelected = false },
            };

            return Nav;
        }
    }
}
