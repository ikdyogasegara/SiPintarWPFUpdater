using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Kepegawaian.MutasiJabatan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class MutasiJabatanViewModel : ViewModelBase
    {
        public MutasiJabatanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnOpenAddDetailFormCommand = new OnOpenAddDetailFormCommand(this, restApi, isTest);
            OnOpenEditDetailFormCommand = new OnOpenEditDetailFormCommand(this, restApi, isTest);
            OnOpenDeleteDetailFormCommand = new OnOpenDeleteDetailFormCommand(this, isTest);
            OnOpenDetailCommand = new OnOpenDetailCommand(this, restApi, isTest);
            OnSaveDetailFormCommand = new OnSaveDetailFormCommand(this, isTest);
            OnDeleteDetailFormCommand = new OnDeleteDetailFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnVerifyCommand = new OnVerifyCommand(this, restApi, isTest);
            OnBrowseFotoCommand = new OnBrowseFotoCommand(this, isTest);
            OnUploadFotoCommand = new OnUploadFotoCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenAddDetailFormCommand { get; }
        public ICommand OnOpenEditDetailFormCommand { get; }
        public ICommand OnOpenDeleteDetailFormCommand { get; }
        public ICommand OnOpenDetailCommand { get; }
        public ICommand OnOpenSelectPegawaiCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnSaveDetailFormCommand { get; }
        public ICommand OnDeleteDetailFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnVerifyCommand { get; }
        public ICommand OnBrowseFotoCommand { get; }
        public ICommand OnUploadFotoCommand { get; }

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

        private bool _isEditDetail;
        public bool IsEditDetail
        {
            get { return _isEditDetail; }
            set { _isEditDetail = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MutasiJabatanDto> _mutasiJabatanList;
        public ObservableCollection<MutasiJabatanDto> MutasiJabatanList
        {
            get { return _mutasiJabatanList; }
            set { _mutasiJabatanList = value; OnPropertyChanged(); }
        }

        private MutasiJabatanDto _selectedData;
        public MutasiJabatanDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MutasiJabatanDetailWpf _selectedDetailData;
        public MutasiJabatanDetailWpf SelectedDetailData
        {
            get { return _selectedDetailData; }
            set { _selectedDetailData = value; OnPropertyChanged(); }
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

        private string _formSk;
        public string FormSk
        {
            get { return _formSk; }
            set { _formSk = value; OnPropertyChanged(); }
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

        private DateTime? _formTglSk;
        public DateTime? FormTglSk
        {
            get { return _formTglSk; }
            set { _formTglSk = value; OnPropertyChanged(); }
        }

        private DateTime? _formTglBerlaku;
        public DateTime? FormTglBerlaku
        {
            get { return _formTglBerlaku; }
            set { _formTglBerlaku = value; OnPropertyChanged(); }
        }

        private string _formKeterangan;
        public string FormKeterangan
        {
            get { return _formKeterangan; }
            set { _formKeterangan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MutasiJabatanDetailWpf> _formDetailList;
        public ObservableCollection<MutasiJabatanDetailWpf> FormDetailList
        {
            get { return _formDetailList; }
            set { _formDetailList = value; OnPropertyChanged(); }
        }

        private MutasiJabatanDetailWpf _formDetailData;
        public MutasiJabatanDetailWpf FormDetailData
        {
            get { return _formDetailData; }
            set { _formDetailData = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        private bool _isUbahNik;
        public bool IsUbahNik
        {
            get { return _isUbahNik; }
            set { _isUbahNik = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJabatanDto> _jabatanFormList;
        public ObservableCollection<MasterJabatanDto> JabatanFormList
        {
            get { return _jabatanFormList; }
            set { _jabatanFormList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDepartemenDto> _departemenFormList;
        public ObservableCollection<MasterDepartemenDto> DepartemenFormList
        {
            get { return _departemenFormList; }
            set { _departemenFormList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAreaKerjaDto> _areaKerjaFormList;
        public ObservableCollection<MasterAreaKerjaDto> AreaKerjaFormList
        {
            get { return _areaKerjaFormList; }
            set { _areaKerjaFormList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterDivisiDto> _divisiFormList;
        public ObservableCollection<MasterDivisiDto> DivisiFormList
        {
            get { return _divisiFormList; }
            set { _divisiFormList = value; OnPropertyChanged(); }
        }

        private Uri _fotoSkUri;
        public Uri FotoSkUri
        {
            get { return _fotoSkUri; }
            set { _fotoSkUri = value; OnPropertyChanged(); }
        }


        #region Filter Combobox Data
        private ObservableCollection<MasterStatusPegawaiDto> _statusPegawaiList;
        public ObservableCollection<MasterStatusPegawaiDto> StatusPegawaiList
        {
            get { return _statusPegawaiList; }
            set { _statusPegawaiList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJabatanDto> _jabatanList;
        public ObservableCollection<MasterJabatanDto> JabatanList
        {
            get { return _jabatanList; }
            set { _jabatanList = value; OnPropertyChanged(); }
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

        private ObservableCollection<MasterAreaKerjaDto> _areaKerjaList;
        public ObservableCollection<MasterAreaKerjaDto> AreaKerjaList
        {
            get { return _areaKerjaList; }
            set { _areaKerjaList = value; OnPropertyChanged(); }
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

        private bool _isStatusPegawaiChecked;
        public bool IsStatusPegawaiChecked
        {
            get { return _isStatusPegawaiChecked; }
            set { _isStatusPegawaiChecked = value; OnPropertyChanged(); }
        }

        private bool _isJabatanChecked;
        public bool IsJabatanChecked
        {
            get { return _isJabatanChecked; }
            set { _isJabatanChecked = value; OnPropertyChanged(); }
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

        private bool _isAreaKerjaChecked;
        public bool IsAreaKerjaChecked
        {
            get { return _isAreaKerjaChecked; }
            set { _isAreaKerjaChecked = value; OnPropertyChanged(); }
        }

        private bool _isJabatanBaruChecked;
        public bool IsJabatanBaruChecked
        {
            get { return _isJabatanBaruChecked; }
            set { _isJabatanBaruChecked = value; OnPropertyChanged(); }
        }

        private bool _isDepartemenBaruChecked;
        public bool IsDepartemenBaruChecked
        {
            get { return _isDepartemenBaruChecked; }
            set { _isDepartemenBaruChecked = value; OnPropertyChanged(); }
        }

        private bool _isDivisiBaruChecked;
        public bool IsDivisiBaruChecked
        {
            get { return _isDivisiBaruChecked; }
            set { _isDivisiBaruChecked = value; OnPropertyChanged(); }
        }

        private bool _isAreaKerjaBaruChecked;
        public bool IsAreaKerjaBaruChecked
        {
            get { return _isAreaKerjaBaruChecked; }
            set { _isAreaKerjaBaruChecked = value; OnPropertyChanged(); }
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

        private int? _filterStatusPegawai;
        public int? FilterStatusPegawai
        {
            get { return _filterStatusPegawai; }
            set { _filterStatusPegawai = value; OnPropertyChanged(); }
        }

        private int? _filterJabatan;
        public int? FilterJabatan
        {
            get { return _filterJabatan; }
            set { _filterJabatan = value; OnPropertyChanged(); }
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

        private int? _filterAreaKerja;
        public int? FilterAreaKerja
        {
            get { return _filterAreaKerja; }
            set { _filterAreaKerja = value; OnPropertyChanged(); }
        }

        private int? _filterJabatanBaru;
        public int? FilterJabatanBaru
        {
            get { return _filterJabatanBaru; }
            set { _filterJabatanBaru = value; OnPropertyChanged(); }
        }

        private int? _filterDepartemenBaru;
        public int? FilterDepartemenBaru
        {
            get { return _filterDepartemenBaru; }
            set { _filterDepartemenBaru = value; OnPropertyChanged(); }
        }

        private int? _filterDivisiBaru;
        public int? FilterDivisiBaru
        {
            get { return _filterDivisiBaru; }
            set { _filterDivisiBaru = value; OnPropertyChanged(); }
        }

        private int? _filterAreaKerjaBaru;
        public int? FilterAreaKerjaBaru
        {
            get { return _filterAreaKerjaBaru; }
            set { _filterAreaKerjaBaru = value; OnPropertyChanged(); }
        }

        #endregion
    }

    public class MutasiJabatanDetailWpf : MutasiJabatanDetailDto
    {
        public int? No { get; set; }
        public int? IdPendidikan { get; set; }
        public string Pendidikan { get; set; }
        public int? IdGolonganPegawai { get; set; }
        public string KodeGolonganPegawai { get; set; }
        public string GolonganPegawai { get; set; }
    }
}
