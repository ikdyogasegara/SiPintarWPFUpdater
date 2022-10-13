using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace SiPintar.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class VmBase : ViewModelBase
    {
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

        private ICommand _onLoadCommand;
        public ICommand OnLoadCommand
        {
            get { return _onLoadCommand; }
            set
            {
                _onLoadCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onRefreshCommand;
        public ICommand OnRefreshCommand
        {
            get { return _onRefreshCommand; }
            set
            {
                _onRefreshCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onResetFilterCommand;
        public ICommand OnResetFilterCommand
        {
            get { return _onResetFilterCommand; }
            set
            {
                _onResetFilterCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onOpenAddFormCommand;
        public ICommand OnOpenAddFormCommand
        {
            get { return _onOpenAddFormCommand; }
            set
            {
                _onOpenAddFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onSubmitAddFormCommand;
        public ICommand OnSubmitAddFormCommand
        {
            get { return _onSubmitAddFormCommand; }
            set
            {
                _onSubmitAddFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onOpenEditFormCommand;
        public ICommand OnOpenEditFormCommand
        {
            get { return _onOpenEditFormCommand; }
            set
            {
                _onOpenEditFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onSubmitEditFormCommand;
        public ICommand OnSubmitEditFormCommand
        {
            get { return _onSubmitEditFormCommand; }
            set
            {
                _onSubmitEditFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onOpenDeleteFormCommand;
        public ICommand OnOpenDeleteFormCommand
        {
            get { return _onOpenDeleteFormCommand; }
            set
            {
                _onOpenDeleteFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onSubmitDeleteFormCommand;
        public ICommand OnSubmitDeleteFormCommand
        {
            get { return _onSubmitDeleteFormCommand; }
            set
            {
                _onSubmitDeleteFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onOpenSettingTableFormCommand;
        public ICommand OnOpenSettingTableFormCommand
        {
            get { return _onOpenSettingTableFormCommand; }
            set
            {
                _onOpenSettingTableFormCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand _onSubmitSettingTableFormCommand;
        public ICommand OnSubmitSettingTableFormCommand
        {
            get { return _onSubmitSettingTableFormCommand; }
            set
            {
                _onSubmitSettingTableFormCommand = value;
                OnPropertyChanged();
            }
        }

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

        private bool _isPageNumberVisible;
        public bool IsPageNumberVisible
        {
            get { return _isPageNumberVisible; }
            set
            {
                _isPageNumberVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}
