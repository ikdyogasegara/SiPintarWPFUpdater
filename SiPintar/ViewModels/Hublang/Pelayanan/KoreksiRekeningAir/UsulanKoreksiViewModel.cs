using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Commands.Hublang.Pelayanan.KoreksiRekeningAir.UsulanKoreksi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir
{
    public class UsulanKoreksiViewModel : ViewModelBase
    {
        private readonly bool _isTest;

        public UsulanKoreksiViewModel(KoreksiRekeningAirViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            Parent = parent;
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _isTest);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this, _isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, _isTest);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi, _isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, _isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, _isTest);
            OnCariPermohonanCommand = new OnCariPermohonanCommand(this, restApi, _isTest);
            GetPiutangListCommand = new GetPiutangListCommand(this, restApi, _isTest);
            OnOpenVerifikasiCommand = new OnOpenVerifikasiCommand(this, restApi, _isTest);
            OnSubmitVerifikasiCommand = new OnSubmitVerifikasiCommand(this, restApi, _isTest);

            OnBrowseFileBuktiCommand = new OnBrowseFileBuktiCommand(this, _isTest);
            OnOpenImageCommand = new OnOpenImageCommand(this, _isTest);
            OnCetakCommandPelangganAirHublang = new OnCetakCommand(restApi, "hublang", "permohonan-koreksi-rekening-cetak", "Cetak Usulan Koreksi Rekening", ErrorHandlingCetak, _isTest);
            OnCetakCommandPelangganAirBilling = new OnCetakCommand(restApi, "billing", "permohonan-koreksi-rekening-cetak", "Cetak Usulan Koreksi Rekening", ErrorHandlingCetak, _isTest);
        }

        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnCariPermohonanCommand { get; }
        public ICommand GetPiutangListCommand { get; }
        public ICommand OnOpenVerifikasiCommand { get; }
        public ICommand OnSubmitVerifikasiCommand { get; }
        public ICommand OnBrowseFileBuktiCommand { get; }
        public ICommand OnOpenImageCommand { get; }
        public ICommand OnCetakCommandPelangganAirHublang { get; }
        public ICommand OnCetakCommandPelangganAirBilling { get; }

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

        private string _noPermohonanForm;
        public string NoPermohonanForm
        {
            get => _noPermohonanForm;
            set
            {
                _noPermohonanForm = value;
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

        private ObservableCollection<PermohonanWpf> _permohonanAirList = new ObservableCollection<PermohonanWpf>();

        public ObservableCollection<PermohonanWpf> PermohonanAirList
        {
            get => _permohonanAirList;
            set
            {
                _permohonanAirList = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedTempPermohonanAir;

        public PermohonanWpf SelectedTempPermohonanAir
        {
            get => _selectedTempPermohonanAir;
            set
            {
                _selectedTempPermohonanAir = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedPermohonanAir;

        public PermohonanWpf SelectedPermohonanAir
        {
            get => _selectedPermohonanAir;
            set
            {
                _selectedPermohonanAir = value;
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

        private List<StatusVerifikasiKoreksi> _statusUsulanList = new List<StatusVerifikasiKoreksi>();

        public List<StatusVerifikasiKoreksi> StatusUsulanList
        {
            get => _statusUsulanList;
            set
            {
                _statusUsulanList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirPiutangWpf _selectedPiutangAir;

        public RekeningAirPiutangWpf SelectedPiutangAir
        {
            get => _selectedPiutangAir;
            set
            {
                _selectedPiutangAir = value;
                OnPropertyChanged();
            }
        }

        private bool _isPakaiChecked = true;

        public bool IsPakaiChecked
        {
            get => _isPakaiChecked;
            set
            {
                _isPakaiChecked = value;

                if (value)
                {
                    IsStanLaluChecked = false;
                    IsStanKiniChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanLaluChecked;

        public bool IsStanLaluChecked
        {
            get => _isStanLaluChecked;
            set
            {
                _isStanLaluChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanKiniChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanKiniChecked;

        public bool IsStanKiniChecked
        {
            get => _isStanKiniChecked;
            set
            {
                _isStanKiniChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanLaluChecked = false;
                    IsStanAngkatChecked = false;
                }

                OnPropertyChanged();
            }
        }

        private bool _isStanAngkatChecked;

        public bool IsStanAngkatChecked
        {
            get => _isStanAngkatChecked;
            set
            {
                _isStanAngkatChecked = value;

                if (value)
                {
                    IsPakaiChecked = false;
                    IsStanLaluChecked = false;
                    IsStanKiniChecked = false;
                }

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

        private ObservableCollection<PermohonanKoreksiRekeningWpf> _dataList = new ObservableCollection<PermohonanKoreksiRekeningWpf>();

        public ObservableCollection<PermohonanKoreksiRekeningWpf> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private PermohonanKoreksiRekeningWpf _selectedData = new PermohonanKoreksiRekeningWpf();

        public PermohonanKoreksiRekeningWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsDataSelected = _selectedData != null;
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

        private bool _isEdit;

        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
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

        private bool _isDetail;

        public bool IsDetail
        {
            get => _isDetail;
            set
            {
                _isDetail = value;
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

        private int _statusVerifikasiForm;
        public int StatusVerifikasiForm
        {
            get => _statusVerifikasiForm;
            set
            {
                _statusVerifikasiForm = value;
                OnPropertyChanged();
            }
        }

        private string _alasanPenolakanForm;

        public string AlasanPenolakanForm
        {
            get { return _alasanPenolakanForm; }
            set
            {
                _alasanPenolakanForm = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDto _hitungKoreksi = new RekeningAirDto();

        public RekeningAirDto HitungKoreksi
        {
            get { return _hitungKoreksi; }
            set
            {
                _hitungKoreksi = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirPiutangWpf _koreksiForm = new RekeningAirPiutangWpf();

        public RekeningAirPiutangWpf KoreksiForm
        {
            get { return _koreksiForm; }
            set
            {
                _koreksiForm = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDetailDto _hitungKoreksiDetail = new RekeningAirDetailDto();

        public RekeningAirDetailDto HitungKoreksiDetail
        {
            get { return _hitungKoreksiDetail; }
            set
            {
                _hitungKoreksiDetail = value;
                OnPropertyChanged();
            }
        }

        #region enable/disable filter

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

        #endregion

        #region filter variable

        private StatusVerifikasiKoreksi _filterStatusPermohonan;

        public StatusVerifikasiKoreksi FilterStatusPermohonan
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

        #endregion

        #region foto dari permohonan
        private Uri _fotoBukti1PermohonanUri;
        public Uri FotoBukti1PermohonanUri
        {
            get => _fotoBukti1PermohonanUri;
            set
            {
                _fotoBukti1PermohonanUri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti1PermohonanFormChecked;
        public bool IsFotoBukti1PermohonanFormChecked
        {
            get => _isFotoBukti1PermohonanFormChecked;
            set
            {
                _isFotoBukti1PermohonanFormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti2PermohonanUri;
        public Uri FotoBukti2PermohonanUri
        {
            get => _fotoBukti2PermohonanUri;
            set
            {
                _fotoBukti2PermohonanUri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti2PermohonanFormChecked;
        public bool IsFotoBukti2PermohonanFormChecked
        {
            get => _isFotoBukti2PermohonanFormChecked;
            set
            {
                _isFotoBukti2PermohonanFormChecked = value;
                OnPropertyChanged();
            }
        }

        private Uri _fotoBukti3PermohonanUri;
        public Uri FotoBukti3PermohonanUri
        {
            get => _fotoBukti3PermohonanUri;
            set
            {
                _fotoBukti3PermohonanUri = value;
                OnPropertyChanged();
            }
        }

        private bool _isFotoBukti3PermohonanFormChecked;
        public bool IsFotoBukti3PermohonanFormChecked
        {
            get => _isFotoBukti3PermohonanFormChecked;
            set
            {
                _isFotoBukti3PermohonanFormChecked = value;
                OnPropertyChanged();
            }
        }
        #endregion

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
    }

    [ExcludeFromCodeCoverage]
    public class StatusVerifikasiKoreksi
    {
        public string Nama { get; set; }
    }
}
