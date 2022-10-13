using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Kepegawaian.SKCalonPegawai;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class SKCalonPegawaiViewModel : ViewModelBase
    {
        public SKCalonPegawaiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnToggleUbahNikCommand = new OnToggleUbahNikCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnOpenAddDetailFormCommand = new OnOpenAddDetailFormCommand(this, restApi, isTest);
            OnOpenEditDetailFormCommand = new OnOpenEditDetailFormCommand(this, restApi, isTest);
            OnOpenDeleteDetailFormCommand = new OnOpenDeleteDetailFormCommand(this, isTest);
            OnOpenDetailCommand = new OnOpenDetailCommand(this, isTest);
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
        public ICommand OnToggleUbahNikCommand { get; }
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

        private ObservableCollection<MutasiStatusCapegDto> _skCalonPegawaiList;
        public ObservableCollection<MutasiStatusCapegDto> SKCalonPegawaiList
        {
            get { return _skCalonPegawaiList; }
            set { _skCalonPegawaiList = value; OnPropertyChanged(); }
        }

        private MutasiStatusCapegDto _selectedData;
        public MutasiStatusCapegDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MutasiStatusCapegDetailDto _selectedDetailData;
        public MutasiStatusCapegDetailDto SelectedDetailData
        {
            get { return _selectedDetailData; }
            set { _selectedDetailData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<CalonPegawaiWpf> _pegawaiList;
        public ObservableCollection<CalonPegawaiWpf> PegawaiList
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

        private ObservableCollection<MutasiStatusCapegDetailDto> _formDetailList;
        public ObservableCollection<MutasiStatusCapegDetailDto> FormDetailList
        {
            get { return _formDetailList; }
            set { _formDetailList = value; OnPropertyChanged(); }
        }

        private MutasiStatusCapegDetailDto _formDetailData;
        public MutasiStatusCapegDetailDto FormDetailData
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

        private ObservableCollection<MasterGolonganPegawaiDto> _golonganFormList;
        public ObservableCollection<MasterGolonganPegawaiDto> GolonganFormList
        {
            get { return _golonganFormList; }
            set { _golonganFormList = value; OnPropertyChanged(); }
        }

        private Uri _fotoSkUri;
        public Uri FotoSkUri
        {
            get { return _fotoSkUri; }
            set { _fotoSkUri = value; OnPropertyChanged(); }
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

    public class CalonPegawaiWpf : MasterPegawaiDto
    {
        public int? MasaKontrakTahun
        {
            get
            {
                if (TglMasuk.HasValue)
                {
                    var currentDate = DateTime.Today;
                    int year = 0;
                    if (currentDate.Year > TglMasuk.Value.Year)
                    {
                        year = currentDate.Year - TglMasuk.Value.Year;
                        if ((currentDate.Month < TglMasuk.Value.Month) || (currentDate.Month == TglMasuk.Value.Month && currentDate.Day < TglMasuk.Value.Day))
                            year--;
                    }
                    return year;
                }
                return null;
            }
        }

        public int? MasaKontrakBulan
        {
            get
            {
                if (TglMasuk.HasValue)
                {
                    var currentDate = DateTime.Today;
                    int month = 0;
                    if (currentDate.Year >= TglMasuk.Value.Year)
                        month = (((currentDate.Year - TglMasuk.Value.Year) * 12) + currentDate.Month - TglMasuk.Value.Month + (currentDate.Day >= TglMasuk.Value.Day ? 0 : -1)) % 12;
                    return month;
                }
                return null;
            }
        }

        public bool IsRecommended { get => MasaKontrakTahun.HasValue && MasaKontrakTahun.Value >= 2; }

    }
}
