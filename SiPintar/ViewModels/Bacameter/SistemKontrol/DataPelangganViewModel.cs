using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.DataPelanggan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class DataPelangganViewModel : ViewModelBase
    {
        public DataPelangganViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = OnLoadCommand;
            OnExportCommand = new OnExportCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, restApi);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnFilterCommand = OnLoadCommand;
            OnResetFilterCommand = new OnResetFilterCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnSubmitAddFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }

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

        private ObservableCollection<MasterPelangganAirDto> _dataList;
        public ObservableCollection<MasterPelangganAirDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _selectedData;
        public MasterPelangganAirDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
            }
        }

        private MasterPelangganAirDto _formData;
        public MasterPelangganAirDto FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        // Filter

        private MasterPelangganAirDto _formFilter = new MasterPelangganAirDto();
        public MasterPelangganAirDto FormFilter
        {
            get { return _formFilter; }
            set
            {
                _formFilter = value;
                OnPropertyChanged();
            }
        }

        #region Tanggal Daftar
        private bool _isTanggalDaftarChecked;
        public bool IsTanggalDaftarChecked
        {
            get { return _isTanggalDaftarChecked; }
            set
            {
                _isTanggalDaftarChecked = value;
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

        #region Golongan
        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                if (!value)
                {
                    GolonganFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGolonganDto> _golonganList;
        public ObservableCollection<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _golonganFilter;
        public MasterGolonganDto GolonganFilter
        {
            get { return _golonganFilter; }
            set
            {
                _golonganFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdGolongan = _golonganFilter?.IdGolongan;
            }
        }
        #endregion

        #region Diameter
        private bool _isDiameterChecked;
        public bool IsDiameterChecked
        {
            get { return _isDiameterChecked; }
            set
            {
                _isDiameterChecked = value;
                if (!value)
                {
                    DiameterFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDiameterDto> _diameterList;
        public ObservableCollection<MasterDiameterDto> DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _diameterFilter;
        public MasterDiameterDto DiameterFilter
        {
            get { return _diameterFilter; }
            set
            {
                _diameterFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdDiameter = _diameterFilter?.IdDiameter;
            }
        }
        #endregion

        #region Rayon
        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get { return _isRayonChecked; }
            set
            {
                _isRayonChecked = value;
                if (!value)
                {
                    RayonFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList;
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _rayonFilter;
        public MasterRayonDto RayonFilter
        {
            get { return _rayonFilter; }
            set
            {
                _rayonFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdRayon = _rayonFilter?.IdRayon;
            }
        }
        #endregion

        #region Blok
        private bool _isBlokChecked;
        public bool IsBlokChecked
        {
            get { return _isBlokChecked; }
            set
            {
                _isBlokChecked = value;
                if (!value)
                {
                    BlokFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBlokDto> _blokList;
        public ObservableCollection<MasterBlokDto> BlokList
        {
            get { return _blokList; }
            set
            {
                _blokList = value;
                OnPropertyChanged();
            }
        }

        private MasterBlokDto _blokFilter;
        public MasterBlokDto BlokFilter
        {
            get { return _blokFilter; }
            set
            {
                _blokFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdBlok = _blokFilter?.IdBlok;
            }
        }
        #endregion

        #region Kelurahan
        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                if (!value)
                {
                    KelurahanFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList;
        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _kelurahanFilter;
        public MasterKelurahanDto KelurahanFilter
        {
            get { return _kelurahanFilter; }
            set
            {
                _kelurahanFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdKelurahan = _kelurahanFilter?.IdKelurahan;
            }
        }
        #endregion

        #region Alamat
        private bool _isAlamatChecked;
        public bool IsAlamatChecked
        {
            get { return _isAlamatChecked; }
            set
            {
                _isAlamatChecked = value;
                if (!value)
                {
                    var temp = FormFilter;
                    temp.Alamat = null;
                    FormFilter = temp;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Wilayah
        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                if (!value)
                {
                    WilayahFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _wilayahFilter;
        public MasterWilayahDto WilayahFilter
        {
            get { return _wilayahFilter; }
            set
            {
                _wilayahFilter = value;
                OnPropertyChanged();

                if (FormFilter != null)
                    FormFilter.IdWilayah = _wilayahFilter?.IdWilayah;
            }
        }
        #endregion


        // End Filter

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }

}
