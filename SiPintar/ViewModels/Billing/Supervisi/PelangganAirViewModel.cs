using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Billing.Supervisi.PelangganAir;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PelangganAirViewModel : ViewModelBase
    {
        public PelangganAirViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnOpenDetailPelangganCommand = new OnOpenDetailPelangganCommand(this, restApi);
            OnOpenPerbaruiDataRekeningCommand = new OnOpenPerbaruiDataRekeningCommand(this, restApi);
            OnOpenHapusSecaraAkuntansiCommand = new OnOpenHapusSecaraAkuntansiCommand(this, restApi);
            OnOpenPiutangCommand = new OnOpenPiutangCommand(this, restApi);
            OnOpenRiwayatPembayaranCommand = new OnOpenRiwayatPembayaranCommand(this, isTest);
            OnGetRiwayatPembayaranCommand = new OnGetRiwayatPembayaranCommand(this, restApi, isTest);
            OnOpenRiwayatPemakaianCommand = new OnOpenRiwayatPemakaianCommand(this, isTest);
            OnGetRiwayatPemakaianCommand = new OnGetRiwayatPemakaianCommand(this, restApi, isTest);
            OnOpenRiwayatMemoCommand = new OnOpenRiwayatMemoCommand(this, restApi);
            OnOpenKoreksiDataPelangganCommand = new OnOpenKoreksiDataPelangganCommand(this, restApi);
            OnSubmitPerbaruiDataRekeningCommand = new OnSubmitPerbaruiDataRekeningCommand(this, restApi);
            OnSubmitHapusSecaraAkuntansiCommand = new OnSubmitHapusSecaraAkuntansiCommand(this, restApi);
            OnOpenKoreksiMapCommand = new OnOpenKoreksiMapCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenDetailPelangganCommand { get; }
        public ICommand OnOpenPerbaruiDataRekeningCommand { get; }
        public ICommand OnOpenHapusSecaraAkuntansiCommand { get; }
        public ICommand OnOpenPiutangCommand { get; }
        public ICommand OnOpenRiwayatPembayaranCommand { get; }
        public ICommand OnGetRiwayatPembayaranCommand { get; }
        public ICommand OnOpenRiwayatPemakaianCommand { get; }
        public ICommand OnGetRiwayatPemakaianCommand { get; }
        public ICommand OnOpenRiwayatMemoCommand { get; }
        public ICommand OnOpenKoreksiDataPelangganCommand { get; }
        public ICommand OnSubmitPerbaruiDataRekeningCommand { get; }
        public ICommand OnSubmitHapusSecaraAkuntansiCommand { get; }
        public ICommand OnOpenKoreksiMapCommand { get; }

        #region HakAkses
        private bool _aksesSetUsulanHapusSecaraAkuntansi = PermissionHelper.HasPermission("Billing.Pelanggan_Air_SetUsulanHapusSecaraAkuntansi");
        public bool AksesSetUsulanHapusSecaraAkuntansi
        {
            get => _aksesSetUsulanHapusSecaraAkuntansi;
            set { _aksesSetUsulanHapusSecaraAkuntansi = value; OnPropertyChanged(); }
        }
        #endregion


        private ObservableCollection<MasterPelangganAirWpf> _masterPelangganList;
        public ObservableCollection<MasterPelangganAirWpf> MasterPelangganList
        {
            get { return _masterPelangganList; }
            set
            {
                _masterPelangganList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPiutangDto> _piutangList;
        public ObservableCollection<RekeningAirPiutangDto> PiutangList
        {
            get { return _piutangList; }
            set
            {
                _piutangList = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirWpf _selectedData;
        public MasterPelangganAirWpf SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDetailDto _detailData;
        public MasterPelangganAirDetailDto DetailData
        {
            get { return _detailData; }
            set
            {
                _detailData = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _pelangganForm;
        public MasterPelangganAirDto PelangganForm
        {
            get { return _pelangganForm; }
            set
            {
                _pelangganForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDetailDto _pelangganDetailForm;
        public MasterPelangganAirDetailDto PelangganDetailForm
        {
            get { return _pelangganDetailForm; }
            set
            {
                _pelangganDetailForm = value;
                OnPropertyChanged();
            }
        }

        private MasterPelangganAirDto _pelangganFilter = new MasterPelangganAirDto();
        public MasterPelangganAirDto PelangganFilter
        {
            get { return _pelangganFilter; }
            set
            {
                _pelangganFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdateMasterPelangganChecked;
        public bool IsUpdateMasterPelangganChecked
        {
            get { return _isUpdateMasterPelangganChecked; }
            set
            {
                _isUpdateMasterPelangganChecked = value;
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
                var temp = PelangganFilter;
                if (!value)
                {
                    temp.IdStatus = null;
                }
                PelangganFilter = temp;
                OnPropertyChanged();
            }
        }

        private bool _isFlagChecked;
        public bool IsFlagChecked
        {
            get { return _isFlagChecked; }
            set
            {
                _isFlagChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdFlag = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.Nama = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.NoSamb = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoRekeningChecked;
        public bool IsNoRekeningChecked
        {
            get { return _isNoRekeningChecked; }
            set
            {
                _isNoRekeningChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NoRekening = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdGolongan = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdKolektif = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.Alamat = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdWilayah = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdKelurahan = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdKecamatan = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdRayon = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBlokChecked;
        public bool IsBlokChecked
        {
            get { return _isBlokChecked; }
            set
            {
                _isBlokChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdBlok = null;
                    PelangganFilter = temp;
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
                    var temp = PelangganFilter;
                    temp.IdCabang = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isMerekMeterChecked;
        public bool IsMerekMeterChecked
        {
            get { return _isMerekMeterChecked; }
            set
            {
                _isMerekMeterChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdMerekMeter = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoSeriMeterChecked;
        public bool IsNoSeriMeterChecked
        {
            get { return _isNoSeriMeterChecked; }
            set
            {
                _isNoSeriMeterChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NoSeriMeter = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isDiameterChecked;
        public bool IsDiameterChecked
        {
            get { return _isDiameterChecked; }
            set
            {
                _isDiameterChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdDiameter = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNoSegelChecked;
        public bool IsNoSegelChecked
        {
            get { return _isNoSegelChecked; }
            set
            {
                _isNoSegelChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.NoSegel = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isKondisiMeterChecked;
        public bool IsKondisiMeterChecked
        {
            get { return _isKondisiMeterChecked; }
            set
            {
                _isKondisiMeterChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdKondisiMeter = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isSumberAirChecked;
        public bool IsSumberAirChecked
        {
            get { return _isSumberAirChecked; }
            set
            {
                _isSumberAirChecked = value;
                if (!value)
                {
                    var temp = PelangganFilter;
                    temp.IdSumberAir = null;
                    PelangganFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RiwayatPemakaianAirDto> _pemakaianList;
        public ObservableCollection<RiwayatPemakaianAirDto> PemakaianList
        {
            get { return _pemakaianList; }
            set
            {
                _pemakaianList = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
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

        private bool _isLoadingMap;
        public bool IsLoadingMap
        {
            get { return _isLoadingMap; }
            set
            {
                _isLoadingMap = value;
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

        private bool _isEmptyDialog;
        public bool IsEmptyDialog
        {
            get { return _isEmptyDialog; }
            set
            {
                _isEmptyDialog = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYearPerbaruiData = new List<int>();
        public List<int> ListYearPerbaruiData
        {
            get { return _listYearPerbaruiData; }
            set
            {
                _listYearPerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private int _yearPerbaruiData;
        public int YearPerbaruiData
        {
            get { return _yearPerbaruiData; }
            set
            {
                _yearPerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private int _monthPerbaruiData;
        public int MonthPerbaruiData
        {
            get { return _monthPerbaruiData; }
            set
            {
                _monthPerbaruiData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPiutangDto> _listPeriodeHapusAkuntansi = new ObservableCollection<RekeningAirPiutangDto>();
        public ObservableCollection<RekeningAirPiutangDto> ListPeriodeHapusAkuntansi
        {
            get { return _listPeriodeHapusAkuntansi; }
            set
            {
                _listPeriodeHapusAkuntansi = value;
                OnPropertyChanged();
            }
        }

        private int? _kodePeriodeHapusSecaraAkuntansi1;
        public int? KodePeriodeHapusSecaraAkuntansi1
        {
            get { return _kodePeriodeHapusSecaraAkuntansi1; }
            set
            {
                _kodePeriodeHapusSecaraAkuntansi1 = value;
                OnPropertyChanged();
            }
        }

        private int? _kodePeriodeHapusSecaraAkuntansi2;
        public int? KodePeriodeHapusSecaraAkuntansi2
        {
            get { return _kodePeriodeHapusSecaraAkuntansi2; }
            set
            {
                _kodePeriodeHapusSecaraAkuntansi2 = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterGolonganDto>? _golonganList;
        public ObservableCollection<MasterGolonganDto>? GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterGolongan));
            }
        }
        public bool LoadMasterGolongan { get => _golonganList == null; }

        private ObservableCollection<MasterDiameterDto>? _diameterList;
        public ObservableCollection<MasterDiameterDto>? DiameterList
        {
            get { return _diameterList; }
            set
            {
                _diameterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDiameter));
            }
        }
        public bool LoadMasterDiameter { get => _diameterList == null; }

        private ObservableCollection<MasterRayonDto>? _rayonList;
        public ObservableCollection<MasterRayonDto>? RayonList
        {
            get { return _rayonList; }
            set
            {
                _rayonList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRayon));
            }
        }
        public bool LoadMasterRayon { get => _rayonList == null; }

        private ObservableCollection<MasterKelurahanDto>? _kelurahanList;
        public ObservableCollection<MasterKelurahanDto>? KelurahanList
        {
            get { return _kelurahanList; }
            set
            {
                _kelurahanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKelurahan));
            }
        }
        public bool LoadMasterKelurahan { get => _kelurahanList == null; }

        private ObservableCollection<MasterKecamatanDto>? _kecamatanList;
        public ObservableCollection<MasterKecamatanDto>? KecamatanList
        {
            get { return _kecamatanList; }
            set
            {
                _kecamatanList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKecamatan));
            }
        }
        public bool LoadMasterKecamatan { get => _kecamatanList == null; }

        private ObservableCollection<MasterKolektifDto>? _kolektifList;
        public ObservableCollection<MasterKolektifDto>? KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKolektif));
            }
        }
        public bool LoadMasterKolektif { get => _kolektifList == null; }

        private ObservableCollection<MasterWilayahDto>? _wilayahList;
        public ObservableCollection<MasterWilayahDto>? WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterWilayah));
            }
        }
        public bool LoadMasterWilayah { get => _wilayahList == null; }

        private ObservableCollection<MasterCabangDto>? _cabangList;
        public ObservableCollection<MasterCabangDto>? CabangList
        {
            get { return _cabangList; }
            set
            {
                _cabangList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterCabang));
            }
        }
        public bool LoadMasterCabang { get => _cabangList == null; }

        private ObservableCollection<MasterSumberAirDto>? _sumberairList;
        public ObservableCollection<MasterSumberAirDto>? SumberAirList
        {
            get { return _sumberairList; }
            set
            {
                _sumberairList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterSumberAir));
            }
        }
        public bool LoadMasterSumberAir { get => _sumberairList == null; }

        private ObservableCollection<MasterDmaDto>? _dmaList;
        public ObservableCollection<MasterDmaDto>? DmaList
        {
            get { return _dmaList; }
            set
            {
                _dmaList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDma));
            }
        }
        public bool LoadMasterDma { get => _dmaList == null; }

        private ObservableCollection<MasterDmzDto>? _dmzList;
        public ObservableCollection<MasterDmzDto>? DmzList
        {
            get { return _dmzList; }
            set
            {
                _dmzList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterDmz));
            }
        }
        public bool LoadMasterDmz { get => _dmzList == null; }

        private ObservableCollection<MasterBlokDto>? _blokList;
        public ObservableCollection<MasterBlokDto>? BlokList
        {
            get { return _blokList; }
            set
            {
                _blokList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterBlok));
            }
        }
        public bool LoadMasterBlok { get => _blokList == null; }


        private ObservableCollection<MasterMerekMeterDto>? _merekmeterList;
        public ObservableCollection<MasterMerekMeterDto>? MerekMeterList
        {
            get { return _merekmeterList; }
            set
            {
                _merekmeterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterMerekMeter));
            }
        }
        public bool LoadMasterMerekMeter { get => _merekmeterList == null; }

        private ObservableCollection<MasterKondisiMeterDto>? _kondisimeterList;
        public ObservableCollection<MasterKondisiMeterDto>? KondisiMeterList
        {
            get { return _kondisimeterList; }
            set
            {
                _kondisimeterList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterKondisiMeter));
            }
        }
        public bool LoadMasterKondisiMeter { get => _kondisimeterList == null; }

        private ObservableCollection<MasterAdministrasiLainDto>? _administrasilainList;
        public ObservableCollection<MasterAdministrasiLainDto>? AdministrasiLainList
        {
            get { return _administrasilainList; }
            set
            {
                _administrasilainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterAdministrasiLain));
            }
        }
        public bool LoadMasterAdministrasiLain { get => _administrasilainList == null; }

        private ObservableCollection<MasterPemeliharaanLainDto>? _pemeliharaanlainList;
        public ObservableCollection<MasterPemeliharaanLainDto>? PemeliharaanLainList
        {
            get { return _pemeliharaanlainList; }
            set
            {
                _pemeliharaanlainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterPemeliharaanLain));
            }
        }
        public bool LoadMasterPemeliharaanLain { get => _pemeliharaanlainList == null; }

        private ObservableCollection<MasterRetribusiLainDto>? _retribusilainList;
        public ObservableCollection<MasterRetribusiLainDto>? RetribusiLainList
        {
            get { return _retribusilainList; }
            set
            {
                _retribusilainList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterRetribusiLain));
            }
        }
        public bool LoadMasterRetribusiLain { get => _retribusilainList == null; }

        private ObservableCollection<MasterStatusDto>? _statusList;
        public ObservableCollection<MasterStatusDto>? StatusList
        {
            get { return _statusList; }
            set
            {
                _statusList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterStatus));
            }
        }
        public bool LoadMasterStatus { get => _statusList == null; }

        private ObservableCollection<MasterFlagDto>? _flagList;
        public ObservableCollection<MasterFlagDto>? FlagList
        {
            get { return _flagList; }
            set
            {
                _flagList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterFlag));
            }
        }
        public bool LoadMasterFlag { get => _flagList == null; }

        private List<int> _listYearPemakaian = new List<int>();
        public List<int> ListYearPemakaian
        {
            get { return _listYearPemakaian; }
            set
            {
                _listYearPemakaian = value;
                OnPropertyChanged();
            }
        }

        private int? _yearPemakaian;
        public int? YearPemakaian
        {
            get { return _yearPemakaian; }
            set
            {
                _yearPemakaian = value;
                if (_yearPemakaian.HasValue)
                {
                    OnGetRiwayatPemakaianCommand.Execute(null);
                }
                OnPropertyChanged();
            }
        }

        #region Riwayat Pembayaran
        private int? _yearPembayaran;
        public int? YearPembayaran
        {
            get { return _yearPembayaran; }
            set
            {
                _yearPembayaran = value;
                if (_yearPembayaran.HasValue)
                {
                    OnGetRiwayatPembayaranCommand.Execute(null);
                }
                OnPropertyChanged();
            }
        }

        private bool _isEmptyRiwayatBayar = true;
        public bool IsEmptyRiwayatBayar
        {
            get { return _isEmptyRiwayatBayar; }
            set
            {
                _isEmptyRiwayatBayar = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmptyRiwayatBayarNonAir = true;
        public bool IsEmptyRiwayatBayarNonAir
        {
            get { return _isEmptyRiwayatBayarNonAir; }
            set
            {
                _isEmptyRiwayatBayarNonAir = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadRiwayatBayar = true;
        public bool IsLoadRiwayatBayar
        {
            get { return _isLoadRiwayatBayar; }
            set
            {
                _isLoadRiwayatBayar = value;
                OnPropertyChanged();
            }
        }

        private bool _isRiwayatTypeAir = true;
        public bool IsRiwayatTypeAir
        {
            get { return _isRiwayatTypeAir; }
            set
            {
                _isRiwayatTypeAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalRekAirRiwayatBayarAir = decimal.Zero;
        public decimal TotalRekAirRiwayatBayarAir
        {
            get { return _totalRekAirRiwayatBayarAir; }
            set
            {
                _totalRekAirRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalDendaRiwayatBayarAir = decimal.Zero;
        public decimal TotalDendaRiwayatBayarAir
        {
            get { return _totalDendaRiwayatBayarAir; }
            set
            {
                _totalDendaRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalJumlahRiwayatBayarAir = decimal.Zero;
        public decimal TotalJumlahRiwayatBayarAir
        {
            get { return _totalJumlahRiwayatBayarAir; }
            set
            {
                _totalJumlahRiwayatBayarAir = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalJumlahRiwayatBayarNonAir = decimal.Zero;
        public decimal TotalJumlahRiwayatBayarNonAir
        {
            get { return _totalJumlahRiwayatBayarNonAir; }
            set
            {
                _totalJumlahRiwayatBayarNonAir = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPelunasanDanPembatalanDto> _pembayaranList;
        public ObservableCollection<RekeningAirPelunasanDanPembatalanDto> PembayaranList
        {
            get { return _pembayaranList; }
            set
            {
                _pembayaranList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningNonAirPelunasanDanPembatalanDto> _pembayaranNonAirList;
        public ObservableCollection<RekeningNonAirPelunasanDanPembatalanDto> PembayaranNonAirList
        {
            get { return _pembayaranNonAirList; }
            set
            {
                _pembayaranNonAirList = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYearPembayaran = new List<int>();
        public List<int> ListYearPembayaran
        {
            get { return _listYearPembayaran; }
            set
            {
                _listYearPembayaran = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
