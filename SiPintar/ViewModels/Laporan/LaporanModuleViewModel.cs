using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands.Global;
using SiPintar.Commands.Laporan.LaporanModule;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Laporan
{
    public class LaporanModuleViewModel : ViewModelBase
    {
        private readonly Dictionary<string, int> _module = new Dictionary<string, int>
        {
            { "billing", 1 },
            { "bacameter", 3 },
            { "hublang", 4 },
            { "perencanaan", 5 },
            { "distribusi", 6 },
            { "gudang", 7 },
            { "personalia", 8 },
            { "akuntansikeuangan", 9 },
            { "keuangan", 10 },
        };
        private readonly bool _isTest;
        public int TestCase;

        private int? _idModule;
        public int? IdModule
        {
            get
            {
                return _idModule;
            }
            set
            {
                _idModule = value;
                OnPropertyChanged();
            }
        }

        private string _selectedModule;
        public string SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                _selectedModule = value;
                OnPropertyChanged();
            }
        }

        public delegate void RenderItemAction();
        public event RenderItemAction RenderReportItemEvent;

        public ICommand OnLoadCommand => _onLoadCommand;

        public ICommand OnOpenItemCommand => _onOpenItemCommand;

        public ICommand OnShowReportCommand => _onShowReportCommand;

        public ICommand OpenDesignerCommand => _openDesignerCommand;
        public ICommand OnCetakCommand => _onCetakCommand;

        public LaporanModuleViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            _onLoadCommand = new OnLoadCommand(this, restApi, isTest);
            _onOpenItemCommand = new OnOpenItemCommand(this, isTest);
            _onShowReportCommand = new OnShowReportCommand(this, restApi, isTest);
            _openDesignerCommand = new OpenDesignerCommand(this, isTest);
            _onCetakCommand = new OnCetakCommand(restApi, "laporan", "master-report", "Cetak Laporan", ErrorHandlingCetak, isTest) { IsReport = true };

        }

        public void UpdateModule(string moduleName = null)
        {
            if (string.IsNullOrWhiteSpace(moduleName))
            {
                IdModule = null;
                return;
            }
            IdModule = _module[moduleName.Replace(" & ", "")];
            SelectedModule = $"{moduleName[0].ToString().ToUpper()}{moduleName[1..]}";
            OnLoadCommand.Execute(null);
        }

        private List<INavigationItem> _navigationItems;
        public List<INavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set { _navigationItems = value; OnPropertyChanged(); }
        }

        public List<INavigationItem> GetNavigationItems()
        {
            var IdBilling = _module["billing"].ToString();
            var IdBacameter = _module["bacameter"].ToString();
            var IdHublang = _module["hublang"].ToString();
            var IdPerencanaan = _module["perencanaan"].ToString();
            var IdDistribusi = _module["distribusi"].ToString();
            var IdGudang = _module["gudang"].ToString();
            var IdPersonalia = _module["personalia"].ToString();
            var IdAkuntansi = _module["akuntansikeuangan"].ToString();

            // var akses = Application.Current.Resources["ModuleAkses"]?.ToString();
            var akses = "1,3,4,5,6,7,8,9";

            if (_isTest && TestCase > 0)
            {
                akses = "1,2,3,4,5,6,7,8,9";
            }
            var moduleAkses = new List<string>();
            if (akses != null)
            {
                var listAkses = akses.Split(',');
                moduleAkses.AddRange(listAkses.Select(item => item.ToLower()));
            }

            var nav = new List<INavigationItem>()
            {
                new SubheaderNavigationItem() { Subheader = "LAPORAN PER MODUL" }
            };
            if (moduleAkses.Count <= 0)
            {
                return nav;
            }

            if (moduleAkses.Contains(IdBilling))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Billing", Icon = PackIconKind.PaymentSettings, IsSelected = false });
            }
            if (moduleAkses.Contains(IdBacameter))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Bacameter", Icon = PackIconKind.CameraMeteringCenter, IsSelected = false });
            }
            if (moduleAkses.Contains(IdHublang))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Hublang", Icon = PackIconKind.PeopleGroup, IsSelected = false });
            }
            if (moduleAkses.Contains(IdPerencanaan))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Perencanaan", Icon = PackIconKind.Planner, IsSelected = false });
            }
            if (moduleAkses.Contains(IdDistribusi))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Distribusi", Icon = PackIconKind.DistributeVerticalCenter, IsSelected = false });
            }
            if (moduleAkses.Contains(IdGudang))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Gudang", Icon = PackIconKind.Warehouse, IsSelected = false });
            }
            if (moduleAkses.Contains(IdPersonalia))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Personalia", Icon = PackIconKind.PeopleSwitch, IsSelected = false });
            }
            if (moduleAkses.Contains(IdAkuntansi))
            {
                nav.Add(new FirstLevelNavigationItem() { Label = "Akuntansi & Keuangan", Icon = PackIconKind.AccountBalance, IsSelected = false });
            }
            return nav;
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _loadingMsg;
        public string LoadingMsg
        {
            get { return _loadingMsg; }
            set
            {
                _loadingMsg = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterReportMainGroupDto> _dataReportGroup;
        public ObservableCollection<MasterReportMainGroupDto> DataReportGroup
        {
            get { return _dataReportGroup; }
            set
            {
                _dataReportGroup = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReportModelDto> _dataReport;
        public ObservableCollection<ReportModelDto> DataReport
        {
            get { return _dataReport; }
            set
            {
                _dataReport = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DataReportFilterList> _dataReportFilter = new ObservableCollection<DataReportFilterList>();
        private readonly ICommand _onLoadCommand;
        private readonly ICommand _onOpenItemCommand;
        private readonly ICommand _onShowReportCommand;
        private readonly ICommand _openDesignerCommand;
        private readonly ICommand _onCetakCommand;

        public ObservableCollection<DataReportFilterList> DataReportFilter
        {
            get { return _dataReportFilter; }
            set
            {
                _dataReportFilter = value;
                OnPropertyChanged();
            }
        }

        public void InvokeDataUpdate()
        {
            RenderReportItemEvent?.Invoke();
        }
    }
}
