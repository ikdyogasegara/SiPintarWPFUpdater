using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.Supervisi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter
{
    public class SupervisiViewModel : ViewModelBase
    {
        public delegate void UpdateDataGridAction();

        public SupervisiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnGetDetailCommand = new OnGetDetailCommand(this, restApi);
            GetListCommand = new GetListCommand(this, restApi);
            GetFotoStanCommand = new GetFotoStanCommand(this);
            GetFotoRumahCommand = new GetFotoRumahCommand(this);
            GetVideoCommand = new GetVideoCommand(this);
            OnChangePeriodeCommand = new OnChangePeriodeCommand(this);
            OnRefreshCommand = GetListCommand;
            OnFilterCommand = GetListCommand;
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnOpenDetailPelangganCommand = new OnOpenDetailPelangganCommand(this);
            OnOpenVerifikasiDialogCommand = new OnOpenVerifikasiDialogCommand(this);
            OnOpenUnverifikasiDialogCommand = new OnOpenUnverifikasiDialogCommand(this);
            OnOpenHitungUlangDialogCommand = new OnOpenHitungUlangDialogCommand(this);
            OnOpenKoreksiDialogCommand = new OnOpenKoreksiDialogCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnOpenSettingFilterFormCommand = new OnOpenSettingFilterFormCommand(this);
            OnOpenTaksasiDialogCommand = new OnOpenTaksasiDialogCommand(this);
            OnOpenMeterTerbalikDialogCommand = new OnOpenMeterTerbalikDialogCommand(this);
            OnOpenTaksasiPemakaianDialogCommand = new OnOpenTaksasiPemakaianDialogCommand(this);
            OnOpenSetStanKembaliMudaDialogCommand = new OnOpenSetStanKembaliMudaDialogCommand(this);
            OnOpenSetBelumDibacaDialogCommand = new OnOpenSetBelumDibacaDialogCommand(this);
            OnOpenSetStatusAktifDialogCommand = new OnOpenSetStatusAktifDialogCommand(this);
            OnOpenHasilBacaUlangDialogCommand = new OnOpenHasilBacaUlangDialogCommand(this, restApi);
            OnOpenPermintaanBacaUlangDialogCommand = new OnOpenPermintaanBacaUlangDialogCommand(this);
            OnOpenLampiranDialogCommand = new OnOpenLampiranDialogCommand(this);
            OnOpenShortcutInfoDialogCommand = new OnOpenShortcutInfoDialogCommand(this);
            OnOpenMapViewCommand = new OnOpenMapViewCommand(this);
            OnOpenLihatFotoStanCommand = new OnOpenLihatFotoStanCommand(this);
            OnOpenLihatFotoRumahCommand = new OnOpenLihatFotoRumahCommand(this);
            OnOpenLihatVideoCommand = new OnOpenLihatVideoCommand(this);
            OnOpenBatalkanBacaUlangDialogCommand = new OnOpenBatalkanBacaUlangDialogCommand(this);
            OnSubmitVerifikasiCommand = new OnSubmitVerifikasiCommand(this, restApi);
            OnSubmitUnverifikasiCommand = new OnSubmitUnverifikasiCommand(this, restApi);
            OnSubmitHitungUlangCommand = new OnSubmitHitungUlangCommand(this, restApi);
            OnSubmitKoreksiCommand = new OnSubmitKoreksiCommand(this, restApi);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnSubmitSettingFilterCommand = new OnSubmitSettingFilterCommand(this);
            OnSubmitTaksasiCommand = new OnSubmitTaksasiCommand(this, restApi);
            OnSubmitMeterTerbalikCommand = new OnSubmitMeterTerbalikCommand(this, restApi);
            OnSubmitSetStanKembaliMudaCommand = new OnSubmitSetStanKembaliMudaCommand(this, restApi);
            OnSubmitSetBelumDibacaCommand = new OnSubmitSetBelumDibacaCommand(this, restApi);
            OnSubmitSetStatusAktifCommand = new OnSubmitSetStatusAktifCommand(this, restApi);
            OnSubmitPermintaanBacaUlangCommand = new OnSubmitPermintaanBacaUlangCommand(this, restApi);
            OnSubmitLampiranCommand = new OnSubmitLampiranCommand(this, restApi);
            OnSubmitKonfirmasiHasilBacaUlangCommand = new OnSubmitKonfirmasiHasilBacaUlangCommand(this, restApi);
            OnSubmitBatalkanHasilBacaUlangCommand = new OnSubmitBatalkanHasilBacaUlangCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
            OnDownloadFotoCommand = new OnDownloadFotoCommand(this);
            OnDownloadVideoCommand = new OnDownloadVideoCommand(this);
            OnSubmitTaksirCommand = new OnSubmitTaksirCommand(this, restApi);
            OnCalCulationKoreksiCommand = new OnCalCulationKoreksiCommand(this);
            OnUpdateVerifikasiCommand = new OnUpdateVerifikasiCommand(restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand GetListCommand { get; }
        public ICommand GetFotoStanCommand { get; }
        public ICommand GetFotoRumahCommand { get; }
        public ICommand GetVideoCommand { get; }
        public ICommand OnChangePeriodeCommand { get; }
        public ICommand OnGetDetailCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnOpenDetailPelangganCommand { get; }
        public ICommand OnOpenSettingFilterFormCommand { get; }
        public ICommand OnOpenVerifikasiDialogCommand { get; }
        public ICommand OnOpenUnverifikasiDialogCommand { get; }
        public ICommand OnOpenHitungUlangDialogCommand { get; }
        public ICommand OnOpenKoreksiDialogCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnOpenTaksasiDialogCommand { get; }
        public ICommand OnOpenMeterTerbalikDialogCommand { get; }
        public ICommand OnOpenTaksasiPemakaianDialogCommand { get; }
        public ICommand OnOpenSetStanKembaliMudaDialogCommand { get; }
        public ICommand OnOpenSetBelumDibacaDialogCommand { get; }
        public ICommand OnOpenSetStatusAktifDialogCommand { get; }
        public ICommand OnOpenHasilBacaUlangDialogCommand { get; }
        public ICommand OnOpenPermintaanBacaUlangDialogCommand { get; }
        public ICommand OnOpenLampiranDialogCommand { get; }
        public ICommand OnOpenShortcutInfoDialogCommand { get; }
        public ICommand OnOpenMapViewCommand { get; }
        public ICommand OnOpenLihatFotoStanCommand { get; }
        public ICommand OnOpenLihatFotoRumahCommand { get; }
        public ICommand OnOpenLihatVideoCommand { get; }
        public ICommand OnOpenBatalkanBacaUlangDialogCommand { get; }
        public ICommand OnSubmitVerifikasiCommand { get; }
        public ICommand OnSubmitUnverifikasiCommand { get; }
        public ICommand OnSubmitHitungUlangCommand { get; }
        public ICommand OnSubmitTaksasiCommand { get; }
        public ICommand OnSubmitMeterTerbalikCommand { get; }
        public ICommand OnSubmitSetStanKembaliMudaCommand { get; }
        public ICommand OnSubmitSetBelumDibacaCommand { get; }
        public ICommand OnSubmitSetStatusAktifCommand { get; }
        public ICommand OnSubmitPermintaanBacaUlangCommand { get; }
        public ICommand OnSubmitLampiranCommand { get; }
        public ICommand OnSubmitKoreksiCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSubmitSettingFilterCommand { get; }
        public ICommand OnSubmitKonfirmasiHasilBacaUlangCommand { get; }
        public ICommand OnSubmitBatalkanHasilBacaUlangCommand { get; }
        public ICommand OnDownloadFotoCommand { get; }
        public ICommand OnDownloadVideoCommand { get; }
        public ICommand OnSubmitTaksirCommand { get; }
        public ICommand OnCalCulationKoreksiCommand { get; }
        public ICommand OnUpdateVerifikasiCommand { get; }

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

        private bool _isTaksasiPemakaian;
        public bool IsTaksasiPemakaian
        {
            get { return _isTaksasiPemakaian; }
            set
            {
                _isTaksasiPemakaian = value;
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
                GetListCommand.Execute(null);
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

        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get { return _periodeList; }
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedPeriode;
        public MasterPeriodeDto SelectedPeriode
        {
            get { return _selectedPeriode; }
            set
            {
                _selectedPeriode = value;
                OnPropertyChanged();

                DetailDataPeriode = value;

                OnChangePeriodeCommand.Execute(null);
            }
        }

        private MasterPeriodeDto _detailDataPeriode;
        public MasterPeriodeDto DetailDataPeriode
        {
            get { return _detailDataPeriode; }
            set
            {
                _detailDataPeriode = value;
                OnPropertyChanged();
            }
        }

        private int _detailBulanIndex;
        public int DetailBulanIndex
        {
            get { return _detailBulanIndex; }
            set
            {
                _detailBulanIndex = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirWpf> _rekeningList;
        public ObservableCollection<RekeningAirWpf> RekeningList
        {
            get { return _rekeningList; }
            set
            {
                _rekeningList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirWpf _selectedData;
        public RekeningAirWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();

                if (_selectedData != null)
                {
                    IsDataSelected = true;
                    if (SelectedPeriode != null)
                        OnGetDetailCommand.Execute(null);
                }
                else
                {
                    IsDataSelected = false;
                    DetailData = null;
                }
            }
        }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDetailDto _detailData;
        public RekeningAirDetailDto DetailData
        {
            get { return _detailData; }
            set
            {
                _detailData = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirDto _koreksiForm;
        public RekeningAirDto KoreksiForm
        {
            get { return _koreksiForm; }
            set
            {
                _koreksiForm = value;
                OnPropertyChanged();
            }
        }

        // Filter

        private ParamRekeningAirFilterDto _rekeningFilter = new ParamRekeningAirFilterDto()
        {
            FlagBaca = null,
            Taksasi = null,
            FlagKoreksi = null,
            FlagVerifikasi = null,
            FlagPublish = null,
            FlagUpload = null,
            AdaFotoMeter = null,
            AdaFotoRumah = null,
            AdaVideo = null,
            FlagMinimumPakai = null
        };
        public ParamRekeningAirFilterDto RekeningFilter
        {
            get { return _rekeningFilter; }
            set
            {
                _rekeningFilter = value;
                OnPropertyChanged();
            }
        }

        #region Status
        private bool _isSemuaStatus = true;
        public bool IsSemuaStatusFilter
        {
            get { return _isSemuaStatus; }
            set
            {
                _isSemuaStatus = value;
                OnPropertyChanged();

                if (_isSemuaStatus == true && RekeningFilter != null)
                    RekeningFilter.FlagBaca = null;
            }
        }

        private bool _isBelumStatus;
        public bool IsBelumStatusFilter
        {
            get { return _isBelumStatus; }
            set
            {
                _isBelumStatus = value;
                OnPropertyChanged();

                if (_isBelumStatus == true && RekeningFilter != null)
                    RekeningFilter.FlagBaca = false;
            }
        }

        private bool _isSudahStatus;
        public bool IsSudahStatusFilter
        {
            get { return _isSudahStatus; }
            set
            {
                _isSudahStatus = value;
                OnPropertyChanged();

                if (_isSudahStatus == true && RekeningFilter != null)
                    RekeningFilter.FlagBaca = true;
            }
        }
        #endregion

        #region Jenis Verifikasi
        public ObservableCollection<KeyValuePair<int, string>> JenisVerifikasiList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Semua Data"),
                    new KeyValuePair<int, string>(2, "Sudah Verifikasi"),
                    new KeyValuePair<int, string>(3, "Belum Verifikasi"),
                    new KeyValuePair<int, string>(4, "Semua yg Aktif"),
                    new KeyValuePair<int, string>(5, "Aktif Tdk Dibaca"),
                    new KeyValuePair<int, string>(6, "Semua Tdk Aktif"),
                    new KeyValuePair<int, string>(7, "Sudah Upload"),
                    new KeyValuePair<int, string>(8, "Belum Upload"),
                };

                return data;
            }
        }

        private KeyValuePair<int, string>? _jenisVerifikasiFilter;
        public KeyValuePair<int, string>? JenisVerifikasiFilter
        {
            get { return _jenisVerifikasiFilter; }
            set
            {
                _jenisVerifikasiFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Jenis Data
        public ObservableCollection<KeyValuePair<int, string>> JenisDataList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Tanggal Koreksi"),
                    new KeyValuePair<int, string>(2, "Tanggal Publish"),
                    new KeyValuePair<int, string>(3, "Tanggal Upload"),
                    new KeyValuePair<int, string>(4, "Tanggal Transaksi"),
                    new KeyValuePair<int, string>(5, "Tanggal Verifikasi"),
                    new KeyValuePair<int, string>(6, "Tanggal Baca")
                };

                return data;
            }
        }

        private KeyValuePair<int, string>? _jenisDataFilter;
        public KeyValuePair<int, string>? JenisDataFilter
        {
            get { return _jenisDataFilter; }
            set
            {
                _jenisDataFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Rentang Waktu
        private bool _isRentangWaktuChecked;
        public bool IsRentangWaktuChecked
        {
            get { return _isRentangWaktuChecked; }
            set
            {
                _isRentangWaktuChecked = value;
                if (!value)
                {
                    JenisDataFilter = null;
                    RentangWaktu1Filter = null;
                    RentangWaktu2Filter = null;
                }
                else
                {
                    JenisDataFilter = JenisDataList[0];
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

        #region Kelainan
        private bool _isKelainanChecked;
        public bool IsKelainanChecked
        {
            get { return _isKelainanChecked; }
            set
            {
                _isKelainanChecked = value;
                if (!value)
                {
                    KelainanFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKelainanDto> _kelainanList;
        public ObservableCollection<MasterKelainanDto> KelainanList
        {
            get { return _kelainanList; }
            set
            {
                _kelainanList = value;
                OnPropertyChanged();
            }
        }

        private MasterKelainanDto _kelainanFilter;
        public MasterKelainanDto KelainanFilter
        {
            get { return _kelainanFilter; }
            set
            {
                _kelainanFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.Kelainan = _kelainanFilter?.Kelainan;
            }
        }
        #endregion

        #region Jumlah Pakai
        private bool _isJumlahPakaiChecked;
        public bool IsJumlahPakaiChecked
        {
            get { return _isJumlahPakaiChecked; }
            set
            {
                _isJumlahPakaiChecked = value;
                if (!value)
                {
                    JumlahPakai1Filter = null;
                    JumlahPakai2Filter = null;
                }
                OnPropertyChanged();
            }
        }

        private string _jumlahPakai1Filter;
        public string JumlahPakai1Filter
        {
            get { return _jumlahPakai1Filter; }
            set
            {
                _jumlahPakai1Filter = value;
                OnPropertyChanged();
            }
        }

        private string _jumlahPakai2Filter;
        public string JumlahPakai2Filter
        {
            get { return _jumlahPakai2Filter; }
            set
            {
                _jumlahPakai2Filter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Stan Awal
        private bool _isStanAwalChecked;
        public bool IsStanAwalChecked
        {
            get { return _isStanAwalChecked; }
            set
            {
                _isStanAwalChecked = value;
                if (!value)
                {
                    StanAwal1Filter = null;
                    StanAwal2Filter = null;
                }
                OnPropertyChanged();
            }
        }

        private string _stanAwal1Filter;
        public string StanAwal1Filter
        {
            get { return _stanAwal1Filter; }
            set
            {
                _stanAwal1Filter = value;
                OnPropertyChanged();
            }
        }

        private string _stanAwal2Filter;
        public string StanAwal2Filter
        {
            get { return _stanAwal2Filter; }
            set
            {
                _stanAwal2Filter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Stan Akhir
        private bool _isStanAkhirChecked;
        public bool IsStanAkhirChecked
        {
            get { return _isStanAkhirChecked; }
            set
            {
                _isStanAkhirChecked = value;
                if (!value)
                {
                    StanAkhir1Filter = null;
                    StanAkhir2Filter = null;
                }
                OnPropertyChanged();
            }
        }

        private string _stanAkhir1Filter;
        public string StanAkhir1Filter
        {
            get { return _stanAkhir1Filter; }
            set
            {
                _stanAkhir1Filter = value;
                OnPropertyChanged();
            }
        }

        private string _stanAkhir2Filter;
        public string StanAkhir2Filter
        {
            get { return _stanAkhir2Filter; }
            set
            {
                _stanAkhir2Filter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Jenis Pipa
        private bool _isJenisPipaChecked;
        public bool IsJenisPipaChecked
        {
            get { return _isJenisPipaChecked; }
            set
            {
                _isJenisPipaChecked = value;
                if (!value)
                {
                    JenisPipaFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KeyValuePair<int, string>> _jenisPipaList;
        public ObservableCollection<KeyValuePair<int, string>> JenisPipaList
        {
            get { return _jenisPipaList; }
            set
            {
                _jenisPipaList = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string>? _jenisPipaFilter;
        public KeyValuePair<int, string>? JenisPipaFilter
        {
            get { return _jenisPipaFilter; }
            set
            {
                _jenisPipaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Hitung Air Limbah
        private bool _isHitungAirLimbahChecked;
        public bool IsHitungAirLimbahChecked
        {
            get { return _isHitungAirLimbahChecked; }
            set
            {
                _isHitungAirLimbahChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Segel Rusak
        private bool _isSegelRusakChecked;
        public bool IsSegelRusakChecked
        {
            get { return _isSegelRusakChecked; }
            set
            {
                _isSegelRusakChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Rayon
        private bool _isKodeRayonChecked;
        public bool IsKodeRayonChecked
        {
            get { return _isKodeRayonChecked; }
            set
            {
                _isKodeRayonChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.KodeRayon = null;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdRayon = _rayonFilter?.IdRayon;
            }
        }
        #endregion

        #region Golongan
        private bool _isKodeGolonganChecked;
        public bool IsKodeGolonganChecked
        {
            get { return _isKodeGolonganChecked; }
            set
            {
                _isKodeGolonganChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.KodeGolongan = null;
                    RekeningFilter = temp;
                }
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

        private ObservableCollection<MasterAdministrasiLainDto> _administrasiLainList;
        public ObservableCollection<MasterAdministrasiLainDto> AdministrasiLainList
        {
            get { return _administrasiLainList; }
            set
            {
                _administrasiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPemeliharaanLainDto> _pemeliharaanLainList;
        public ObservableCollection<MasterPemeliharaanLainDto> PemeliharaanLainList
        {
            get { return _pemeliharaanLainList; }
            set
            {
                _pemeliharaanLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRetribusiLainDto> _retribusiLainList;
        public ObservableCollection<MasterRetribusiLainDto> RetribusiLainList
        {
            get { return _retribusiLainList; }
            set
            {
                _retribusiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterMeteraiDto> _meteraiList;
        public ObservableCollection<MasterMeteraiDto> MeteraiList
        {
            get { return _meteraiList; }
            set
            {
                _meteraiList = value;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdGolongan = _golonganFilter?.IdGolongan;
            }
        }
        #endregion

        #region Kelurahan
        private bool _isKodeKelurahanChecked;
        public bool IsKodeKelurahanChecked
        {
            get { return _isKodeKelurahanChecked; }
            set
            {
                _isKodeKelurahanChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.KodeKelurahan = null;
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

                if (RekeningFilter != null)
                    RekeningFilter.IdKelurahan = _kelurahanFilter?.IdKelurahan;
            }
        }
        #endregion

        #region Kecamatan
        private bool _isKodeKecamatanChecked;
        public bool IsKodeKecamatanChecked
        {
            get { return _isKodeKecamatanChecked; }
            set
            {
                _isKodeKecamatanChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.KodeKecamatan = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKecamatanChecked;
        public bool IsKecamatanChecked
        {
            get { return _isKecamatanChecked; }
            set
            {
                _isKecamatanChecked = value;
                if (!value)
                {
                    KecamatanFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKecamatanDto> _kecamatanList;
        public ObservableCollection<MasterKecamatanDto> KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
            }
        }

        private MasterKecamatanDto _kecamatanFilter;
        public MasterKecamatanDto KecamatanFilter
        {
            get { return _kecamatanFilter; }
            set
            {
                _kecamatanFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.KodeKecamatan = _kecamatanFilter?.KodeKecamatan;
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
                    var temp = RekeningFilter;
                    temp.Alamat = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Luas Rumah
        private bool _isLuasRumahChecked;
        public bool IsLuasRumahChecked
        {
            get { return _isLuasRumahChecked; }
            set
            {
                _isLuasRumahChecked = value;
                if (!value)
                {
                    LuasRumah1Filter = null;
                    LuasRumah2Filter = null;
                }
                OnPropertyChanged();
            }
        }

        private string _luasRumah1Filter;
        public string LuasRumah1Filter
        {
            get { return _luasRumah1Filter; }
            set
            {
                _luasRumah1Filter = value;
                OnPropertyChanged();
            }
        }

        private string _luasRumah2Filter;
        public string LuasRumah2Filter
        {
            get { return _luasRumah2Filter; }
            set
            {
                _luasRumah2Filter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Keakuratan
        private bool _isKeakuratanChecked;
        public bool IsKeakuratanChecked
        {
            get { return _isKeakuratanChecked; }
            set
            {
                _isKeakuratanChecked = value;
                if (!value)
                {
                    KeakuratanFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private string _keakuratanFilter;
        public string KeakuratanFilter
        {
            get { return _keakuratanFilter; }
            set
            {
                _keakuratanFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private bool _isTaksirChecked;
        public bool IsTaksirChecked
        {
            get { return _isTaksirChecked; }
            set
            {
                _isTaksirChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTaksasiChecked;
        public bool IsTaksasiChecked
        {
            get { return _isTaksasiChecked; }
            set
            {
                _isTaksasiChecked = value;
                OnPropertyChanged();
            }
        }

        #region Petugas Baca
        private bool _isPetugasBacaChecked;
        public bool IsPetugasBacaChecked
        {
            get { return _isPetugasBacaChecked; }
            set
            {
                _isPetugasBacaChecked = value;
                if (!value)
                {
                    PetugasBacaFilter = null;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPetugasBacaDto> _petugasBacaList;
        public ObservableCollection<MasterPetugasBacaDto> PetugasBacaList
        {
            get { return _petugasBacaList; }
            set
            {
                _petugasBacaList = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedDataPetugasBaca = new MasterPetugasBacaDto();
        public MasterPetugasBacaDto SelectedDataPetugasBaca
        {
            get => _selectedDataPetugasBaca;
            set { _selectedDataPetugasBaca = value; OnPropertyChanged(); }
        }

        private MasterKelainanDto _selectedDataKelainan = new MasterKelainanDto();
        public MasterKelainanDto SelectedDataKelainan
        {
            get => _selectedDataKelainan;
            set { _selectedDataKelainan = value; OnPropertyChanged(); }
        }

        private MasterPetugasBacaDto _petugasBacaFilter;
        public MasterPetugasBacaDto PetugasBacaFilter
        {
            get { return _petugasBacaFilter; }
            set
            {
                _petugasBacaFilter = value;
                OnPropertyChanged();

                if (RekeningFilter != null)
                    RekeningFilter.PetugasBaca = _petugasBacaFilter?.PetugasBaca;
            }
        }
        #endregion

        #region Nama Pelanggan
        private bool _isNamaPelangganChecked;
        public bool IsNamaPelangganChecked
        {
            get { return _isNamaPelangganChecked; }
            set
            {
                _isNamaPelangganChecked = value;
                if (!value)
                {
                    var temp = RekeningFilter;
                    temp.Nama = null;
                    RekeningFilter = temp;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region No Samb
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
        #endregion

        #region Lainnya
        private bool _isLainnyaChecked;
        public bool IsLainnyaChecked
        {
            get { return _isLainnyaChecked; }
            set
            {
                _isLainnyaChecked = value;
                if (!value)
                {
                    LainnyaFilter = null;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> LainnyaList
        {
            get
            {
                return new ObservableCollection<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Sudah dibaca ulang"),
                    new KeyValuePair<int, string>(2, "Request dibaca ulang"),
                    new KeyValuePair<int, string>(3, "Terdapat stan angkat"),
                    new KeyValuePair<int, string>(4, "Yang terdapat foto rumah"),
                    new KeyValuePair<int, string>(5, "Yang terdapat video"),
                    new KeyValuePair<int, string>(6, "Terdapat Kelainan"),
                    new KeyValuePair<int, string>(8, "Hanya yang di-taksir"),
                    new KeyValuePair<int, string>(9, "Di-taksir selama 2 bulan"),
                    new KeyValuePair<int, string>(10, "Di-taksir selama 3 bulan"),
                    new KeyValuePair<int, string>(11, "Pemakaian Minus"),
                    new KeyValuePair<int, string>(12, "Belum lunas"),
                    new KeyValuePair<int, string>(13, "Sudah lunas"),
                    new KeyValuePair<int, string>(14, "Stan awal=0"),
                    new KeyValuePair<int, string>(15, "Stan awal=stan akhir"),
                    new KeyValuePair<int, string>(16, "Stan awal=stan akhir,Ada Pakai"),
                    new KeyValuePair<int, string>(18, "Total rekening tdk sama"),
                    new KeyValuePair<int, string>(19, "Tidak dapat dibaca"),
                    new KeyValuePair<int, string>(20, "Data lapangan != stan skrg"),
                    new KeyValuePair<int, string>(21, "Terdapat memo lapangan"),
                    new KeyValuePair<int, string>(22, "Tukar hasil baca ulang"),
                    new KeyValuePair<int, string>(23, "Pemakaian sama 3 bln"),
                    new KeyValuePair<int, string>(24, "Taksasi tanpa ubah stan akhir"),
                    new KeyValuePair<int, string>(25, "Meter terbalik"),
                    new KeyValuePair<int, string>(26, "Terdapat Lampiran"),
                    new KeyValuePair<int, string>(27, "Pemakaian minimum selama 3 bln"),
                    new KeyValuePair<int, string>(28, "Pemakaian minimum selama 6 bln"),
                    new KeyValuePair<int, string>(30, "Kelainan Sama 3 Bulan"),
                    new KeyValuePair<int, string>(31, "Dibaca Manual dari SUPERVISI"),
                };
            }
        }

        private KeyValuePair<int, string>? _lainnyaFilter;
        public KeyValuePair<int, string>? LainnyaFilter
        {
            get { return _lainnyaFilter; }
            set
            {
                _lainnyaFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        // End Filter

        // setting table

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

        private object _filterSetting;
        public object FilterSetting
        {
            get { return _filterSetting; }
            set
            {
                _filterSetting = value;
                OnPropertyChanged();
            }
        }

        // end setting table

        // koreksi hasil baca form

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

        private MasterKelainanDto _koreksiSelectedKelainan;
        public MasterKelainanDto KoreksiSelectedKelainan
        {
            get { return _koreksiSelectedKelainan; }
            set
            {
                _koreksiSelectedKelainan = value;
                OnPropertyChanged();

                if (KoreksiForm != null)
                    KoreksiForm.Kelainan = _koreksiSelectedKelainan?.Kelainan;
            }
        }

        private MasterPetugasBacaDto _koreksiSelectedPetugasBaca;
        public MasterPetugasBacaDto KoreksiSelectedPetugasBaca
        {
            get { return _koreksiSelectedPetugasBaca; }
            set
            {
                _koreksiSelectedPetugasBaca = value;
                OnPropertyChanged();

                if (KoreksiForm != null)
                    KoreksiForm.PetugasBaca = _koreksiSelectedPetugasBaca?.PetugasBaca;
            }
        }

        // end koreksi hasil baca form

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

        private bool _isSelectedAll;
        public bool IsSelectedAll
        {
            get => _isSelectedAll;
            set
            {
                _isSelectedAll = value;
                OnPropertyChanged();

                var temp = new ObservableCollection<RekeningAirWpf>(RekeningList);
                foreach (var item in temp)
                    item.IsSelected = _isSelectedAll;

                RekeningList = temp;
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }

        private string _konfirmasiPassword;
        public string KonfirmasiPassword
        {
            get => _konfirmasiPassword;
            set { _konfirmasiPassword = value; OnPropertyChanged(); }
        }

        private bool _hasFotoStan;
        public bool HasFotoStan
        {
            get => _hasFotoStan;
            set
            {
                _hasFotoStan = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoStanUlang;
        public bool HasFotoStanUlang
        {
            get => _hasFotoStanUlang;
            set
            {
                _hasFotoStanUlang = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _fileFotoStan = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage FileFotoStan
        {
            get => _fileFotoStan;
            set
            {
                _fileFotoStan = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoStan = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoStan
        {
            get => _thumbnailFotoStan;
            set
            {
                _thumbnailFotoStan = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoStanUlang = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoStanUlang
        {
            get => _thumbnailFotoStanUlang;
            set
            {
                _thumbnailFotoStanUlang = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoRumah;
        public bool HasFotoRumah
        {
            get => _hasFotoRumah;
            set
            {
                _hasFotoRumah = value;
                OnPropertyChanged();
            }
        }

        private bool _hasFotoRumahUlang;
        public bool HasFotoRumahUlang
        {
            get => _hasFotoRumahUlang;
            set
            {
                _hasFotoRumahUlang = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _fileFotoRumah = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage FileFotoRumah
        {
            get => _fileFotoRumah;
            set
            {
                _fileFotoRumah = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoRumah = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoRumah
        {
            get => _thumbnailFotoRumah;
            set
            {
                _thumbnailFotoRumah = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage _thumbnailFotoRumahUlang = new BitmapImage() { UriSource = new Uri($"/SiPintar;component/Assets/Images/no_image.png", UriKind.RelativeOrAbsolute) };
        public BitmapImage ThumbnailFotoRumahUlang
        {
            get => _thumbnailFotoRumahUlang;
            set
            {
                _thumbnailFotoRumahUlang = value;
                OnPropertyChanged();
            }
        }

        private bool _hasVideo;
        public bool HasVideo
        {
            get => _hasVideo;
            set
            {
                _hasVideo = value;
                OnPropertyChanged();
            }
        }

        private bool _hasVideoUlang;
        public bool HasVideoUlang
        {
            get => _hasVideoUlang;
            set
            {
                _hasVideoUlang = value;
                OnPropertyChanged();
            }
        }

        private Uri _fileVideo;
        public Uri FileVideo
        {
            get => _fileVideo;
            set
            {
                _fileVideo = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingBukti;
        public bool IsLoadingBukti
        {
            get => _isLoadingBukti;
            set
            {
                _isLoadingBukti = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirHasilBacaUlangDto _hasilBacaUlang;
        public RekeningAirHasilBacaUlangDto HasilBacaUlang
        {
            get => _hasilBacaUlang;
            set
            {
                _hasilBacaUlang = value;
                OnPropertyChanged();
            }
        }

        private bool _isOpenBacaUlang;
        public bool IsOpenBacaUlang
        {
            get => _isOpenBacaUlang;
            set
            {
                _isOpenBacaUlang = value;
                OnPropertyChanged();
            }
        }
    }

}
