using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.GajiPokok.TenagaHarian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.GajiPokok
{
    public class TenagaHarianViewModel : ViewModelBase
    {
        public TenagaHarianViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnSelectPegawaiCommand = new OnSelectPegawaiCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenSelectPegawaiCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnSelectPegawaiCommand { get; }
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

        private ObservableCollection<MasterPegawaiGajiHarianDto> _tenagaHarianList;
        public ObservableCollection<MasterPegawaiGajiHarianDto> TenagaHarianList
        {
            get { return _tenagaHarianList; }
            set { _tenagaHarianList = value; OnPropertyChanged(); }
        }

        private MasterPegawaiGajiHarianDto _selectedData;
        public MasterPegawaiGajiHarianDto SelectedData
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

        private MasterPegawaiDto _selectedDataPegawaiForm;
        public MasterPegawaiDto SelectedDataPegawaiForm
        {
            get { return _selectedDataPegawaiForm; }
            set { _selectedDataPegawaiForm = value; OnPropertyChanged(); }
        }

        private decimal? _formGaji;
        public decimal? FormGaji
        {
            get { return _formGaji; }
            set { _formGaji = value; OnPropertyChanged(); }
        }

        private string _formKeteranganGaji;
        public string FormKeteranganGaji
        {
            get { return _formKeteranganGaji; }
            set { _formKeteranganGaji = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        #region Filter Combobox Data
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

        private ObservableCollection<MasterJabatanDto> _jabatanList;
        public ObservableCollection<MasterJabatanDto> JabatanList
        {
            get { return _jabatanList; }
            set { _jabatanList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPendidikanDto> _pendidikanList;
        public ObservableCollection<MasterPendidikanDto> PendidikanList
        {
            get { return _pendidikanList; }
            set { _pendidikanList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisKelaminDto> _jenisKelaminList;
        public ObservableCollection<MasterJenisKelaminDto> JenisKelaminList
        {
            get { return _jenisKelaminList; }
            set { _jenisKelaminList = value; OnPropertyChanged(); }
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

        private bool _isKawinChecked;
        public bool IsKawinChecked
        {
            get { return _isKawinChecked; }
            set { _isKawinChecked = value; OnPropertyChanged(); }
        }

        private bool _isPendidikanChecked;
        public bool IsPendidikanChecked
        {
            get { return _isPendidikanChecked; }
            set { _isPendidikanChecked = value; OnPropertyChanged(); }
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

        private int? _filterJenisKelamin;
        public int? FilterJenisKelamin
        {
            get { return _filterJenisKelamin; }
            set { _filterJenisKelamin = value; OnPropertyChanged(); }
        }

        private bool? _filterKawin;
        public bool? FilterKawin
        {
            get { return _filterKawin; }
            set { _filterKawin = value; OnPropertyChanged(); }
        }

        private int? _filterPendidikan;
        public int? FilterPendidikan
        {
            get { return _filterPendidikan; }
            set { _filterPendidikan = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
