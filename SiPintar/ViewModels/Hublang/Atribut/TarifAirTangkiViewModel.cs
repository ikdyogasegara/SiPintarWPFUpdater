using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Atribut.TarifAirTangki;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Atribut
{
    public class TarifAirTangkiViewModel : ViewModelBase
    {
        public TarifAirTangkiViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnLimitDataChangeCommand = new OnLoadCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this, isTest);
            OnNextPageCommand = new OnNextPageCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get { return OnLoadCommand; } }
        public ICommand OnRefreshCommand { get { return OnLoadCommand; } }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnLimitDataChangeCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }


        private ObservableCollection<MasterTarifTangkiDto> _dataList = new ObservableCollection<MasterTarifTangkiDto>();
        public ObservableCollection<MasterTarifTangkiDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }


        private MasterTarifTangkiDto _selectedData;
        public MasterTarifTangkiDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisNonAirDto> _jenisNonAir;
        public ObservableCollection<MasterJenisNonAirDto> JenisNonAir
        {
            get { return _jenisNonAir; }
            set
            {
                _jenisNonAir = value;
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

        private bool _isKategoriChecked;
        public bool IsKategoriChecked
        {
            get { return _isKategoriChecked; }
            set
            {
                _isKategoriChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKodeTarifChecked;
        public bool IsKodeTarifChecked
        {
            get { return _isKodeTarifChecked; }
            set
            {
                _isKodeTarifChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaTarifChecked;
        public bool IsNamaTarifChecked
        {
            get { return _isNamaTarifChecked; }
            set
            {
                _isNamaTarifChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterKategori = "";
        public string FilterKategori
        {
            get { return _filterKategori; }
            set
            {
                _filterKategori = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodeTarif = "";
        public string FilterKodeTarif
        {
            get { return _filterKodeTarif; }
            set
            {
                _filterKodeTarif = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaTarif = "";
        public string FilterNamaTarif
        {
            get { return _filterNamaTarif; }
            set
            {
                _filterNamaTarif = value;
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
                OnLoadCommand.Execute(null);
            }
        }

        //<<prev next page data
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
        //prev next page data>>

        public bool IsEdit { get; set; }


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

    }
}
