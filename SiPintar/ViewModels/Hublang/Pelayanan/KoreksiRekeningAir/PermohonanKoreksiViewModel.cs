using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir
{
    public class PermohonanKoreksiViewModel : ViewModelBase
    {
        private readonly bool _isTest;

        public PermohonanKoreksiViewModel(KoreksiRekeningAirViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            Parent = parent;
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi, isTest);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi, isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnCariPelangganCommand = new OnCariPelangganCommand(this, restApi);
            GetPiutangListCommand = new GetPiutangListCommand(this, restApi);
            OnBrowseFileBuktiCommand = new OnBrowseFileBuktiCommand(this, _isTest);
            OnOpenImageCommand = new OnOpenImageCommand(this, _isTest);
            OnCetakCommandPelangganAir = new OnCetakCommand(restApi, "hublang", "permohonan-pelanggan-air-cetak", "Cetak Permohonan-Pengaduan Pelanggan Air", ErrorHandlingCetak, _isTest);
        }

        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnCetakCommandPelangganAir { get; }
        public ICommand OnCariPelangganCommand { get; }
        public ICommand GetPiutangListCommand { get; }
        public ICommand OnBrowseFileBuktiCommand { get; }
        public ICommand OnOpenImageCommand { get; }


        private KoreksiRekeningAirViewModel _parent;

        public KoreksiRekeningAirViewModel Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                OnPropertyChanged();
            }
        }

        private int _currentStep = 1;

        public int CurrentStep
        {
            get => _currentStep;
            set
            {
                _currentStep = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingCariPelanggan;
        public bool IsLoadingCariPelanggan
        {
            get { return _isLoadingCariPelanggan; }
            set
            {
                _isLoadingCariPelanggan = value;
                OnPropertyChanged();
            }
        }

        private string _namaPelangganForm;

        public string NamaPelangganForm
        {
            get => _namaPelangganForm;
            set
            {
                _namaPelangganForm = value;
                OnPropertyChanged();
            }
        }

        private string _noSambForm;

        public string NoSambForm
        {
            get => _noSambForm;
            set
            {
                _noSambForm = value;
                OnPropertyChanged();
            }
        }

        private string _alamatForm;

        public string AlamatForm
        {
            get => _alamatForm;
            set
            {
                _alamatForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganGlobalWpf> _dataPelanggan = new ObservableCollection<MasterPelangganGlobalWpf>();

        public ObservableCollection<MasterPelangganGlobalWpf> DataPelanggan
        {
            get => _dataPelanggan;
            set
            {
                _dataPelanggan = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganGlobalWpf _selectedPelangganAir;

        public MasterPelangganGlobalWpf SelectedPelangganAir
        {
            get => _selectedPelangganAir;
            set
            {
                _selectedPelangganAir = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPiutangWpf> _piutangAirList = new ObservableCollection<RekeningAirPiutangWpf>();

        public ObservableCollection<RekeningAirPiutangWpf> PiutangAirList
        {
            get => _piutangAirList;
            set
            {
                _piutangAirList = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyForm1;

        [ExcludeFromCodeCoverage]
        public bool IsEmptyForm1
        {
            get => _isEmptyForm1;
            set
            {
                _isEmptyForm1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyForm2;

        [ExcludeFromCodeCoverage]
        public bool IsEmptyForm2
        {
            get => _isEmptyForm2;
            set
            {
                _isEmptyForm2 = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalPiutangBiayaPemakaian = 0;

        [ExcludeFromCodeCoverage]
        public decimal TotalPiutangBiayaPemakaian
        {
            get => _totalPiutangBiayaPemakaian;
            set
            {
                _totalPiutangBiayaPemakaian = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PermohonanWpf> _dataList = new ObservableCollection<PermohonanWpf>();

        public ObservableCollection<PermohonanWpf> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedData;

        public PermohonanWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
            }
        }

        private bool _isExists;
        public bool IsExists
        {
            get => _isExists;
            set
            {
                _isExists = value; OnPropertyChanged();
            }
        }

        private string _isFor;

        public string IsFor
        {
            get => _isFor;
            set
            {
                _isFor = value;
                OnPropertyChanged();
            }
        }

        private bool _isCanEdit;

        public bool IsCanEdit
        {
            get => _isCanEdit;
            set
            {
                _isCanEdit = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty = true;

        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;

        public bool IsLoadingForm
        {
            get => _isLoadingForm;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(20, "20");

        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataPelanggan = new KeyValuePair<int, string>(10, "10");

        public KeyValuePair<int, string> LimitDataPelanggan
        {
            get => _limitDataPelanggan;
            set
            {
                _limitDataPelanggan = value;
                OnPropertyChanged();
            }
        }

        #region prev next page data pelanggan

        private int _currentPagePelanggan = 1;

        public int CurrentPagePelanggan
        {
            get { return _currentPagePelanggan; }
            set
            {
                _currentPagePelanggan = value;
                OnPropertyChanged();
            }
        }

        private int _totalPagePelanggan = 1;

        public int TotalPagePelanggan
        {
            get { return _totalPagePelanggan; }
            set
            {
                _totalPagePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabledPelanggan;

        public bool IsPreviousButtonEnabledPelanggan
        {
            get { return _isPreviousButtonEnabledPelanggan; }
            set
            {
                _isPreviousButtonEnabledPelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledPelanggan;

        public bool IsNextButtonEnabledPelanggan
        {
            get { return _isNextButtonEnabledPelanggan; }
            set
            {
                _isNextButtonEnabledPelanggan = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecordPelanggan;

        public long TotalRecordPelanggan
        {
            get { return _totalRecordPelanggan; }
            set
            {
                _totalRecordPelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisiblePelanggan;

        public bool IsLeftPageNumberFillerVisiblePelanggan
        {
            get { return _isLeftPageNumberFillerVisiblePelanggan; }
            set
            {
                _isLeftPageNumberFillerVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisiblePelanggan;

        public bool IsRightPageNumberFillerVisiblePelanggan
        {
            get { return _isRightPageNumberFillerVisiblePelanggan; }
            set
            {
                _isRightPageNumberFillerVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActivePelanggan;

        public bool IsLeftPageNumberActivePelanggan
        {
            get { return _isLeftPageNumberActivePelanggan; }
            set
            {
                _isLeftPageNumberActivePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActivePelanggan;

        public bool IsRightPageNumberActivePelanggan
        {
            get { return _isRightPageNumberActivePelanggan; }
            set
            {
                _isRightPageNumberActivePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisiblePelanggan;

        public bool IsMiddlePageNumberVisiblePelanggan
        {
            get { return _isMiddlePageNumberVisiblePelanggan; }
            set
            {
                _isMiddlePageNumberVisiblePelanggan = value;
                OnPropertyChanged();
            }
        }

        #endregion

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
                var listOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in listOption)
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

        private string _alasanForm;

        public string AlasanForm
        {
            get => _alasanForm;
            set
            {
                _alasanForm = value;
                OnPropertyChanged();
            }
        }

        private decimal? _biayaForm = 0;

        public decimal? BiayaForm
        {
            get => _biayaForm;
            set
            {
                _biayaForm = value;
                OnPropertyChanged();
            }
        }

        #region enable/disable filter

        private bool _isStatusPermohonanChecked;
        public bool IsStatusPermohonanChecked
        {
            get { return _isStatusPermohonanChecked; }
            set
            {
                _isStatusPermohonanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isRayonChecked;
        public bool IsRayonChecked
        {
            get { return _isRayonChecked; }
            set
            {
                _isRayonChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isCabangChecked;
        public bool IsCabangChecked
        {
            get { return _isCabangChecked; }
            set
            {
                _isCabangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNomorRegisterChecked;
        public bool IsNomorRegisterChecked
        {
            get { return _isNomorRegisterChecked; }
            set
            {
                _isNomorRegisterChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNoSambunganChecked;
        public bool IsNoSambunganChecked
        {
            get { return _isNoSambunganChecked; }
            set
            {
                _isNoSambunganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set
            {
                _isNamaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAlamatChecked;
        public bool IsAlamatChecked
        {
            get { return _isAlamatChecked; }
            set
            {
                _isAlamatChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTanggalInputChecked;
        public bool IsTanggalInputChecked
        {
            get { return _isTanggalInputChecked; }
            set
            {
                _isTanggalInputChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUserInputChecked;
        public bool IsUserInputChecked
        {
            get { return _isUserInputChecked; }
            set
            {
                _isUserInputChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter variable

        private string _filterStatusPermohonan;
        public string FilterStatusPermohonan
        {
            get { return _filterStatusPermohonan; }
            set
            {
                _filterStatusPermohonan = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _filterGolongan;
        public MasterGolonganDto FilterGolongan
        {
            get { return _filterGolongan; }
            set
            {
                _filterGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _filterRayon;
        public MasterRayonDto FilterRayon
        {
            get { return _filterRayon; }
            set
            {
                _filterRayon = value;
                OnPropertyChanged();
            }
        }

        private MasterWilayahDto _filterWilayah;
        public MasterWilayahDto FilterWilayah
        {
            get { return _filterWilayah; }
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _filterKelurahan;
        public MasterKelurahanDto FilterKelurahan
        {
            get { return _filterKelurahan; }
            set
            {
                _filterKelurahan = value;
                OnPropertyChanged();
            }
        }

        private MasterCabangDto _filterCabang;
        public MasterCabangDto FilterCabang
        {
            get { return _filterCabang; }
            set
            {
                _filterCabang = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorRegister;
        public string FilterNomorRegister
        {
            get { return _filterNomorRegister; }
            set
            {
                _filterNomorRegister = value;
                OnPropertyChanged();
            }
        }

        private string _filterNoSambungan;
        public string FilterNoSambungan
        {
            get { return _filterNoSambungan; }
            set
            {
                _filterNoSambungan = value;
                OnPropertyChanged();
            }
        }

        private string _filterNama;
        public string FilterNama
        {
            get { return _filterNama; }
            set
            {
                _filterNama = value;
                OnPropertyChanged();
            }
        }

        private string _filterAlamat;
        public string FilterAlamat
        {
            get { return _filterAlamat; }
            set
            {
                _filterAlamat = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalInputAwal;
        public DateTime? FilterTanggalInputAwal
        {
            get { return _filterTanggalInputAwal; }
            set
            {
                _filterTanggalInputAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalInputAkhir;
        public DateTime? FilterTanggalInputAkhir
        {
            get { return _filterTanggalInputAkhir; }
            set
            {
                _filterTanggalInputAkhir = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterUserInput;
        public MasterUserDto FilterUserInput
        {
            get { return _filterUserInput; }
            set
            {
                _filterUserInput = value;
                OnPropertyChanged();
            }
        }

        #endregion


        private Uri _fotoBukti1Uri;
        public Uri FotoBukti1Uri
        {
            get => _fotoBukti1Uri;
            set
            {
                _fotoBukti1Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti1;
        public bool IsNewFotoBukti1
        {
            get => _isNewFotoBukti1;
            set
            {
                _isNewFotoBukti1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti1FormChecked;
        public bool IsFotoBukti1FormChecked
        {
            get => _isFotoBukti1FormChecked;
            set
            {
                _isFotoBukti1FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti2Uri;
        public Uri FotoBukti2Uri
        {
            get => _fotoBukti2Uri;
            set
            {
                _fotoBukti2Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti2;
        public bool IsNewFotoBukti2
        {
            get => _isNewFotoBukti2;
            set
            {
                _isNewFotoBukti2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti2FormChecked;
        public bool IsFotoBukti2FormChecked
        {
            get => _isFotoBukti2FormChecked;
            set
            {
                _isFotoBukti2FormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti3Uri;
        public Uri FotoBukti3Uri
        {
            get => _fotoBukti3Uri;
            set
            {
                _fotoBukti3Uri = value;
                OnPropertyChanged();
            }
        }

        private bool _isNewFotoBukti3;
        public bool IsNewFotoBukti3
        {
            get => _isNewFotoBukti3;
            set
            {
                _isNewFotoBukti3 = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti3FormChecked;
        public bool IsFotoBukti3FormChecked
        {
            get => _isFotoBukti3FormChecked;
            set
            {
                _isFotoBukti3FormChecked = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _previewFile;
        public BitmapImage PreviewFile
        {
            get => _previewFile;
            set
            {
                _previewFile = value;
                OnPropertyChanged();
            }
        }

    }
}
