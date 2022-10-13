using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Main.DaftarPengguna;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Main
{
    public class DaftarPenggunaViewModel : ViewModelBase
    {
        public DaftarPenggunaViewModel(MainViewModel parent, IRestApiClientModel restApi)
        {
            _ = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = OnLoadCommand;
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenResetPasswordConfirmationCommand = new OnOpenResetPasswordConfirmationCommand(this);
            OnOpenResetPasswordFormCommand = new OnOpenResetPasswordFormCommand(this);
            OnOpenDetailUserCommand = new OnOpenDetailUserCommand(this, restApi);
            OnSubmitAddUserCommand = new OnSubmitAddUserCommand(this, restApi);
            OnSubmitEditUserCommand = new OnSubmitEditUserCommand(this, restApi);
            OnSubmitResetPasswordCommand = new OnSubmitResetPasswordCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenResetPasswordConfirmationCommand { get; }
        public ICommand OnOpenResetPasswordFormCommand { get; }
        public ICommand OnOpenDetailUserCommand { get; }
        public ICommand OnSubmitAddUserCommand { get; }
        public ICommand OnSubmitEditUserCommand { get; }
        public ICommand OnSubmitResetPasswordCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }


        private ObservableCollection<MasterUserDto> _dataList;
        public ObservableCollection<MasterUserDto> DataList
        {
            get => _dataList;
            set { _dataList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isDataSelected;
        public bool IsDataSelected
        {
            get => _isDataSelected;
            set { _isDataSelected = value; OnPropertyChanged(); }
        }

        private MasterUserDto _selectedData;
        public MasterUserDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
            }
        }

        private MasterUserDto _detailData;
        public MasterUserDto DetailData
        {
            get => _detailData;
            set { _detailData = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterUserRoleDto> _userRoleList;
        public ObservableCollection<MasterUserRoleDto> UserRoleList
        {
            get => _userRoleList;
            set { _userRoleList = value; OnPropertyChanged(); }
        }

        private MasterUserRoleDto _selectedRole;
        public MasterUserRoleDto SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.IdRole = _selectedRole?.IdRole;
            }
        }

        private ObservableCollection<MasterLoketDto> _loketList;
        public ObservableCollection<MasterLoketDto> LoketList
        {
            get => _loketList;
            set { _loketList = value; OnPropertyChanged(); }
        }

        private MasterLoketDto _selectedLoket;
        public MasterLoketDto SelectedLoket
        {
            get => _selectedLoket;
            set
            {
                _selectedLoket = value;
                OnPropertyChanged();

                //if (FormData != null)
                //    FormData.IdLoket = _selectedLoket?.IdLoket;
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> StatusList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Aktif"),
                    new KeyValuePair<int, string>(2, "Non Aktif")
                };
            }
        }

        private KeyValuePair<int, string>? _selectedStatus;
        public KeyValuePair<int, string>? SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();

                if (FormData != null)
                    FormData.Aktif = _selectedStatus?.Key == 1;
            }
        }

        private ParamMasterUserDto _formData;
        public ParamMasterUserDto FormData
        {
            get => _formData;
            set { _formData = value; OnPropertyChanged(); }
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
                OnRefreshCommand.Execute(null);
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

        private object _detailPermission;
        public object DetailPermission
        {
            get { return _detailPermission; }
            set
            {
                _detailPermission = value;
                OnPropertyChanged();
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
