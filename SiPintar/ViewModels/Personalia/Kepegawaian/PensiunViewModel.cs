using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Kepegawaian.Pensiun;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class PensiunViewModel : ViewModelBase
    {
        public PensiunViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenPostingFormCommand = new OnOpenPostingFormCommand(this, isTest);
            OnSubmitPostingFormCommand = new OnSubmitPostingFormCommand(this, restApi, isTest);
            OnOpenVerifikasiFormCommand = new OnOpenVerifikasiFormCommand(this, restApi, isTest);
            OnSubmitVerifikasiFormCommand = new OnSubmitVerifikasiFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenPostingFormCommand { get; }
        public ICommand OnSubmitPostingFormCommand { get; }
        public ICommand OnOpenVerifikasiFormCommand { get; }
        public ICommand OnSubmitVerifikasiFormCommand { get; }

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

        private ObservableCollection<MasterPegawaiDto> _pensiunList;
        public ObservableCollection<MasterPegawaiDto> PensiunList
        {
            get { return _pensiunList; }
            set { _pensiunList = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedData;
        public MasterPegawaiDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MasterPegawaiMkgDto _masaKerjaPegawai;
        public MasterPegawaiMkgDto MasaKerjaPegawai
        {
            get { return _masaKerjaPegawai; }
            set { _masaKerjaPegawai = value; OnPropertyChanged(); }
        }

        private int? _formJenisPensiun;
        public int? FormJenisPensiun
        {
            get { return _formJenisPensiun; }
            set { _formJenisPensiun = value; OnPropertyChanged(); }
        }

        private string _formSk1;
        public string FormSk1
        {
            get { return _formSk1; }
            set { _formSk1 = value; OnPropertyChanged(); }
        }

        private string _formSk2;
        public string FormSk2
        {
            get { return _formSk2; }
            set { _formSk2 = value; OnPropertyChanged(); }
        }

        private string _formSk3;
        public string FormSk3
        {
            get { return _formSk3; }
            set { _formSk3 = value; OnPropertyChanged(); }
        }

        private string _formSk4;
        public string FormSk4
        {
            get { return _formSk4; }
            set { _formSk4 = value; OnPropertyChanged(); }
        }

        private string _formSk5;
        public string FormSk5
        {
            get { return _formSk5; }
            set { _formSk5 = value; OnPropertyChanged(); }
        }

        private DateTime? _formTgl;
        public DateTime? FormTgl
        {
            get { return _formTgl; }
            set { _formTgl = value; OnPropertyChanged(); }
        }

        private Uri _fotoPegawaiUri;
        public Uri FotoPegawaiUri
        {
            get { return _fotoPegawaiUri; }
            set { _fotoPegawaiUri = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisPensiunDto> _jenisPensiunFormList;
        public ObservableCollection<MasterJenisPensiunDto> JenisPensiunFormList
        {
            get { return _jenisPensiunFormList; }
            set { _jenisPensiunFormList = value; OnPropertyChanged(); }
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

        private bool _isStatusPegawaiChecked;
        public bool IsStatusPegawaiChecked
        {
            get { return _isStatusPegawaiChecked; }
            set { _isStatusPegawaiChecked = value; OnPropertyChanged(); }
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

        private int? _filterGolonganPegawai;
        public int? FilterGolonganPegawai
        {
            get { return _filterGolonganPegawai; }
            set { _filterGolonganPegawai = value; OnPropertyChanged(); }
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

        private int? _filterStatusPegawai;
        public int? FilterStatusPegawai
        {
            get { return _filterStatusPegawai; }
            set { _filterStatusPegawai = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
