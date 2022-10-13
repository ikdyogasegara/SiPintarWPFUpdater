using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.RotasiMeter;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class RotasiMeterViewModel : ViewModelBase
    {
        private readonly bool _isTest;
        public RotasiMeterViewModel(PelayananViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            _isTest = isTest;
            DataList = new ObservableCollection<JadwalGantiMeterWpf>();
            DataPelanggan = new ObservableCollection<MasterPelangganGlobalWpf>();
            DataParamPermohonan = new ObservableCollection<ParamPermohonanList>();
            OnLoadCommand = new OnLoadCommand(this, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _isTest);
            OnCariPelangganCommand = new OnCariPelangganCommand(this, restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, _isTest);

            var tahunList = new List<TahunDto>();
            var currentYear = DateTime.Now.Year;

            for (var i = 0; i < 10; i++)
            {
                tahunList.Add(new TahunDto()
                {
                    Tahun = currentYear--
                });
            }

            TahunList = tahunList;
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnCariPelangganCommand { get; }
        public ICommand OnSubmitFormCommand { get; }


        private TahunDto _filterTahun;
        public TahunDto FilterTahun
        {
            get { return _filterTahun; }
            set
            {
                _filterTahun = value;
                OnPropertyChanged();
            }
        }

        private string _isFor;
        public string IsFor
        {
            get { return _isFor; }
            set
            {
                _isFor = value;
                OnPropertyChanged();

                if (_isFor == "detail")
                {
                    IsCanEdit = false;
                }
                else
                {
                    IsCanEdit = true;
                }
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


        private bool _isCanEdit = true;
        public bool IsCanEdit
        {
            get => _isCanEdit;
            set
            {
                _isCanEdit = value; OnPropertyChanged();
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
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isLoadingMap;
        public bool IsLoadingMap
        {
            get => _isLoadingMap;
            set { _isLoadingMap = value; OnPropertyChanged(); }
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

        private bool _isCariPelangganForm = true;
        public bool IsCariPelangganForm
        {
            get { return _isCariPelangganForm; }
            set
            {
                _isCariPelangganForm = value;
                OnPropertyChanged();
            }
        }

        #region enable/disable filter

        private bool _isRutinChecked;
        public bool IsRutinChecked
        {
            get { return _isRutinChecked; }
            set
            {
                _isRutinChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isJenisGantiMeterChecked;
        public bool IsJenisGantiMeterChecked
        {
            get { return _isJenisGantiMeterChecked; }
            set
            {
                _isJenisGantiMeterChecked = value;
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

        private bool _isTglJadwalChecked;
        public bool IsTglJadwalChecked
        {
            get { return _isTglJadwalChecked; }
            set
            {
                _isTglJadwalChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTglMeterChecked;
        public bool IsTglMeterChecked
        {
            get { return _isTglMeterChecked; }
            set
            {
                _isTglMeterChecked = value;
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

        private MasterJenisGantiMeterDto _jenisGantiMeterForm;
        public MasterJenisGantiMeterDto JenisGantiMeterForm
        {
            get { return _jenisGantiMeterForm; }
            set
            {
                _jenisGantiMeterForm = value;
                OnPropertyChanged();
            }
        }

        #region filter variable

        private string _filterRutin;
        public string FilterRutin
        {
            get { return _filterRutin; }
            set
            {
                _filterRutin = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisGantiMeterDto _filterJenisGantiMeter;
        public MasterJenisGantiMeterDto FilterJenisGantiMeter
        {
            get { return _filterJenisGantiMeter; }
            set
            {
                _filterJenisGantiMeter = value;
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

        private DateTime? _filterTglJadwalAwal;
        public DateTime? FilterTglJadwalAwal
        {
            get { return _filterTglJadwalAwal; }
            set
            {
                _filterTglJadwalAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTglJadwalAkhir;
        public DateTime? FilterTglJadwalAkhir
        {
            get { return _filterTglJadwalAkhir; }
            set
            {
                _filterTglJadwalAkhir = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTglMeterAwal;
        public DateTime? FilterTglMeterAwal
        {
            get { return _filterTglMeterAwal; }
            set
            {
                _filterTglMeterAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTglMeterAkhir;
        public DateTime? FilterTglMeterAkhir
        {
            get { return _filterTglMeterAkhir; }
            set
            {
                _filterTglMeterAkhir = value;
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
        private List<TahunDto> _tahunList = new List<TahunDto>();
        public List<TahunDto> TahunList
        {
            get { return _tahunList; }
            set
            {
                _tahunList = value;
                OnPropertyChanged();
            }
        }

        public class TahunDto
        {
            public int Tahun { get; set; }
        }

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

        private ObservableCollection<MasterDiameterDto> _diameterList = new ObservableCollection<MasterDiameterDto>();
        public ObservableCollection<MasterDiameterDto> DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAdministrasiLainDto> _administrasiLainList = new ObservableCollection<MasterAdministrasiLainDto>();
        public ObservableCollection<MasterAdministrasiLainDto> AdministrasiLainList
        {
            get { return _administrasiLainList; }
            set
            {
                _administrasiLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPemeliharaanLainDto> _PemeliharaanLainList = new ObservableCollection<MasterPemeliharaanLainDto>();
        public ObservableCollection<MasterPemeliharaanLainDto> PemeliharaanLainList
        {
            get { return _PemeliharaanLainList; }
            set
            {
                _PemeliharaanLainList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterRetribusiLainDto> _RetribusiLainList = new ObservableCollection<MasterRetribusiLainDto>();
        public ObservableCollection<MasterRetribusiLainDto> RetribusiLainList
        {
            get { return _RetribusiLainList; }
            set
            {
                _RetribusiLainList = value;
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

        private JadwalGantiMeterWpf _formData;
        public JadwalGantiMeterWpf FormData
        {
            get { return _formData; }
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<ParamPermohonanList> _dataParamPermohonan;
        public ObservableCollection<ParamPermohonanList> DataParamPermohonan
        {
            get { return _dataParamPermohonan; }
            set
            {
                _dataParamPermohonan = value;
                OnPropertyChanged();
            }
        }

        private List<ParamPermohonanParameterDto> _formDataDetailParameter;
        public List<ParamPermohonanParameterDto> FormDataDetailParameter
        {
            get { return _formDataDetailParameter; }
            set
            {
                _formDataDetailParameter = value;
                OnPropertyChanged();
            }
        }

        private List<ParamPermohonanBiayaDto> _formDataDetailBiaya;
        public List<ParamPermohonanBiayaDto> FormDataDetailBiaya
        {
            get { return _formDataDetailBiaya; }
            set
            {
                _formDataDetailBiaya = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<JadwalGantiMeterWpf> _dataList;
        public ObservableCollection<JadwalGantiMeterWpf> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private JadwalGantiMeterWpf _selectedData;
        public JadwalGantiMeterWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPelangganGlobalWpf> _dataPelanggan;
        public ObservableCollection<MasterPelangganGlobalWpf> DataPelanggan
        {
            get { return _dataPelanggan; }
            set
            {
                _dataPelanggan = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganGlobalWpf _selectedDataPelanggan;
        public MasterPelangganGlobalWpf SelectedDataPelanggan
        {
            get { return _selectedDataPelanggan; }
            set
            {
                _selectedDataPelanggan = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PermohonanWpf> _dataPermohonan;
        public ObservableCollection<PermohonanWpf> DataPermohonan
        {
            get { return _dataPermohonan; }
            set
            {
                _dataPermohonan = value;
                OnPropertyChanged();
            }
        }

        private PermohonanWpf _selectedDataPermohonan;
        public PermohonanWpf SelectedDataPermohonan
        {
            get { return _selectedDataPermohonan; }
            set
            {
                _selectedDataPermohonan = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterJenisGantiMeterDto> _jenisGantiMeterList = new ObservableCollection<MasterJenisGantiMeterDto>();
        public ObservableCollection<MasterJenisGantiMeterDto> JenisGantiMeterList
        {
            get { return _jenisGantiMeterList; }
            set
            {
                _jenisGantiMeterList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterPekerjaanDto> _pekerjaanList = new ObservableCollection<MasterPekerjaanDto>();
        public ObservableCollection<MasterPekerjaanDto> PekerjaanList
        {
            get { return _pekerjaanList; }
            set
            {
                _pekerjaanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBlokDto> _blokList = new ObservableCollection<MasterBlokDto>();
        public ObservableCollection<MasterBlokDto> BlokList
        {
            get { return _blokList; }
            set
            {
                _blokList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterSumberAirDto> _sumberAirList = new ObservableCollection<MasterSumberAirDto>();
        public ObservableCollection<MasterSumberAirDto> SumberAirList
        {
            get { return _sumberAirList; }
            set
            {
                _sumberAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterJenisBangunanDto> _jenisBangunanList = new ObservableCollection<MasterJenisBangunanDto>();
        public ObservableCollection<MasterJenisBangunanDto> JenisBangunanList
        {
            get { return _jenisBangunanList; }
            set
            {
                _jenisBangunanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKepemilikanDto> _kepemilikanBangunanList = new ObservableCollection<MasterKepemilikanDto>();
        public ObservableCollection<MasterKepemilikanDto> KepemilikanBangunanList
        {
            get { return _kepemilikanBangunanList; }
            set
            {
                _kepemilikanBangunanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeruntukanDto> _peruntukanList = new ObservableCollection<MasterPeruntukanDto>();
        public ObservableCollection<MasterPeruntukanDto> PeruntukanList
        {
            get { return _peruntukanList; }
            set
            {
                _peruntukanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterTipePendaftaranSambunganDto> _tipePendaftaranList = new ObservableCollection<MasterTipePendaftaranSambunganDto>();
        public ObservableCollection<MasterTipePendaftaranSambunganDto> TipePendaftaranList
        {
            get { return _tipePendaftaranList; }
            set
            {
                _tipePendaftaranList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDmaDto> _dmaList = new ObservableCollection<MasterDmaDto>();
        public ObservableCollection<MasterDmaDto> DmaList
        {
            get { return _dmaList; }
            set
            {
                _dmaList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterDmzDto> _dmzList = new ObservableCollection<MasterDmzDto>();
        public ObservableCollection<MasterDmzDto> DmzList
        {
            get { return _dmzList; }
            set
            {
                _dmzList = value;
                OnPropertyChanged();
            }
        }

        private MasterPaketDto _selectedPaketRabPipaPersil;
        public MasterPaketDto SelectedPaketRabPipaPersil
        {
            get { return _selectedPaketRabPipaPersil; }
            set
            {
                _selectedPaketRabPipaPersil = value;
                OnPropertyChanged();
            }
        }

        private MasterPaketDto _selectedPaketRabPipaDistribusi;
        public MasterPaketDto SelectedPaketRabPipaDistribusi
        {
            get { return _selectedPaketRabPipaDistribusi; }
            set
            {
                _selectedPaketRabPipaDistribusi = value;
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
                    new KeyValuePair<int, string>(1000, "1000"),
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

        #region prev next page data permohonan
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

        #region prev next page data material dan ongkos
        private int _currentPageMaterialOngkos = 1;
        public int CurrentPageMaterialOngkos
        {
            get { return _currentPageMaterialOngkos; }
            set
            {
                _currentPageMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private int _totalPageMaterialOngkos = 1;
        public int TotalPageMaterialOngkos
        {
            get { return _totalPageMaterialOngkos; }
            set
            {
                _totalPageMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabledMaterialOngkos;
        public bool IsPreviousButtonEnabledMaterialOngkos
        {
            get { return _isPreviousButtonEnabledMaterialOngkos; }
            set
            {
                _isPreviousButtonEnabledMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledMaterialOngkos;
        public bool IsNextButtonEnabledMaterialOngkos
        {
            get { return _isNextButtonEnabledMaterialOngkos; }
            set
            {
                _isNextButtonEnabledMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private long _totalRecordMaterialOngkos;
        public long TotalRecordMaterialOngkos
        {
            get { return _totalRecordMaterialOngkos; }
            set
            {
                _totalRecordMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisibleMaterialOngkos;
        public bool IsLeftPageNumberFillerVisibleMaterialOngkos
        {
            get { return _isLeftPageNumberFillerVisibleMaterialOngkos; }
            set
            {
                _isLeftPageNumberFillerVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisibleMaterialOngkos;
        public bool IsRightPageNumberFillerVisibleMaterialOngkos
        {
            get { return _isRightPageNumberFillerVisibleMaterialOngkos; }
            set
            {
                _isRightPageNumberFillerVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActiveMaterialOngkos;
        public bool IsLeftPageNumberActiveMaterialOngkos
        {
            get { return _isLeftPageNumberActiveMaterialOngkos; }
            set
            {
                _isLeftPageNumberActiveMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActiveMaterialOngkos;
        public bool IsRightPageNumberActiveMaterialOngkos
        {
            get { return _isRightPageNumberActiveMaterialOngkos; }
            set
            {
                _isRightPageNumberActiveMaterialOngkos = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisibleMaterialOngkos;
        public bool IsMiddlePageNumberVisibleMaterialOngkos
        {
            get { return _isMiddlePageNumberVisibleMaterialOngkos; }
            set
            {
                _isMiddlePageNumberVisibleMaterialOngkos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region foto map

        private bool _isDenahLokasiFormChecked;
        public bool IsDenahLokasiFormChecked
        {
            get => _isDenahLokasiFormChecked;
            set
            {
                _isDenahLokasiFormChecked = value;
                OnPropertyChanged();
            }
        }

        private string _denahLokasiForm;
        public string DenahLokasiForm
        {
            get => _denahLokasiForm;
            set
            {
                _denahLokasiForm = value;
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
        #endregion

        private ObservableCollection<MasterPegawaiDto> _pegawaiList;
        public ObservableCollection<MasterPegawaiDto> PegawaiList
        {
            get { return _pegawaiList; }
            set
            {
                _pegawaiList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPegawaiDto> _selectedPegawai = new ObservableCollection<MasterPegawaiDto>();
        public ObservableCollection<MasterPegawaiDto> SelectedPegawai
        {
            get { return _selectedPegawai; }
            set
            {
                _selectedPegawai = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanSpkDto _formSpk;
        public ParamPermohonanSpkDto FormSpk
        {
            get { return _formSpk; }
            set
            {
                _formSpk = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanRabDto _formRab;
        public ParamPermohonanRabDto FormRab
        {
            get { return _formRab; }
            set
            {
                _formRab = value;
                OnPropertyChanged();
            }
        }

        private ParamPermohonanBaDto _formBa;
        public ParamPermohonanBaDto FormBa
        {
            get { return _formBa; }
            set
            {
                _formBa = value;
                OnPropertyChanged();
            }
        }

        private MasterPekerjaanDto _pekerjaanForm;
        public MasterPekerjaanDto PekerjaanForm
        {
            get { return _pekerjaanForm; }
            set
            {
                _pekerjaanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterBlokDto _blokForm;
        public MasterBlokDto BlokForm
        {
            get => _blokForm;
            set
            {
                _blokForm = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBangunanDto _jenisBangunanForm;
        public MasterJenisBangunanDto JenisBangunanForm
        {
            get => _jenisBangunanForm;
            set
            {
                _jenisBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterKepemilikanDto _kepemilikanBangunanForm;
        public MasterKepemilikanDto KepemilikanBangunanForm
        {
            get => _kepemilikanBangunanForm;
            set
            {
                _kepemilikanBangunanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPeruntukanDto _peruntukanForm;
        public MasterPeruntukanDto PeruntukanForm
        {
            get => _peruntukanForm;
            set
            {
                _peruntukanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterSumberAirDto _sumberAirForm;
        public MasterSumberAirDto SumberAirForm
        {
            get => _sumberAirForm;
            set
            {
                _sumberAirForm = value;
                OnPropertyChanged();
            }
        }


        private MasterGolonganDto _golonganForm;
        public MasterGolonganDto GolonganForm
        {
            get => _golonganForm;
            set
            {
                _golonganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDiameterDto _diameterForm;
        public MasterDiameterDto DiameterForm
        {
            get => _diameterForm;
            set
            {
                _diameterForm = value;
                OnPropertyChanged();
            }
        }

        private MasterAdministrasiLainDto _administrasiLainForm;
        public MasterAdministrasiLainDto AdministrasiLainForm
        {
            get => _administrasiLainForm;
            set
            {
                _administrasiLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPemeliharaanLainDto _pemeliharaanLainForm;
        public MasterPemeliharaanLainDto PemeliharaanLainForm
        {
            get => _pemeliharaanLainForm;
            set
            {
                _pemeliharaanLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterRetribusiLainDto _retribusiLainForm;
        public MasterRetribusiLainDto RetribusiLainForm
        {
            get => _retribusiLainForm;
            set
            {
                _retribusiLainForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmaDto _dmaForm;
        public MasterDmaDto DmaForm
        {
            get => _dmaForm;
            set
            {
                _dmaForm = value;
                OnPropertyChanged();
            }
        }

        private MasterDmzDto _dmzForm;
        public MasterDmzDto DmzForm
        {
            get => _dmzForm;
            set
            {
                _dmzForm = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _tanggalBppi;
        public DateTime? TanggalBppi
        {
            get => _tanggalBppi;
            set
            {
                _tanggalBppi = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tanggalSpkp;
        public DateTime? TanggalSpkp
        {
            get => _tanggalSpkp;
            set
            {
                _tanggalSpkp = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _tanggalSppb;
        public DateTime? TanggalSppb
        {
            get => _tanggalSppb;
            set
            {
                _tanggalSppb = value;
                OnPropertyChanged();
            }
        }
    }
}
