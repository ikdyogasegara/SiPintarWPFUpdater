using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Hublang.Verifikasi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang
{
    public class VerifikasiViewModel : ViewModelBase
    {
        private readonly bool _isTest;
        public VerifikasiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            OnLoadCommand = new OnLoadCommand(this, restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnOpenVerifikasiConfirmationCommand = new OnOpenVerifikasiConfirmationCommand(this, _isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, _isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, _isTest);
            OnSubmitVerifikasiCommand = new OnSubmitVerifikasiCommand(this, restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenVerifikasiConfirmationCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSubmitVerifikasiCommand { get; }
        public ICommand OnResetFilterCommand { get; }

        private bool _isLoadingJenis;
        public bool IsLoadingJenis
        {
            get => _isLoadingJenis;
            set { _isLoadingJenis = value; OnPropertyChanged(); }
        }

        private bool _isEmptyJenis = true;
        public bool IsEmptyJenis
        {
            get => _isEmptyJenis;
            set { _isEmptyJenis = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ListPermohonanWpf> _jenisList = new ObservableCollection<ListPermohonanWpf>();
        public ObservableCollection<ListPermohonanWpf> JenisList
        {
            get => _jenisList;
            set { _jenisList = value; OnPropertyChanged(); }
        }

        private ListPermohonanWpf _selectedJenis;
        public ListPermohonanWpf SelectedJenis
        {
            get => _selectedJenis;
            set
            {
                _selectedJenis = value;
                OnPropertyChanged();
            }
        }

        private bool _isShowListData;
        public bool IsShowListData
        {
            get => _isShowListData;
            set { _isShowListData = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
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

        private bool _isPelangganAir;
        public bool IsPelangganAir
        {
            get => _isPelangganAir;
            set
            {
                _isPelangganAir = value;
                OnPropertyChanged();

                if (value)
                {
                    JenisPelanggan = "Pelanggan Air";
                    EndPoint = "permohonan-pelanggan-air";
                    EndPointVerifikasi = "permohonan-pelanggan-air-verifikasi";
                }
            }
        }

        private bool _isPelangganLimbah;
        public bool IsPelangganLimbah
        {
            get => _isPelangganLimbah;
            set
            {
                _isPelangganLimbah = value;
                OnPropertyChanged();
                if (value)
                {
                    JenisPelanggan = "Pelanggan Limbah";
                    EndPoint = "permohonan-pelanggan-limbah";
                    EndPointVerifikasi = "permohonan-pelanggan-limbah-verifikasi";
                }
            }
        }

        private bool _isPelangganLltt;
        public bool IsPelangganLltt
        {
            get => _isPelangganLltt;
            set
            {
                _isPelangganLltt = value;
                OnPropertyChanged();
                if (value)
                {
                    JenisPelanggan = "Pelanggan L2T2";
                    EndPoint = "permohonan-pelanggan-lltt";
                    EndPointVerifikasi = "permohonan-pelanggan-lltt-verifikasi";
                }
            }
        }

        private bool _isNonPelanggan;
        public bool IsNonPelanggan
        {
            get => _isNonPelanggan;
            set
            {
                _isNonPelanggan = value;
                OnPropertyChanged();
                if (value)
                {
                    JenisPelanggan = "Non Pelanggan";
                    EndPoint = "permohonan-non-pelanggan";
                    EndPointVerifikasi = "permohonan-non-pelanggan-verifikasi";
                }
            }
        }

        private string _jenisPelanggan = "Pelanggan Air";
        public string JenisPelanggan
        {
            get => _jenisPelanggan;
            set { _jenisPelanggan = value; OnPropertyChanged(); }
        }

        private string _endPoint = "permohonan-pelanggan-air";
        public string EndPoint
        {
            get => _endPoint;
            set { _endPoint = value; OnPropertyChanged(); }
        }

        private string _endPointVerifikasi = "permohonan-pelanggan-air-verifikasi";
        public string EndPointVerifikasi
        {
            get => _endPointVerifikasi;
            set { _endPointVerifikasi = value; OnPropertyChanged(); }
        }

        private string _labelPelangganAir = "Pelanggan Air";
        public string LabelPelangganAir
        {
            get => _labelPelangganAir;
            set { _labelPelangganAir = value; OnPropertyChanged(); }
        }

        private string _labelPelangganLimbah = "Pelanggan Limbah";
        public string LabelPelangganLimbah
        {
            get => _labelPelangganLimbah;
            set { _labelPelangganLimbah = value; OnPropertyChanged(); }
        }

        private string _labelPelangganLltt = "Pelanggan L2T2";
        public string LabelPelangganLltt
        {
            get => _labelPelangganLltt;
            set { _labelPelangganLltt = value; OnPropertyChanged(); }
        }

        private string _labelNonPelanggan = "Non Pelanggan";
        public string LabelNonPelanggan
        {
            get => _labelNonPelanggan;
            set { _labelNonPelanggan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PermohonanWpf> _dataList = new ObservableCollection<PermohonanWpf>();
        public ObservableCollection<PermohonanWpf> DataList
        {
            get => _dataList;
            set { _dataList = value; OnPropertyChanged(); }
        }

        private PermohonanWpf _selectedData;
        public PermohonanWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
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

        private bool _isTarifLimbahChecked;
        public bool IsTarifLimbahChecked
        {
            get { return _isTarifLimbahChecked; }
            set
            {
                _isTarifLimbahChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTarifLlttChecked;
        public bool IsTarifLlttChecked
        {
            get { return _isTarifLlttChecked; }
            set
            {
                _isTarifLlttChecked = value;
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

        private bool _isUserBeritaAcaraChecked;
        public bool IsUserBeritaAcaraChecked
        {
            get { return _isUserBeritaAcaraChecked; }
            set
            {
                _isUserBeritaAcaraChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAlamatMapFormChecked;
        public bool IsAlamatMapFormChecked
        {
            get { return _isAlamatMapFormChecked; }
            set
            {
                _isAlamatMapFormChecked = value;
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

        private MasterTarifLimbahDto _filterTarifLimbah;
        public MasterTarifLimbahDto FilterTarifLimbah
        {
            get { return _filterTarifLimbah; }
            set
            {
                _filterTarifLimbah = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLlttDto _filterTarifLltt;
        public MasterTarifLlttDto FilterTarifLltt
        {
            get { return _filterTarifLltt; }
            set
            {
                _filterTarifLltt = value;
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

        private MasterUserDto _filterUserBeritaAcara;
        public MasterUserDto FilterUserBeritaAcara
        {
            get { return _filterUserBeritaAcara; }
            set
            {
                _filterUserBeritaAcara = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter data list
        private ObservableCollection<MasterGolonganDto> _golonganList = new ObservableCollection<MasterGolonganDto>();
        public ObservableCollection<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLimbahDto> _tarifLimbahList = new ObservableCollection<MasterTarifLimbahDto>();
        public ObservableCollection<MasterTarifLimbahDto> TarifLimbahList
        {
            get { return _tarifLimbahList; }
            set
            {
                _tarifLimbahList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTarifLlttDto> _tarifLlttList = new ObservableCollection<MasterTarifLlttDto>();
        public ObservableCollection<MasterTarifLlttDto> TarifLlttList
        {
            get { return _tarifLlttList; }
            set
            {
                _tarifLlttList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAreaDto> _areaList = new ObservableCollection<MasterAreaDto>();
        public ObservableCollection<MasterAreaDto> AreaList
        {
            get { return _areaList; }
            set
            {
                _areaList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new ObservableCollection<MasterWilayahDto>();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKecamatanDto> _kecamatanList = new ObservableCollection<MasterKecamatanDto>();
        public ObservableCollection<MasterKecamatanDto> KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKelurahanDto> _kelurahanList = new ObservableCollection<MasterKelurahanDto>();
        public ObservableCollection<MasterKelurahanDto> KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterCabangDto> _cabangList = new ObservableCollection<MasterCabangDto>();
        public ObservableCollection<MasterCabangDto> CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _userList = new ObservableCollection<MasterUserDto>();
        public ObservableCollection<MasterUserDto> UserList
        {
            get { return _userList; }
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        #endregion

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

        #region prev next page data permohonan
        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(key: 20, "20");
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
