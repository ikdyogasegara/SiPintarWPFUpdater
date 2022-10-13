using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.LogAksesUser;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class LogAksesUserViewModel : ViewModelBase
    {
        public LogAksesUserViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = OnLoadCommand;
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnExportCommand { get; }

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
        private bool _isDataSelected;
        public bool IsDataSelected
        {
            get { return _isDataSelected; }
            set
            {
                _isDataSelected = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
                OnLoadCommand.Execute(null);
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

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var ListOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
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

        private ObservableCollection<MasterLoggerDto> _dataList;
        public ObservableCollection<MasterLoggerDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private MasterLoggerDto _selectedData;
        public MasterLoggerDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
            }
        }

        private ParamMasterLoggerFilterDto _filterData = new ParamMasterLoggerFilterDto();
        public ParamMasterLoggerFilterDto FilterData
        {
            get { return _filterData; }
            set
            {
                _filterData = value;
                OnPropertyChanged();
            }
        }

        // Filter
        #region Rentang Waktu
        private bool _isRentangWaktuChecked;
        public bool IsRentangWaktuChecked
        {
            get { return _isRentangWaktuChecked; }
            set
            {
                _isRentangWaktuChecked = value;
                if (!value)
                {
                    RentangWaktu1Filter = null;
                    RentangWaktu2Filter = null;
                }

                OnPropertyChanged();
            }
        }

        private string _rentangWaktu1Filter;
        public string RentangWaktu1Filter
        {
            get { return _rentangWaktu1Filter; }
            set
            {
                _rentangWaktu1Filter = value;
                OnPropertyChanged();
            }
        }

        private string _rentangWaktu2Filter;
        public string RentangWaktu2Filter
        {
            get { return _rentangWaktu2Filter; }
            set
            {
                _rentangWaktu2Filter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Tipe
        private bool _isTipeChecked;
        public bool IsTipeChecked
        {
            get { return _isTipeChecked; }
            set
            {
                _isTipeChecked = value;
                if (!value)
                {
                    TipeFilter = null;
                }

                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> TipeList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Insert"),
                    new KeyValuePair<int, string>(2, "Update"),
                    new KeyValuePair<int, string>(3, "Delete"),
                    new KeyValuePair<int, string>(4, "Select")
                };

                return data;
            }
        }

        private KeyValuePair<int, string>? _tipeFilter;
        public KeyValuePair<int, string>? TipeFilter
        {
            get { return _tipeFilter; }
            set
            {
                _tipeFilter = value;
                OnPropertyChanged();

                if (FilterData != null)
                    FilterData.Tipe = _tipeFilter?.Value;
            }
        }
        #endregion

        #region Status
        private bool _isStatusChecked;
        public bool IsStatusChecked
        {
            get { return _isStatusChecked; }
            set
            {
                _isStatusChecked = value;
                if (!value)
                {
                    StatusFilter = null;
                }

                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Sukses"),
                    new KeyValuePair<int, string>(0, "Gagal"),
                };

                return data;
            }
        }

        private KeyValuePair<int, string>? _statusFilter;
        public KeyValuePair<int, string>? StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value;
                OnPropertyChanged();

                if (FilterData != null)
                {
                    if (_statusFilter != null)
                        FilterData.Sukses = _statusFilter?.Key == 1;
                    else
                        FilterData.Sukses = null;
                }
            }
        }
        #endregion

        #region Role
        private bool _isRoleChecked;
        public bool IsRoleChecked
        {
            get { return _isRoleChecked; }
            set
            {
                _isRoleChecked = value;
                if (!value)
                {
                    StatusFilter = null;
                }

                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserRoleDto> _roleList;
        public ObservableCollection<MasterUserRoleDto> RoleList
        {
            get { return _roleList; }
            set
            {
                _roleList = value;
                OnPropertyChanged();
            }
        }

        private MasterUserRoleDto _roleFilter;
        public MasterUserRoleDto RoleFilter
        {
            get { return _roleFilter; }
            set
            {
                _roleFilter = value;
                OnPropertyChanged();

                if (FilterData != null)
                    FilterData.IdRole = _roleFilter?.IdRole;
            }
        }
        #endregion

        #region Nama User
        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set
            {
                _isNamaChecked = value;
                if (!value)
                {
                    var temp = FilterData;
                    temp.Nama = null;
                    FilterData = temp;
                }

                OnPropertyChanged();
            }
        }
        #endregion

        private int _testId;
        public int TestId
        {
            get { return _testId; }
            set
            {
                _testId = value;
                OnPropertyChanged();
            }
        }
    }
}
