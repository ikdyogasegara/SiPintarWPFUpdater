using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Atribut.TipePermohonan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Atribut
{
    public class TipePermohonanViewModel : ViewModelBase
    {
        public TipePermohonanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
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

        private ObservableCollection<MasterJenisNonAirDto> _masterJenisNonAirList = new ObservableCollection<MasterJenisNonAirDto>();
        public ObservableCollection<MasterJenisNonAirDto> MasterJenisNonAirList
        {
            get { return _masterJenisNonAirList; }
            set
            {
                _masterJenisNonAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipePermohonanDto> _masterTipePermohonanList = new ObservableCollection<MasterTipePermohonanDto>();
        public ObservableCollection<MasterTipePermohonanDto> MasterTipePermohonanList
        {
            get { return _masterTipePermohonanList; }
            set
            {
                _masterTipePermohonanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipePermohonanDto> _tipePermohonanList = new ObservableCollection<MasterTipePermohonanDto>();
        public ObservableCollection<MasterTipePermohonanDto> TipePermohonanList
        {
            get { return _tipePermohonanList; }
            set
            {
                _tipePermohonanList = value;
                OnPropertyChanged();
            }
        }

        private MasterTipePermohonanDto _tipePermohonanFilter = new MasterTipePermohonanDto();
        public MasterTipePermohonanDto TipePermohonanFilter
        {
            get { return _tipePermohonanFilter; }
            set
            {
                _tipePermohonanFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isTipePermohonanChecked;
        public bool IsTipePermohonanChecked
        {
            get { return _isTipePermohonanChecked; }
            set
            {
                _isTipePermohonanChecked = value;
                if (!value)
                {
                    var temp = TipePermohonanFilter;
                    temp.IdTipePermohonan = null;
                    TipePermohonanFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNamaJenisNonairChecked;
        public bool IsNamaJenisNonairChecked
        {
            get { return _isNamaJenisNonairChecked; }
            set
            {
                _isNamaJenisNonairChecked = value;
                if (!value)
                {
                    var temp = TipePermohonanFilter;
                    temp.IdJenisNonAir = null;
                    TipePermohonanFilter = temp;
                }
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

        private int _totalRecord;
        public int TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private int _limit = 50;
        public int Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                OnPropertyChanged();
                OnFilterCommand.Execute(null);
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
    }
}
