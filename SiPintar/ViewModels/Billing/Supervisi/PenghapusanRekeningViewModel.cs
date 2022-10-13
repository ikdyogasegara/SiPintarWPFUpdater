using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Supervisi.PenghapusanRekening;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PenghapusanRekeningViewModel : ViewModelBase
    {
        public PenghapusanRekeningViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnLoadCommand = new OnLoadCommand(this);
            OnOpenVerifikasiHapusRekeningCommand = new OnOpenVerifikasiHapusRekeningCommand(this);
            OnOpenConfirmVerifikasiCommand = new OnOpenConfirmVerifikasiCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnSubmitVerifikasiHapusRekeningCommand = new OnSubmitVerifikasiHapusRekeningCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitSettingTableCommand = new OnSubmitSettingTableCommand(this);
            OnSearchPiutangCommand = new OnSearchPiutangCommand(this, restApi);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
        }

        public ICommand OnFilterCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenVerifikasiHapusRekeningCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenConfirmVerifikasiCommand { get; }
        public ICommand OnOpenSettingTableFormCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnSubmitVerifikasiHapusRekeningCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitSettingTableCommand { get; }
        public ICommand OnSearchPiutangCommand { get; }
        public ICommand OnToggleFilterCommand { get; }

        #region HakAkses
        private bool _aksesTambah = PermissionHelper.HasPermission("Billing.Penghapusan_Rekening_Tambah");
        public bool AksesTambah
        {
            get => _aksesTambah;
            set { _aksesTambah = value; OnPropertyChanged(); }
        }

        private bool _aksesBatalkan = PermissionHelper.HasPermission("Billing.Penghapusan_Rekening_Batalkan");
        public bool AksesBatalkan
        {
            get => _aksesBatalkan;
            set { _aksesBatalkan = value; OnPropertyChanged(); }
        }

        private bool _aksesVerifikasi = PermissionHelper.HasPermission("Billing.Penghapusan_Rekening_Verifikasi");
        public bool AksesVerifikasi
        {
            get => _aksesVerifikasi;
            set { _aksesVerifikasi = value; OnPropertyChanged(); }
        }
        #endregion

        private bool _isUsulan;
        public bool IsUsulan
        {
            get { return _isUsulan; }
            set
            {
                _isUsulan = value;
                OnPropertyChanged();
            }
        }

        private bool _isHapusSecaraAkuntansi;
        public bool IsHapusSecaraAkuntansi
        {
            get { return _isHapusSecaraAkuntansi; }
            set
            {
                _isHapusSecaraAkuntansi = value;
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

        private RekeningAirHapusSecaraAkuntansiDto _selectedData;
        public RekeningAirHapusSecaraAkuntansiDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
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

        private ParamRekeningAirHapusSecaraAkuntansiDto _filter = new ParamRekeningAirHapusSecaraAkuntansiDto();
        public ParamRekeningAirHapusSecaraAkuntansiDto Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<RekeningAirHapusSecaraAkuntansiWpf> _rekeningAirHapusSecaraAkuntansiList;
        public ObservableCollection<RekeningAirHapusSecaraAkuntansiWpf> RekeningAirHapusSecaraAkuntansiList
        {
            get { return _rekeningAirHapusSecaraAkuntansiList; }
            set
            {
                _rekeningAirHapusSecaraAkuntansiList = value;
                OnPropertyChanged();
            }
        }

        private string _noSambPiutang;
        public string NoSambPiutang
        {
            get { return _noSambPiutang; }
            set
            {
                _noSambPiutang = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelectAllPiutang;
        public bool IsSelectAllPiutang
        {
            get { return _isSelectAllPiutang; }
            set
            {
                _isSelectAllPiutang = value;
                OnPropertyChanged();

                var temp = new ObservableCollection<RekeningAirPiutangWpf>(PiutangList);
                foreach (var item in temp)
                    item.IsSelected = _isSelectAllPiutang;

                PiutangList = temp;
            }
        }

        private ObservableCollection<RekeningAirPiutangWpf> _piutangList;
        public ObservableCollection<RekeningAirPiutangWpf> PiutangList
        {
            get { return _piutangList; }
            set
            {
                _piutangList = value;
                OnPropertyChanged();
            }
        }

        private RekeningAirPiutangWpf _firstPiutang;
        public RekeningAirPiutangWpf FirstPiutang
        {
            get { return _firstPiutang; }
            set
            {
                _firstPiutang = value;
                OnPropertyChanged();
            }
        }

        private decimal? _totalPiutang;
        public decimal? TotalPiutang
        {
            get { return _totalPiutang; }
            set
            {
                _totalPiutang = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglHapus;
        public DateTime? TglHapus
        {
            get { return _tglHapus; }
            set
            {
                _tglHapus = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
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

        #region Filter Combobox List

        private ObservableCollection<int?> _tahunPeriodeList;
        public ObservableCollection<int?> TahunPeriodeList
        {
            get { return _tahunPeriodeList; }
            set
            {
                _tahunPeriodeList = value;
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

        #endregion

        #region Filter Checkbox
        private bool _isPeriodeChecked;
        public bool IsPeriodeChecked
        {
            get { return _isPeriodeChecked; }
            set
            {
                _isPeriodeChecked = value;
                if (!value)
                {
                    var temp = Filter;
                    temp.TahunPeriode = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.NoSamb = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.Nama = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.Alamat = null;
                    Filter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isRekeningAirChecked;
        public bool IsRekeningAirChecked
        {
            get { return _isRekeningAirChecked; }
            set
            {
                _isRekeningAirChecked = value;
                if (!value)
                {
                    var temp = Filter;
                    temp.RekAirAwal = null;
                    temp.RekAirAkhir = null;
                    Filter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isPakaiChecked;
        public bool IsPakaiChecked
        {
            get { return _isPakaiChecked; }
            set
            {
                _isPakaiChecked = value;
                if (!value)
                {
                    var temp = Filter;
                    temp.PakaiAwal = null;
                    temp.PakaiAkhir = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.IdKelurahan = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.IdRayon = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.IdWilayah = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.IdGolongan = null;
                    Filter = temp;
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
                    var temp = Filter;
                    temp.IdCabang = null;
                    Filter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isTglHapusChecked;
        public bool IsTglHapusChecked
        {
            get { return _isTglHapusChecked; }
            set
            {
                _isTglHapusChecked = value;
                if (!value)
                {
                    var temp = Filter;
                    temp.TglHapusSecaraAkuntansiAwal = null;
                    temp.TglHapusSecaraAkuntansiAkhir = null;
                    Filter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isLainnyaChecked;
        public bool IsLainnyaChecked
        {
            get { return _isLainnyaChecked; }
            set
            {
                _isLainnyaChecked = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region pagination

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

    public class RekeningAirHapusSecaraAkuntansiWpf : RekeningAirHapusSecaraAkuntansiDto
    {
        public string GroupContent { get => $"{NoSamb} - {Nama}"; }

    }
}
