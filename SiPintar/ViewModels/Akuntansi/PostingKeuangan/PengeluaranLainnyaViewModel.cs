using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.PostingKeuangan.PengeluaranLainnya;
using SiPintar.Commands.Global;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.PostingKeuangan
{
    public class PengeluaranLainnyaViewModel : ViewModelBase
    {
        public PengeluaranLainnyaViewModel(PostingKeuanganViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnPratinjauCommand = new OnPratinjauCommand(this, restApi, isTest);
            OnCetakCommand = new OnCetakCommand(restApi, "akuntansi", "payment-cetak", "Pratinjau Pengeluaran Lainnya", ErrorHandlingCetak, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnPratinjauCommand { get; }
        public ICommand OnCetakCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }

        private bool _isLoading { get; set; }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DaftarBiayaKasDataGridDto> _dataList = new();
        public ObservableCollection<DaftarBiayaKasDataGridDto> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private DaftarBiayaKasDataGridDto _selectedData = new();
        public DaftarBiayaKasDataGridDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeAkuntansiDto> _periodeList = new();
        public ObservableCollection<MasterPeriodeAkuntansiDto> PeriodeList
        {
            get => _periodeList;
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeAkuntansiDto> _periodeTutupBukuList = new();
        public ObservableCollection<MasterPeriodeAkuntansiDto> PeriodeTutupBukuList
        {
            get => _periodeTutupBukuList;
            set
            {
                _periodeTutupBukuList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraan3List = new();
        public ObservableCollection<MasterPerkiraan3Dto> Perkiraan3List
        {
            get => _perkiraan3List;
            set { _perkiraan3List = value; OnPropertyChanged(); }
        }

        #region table setting
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
        #endregion

        #region Form
        private ParamDaftarBiayaKasDto _pengeluaranLainnyaForm = new();
        public ParamDaftarBiayaKasDto PengeluaranLainnyaForm
        {
            get => _pengeluaranLainnyaForm;
            set { _pengeluaranLainnyaForm = value; OnPropertyChanged(); }
        }

        private string _notransForm = string.Empty;
        public string NotransForm
        {
            get => _notransForm;
            set { _notransForm = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto _selectedWilayah;
        public MasterWilayahDto SelectedWilayah
        {
            get => _selectedWilayah;
            set { _selectedWilayah = value; OnPropertyChanged(); }
        }

        private MasterPerkiraan3Dto _selectedDebet;
        public MasterPerkiraan3Dto SelectedDebet
        {
            get => _selectedDebet;
            set { _selectedDebet = value; OnPropertyChanged(); }
        }

        private MasterPerkiraan3Dto _selectedKredit;
        public MasterPerkiraan3Dto SelectedKredit
        {
            get => _selectedKredit;
            set { _selectedKredit = value; OnPropertyChanged(); }
        }
        #endregion


        #region Filter
        #region Tanggal Input
        private bool _isTanggalInputChecked;
        public bool IsTanggalInputChecked
        {
            get => _isTanggalInputChecked;
            set
            {
                _isTanggalInputChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterTanggalInputAwal = string.Empty;
        public string FilterTanggalInputAwal
        {
            get => _filterTanggalInputAwal;
            set
            {
                _filterTanggalInputAwal = value;
                OnPropertyChanged();
            }
        }

        private string _filterTanggalInputAkhir = string.Empty;
        public string FilterTanggalInputAkhir
        {
            get => _filterTanggalInputAkhir;
            set
            {
                _filterTanggalInputAkhir = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Uraian
        private bool _isUraianCheckedChecked;
        public bool IsUraianChecked
        {
            get => _isUraianCheckedChecked;
            set
            {
                _isUraianCheckedChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterUraian = string.Empty;
        public string FilterUraian
        {
            get => _filterUraian;
            set
            {
                _filterUraian = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region NoTrans
        private bool _isNoTransChecked;
        public bool IsNoTransChecked
        {
            get => _isNoTransChecked;
            set
            {
                _isNoTransChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterNoTrans = string.Empty;
        public string FilterNoTrans
        {
            get => _filterNoTrans;
            set
            {
                _filterNoTrans = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Wilayah
        private bool _isWilayahCheckedChecked;
        public bool IsWilayahChecked
        {
            get => _isWilayahCheckedChecked;
            set
            {
                _isWilayahCheckedChecked = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _filterWilayah = new();
        public MasterWilayahDto FilterWilayah
        {
            get => _filterWilayah;
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Jenis Hutang
        private bool _isJenisHutangChecked;
        public bool IsJenisHutangChecked
        {
            get => _isJenisHutangChecked;
            set
            {
                _isJenisHutangChecked = value;
                OnPropertyChanged();
            }
        }

        private dynamic _filterJenisHutang;
        public dynamic FilterJenisHutang
        {
            get => _filterJenisHutang;
            set
            {
                _filterJenisHutang = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Perkiraan Debet
        private bool _isPerkiraanDebetChecked;
        public bool IsPerkiraanDebetChecked
        {
            get => _isPerkiraanDebetChecked;
            set
            {
                _isPerkiraanDebetChecked = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterPerkiraanDebet = new();
        public MasterPerkiraan3Dto FilterPerkiraanDebet
        {
            get => _filterPerkiraanDebet;
            set
            {
                _filterPerkiraanDebet = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Perkiraan Kredit
        private bool _isPerkiraanKreditChecked;
        public bool IsPerkiraanKreditChecked
        {
            get => _isPerkiraanKreditChecked;
            set
            {
                _isPerkiraanKreditChecked = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterPerkiraanKredit = new();
        public MasterPerkiraan3Dto FilterPerkiraanKredit
        {
            get => _filterPerkiraanKredit;
            set
            {
                _filterPerkiraanKredit = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #endregion

        #region Pagination prop
        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

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

        private KeyValuePair<int, string> _limitData = new(20, "20");
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
