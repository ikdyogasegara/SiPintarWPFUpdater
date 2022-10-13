using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Atribut.TipePendaftaran;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Atribut
{
    public class TipePendaftaranViewModel : ViewModelBase
    {
        public TipePendaftaranViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnLimitDataChangeCommand = new OnLoadCommand(this, restApi, isTest);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnLimitDataChangeCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }


        private bool _isEdit;
        public bool IsEdit
        {
            get { return _isEdit; }
            set
            {
                _isEdit = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipePendaftaranSambunganDto> _TipePendaftaranList = new ObservableCollection<MasterTipePendaftaranSambunganDto>();
        public ObservableCollection<MasterTipePendaftaranSambunganDto> TipePendaftaranList
        {
            get { return _TipePendaftaranList; }
            set
            {
                _TipePendaftaranList = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePendaftaranSambunganDto _selectedData;
        public MasterTipePendaftaranSambunganDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePendaftaranSambunganDto _formData;
        public MasterTipePendaftaranSambunganDto FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilterVisible = true;
        public bool IsFilterVisible
        {
            get { return _isFilterVisible; }
            set
            {
                _isFilterVisible = value;
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


        private bool _isNamaTipePendaftaranSambunganChecked;
        public bool IsNamaTipePendaftaranSambunganChecked
        {
            get { return _isNamaTipePendaftaranSambunganChecked; }
            set
            {
                _isNamaTipePendaftaranSambunganChecked = value;
                if (!value)
                {
                    FilterNamaTipePendaftaranSambungan = null;
                }
                OnPropertyChanged();
            }
        }

        private string _filterNamaTipePendaftaranSambungan;
        public string FilterNamaTipePendaftaranSambungan
        {
            get { return _filterNamaTipePendaftaranSambungan; }
            set
            {
                _filterNamaTipePendaftaranSambungan = value;
                OnPropertyChanged();
            }
        }

        #region Pagination

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
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

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var record = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(10, "10"),
                    new KeyValuePair<int, string>(20, "20"),
                    new KeyValuePair<int, string>(50, "50"),
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(300, "300"),
                    new KeyValuePair<int, string>(500, "500"),
                    //new KeyValuePair<int, string>(0, "Semua")
                };

                return record;
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

        #endregion

    }
}
