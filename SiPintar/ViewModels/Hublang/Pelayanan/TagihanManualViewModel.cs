using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.TagihanManual;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Views.Global.Dialog;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class TagihanManualViewModel : ViewModelBase
    {
        private readonly bool _isTest;
        private readonly IRestApiClientModel _restApi;

        public TagihanManualViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _isTest = isTest;
            _restApi = restApi;
            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenDetailBiayaCommand = new OnOpenDetailBiayaCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnLoadDetailJenisNonairCommand = new OnLoadDetailJenisNonairCommand(this, restApi, isTest);
            OnLoadPiutangNonairCommand = new OnLoadPiutangNonairCommand(this, restApi, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenDetailBiayaCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnLoadDetailJenisNonairCommand { get; }
        public ICommand OnLoadPiutangNonairCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }

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

        private ObservableCollection<RekeningNonAirWpf> _dataList = new ObservableCollection<RekeningNonAirWpf>();
        public ObservableCollection<RekeningNonAirWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private RekeningNonAirWpf _selectedData;
        public RekeningNonAirWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private RekeningNonAirDto _formData;
        public RekeningNonAirDto FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        private MasterKelurahanDto _kelurahanForm;
        public MasterKelurahanDto KelurahanForm
        {
            get { return _kelurahanForm; }
            set
            {
                _kelurahanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _rayonForm;
        public MasterRayonDto RayonForm
        {
            get { return _rayonForm; }
            set
            {
                _rayonForm = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _golonganForm;
        public MasterGolonganDto GolonganForm
        {
            get { return _golonganForm; }
            set
            {
                _golonganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _tarifLimbahForm;
        public MasterTarifLimbahDto TarifLimbahForm
        {
            get { return _tarifLimbahForm; }
            set
            {
                _tarifLimbahForm = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLlttDto _tarifLlttForm;
        public MasterTarifLlttDto TarifLlttForm
        {
            get { return _tarifLlttForm; }
            set
            {
                _tarifLlttForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningNonAirDetailDto> _formDataDetail;
        public ObservableCollection<RekeningNonAirDetailDto> FormDataDetail
        {
            get { return _formDataDetail; }
            set
            {
                _formDataDetail = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningNonAirDto> _piutangNonAirList;
        public ObservableCollection<RekeningNonAirDto> PiutangNonAirList
        {
            get { return _piutangNonAirList; }
            set
            {
                _piutangNonAirList = value;
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

        private bool _isStatusChecked = true;
        public bool IsStatusChecked
        {
            get { return _isStatusChecked; }
            set
            {
                _isStatusChecked = value;
                if (!value)
                {
                    FilterStatus = null;
                }
                OnPropertyChanged();
            }
        }

        private string _filterStatus = "Belum Lunas";
        public string FilterStatus
        {
            get { return _filterStatus; }
            set
            {
                _filterStatus = value;
                OnPropertyChanged();
            }
        }


        private bool _isJenisTipePelangganChecked;
        public bool IsJenisTipePelangganChecked
        {
            get { return _isJenisTipePelangganChecked; }
            set
            {
                _isJenisTipePelangganChecked = value;
                if (!value)
                {
                    FilterJenisTipePelanggan = null;
                }
                OnPropertyChanged();
            }
        }

        private JenisPelangganDto _filterJenisTipePelanggan;
        public JenisPelangganDto FilterJenisTipePelanggan
        {
            get { return _filterJenisTipePelanggan; }
            set
            {
                _filterJenisTipePelanggan = value;
                OnPropertyChanged();
            }
        }

        private bool _isJenisNonAirChecked;
        public bool IsJenisNonAirChecked
        {
            get { return _isJenisNonAirChecked; }
            set
            {
                _isJenisNonAirChecked = value;
                if (!value)
                {
                    FilterJenisNonAir = null;
                }
                OnPropertyChanged();
            }
        }

        private MasterJenisNonAirDto _filterJenisNonAir;
        public MasterJenisNonAirDto FilterJenisNonAir
        {
            get { return _filterJenisNonAir; }
            set
            {
                _filterJenisNonAir = value;
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
                if (!value)
                {
                    FilterRayon = null;
                }
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

        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                if (!value)
                {
                    FilterWilayah = null;
                }
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

        private bool _isKelurahanChecked;
        public bool IsKelurahanChecked
        {
            get { return _isKelurahanChecked; }
            set
            {
                _isKelurahanChecked = value;
                if (!value)
                {
                    FilterKelurahan = null;
                }
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

        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                if (!value)
                {
                    FilterGolongan = null;
                }
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

        private bool _isTarifLimbahChecked;
        public bool IsTarifLimbahChecked
        {
            get { return _isTarifLimbahChecked; }
            set
            {
                _isTarifLimbahChecked = value;
                if (!value)
                {
                    FilterTarifLimbah = null;
                }
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

        private bool _isTarifLlttChecked;
        public bool IsTarifLlttChecked
        {
            get { return _isTarifLlttChecked; }
            set
            {
                _isTarifLlttChecked = value;
                if (!value)
                {
                    FilterTarifLltt = null;
                }
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

        private dynamic _selectedPelanggan;
        public dynamic SelectedPelanggan
        {
            get => _selectedPelanggan;
            set
            {
                _selectedPelanggan = value;
                OnPropertyChanged();
            }
        }


        private bool _isNomorNonAirChecked;
        public bool IsNomorNonAirChecked
        {
            get { return _isNomorNonAirChecked; }
            set
            {
                _isNomorNonAirChecked = value;
                if (!value)
                {
                    FilterNomorNonAir = null;
                }
                OnPropertyChanged();
            }
        }

        private string _filterNomorNonAir;
        public string FilterNomorNonAir
        {
            get { return _filterNomorNonAir; }
            set
            {
                _filterNomorNonAir = value;
                OnPropertyChanged();
            }
        }


        private bool _isNomorPelangganChecked;
        public bool IsNomorPelangganChecked
        {
            get { return _isNomorPelangganChecked; }
            set
            {
                _isNomorPelangganChecked = value;
                if (!value)
                {
                    FilterNomorPelanggan = null;
                }
                OnPropertyChanged();
            }
        }

        private string _filterNomorPelanggan;
        public string FilterNomorPelanggan
        {
            get { return _filterNomorPelanggan; }
            set
            {
                _filterNomorPelanggan = value;
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
                if (!value)
                {
                    FilterNama = null;
                }
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


        private bool _isAlamatChecked;
        public bool IsAlamatChecked
        {
            get { return _isAlamatChecked; }
            set
            {
                _isAlamatChecked = value;
                if (!value)
                {
                    FilterAlamat = null;
                }
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

        private ObservableCollection<JenisPelangganDto> _jenisPelangganKategori;
        public ObservableCollection<JenisPelangganDto> JenisPelangganKategori
        {
            get { return _jenisPelangganKategori; }
            set
            {
                _jenisPelangganKategori = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<JenisPelangganDto> _jenisPelangganList;
        public ObservableCollection<JenisPelangganDto> JenisPelangganList
        {
            get { return _jenisPelangganList; }
            set
            {
                _jenisPelangganList = value;
                OnPropertyChanged();
            }
        }

        private JenisPelangganDto _jenisPelangganSelected;
        public JenisPelangganDto JenisPelangganSelected
        {
            get { return _jenisPelangganSelected; }
            set
            {
                _jenisPelangganSelected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisNonAirDto> _jenisNonAirList;
        public ObservableCollection<MasterJenisNonAirDto> JenisNonAirList
        {
            get { return _jenisNonAirList; }
            set
            {
                _jenisNonAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisNonAirDetailDto> _jenisNonAirDetailList;
        public ObservableCollection<MasterJenisNonAirDetailDto> JenisNonAirDetailList
        {
            get { return _jenisNonAirDetailList; }
            set
            {
                _jenisNonAirDetailList = value;
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

        private ObservableCollection<MasterTarifLimbahDto> _tarifLimbahList;
        public ObservableCollection<MasterTarifLimbahDto> TarifLimbahList
        {
            get { return _tarifLimbahList; }
            set
            {
                _tarifLimbahList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterTarifLlttDto> _tarifLlttList;
        public ObservableCollection<MasterTarifLlttDto> TarifLlttList
        {
            get { return _tarifLlttList; }
            set
            {
                _tarifLlttList = value;
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


        private int? _selectedIdJenisNonAir;
        public int? SelectedIdJenisNonAir
        {
            get { return _selectedIdJenisNonAir; }
            set
            {
                _selectedIdJenisNonAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _total = 0;
        public decimal Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        #region Pagination

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

        public bool SetSelectedPelanggan(dynamic param)
        {
            SelectedPelanggan = param;
            return true;
        }

        public bool TriggerOpenAddForm()
        {
            OnOpenAddFormCommand.Execute(true);
            return true;
        }

        public async Task OpenSelectPelangganAsync()
        {
            _ = await DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "HublangRootDialog", GetInstanceSelectPelanggan);
        }

        [ExcludeFromCodeCoverage]
        private object GetInstanceSelectPelanggan() => new PilihPelangganDialog(_restApi, SetSelectedPelanggan, TriggerOpenAddForm);
    }
}
