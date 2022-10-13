using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Hublang.Pelayanan.BAPengembalian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Hublang.Pelayanan
{
    public class BaPengembalianViewModel : ViewModelBase
    {

        public BaPengembalianViewModel(PelayananViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parentViewModel;
            DataList = new ObservableCollection<RekeningAirPengembalianDto>();
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenPilihPelangganCommand = new OnOpenPilihPelangganCommand(this, restApi, isTest);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this, isTest);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi, isTest);
            OnHitungCommand = new OnHitungCommand(this, restApi, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnShowConfirmSubmitCommand = new OnShowConfirmSubmitCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnOpenDetailFormCommand = new OnOpenDetailFormCommand(this, isTest);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnExportExcelCommand = new OnExportExcelCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportExcelCommand { get; }
        public ICommand OnOpenDetailFormCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenPilihPelangganCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnHitungCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnShowConfirmSubmitCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }

        private bool _isAdd = true;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
                OnPropertyChanged();
            }
        }

        private ParamRekeningAirPengembalianDto _paramSubmit;
        public ParamRekeningAirPengembalianDto ParamSubmit
        {
            get { return _paramSubmit; }
            set
            {
                _paramSubmit = value;
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

        private ResultKalkulasiRekeningAirDto _kalkulasiRekening;
        public ResultKalkulasiRekeningAirDto KalkulasiRekening
        {
            get { return _kalkulasiRekening; }
            set
            {
                _kalkulasiRekening = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<RekeningAirPelunasanDanPembatalanDto> _periodeTransaksiList;
        public ObservableCollection<RekeningAirPelunasanDanPembatalanDto> PeriodeTransaksiList
        {
            get { return _periodeTransaksiList; }
            set
            {
                _periodeTransaksiList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirPelunasanDanPembatalanDto _selectedPeriodeTransaksi = new RekeningAirPelunasanDanPembatalanDto();
        public RekeningAirPelunasanDanPembatalanDto SelectedPeriodeTransaksi
        {
            get { return _selectedPeriodeTransaksi; }
            set
            {
                _selectedPeriodeTransaksi = value;
                OnPropertyChanged();
            }
        }

        private DateTime _selectedTanggalBA;
        public DateTime SelectedTanggalBA
        {
            get { return _selectedTanggalBA; }
            set
            {
                _selectedTanggalBA = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterKondisiMeterDto> _kondisiMeterList;
        public ObservableCollection<MasterKondisiMeterDto> KondisiMeterList
        {
            get { return _kondisiMeterList; }
            set
            {
                _kondisiMeterList = value;
                OnPropertyChanged();
            }
        }

        private MasterKondisiMeterDto _selectedKondisiMeter;
        public MasterKondisiMeterDto SelectedKondisiMeter
        {
            get { return _selectedKondisiMeter; }
            set
            {
                _selectedKondisiMeter = value;
                OnPropertyChanged();
            }
        }



        private ObservableCollection<MasterPegawaiDto> _masterPegawaiList;
        public ObservableCollection<MasterPegawaiDto> MasterPegawaiList
        {
            get { return _masterPegawaiList; }
            set
            {
                _masterPegawaiList = value;
                OnPropertyChanged();
            }
        }

        private MasterPegawaiDto _selectedMasterPegawai;
        public MasterPegawaiDto SelectedMasterPegawai
        {
            get { return _selectedMasterPegawai; }
            set
            {
                _selectedMasterPegawai = value;
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

        private RekeningAirPengembalianDto _selectedData;
        public RekeningAirPengembalianDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirPengembalianDto> _dataList;
        public ObservableCollection<RekeningAirPengembalianDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        #region enable/disable filter

        private bool _isNomorBaChecked;
        public bool IsNomorBaChecked
        {
            get { return _isNomorBaChecked; }
            set
            {
                _isNomorBaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTanggalBaChecked;
        public bool IsTanggalBaChecked
        {
            get { return _isTanggalBaChecked; }
            set
            {
                _isTanggalBaChecked = value;
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

        private bool _isUserVerifikasiChecked;
        public bool IsUserVerifikasiChecked
        {
            get { return _isUserVerifikasiChecked; }
            set
            {
                _isUserVerifikasiChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region filter data list

        private ObservableCollection<MasterFlagDto> _flagList = new ObservableCollection<MasterFlagDto>();
        public ObservableCollection<MasterFlagDto> FlagList
        {
            get { return _flagList; }
            set
            {
                _flagList = value;
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


        #region filter variable

        private string _filterNomorBa;
        public string FilterNomorBa
        {
            get { return _filterNomorBa; }
            set
            {
                _filterNomorBa = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalBaAwal;
        public DateTime? FilterTanggalBaAwal
        {
            get { return _filterTanggalBaAwal; }
            set
            {
                _filterTanggalBaAwal = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTanggalBaAkhir;
        public DateTime? FilterTanggalBaAkhir
        {
            get { return _filterTanggalBaAkhir; }
            set
            {
                _filterTanggalBaAkhir = value;
                OnPropertyChanged();
            }
        }

        private MasterFlagDto _filterFlag;
        public MasterFlagDto FilterFlag
        {
            get { return _filterFlag; }
            set
            {
                _filterFlag = value;
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

        private MasterUserDto _filterUserVerifikasi;
        public MasterUserDto FilterUserVerifikasi
        {
            get { return _filterUserVerifikasi; }
            set
            {
                _filterUserVerifikasi = value;
                OnPropertyChanged();
            }
        }

        #endregion

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
                OnLoadCommand.Execute(null);
            }
        }

        #region prev next page data pengaduan
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
