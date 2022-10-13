using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Tunjangan.TunjanganBulanan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Tunjangan
{
    public class TunjanganBulananViewModel : ViewModelBase
    {
        public TunjanganBulananViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPeriodeCommand = new OnOpenSelectPeriodeCommand(this, restApi, isTest);
            OnSelectPeriodeCommand = new OnSelectPeriodeCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnSelectPegawaiCommand = new OnSelectPegawaiCommand(this, isTest);
            OnNextFormPageCommand = new OnNextFormPageCommand(this, isTest);
            OnPreviousFormPageCommand = new OnPreviousFormPageCommand(this);
            OnConfirmDeleteFormCommand = new OnConfirmDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenSelectPeriodeCommand { get; }
        public ICommand OnSelectPeriodeCommand { get; }
        public ICommand OnOpenSelectPegawaiCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnSelectPegawaiCommand { get; }
        public ICommand OnNextFormPageCommand { get; }
        public ICommand OnPreviousFormPageCommand { get; }
        public ICommand OnConfirmDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isFilterVisible = true;
        public bool IsFilterVisible
        {
            get { return _isFilterVisible; }
            set { _isFilterVisible = value; OnPropertyChanged(); }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEdit = true;
        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private string _periodeTerpilih;
        public string PeriodeTerpilih
        {
            get { return _periodeTerpilih; }
            set { _periodeTerpilih = value; OnPropertyChanged(); }
        }

        private string _kodeGajiTerpilih;
        public string KodeGajiTerpilih
        {
            get { return _kodeGajiTerpilih; }
            set { _kodeGajiTerpilih = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterSetTunjanganBulananWpf> _tunjanganBulananList;
        public ObservableCollection<MasterSetTunjanganBulananWpf> TunjanganBulananList
        {
            get { return _tunjanganBulananList; }
            set { _tunjanganBulananList = value; OnPropertyChanged(); }
        }

        private MasterSetTunjanganBulananDto _selectedData;
        public MasterSetTunjanganBulananDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _pegawaiList;
        public ObservableCollection<MasterPegawaiDto> PegawaiList
        {
            get { return _pegawaiList; }
            set { _pegawaiList = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedDataPegawai;
        public MasterPegawaiDto SelectedDataPegawai
        {
            get { return _selectedDataPegawai; }
            set { _selectedDataPegawai = value; OnPropertyChanged(); }
        }

        private bool _deleteMode1 = true;
        public bool DeleteMode1
        {
            get { return _deleteMode1; }
            set { _deleteMode1 = value; OnPropertyChanged(); }
        }

        private bool _deleteMode2;
        public bool DeleteMode2
        {
            get { return _deleteMode2; }
            set { _deleteMode2 = value; OnPropertyChanged(); }
        }

        private bool _deleteMode3;
        public bool DeleteMode3
        {
            get { return _deleteMode3; }
            set { _deleteMode3 = value; OnPropertyChanged(); }
        }

        #region Form Data
        private int _formCurrentPage = 1;
        public int FormCurrentPage
        {
            get { return _formCurrentPage; }
            set { _formCurrentPage = value; OnPropertyChanged(); }
        }

        #region Page 1
        private int? _formJenisTunjangan;
        public int? FormJenisTunjangan
        {
            get { return _formJenisTunjangan; }
            set { _formJenisTunjangan = value; OnPropertyChanged(); }
        }

        private bool? _formFlagAbsensi;
        public bool? FormFlagAbsensi
        {
            get { return _formFlagAbsensi; }
            set { _formFlagAbsensi = value; OnPropertyChanged(); }
        }

        private int? _formUpahKehadiran;
        public int? FormUpahKehadiran
        {
            get { return _formUpahKehadiran; }
            set { _formUpahKehadiran = value; OnPropertyChanged(); }
        }

        private decimal? _formNominal;
        public decimal? FormNominal
        {
            get { return _formNominal; }
            set { _formNominal = value; OnPropertyChanged(); }
        }

        private string _formBeban;
        public string FormBeban
        {
            get { return _formBeban; }
            set { _formBeban = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPersen;
        public bool? FormFlagPersen
        {
            get { return _formFlagPersen; }
            set
            {
                _formFlagPersen = value;
                if (value == false)
                {
                    FormIsNominalMinChecked = false;
                    FormIsNominalMaxChecked = false;
                }
                OnPropertyChanged();
            }
        }

        private string _formPersenDari;
        public string FormPersenDari
        {
            get { return _formPersenDari; }
            set { _formPersenDari = value; OnPropertyChanged(); }
        }

        private bool? _formIsNominalMinChecked;
        public bool? FormIsNominalMinChecked
        {
            get { return _formIsNominalMinChecked; }
            set
            {
                _formIsNominalMinChecked = value;
                if (value == false)
                    FormNominalMin = 0;
                OnPropertyChanged();
            }
        }

        private decimal? _formNominalMin;
        public decimal? FormNominalMin
        {
            get { return _formNominalMin; }
            set { _formNominalMin = value; OnPropertyChanged(); }
        }

        private bool? _formIsNominalMaxChecked;
        public bool? FormIsNominalMaxChecked
        {
            get { return _formIsNominalMaxChecked; }
            set
            {
                _formIsNominalMaxChecked = value;
                if (value == false)
                    FormNominalMax = 0;
                OnPropertyChanged();
            }
        }

        private decimal? _formNominalMax;
        public decimal? FormNominalMax
        {
            get { return _formNominalMax; }
            set { _formNominalMax = value; OnPropertyChanged(); }
        }

        private string _formKeterangan;
        public string FormKeterangan
        {
            get { return _formKeterangan; }
            set { _formKeterangan = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPotongPph;
        public bool? FormFlagPotongPph
        {
            get { return _formFlagPotongPph; }
            set { _formFlagPotongPph = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPersenPph;
        public bool? FormFlagPersenPph
        {
            get { return _formFlagPersenPph; }
            set { _formFlagPersenPph = value; OnPropertyChanged(); }
        }

        private decimal? _formNominalPph;
        public decimal? FormNominalPph
        {
            get { return _formNominalPph; }
            set { _formNominalPph = value; OnPropertyChanged(); }
        }
        #endregion

        #region Page 2
        private bool? _formFlagSemuaPegawai;
        public bool? FormFlagSemuaPegawai
        {
            get { return _formFlagSemuaPegawai; }
            set { _formFlagSemuaPegawai = value; OnPropertyChanged(); }
        }

        private bool _formIsAgamaChecked;
        public bool FormIsAgamaChecked
        {
            get { return _formIsAgamaChecked; }
            set { _formIsAgamaChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsAreaKerjaChecked;
        public bool FormIsAreaKerjaChecked
        {
            get { return _formIsAreaKerjaChecked; }
            set { _formIsAreaKerjaChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsDepartemenChecked;
        public bool FormIsDepartemenChecked
        {
            get { return _formIsDepartemenChecked; }
            set { _formIsDepartemenChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsDivisiChecked;
        public bool FormIsDivisiChecked
        {
            get { return _formIsDivisiChecked; }
            set { _formIsDivisiChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsGolonganPegawaiChecked;
        public bool FormIsGolonganPegawaiChecked
        {
            get { return _formIsGolonganPegawaiChecked; }
            set { _formIsGolonganPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsMkgChecked;
        public bool FormIsMkgChecked
        {
            get { return _formIsMkgChecked; }
            set { _formIsMkgChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsJabatanChecked;
        public bool FormIsJabatanChecked
        {
            get { return _formIsJabatanChecked; }
            set { _formIsJabatanChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsJenisKelaminChecked;
        public bool FormIsJenisKelaminChecked
        {
            get { return _formIsJenisKelaminChecked; }
            set { _formIsJenisKelaminChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsPegawaiChecked;
        public bool FormIsPegawaiChecked
        {
            get { return _formIsPegawaiChecked; }
            set { _formIsPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsPendidikanChecked;
        public bool FormIsPendidikanChecked
        {
            get { return _formIsPendidikanChecked; }
            set { _formIsPendidikanChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsTipeKeluargaChecked;
        public bool FormIsTipeKeluargaChecked
        {
            get { return _formIsTipeKeluargaChecked; }
            set { _formIsTipeKeluargaChecked = value; OnPropertyChanged(); }
        }

        private bool _formIsStatusPegawaiChecked;
        public bool FormIsStatusPegawaiChecked
        {
            get { return _formIsStatusPegawaiChecked; }
            set { _formIsStatusPegawaiChecked = value; OnPropertyChanged(); }
        }

        private int? _formAgama;
        public int? FormAgama
        {
            get { return _formAgama; }
            set { _formAgama = value; OnPropertyChanged(); }
        }

        private int? _formAreaKerja;
        public int? FormAreaKerja
        {
            get { return _formAreaKerja; }
            set { _formAreaKerja = value; OnPropertyChanged(); }
        }

        private int? _formDepartemen;
        public int? FormDepartemen
        {
            get { return _formDepartemen; }
            set { _formDepartemen = value; OnPropertyChanged(); }
        }

        private int? _formDivisi;
        public int? FormDivisi
        {
            get { return _formDivisi; }
            set { _formDivisi = value; OnPropertyChanged(); }
        }

        private int? _formGolonganPegawai;
        public int? FormGolonganPegawai
        {
            get { return _formGolonganPegawai; }
            set { _formGolonganPegawai = value; OnPropertyChanged(); }
        }

        private int? _formMkg;
        public int? FormMkg
        {
            get { return _formMkg; }
            set { _formMkg = value; OnPropertyChanged(); }
        }

        private int? _formJabatan;
        public int? FormJabatan
        {
            get { return _formJabatan; }
            set { _formJabatan = value; OnPropertyChanged(); }
        }

        private int? _formJenisKelamin;
        public int? FormJenisKelamin
        {
            get { return _formJenisKelamin; }
            set { _formJenisKelamin = value; OnPropertyChanged(); }
        }

        private int? _formPegawai;
        public int? FormPegawai
        {
            get { return _formPegawai; }
            set { _formPegawai = value; OnPropertyChanged(); }
        }

        private int? _formPendidikan;
        public int? FormPendidikan
        {
            get { return _formPendidikan; }
            set { _formPendidikan = value; OnPropertyChanged(); }
        }

        private int? _formTipeKeluarga;
        public int? FormTipeKeluarga
        {
            get { return _formTipeKeluarga; }
            set { _formTipeKeluarga = value; OnPropertyChanged(); }
        }

        private int? _formStatusPegawai;
        public int? FormStatusPegawai
        {
            get { return _formStatusPegawai; }
            set { _formStatusPegawai = value; OnPropertyChanged(); }
        }


        #endregion

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }
        #endregion

        #region Form Combobox Data
        private ObservableCollection<MasterJenisTunjanganDto> _jenisTunjanganListForm;
        public ObservableCollection<MasterJenisTunjanganDto> JenisTunjanganListForm
        {
            get { return _jenisTunjanganListForm; }
            set { _jenisTunjanganListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterUpahKehadiranDto> _upahKehadiranListForm;
        public ObservableCollection<MasterUpahKehadiranDto> UpahKehadiranListForm
        {
            get { return _upahKehadiranListForm; }
            set { _upahKehadiranListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAgamaDto> _agamaListForm;
        public ObservableCollection<MasterAgamaDto> AgamaListForm
        {
            get { return _agamaListForm; }
            set { _agamaListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAreaKerjaDto> _areaKerjaListForm;
        public ObservableCollection<MasterAreaKerjaDto> AreaKerjaListForm
        {
            get { return _areaKerjaListForm; }
            set { _areaKerjaListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDepartemenDto> _departemenListForm;
        public ObservableCollection<MasterDepartemenDto> DepartemenListForm
        {
            get { return _departemenListForm; }
            set { _departemenListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDivisiDto> _divisiListForm;
        public ObservableCollection<MasterDivisiDto> DivisiListForm
        {
            get { return _divisiListForm; }
            set { _divisiListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganPegawaiDto> _golonganPegawaiListForm;
        public ObservableCollection<MasterGolonganPegawaiDto> GolonganPegawaiListForm
        {
            get { return _golonganPegawaiListForm; }
            set { _golonganPegawaiListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJabatanDto> _jabatanListForm;
        public ObservableCollection<MasterJabatanDto> JabatanListForm
        {
            get { return _jabatanListForm; }
            set { _jabatanListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisKelaminDto> _jenisKelaminListForm;
        public ObservableCollection<MasterJenisKelaminDto> JenisKelaminListForm
        {
            get { return _jenisKelaminListForm; }
            set { _jenisKelaminListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPendidikanDto> _pendidikanListForm;
        public ObservableCollection<MasterPendidikanDto> PendidikanListForm
        {
            get { return _pendidikanListForm; }
            set { _pendidikanListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterTipeKeluargaDto> _tipeKeluargaListForm;
        public ObservableCollection<MasterTipeKeluargaDto> TipeKeluargaListForm
        {
            get { return _tipeKeluargaListForm; }
            set { _tipeKeluargaListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterStatusPegawaiDto> _statusPegawaiListForm;
        public ObservableCollection<MasterStatusPegawaiDto> StatusPegawaiListForm
        {
            get { return _statusPegawaiListForm; }
            set { _statusPegawaiListForm = value; OnPropertyChanged(); }
        }
        #endregion

        #region Filter Combobox Data
        private ObservableCollection<MasterPeriodeDto> _periodeList;
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get { return _periodeList; }
            set { _periodeList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKodeGajiDto> _kodeGajiList;
        public ObservableCollection<MasterKodeGajiDto> KodeGajiList
        {
            get { return _kodeGajiList; }
            set { _kodeGajiList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAgamaDto> _agamaList;
        public ObservableCollection<MasterAgamaDto> AgamaList
        {
            get { return _agamaList; }
            set { _agamaList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAreaKerjaDto> _areaKerjaList;
        public ObservableCollection<MasterAreaKerjaDto> AreaKerjaList
        {
            get { return _areaKerjaList; }
            set { _areaKerjaList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDepartemenDto> _departemenList;
        public ObservableCollection<MasterDepartemenDto> DepartemenList
        {
            get { return _departemenList; }
            set { _departemenList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDivisiDto> _divisiList;
        public ObservableCollection<MasterDivisiDto> DivisiList
        {
            get { return _divisiList; }
            set { _divisiList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganPegawaiDto> _golonganPegawaiList;
        public ObservableCollection<MasterGolonganPegawaiDto> GolonganPegawaiList
        {
            get { return _golonganPegawaiList; }
            set { _golonganPegawaiList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJabatanDto> _jabatanList;
        public ObservableCollection<MasterJabatanDto> JabatanList
        {
            get { return _jabatanList; }
            set { _jabatanList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisKelaminDto> _jenisKelaminList;
        public ObservableCollection<MasterJenisKelaminDto> JenisKelaminList
        {
            get { return _jenisKelaminList; }
            set { _jenisKelaminList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPendidikanDto> _pendidikanList;
        public ObservableCollection<MasterPendidikanDto> PendidikanList
        {
            get { return _pendidikanList; }
            set { _pendidikanList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterTipeKeluargaDto> _tipeKeluargaList;
        public ObservableCollection<MasterTipeKeluargaDto> TipeKeluargaList
        {
            get { return _tipeKeluargaList; }
            set { _tipeKeluargaList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterStatusPegawaiDto> _statusPegawaiList;
        public ObservableCollection<MasterStatusPegawaiDto> StatusPegawaiList
        {
            get { return _statusPegawaiList; }
            set { _statusPegawaiList = value; OnPropertyChanged(); }
        }
        #endregion

        #region Filter
        private bool _isNoPegawaiChecked;
        public bool IsNoPegawaiChecked
        {
            get { return _isNoPegawaiChecked; }
            set { _isNoPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _isNamaPegawaiChecked;
        public bool IsNamaPegawaiChecked
        {
            get { return _isNamaPegawaiChecked; }
            set { _isNamaPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _isAgamaChecked;
        public bool IsAgamaChecked
        {
            get { return _isAgamaChecked; }
            set { _isAgamaChecked = value; OnPropertyChanged(); }
        }

        private bool _isAreaKerjaChecked;
        public bool IsAreaKerjaChecked
        {
            get { return _isAreaKerjaChecked; }
            set { _isAreaKerjaChecked = value; OnPropertyChanged(); }
        }

        private bool _isDepartemenChecked;
        public bool IsDepartemenChecked
        {
            get { return _isDepartemenChecked; }
            set { _isDepartemenChecked = value; OnPropertyChanged(); }
        }

        private bool _isDivisiChecked;
        public bool IsDivisiChecked
        {
            get { return _isDivisiChecked; }
            set { _isDivisiChecked = value; OnPropertyChanged(); }
        }

        private bool _isGolonganPegawaiChecked;
        public bool IsGolonganPegawaiChecked
        {
            get { return _isGolonganPegawaiChecked; }
            set { _isGolonganPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _isJabatanChecked;
        public bool IsJabatanChecked
        {
            get { return _isJabatanChecked; }
            set { _isJabatanChecked = value; OnPropertyChanged(); }
        }

        private bool _isJenisKelaminChecked;
        public bool IsJenisKelaminChecked
        {
            get { return _isJenisKelaminChecked; }
            set { _isJenisKelaminChecked = value; OnPropertyChanged(); }
        }

        private bool _isPendidikanChecked;
        public bool IsPendidikanChecked
        {
            get { return _isPendidikanChecked; }
            set { _isPendidikanChecked = value; OnPropertyChanged(); }
        }

        private bool _isTipeKeluargaChecked;
        public bool IsTipeKeluargaChecked
        {
            get { return _isTipeKeluargaChecked; }
            set { _isTipeKeluargaChecked = value; OnPropertyChanged(); }
        }

        private bool _isStatusPegawaiChecked;
        public bool IsStatusPegawaiChecked
        {
            get { return _isStatusPegawaiChecked; }
            set { _isStatusPegawaiChecked = value; OnPropertyChanged(); }
        }

        private MasterPeriodeDto _filterPeriode;
        public MasterPeriodeDto FilterPeriode
        {
            get { return _filterPeriode; }
            set { _filterPeriode = value; OnPropertyChanged(); }
        }

        private MasterKodeGajiDto _filterKodeGaji;
        public MasterKodeGajiDto FilterKodeGaji
        {
            get { return _filterKodeGaji; }
            set { _filterKodeGaji = value; OnPropertyChanged(); }
        }

        private string _filterNoPegawai;
        public string FilterNoPegawai
        {
            get { return _filterNoPegawai; }
            set { _filterNoPegawai = value; OnPropertyChanged(); }
        }

        private string _filterNamaPegawai;
        public string FilterNamaPegawai
        {
            get { return _filterNamaPegawai; }
            set { _filterNamaPegawai = value; OnPropertyChanged(); }
        }

        private int? _filterAgama;
        public int? FilterAgama
        {
            get { return _filterAgama; }
            set { _filterAgama = value; OnPropertyChanged(); }
        }

        private int? _filterAreaKerja;
        public int? FilterAreaKerja
        {
            get { return _filterAreaKerja; }
            set { _filterAreaKerja = value; OnPropertyChanged(); }
        }

        private int? _filterDepartemen;
        public int? FilterDepartemen
        {
            get { return _filterDepartemen; }
            set { _filterDepartemen = value; OnPropertyChanged(); }
        }

        private int? _filterDivisi;
        public int? FilterDivisi
        {
            get { return _filterDivisi; }
            set { _filterDivisi = value; OnPropertyChanged(); }
        }

        private int? _filterJabatan;
        public int? FilterJabatan
        {
            get { return _filterJabatan; }
            set { _filterJabatan = value; OnPropertyChanged(); }
        }

        private int? _filterGolonganPegawai;
        public int? FilterGolonganPegawai
        {
            get { return _filterGolonganPegawai; }
            set { _filterGolonganPegawai = value; OnPropertyChanged(); }
        }

        private int? _filterJenisKelamin;
        public int? FilterJenisKelamin
        {
            get { return _filterJenisKelamin; }
            set { _filterJenisKelamin = value; OnPropertyChanged(); }
        }

        private int? _filterPendidikan;
        public int? FilterPendidikan
        {
            get { return _filterPendidikan; }
            set { _filterPendidikan = value; OnPropertyChanged(); }
        }

        private int? _filterTipeKeluarga;
        public int? FilterTipeKeluarga
        {
            get { return _filterTipeKeluarga; }
            set { _filterTipeKeluarga = value; OnPropertyChanged(); }
        }

        private int? _filterStatusPegawai;
        public int? FilterStatusPegawai
        {
            get { return _filterStatusPegawai; }
            set { _filterStatusPegawai = value; OnPropertyChanged(); }
        }

        #endregion
    }

    public class MasterSetTunjanganBulananWpf : MasterSetTunjanganBulananDto
    {
        public string Berdasarkan
        {
            get
            {
                var joined = new ObservableCollection<string>();
                if (IdAgama != null)
                    joined.Add("AGAMA");
                if (IdAreaKerja != null)
                    joined.Add("AREA KERJA");
                if (IdDepartemen != null)
                    joined.Add("DEPARTEMEN");
                if (IdDivisi != null)
                    joined.Add("DIVISI");
                if (IdGolonganPegawai != null)
                    joined.Add("GOLONGAN");
                if (Mkg != null)
                    joined.Add("MKG");
                if (IdJabatan != null)
                    joined.Add("JABATAN");
                if (IdJenisKelamin != null)
                    joined.Add("JENIS KELAMIN");
                if (IdPegawai != null)
                    joined.Add("PEGAWAI");
                if (IdPendidikan != null)
                    joined.Add("PENDIDIKAN");
                if (IdTipeKeluarga != null)
                    joined.Add("STATUS KELUARGA");
                if (IdStatusPegawai != null)
                    joined.Add("STATUS PEGAWAI");

                return string.Join("; ", joined);
            }
        }

    }
}
