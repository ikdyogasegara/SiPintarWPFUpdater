using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Supervisi.RekeningLimbah;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class RekeningLimbahViewModel : ViewModelBase
    {
        public RekeningLimbahViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnOpenPublishSemuaConfirmationCommand = new OnOpenPublishSemuaConfirmationCommand(this);
            OnOpenPublishSemuaPasswordCommand = new OnOpenPublishSemuaPasswordCommand(this);
            OnSubmitPublishSemuaCommand = new OnSubmitPublishSemuaCommand(this, restApi);
            OnOpenTerbitkanPelangganCommand = new OnOpenTerbitkanPelangganCommand(this, restApi);
            OnRefreshTerbitkanPelangganCommand = new OnRefreshTerbitkanPelangganCommand(this, restApi);
            OnSubmitTerbitkanPelangganCommand = new OnSubmitTerbitkanPelangganCommand(this, restApi);
            OnOpenKoreksiRekeningCommand = new OnOpenKoreksiRekeningCommand(this);
            OnSubmitKoreksiRekeningCommand = new OnSubmitKoreksiRekeningCommand(this, restApi);
            OnOpenPublishSatuanCommand = new OnOpenPublishSatuanCommand(this);
            OnSubmitPublishSatuanCommand = new OnSubmitPublishSatuanCommand(this, restApi);
            OnOpenUnpublishSatuanCommand = new OnOpenUnpublishSatuanCommand(this);
            OnSubmitUnpublishSatuanCommand = new OnSubmitUnpublishSatuanCommand(this, restApi);
            OnOpenUploadUlangCommand = new OnOpenUploadUlangCommand(this);
            OnSubmitUploadUlangCommand = new OnSubmitUploadUlangCommand(this, restApi);
            OnOpenHapusRekeningCommand = new OnOpenHapusRekeningCommand(this);
            OnSubmitHapusRekeningCommand = new OnSubmitHapusRekeningCommand(this, restApi);
            OnOpenPerbaruiDataRekeningCommand = new OnOpenPerbaruiDataRekeningCommand(this);
            OnSubmitPerbaruiDataRekeningCommand = new OnSubmitPerbaruiDataRekeningCommand(this, restApi);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnOpenHitungUlangCommand = new OnOpenHitungUlangCommand(this);
            OnSubmitHitungUlangCommand = new OnSubmitHitungUlangCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnOpenPublishSemuaConfirmationCommand { get; }
        public ICommand OnOpenPublishSemuaPasswordCommand { get; }
        public ICommand OnSubmitPublishSemuaCommand { get; }
        public ICommand OnOpenTerbitkanPelangganCommand { get; }
        public ICommand OnRefreshTerbitkanPelangganCommand { get; }
        public ICommand OnSubmitTerbitkanPelangganCommand { get; }
        public ICommand OnOpenKoreksiRekeningCommand { get; }
        public ICommand OnSubmitKoreksiRekeningCommand { get; }
        public ICommand OnOpenPublishSatuanCommand { get; }
        public ICommand OnSubmitPublishSatuanCommand { get; }
        public ICommand OnOpenUnpublishSatuanCommand { get; }
        public ICommand OnSubmitUnpublishSatuanCommand { get; }
        public ICommand OnOpenUploadUlangCommand { get; }
        public ICommand OnSubmitUploadUlangCommand { get; }
        public ICommand OnOpenHapusRekeningCommand { get; }
        public ICommand OnSubmitHapusRekeningCommand { get; }
        public ICommand OnOpenPerbaruiDataRekeningCommand { get; }
        public ICommand OnSubmitPerbaruiDataRekeningCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnOpenHitungUlangCommand { get; }
        public ICommand OnSubmitHitungUlangCommand { get; }
        public ICommand OnExportCommand { get; }


        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(10, "10");
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

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isPublished;
        public bool IsPublished
        {
            get => _isPublished;
            set { _isPublished = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
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

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEmptyDialog = true;
        public bool IsEmptyDialog
        {
            get => _isEmptyDialog;
            set { _isEmptyDialog = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RekeningLimbahDto> _rekeningList = new ObservableCollection<RekeningLimbahDto>();
        public ObservableCollection<RekeningLimbahDto> RekeningList
        {
            get => _rekeningList;
            set { _rekeningList = value; IsEmpty = _rekeningList.Count == 0; OnPropertyChanged(); }
        }

        private RekeningLimbahDto _selectedData;
        public RekeningLimbahDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                IsPublished = _selectedData != null && _selectedData.FlagPublish != null && (bool)_selectedData.FlagPublish;
            }
        }

        // Filter

        private RekeningLimbahDto _rekeningFilter = new RekeningLimbahDto { FlagHapus = null };
        public RekeningLimbahDto RekeningFilter
        {
            get { return _rekeningFilter; }
            set
            {
                _rekeningFilter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterStatusDto> _statusList;
        public ObservableCollection<MasterStatusDto> StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
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

        private ObservableCollection<MasterKolektifDto> _kolektifList;
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _kasirList;
        public ObservableCollection<MasterUserDto> KasirList
        {
            get { return _kasirList; }
            set
            {
                _kasirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterLoketDto> _loketList;
        public ObservableCollection<MasterLoketDto> LoketList
        {
            get { return _loketList; }
            set
            {
                _loketList = value;
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

        private ObservableCollection<MasterCabangDto> _cabangList;
        public ObservableCollection<MasterCabangDto> CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<object> _filterLainnyaList;
        public ObservableCollection<object> FilterLainnyaList
        {
            get { return _filterLainnyaList; }
            set
            {
                _filterLainnyaList = value;
                OnPropertyChanged();
            }
        }


        private bool _isStatusChecked;
        public bool IsStatusChecked
        {
            get { return _isStatusChecked; }
            set
            {
                _isStatusChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.IdStatus = null;
                    RekeningFilter = temp;
                }
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
                    var temp = RekeningFilter;
                    temp.Nama = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoSambChecked;
        public bool IsNoSambChecked
        {
            get { return _isNoSambChecked; }
            set
            {
                _isNoSambChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.NoSamb = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoLimbahChecked;
        public bool IsNoLimbahChecked
        {
            get { return _isNoLimbahChecked; }
            set
            {
                _isNoLimbahChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.NomorLimbah = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTarifChecked;
        public bool IsTarifChecked
        {
            get { return _isTarifChecked; }
            set
            {
                _isTarifChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.IdTarifLimbah = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKolektifChecked;
        public bool IsKolektifChecked
        {
            get { return _isKolektifChecked; }
            set
            {
                _isKolektifChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.IdKolektif = null;
                    RekeningFilter = temp;
                }
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
                    var temp = RekeningFilter;
                    temp.Alamat = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTglBayarChecked;
        public bool IsTglBayarChecked
        {
            get { return _isTglBayarChecked; }
            set
            {
                _isTglBayarChecked = value;
                if (!value)
                {
                    TglBayarAwalFilter = null;
                    TglBayarAkhirFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTglUploadChecked;
        public bool IsTglUploadChecked
        {
            get { return _isTglUploadChecked; }
            set
            {
                _isTglUploadChecked = value;
                if (!value)
                {
                    TglUploadAwalFilter = null;
                    TglUploadAkhirFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTglPublishChecked;
        public bool IsTglPublishChecked
        {
            get { return _isTglPublishChecked; }
            set
            {
                _isTglPublishChecked = value;
                if (!value)
                {
                    TglPublishAwalFilter = null;
                    TglPublishAkhirFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBiayaChecked;
        public bool IsBiayaChecked
        {
            get { return _isBiayaChecked; }
            set
            {
                _isBiayaChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.Biaya = null;
                    RekeningFilter = temp;

                    BiayaAwalFilter = null;
                    BiayaAkhirFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKasirChecked;
        public bool IsKasirChecked
        {
            get { return _isKasirChecked; }
            set
            {
                _isKasirChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.NamaUser = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isLoketBayarChecked;
        public bool IsLoketBayarChecked
        {
            get { return _isLoketBayarChecked; }
            set
            {
                _isLoketBayarChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.NamaLoket = null;
                    RekeningFilter = temp;
                }
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
                    var temp = RekeningFilter;
                    temp.IdWilayah = null;
                    RekeningFilter = temp;
                }
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
                    var temp = RekeningFilter;
                    temp.IdKelurahan = null;
                    RekeningFilter = temp;
                }
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
                    var temp = RekeningFilter;
                    temp.IdRayon = null;
                    RekeningFilter = temp;
                }
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
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.IdCabang = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isFilterLainnyaChecked; /// ????????
        public bool IsFilterLainnyaChecked
        {
            get { return _isFilterLainnyaChecked; }
            set
            {
                _isFilterLainnyaChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private MasterStatusDto _statusFilter;
        public MasterStatusDto StatusFilter
        {
            get { return _statusFilter; }
            set
            {
                _statusFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.IdStatus = _statusFilter?.IdStatus;
            }
        }

        private MasterTarifLimbahDto _tarifLimbahFilter;
        public MasterTarifLimbahDto TarifLimbahFilter
        {
            get { return _tarifLimbahFilter; }
            set
            {
                _tarifLimbahFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.IdTarifLimbah = _tarifLimbahFilter?.IdTarifLimbah;
            }
        }

        private MasterKolektifDto _kolektifFilter;
        public MasterKolektifDto KolektifFilter
        {
            get { return _kolektifFilter; }
            set
            {
                _kolektifFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.IdKolektif = _kolektifFilter?.IdKolektif;
            }
        }

        private MasterUserDto _kasirFilter;
        public MasterUserDto KasirFilter
        {
            get { return _kasirFilter; }
            set
            {
                _kasirFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.NamaUser = _kasirFilter?.NamaUser;
            }
        }

        private MasterLoketDto _loketFilter;
        public MasterLoketDto LoketFilter
        {
            get { return _loketFilter; }
            set
            {
                _loketFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.NamaLoket = _loketFilter?.NamaLoket;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdWilayah = _wilayahFilter?.IdWilayah;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdKelurahan = _kelurahanFilter?.IdKelurahan;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdRayon = _rayonFilter?.IdRayon;
            }
        }

        private MasterCabangDto _cabangFilter;
        public MasterCabangDto CabangFilter
        {
            get { return _cabangFilter; }
            set
            {
                _cabangFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.IdCabang = _cabangFilter?.IdCabang;
            }
        }

        private MasterPeriodeDto _periodeFilter;
        public MasterPeriodeDto PeriodeFilter
        {
            get { return _periodeFilter; }
            set
            {
                _periodeFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.IdPeriode = _periodeFilter?.IdPeriode;
            }
        }

        private string _biayaAwalFilter;
        public string BiayaAwalFilter
        {
            get { return _biayaAwalFilter; }
            set
            {
                _biayaAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private string _biayaAkhirFilter;
        public string BiayaAkhirFilter
        {
            get { return _biayaAkhirFilter; }
            set
            {
                _biayaAkhirFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglBayarAwalFilter;
        public string TglBayarAwalFilter
        {
            get { return _tglBayarAwalFilter; }
            set
            {
                _tglBayarAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglBayarAkhirFilter;
        public string TglBayarAkhirFilter
        {
            get { return _tglBayarAkhirFilter; }
            set
            {
                _tglBayarAkhirFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglUploadAwalFilter;
        public string TglUploadAwalFilter
        {
            get { return _tglUploadAwalFilter; }
            set
            {
                _tglUploadAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglUploadAkhirFilter;
        public string TglUploadAkhirFilter
        {
            get { return _tglUploadAkhirFilter; }
            set
            {
                _tglUploadAkhirFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglPublishAwalFilter;
        public string TglPublishAwalFilter
        {
            get { return _tglPublishAwalFilter; }
            set
            {
                _tglPublishAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private string _tglPublishAkhirFilter;
        public string TglPublishAkhirFilter
        {
            get { return _tglPublishAkhirFilter; }
            set
            {
                _tglPublishAkhirFilter = value;
                OnPropertyChanged();
            }
        }

        // End Filter

        // Setting Table

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

        // End Setting Table

        private string _confirmationPasswordForm;
        public string ConfirmationPasswordForm
        {
            get { return _confirmationPasswordForm; }
            set
            {
                _confirmationPasswordForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelectAllTerbitkanPelanggan;
        public bool IsSelectAllTerbitkanPelanggan
        {
            get { return _isSelectAllTerbitkanPelanggan; }
            set
            {
                _isSelectAllTerbitkanPelanggan = value;
                OnPropertyChanged();

                var temp = new ObservableCollection<MasterPelangganLimbahWpf>(TerbitkanPelangganList);
                foreach (var item in temp)
                    item.IsSelected = _isSelectAllTerbitkanPelanggan;

                TerbitkanPelangganList = temp;
            }
        }

        private ObservableCollection<MasterPelangganLimbahWpf> _terbitkanPelangganList;
        public ObservableCollection<MasterPelangganLimbahWpf> TerbitkanPelangganList
        {
            get { return _terbitkanPelangganList; }
            set
            {
                _terbitkanPelangganList = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _tarifKoreksiForm;
        public MasterTarifLimbahDto TarifKoreksiForm
        {
            get { return _tarifKoreksiForm; }
            set
            {
                _tarifKoreksiForm = value;
                OnPropertyChanged();
            }
        }

        private string _biayaKoreksiForm;
        public string BiayaKoreksiForm
        {
            get { return _biayaKoreksiForm; }
            set
            {
                _biayaKoreksiForm = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get => _periodeList;
            set { _periodeList = value; OnPropertyChanged(); }
        }

        private MasterPeriodeDto _periodePerbaruiForm;
        public MasterPeriodeDto PeriodePerbaruiForm
        {
            get => _periodePerbaruiForm;
            set
            {
                _periodePerbaruiForm = value;
                OnPropertyChanged();
            }
        }
    }
}
