using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using LiveCharts;
using SiPintar.Commands;
using SiPintar.Commands.Billing.Supervisi.Periode;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PeriodeViewModel : ViewModelBase
    {
        public PeriodeViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi, bool istest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, istest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, istest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, istest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, istest);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this, istest);
            OnOpenLimitChartFormCommand = new OnOpenLimitChartFormCommand(this, restApi, istest);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, istest);
            OnSubmitAddAsBackgroundCommand = new OnSubmitAddAsBackgroundCommand(this, restApi, istest);
            OnSubmitAddAsNormalCommand = new OnSubmitAddAsNormalCommand(this, restApi, istest);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi, istest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, istest);
            OnSubmitChangeStatusCommand = new OnSubmitChangeStatusCommand(this, restApi, istest);
            OnSubmitLimitChartCommand = new OnSubmitLimitChartCommand(this, restApi, istest);
            OnSetChartCommand = new OnSetChartCommand(this, istest);
            OnOpenChangeStatusConfirmCommand = new OnOpenChangeStatusConfirmCommand(this, istest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, istest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, istest);
            OnGetTglDendaCommand = new OnGetTglDendaCommand(this, restApi, istest);

            ZoomingModeX = ZoomingOptions.X;
            ZoomingModeY = ZoomingOptions.Y;
            Formatter = value => value.ToString("N0");
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenLimitChartFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitAddAsBackgroundCommand { get; }
        public ICommand OnSubmitAddAsNormalCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitChangeStatusCommand { get; }
        public ICommand OnSubmitLimitChartCommand { get; }
        public ICommand OnSetChartCommand { get; }
        public ICommand OnOpenChangeStatusConfirmCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnGetTglDendaCommand { get; }

        #region HakAkses
        private bool _aksesTambah = PermissionHelper.HasPermission("Billing.Periode_Tambah");
        public bool AksesTambah
        {
            get => _aksesTambah;
            set { _aksesTambah = value; OnPropertyChanged(); }
        }

        private bool _aksesHapus = PermissionHelper.HasPermission("Billing.Periode_Hapus");
        public bool AksesHapus
        {
            get => _aksesHapus;
            set { _aksesHapus = value; OnPropertyChanged(); }
        }

        private bool _aksesKoreksiTanggalDenda = PermissionHelper.HasPermission("Billing.Periode_Koreksi_Tanggal_Denda");
        public bool AksesKoreksiTanggalDenda
        {
            get => _aksesKoreksiTanggalDenda;
            set { _aksesKoreksiTanggalDenda = value; OnPropertyChanged(); }
        }

        private bool _aksesBukaTutup = PermissionHelper.HasPermission("Billing.Periode_BukaTutup");
        public bool AksesBukaTutup
        {
            get => _aksesBukaTutup;
            set { _aksesBukaTutup = value; OnPropertyChanged(); }
        }
        #endregion

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private string _statusSection;
        public string StatusSection
        {
            get => _statusSection;
            set { _statusSection = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeWpf> _periodeList = new ObservableCollection<MasterPeriodeWpf>();
        public ObservableCollection<MasterPeriodeWpf> PeriodeList
        {
            get => _periodeList;
            set { _periodeList = value; OnPropertyChanged(); }
        }


        private bool _isCheckedRayon;
        public bool IsCheckedRayon
        {
            get => _isCheckedRayon;
            set
            {
                _isCheckedRayon = value;

                if (value)
                {
                    IsCheckedWilayah = false;
                }

                OnPropertyChanged();
            }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get => _selectedRayon;
            set
            {
                _selectedRayon = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRayonDto>? _rayonList;
        public ObservableCollection<MasterRayonDto>? RayonList
        {
            get => _rayonList;
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedWilayah;
        public bool IsCheckedWilayah
        {
            get => _isCheckedWilayah;
            set
            {
                _isCheckedWilayah = value;

                if (value)
                {
                    IsCheckedRayon = false;
                }

                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _selectedWilayah;
        public MasterWilayahDto SelectedWilayah
        {
            get => _selectedWilayah;
            set
            {
                _selectedWilayah = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto>? _wilayahList;
        public ObservableCollection<MasterWilayahDto>? WilayahList
        {
            get => _wilayahList;
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeWpf _selectedData;
        public MasterPeriodeWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                if (value != null)
                {
                    OnSetChartCommand.Execute(null);
                }
            }
        }

        private bool _isEmptyChart = true;
        public bool IsEmptyChart
        {
            get => _isEmptyChart;
            set { _isEmptyChart = value; OnPropertyChanged(); }
        }

        private bool _isLoadingChart;
        public bool IsLoadingChart
        {
            get => _isLoadingChart;
            set { _isLoadingChart = value; OnPropertyChanged(); }
        }

        private ZoomingOptions _zoomingModeX;
        public ZoomingOptions ZoomingModeX
        {
            get => _zoomingModeX;
            set { _zoomingModeX = value; OnPropertyChanged(); }
        }

        private ZoomingOptions _zoomingModeY;
        public ZoomingOptions ZoomingModeY
        {
            get => _zoomingModeY;
            set { _zoomingModeY = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _jumlahTagihanChart;
        public ChartValues<int> JumlahTagihanChart
        {
            get => _jumlahTagihanChart;
            set { _jumlahTagihanChart = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _jumlahPiutangChart;
        public ChartValues<int> JumlahPiutangChart
        {
            get => _jumlahPiutangChart;
            set { _jumlahPiutangChart = value; OnPropertyChanged(); }
        }

        private ChartValues<int> _jumlahTerbayarChart;
        public ChartValues<int> JumlahTerbayarChart
        {
            get => _jumlahTerbayarChart;
            set { _jumlahTerbayarChart = value; OnPropertyChanged(); }
        }

        private string[] _labels;
        public string[] Labels
        {
            get => _labels;
            set { _labels = value; OnPropertyChanged(); }
        }

        public Func<double, string> Formatter { get; set; }

        private bool _isBuatPeriodeAirForm;
        public bool IsBuatPeriodeAirForm
        {
            get => _isBuatPeriodeAirForm;
            set { _isBuatPeriodeAirForm = value; OnPropertyChanged(); }
        }

        public ObservableCollection<KeyValuePair<string, string>> BulanList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>();

                int index = 1;
                foreach (var item in new CultureInfo("id-ID").DateTimeFormat.MonthNames)
                {
                    string MonthIndex = index.ToString();
                    if (MonthIndex.Length < 2)
                        MonthIndex = "0" + MonthIndex;

                    if (!string.IsNullOrWhiteSpace(item))
                        data.Add(new KeyValuePair<string, string>(MonthIndex, item));
                    index++;
                }

                return data;
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> TahunList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>();

                int startYear = 2016;
                int endYear = DateTime.Now.Year + 1;
                for (int i = startYear; i <= endYear; i++)
                    data.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));

                return data;
            }
        }

        private KeyValuePair<string, string> _bulanForm;
        public KeyValuePair<string, string> BulanForm
        {
            get => _bulanForm;
            set
            {
                _bulanForm = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _tahunForm;
        public KeyValuePair<string, string> TahunForm
        {
            get => _tahunForm;
            set
            {
                _tahunForm = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglMulaiTagihForm;
        public DateTime? TglMulaiTagihForm
        {
            get => _tglMulaiTagihForm;
            set
            {
                _tglMulaiTagihForm = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda1Form;
        public DateTime? TglDenda1Form
        {
            get => _tglDenda1Form;
            set
            {
                _tglDenda1Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda2Form;
        public DateTime? TglDenda2Form
        {
            get => _tglDenda2Form;
            set
            {
                _tglDenda2Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda3Form;
        public DateTime? TglDenda3Form
        {
            get => _tglDenda3Form;
            set
            {
                _tglDenda3Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda4Form;
        public DateTime? TglDenda4Form
        {
            get => _tglDenda4Form;
            set
            {
                _tglDenda4Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglMulaiDendaForm;
        public DateTime? TglMulaiDendaForm
        {
            get => _tglMulaiDendaForm;
            set
            {
                _tglMulaiDendaForm = value;
                OnPropertyChanged();
            }
        }

        private bool _agreementForm;
        public bool AgreementForm
        {
            get => _agreementForm;
            set { _agreementForm = value; OnPropertyChanged(); }
        }

        private string _limitChart = "0";
        public string LimitChart
        {
            get => _limitChart;
            set { _limitChart = value; OnPropertyChanged(); }
        }

        private bool _isProcessAsBackgroundForm;
        public bool IsProcessAsBackgroundForm
        {
            get => _isProcessAsBackgroundForm;
            set { _isProcessAsBackgroundForm = value; OnPropertyChanged(); }
        }

        // setting table

        private object _tableSetting;
        public object TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        // end setting table

        #region pagination

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(10, "10"),
                    new KeyValuePair<int, string>(20, "20"),
                    new KeyValuePair<int, string>(50, "50"),
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(300, "300"),
                    new KeyValuePair<int, string>(500, "500"),
                };
                return data;
            }
        }

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage = 1;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
