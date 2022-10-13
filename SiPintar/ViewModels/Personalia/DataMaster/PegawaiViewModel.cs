using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.Pegawai;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class PegawaiViewModel : ViewModelBase
    {
        public PegawaiViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnPreviousFormPageCommand = new OnPreviousFormPageCommand(this);
            OnNextFormPageCommand = new OnNextFormPageCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnOpenFotoCommand = new OnOpenFotoCommand(this, isTest);
            OnBrowseFotoCommand = new OnBrowseFotoCommand(this, isTest);
            OnDeleteFotoCommand = new OnDeleteFotoCommand(this, isTest);
            OnUploadFileCommand = new OnUploadFileCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextFormPageCommand { get; }
        public ICommand OnPreviousFormPageCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnOpenFotoCommand { get; }
        public ICommand OnBrowseFotoCommand { get; }
        public ICommand OnDeleteFotoCommand { get; }
        public ICommand OnUploadFileCommand { get; }

        private bool _isFilterVisible = true;
        public bool IsFilterVisible
        {
            get { return _isFilterVisible; }
            set { _isFilterVisible = value; OnPropertyChanged(); }
        }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
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

        private string _jenisFormPegawai;
        public string JenisFormPegawai
        {
            get { return _jenisFormPegawai; }
            set { _jenisFormPegawai = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _pegawaiList;
        public ObservableCollection<MasterPegawaiDto> PegawaiList
        {
            get { return _pegawaiList; }
            set { _pegawaiList = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedData;
        public MasterPegawaiDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _formData;
        public MasterPegawaiDto FormData
        {
            get { return _formData; }
            set { _formData = value; OnPropertyChanged(); }
        }

        private Uri _fotoPegawaiUri;
        public Uri FotoPegawaiUri
        {
            get { return _fotoPegawaiUri; }
            set { _fotoPegawaiUri = value; OnPropertyChanged(); }
        }

        private Uri _fotoKtpUri;
        public Uri FotoKtpUri
        {
            get { return _fotoKtpUri; }
            set
            {
                _fotoKtpUri = value;
                if (FormData != null)
                    FormData.FotoKtp = value?.OriginalString;
                OnPropertyChanged();
            }
        }

        private Uri _fotoKkUri;
        public Uri FotoKkUri
        {
            get { return _fotoKkUri; }
            set
            {
                _fotoKkUri = value;
                if (FormData != null)
                    FormData.FotoKk = value?.OriginalString;
                OnPropertyChanged();
            }
        }

        private bool _isFotoKtpChecked;
        public bool IsFotoKtpChecked
        {
            get { return _isFotoKtpChecked; }
            set
            {
                _isFotoKtpChecked = value;
                if (!value)
                    FotoKtpUri = null;
                OnPropertyChanged();
            }
        }

        private bool _isFotoKkChecked;
        public bool IsFotoKkChecked
        {
            get { return _isFotoKkChecked; }
            set
            {
                _isFotoKkChecked = value;
                if (!value)
                    FotoKkUri = null;
                OnPropertyChanged();
            }
        }

        private BitmapImage _previewFile;
        public BitmapImage PreviewFile
        {
            get => _previewFile;
            set { _previewFile = value; OnPropertyChanged(); }
        }

        private int _formCurrentPage = 1;
        public int FormCurrentPage
        {
            get { return _formCurrentPage; }
            set { _formCurrentPage = value; OnPropertyChanged(); }
        }

        private bool _isStatusPegawaiTetap;
        public bool IsStatusPegawaiTetap
        {
            get { return _isStatusPegawaiTetap; }
            set { _isStatusPegawaiTetap = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganDarahDto> _golonganDarahList;
        public ObservableCollection<MasterGolonganDarahDto> GolonganDarahList
        {
            get { return _golonganDarahList; }
            set { _golonganDarahList = value; OnPropertyChanged(); }
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

        private ObservableCollection<MasterDivisiDto> _divisiCopyList;
        public ObservableCollection<MasterDivisiDto> DivisiCopyList
        {
            get { return _divisiCopyList; }
            set { _divisiCopyList = value; OnPropertyChanged(); }
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

        private ObservableCollection<MasterStatusPegawaiDto> _statusPegawaiCopyList;
        public ObservableCollection<MasterStatusPegawaiDto> StatusPegawaiCopyList
        {
            get { return _statusPegawaiCopyList; }
            set { _statusPegawaiCopyList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Enable / Disable Filter

        private bool _isPegawaiNonAktifChecked;
        public bool IsPegawaiNonAktifChecked
        {
            get { return _isPegawaiNonAktifChecked; }
            set { _isPegawaiNonAktifChecked = value; OnPropertyChanged(); }
        }

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

        #region Pagination
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
                OnLoadCommand.Execute(null);
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
}
