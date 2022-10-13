using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.Keluarga;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class KeluargaViewModel : ViewModelBase
    {
        public KeluargaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnOpenDetailFormCommand = new OnOpenDetailFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnOpenAddActionCommand = new OnOpenAddActionCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnSelectPegawaiCommand = new OnSelectPegawaiCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnDeleteCommand = new OnDeleteCommand(this, isTest);
            OnSaveFormCommand = new OnSaveFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenDetailFormCommand { get; }
        public ICommand OnOpenSelectPegawaiCommand { get; }
        public ICommand OnOpenAddActionCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnSelectPegawaiCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnDeleteCommand { get; }
        public ICommand OnSaveFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }

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

        private ObservableCollection<MasterKeluargaWpf> _keluargaList;
        public ObservableCollection<MasterKeluargaWpf> KeluargaList
        {
            get { return _keluargaList; }
            set { _keluargaList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKeluargaDto> _selectedKeluargaList;
        public ObservableCollection<MasterKeluargaDto> SelectedKeluargaList
        {
            get { return _selectedKeluargaList; }
            set { _selectedKeluargaList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterKeluargaDto> _selectedKeluargaListForm;
        public ObservableCollection<MasterKeluargaDto> SelectedKeluargaListForm
        {
            get { return _selectedKeluargaListForm; }
            set { _selectedKeluargaListForm = value; OnPropertyChanged(); }
        }

        private MasterKeluargaDto _selectedData;
        public MasterKeluargaDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterStatusPegawaiDto> _statusPegawaiList;
        public ObservableCollection<MasterStatusPegawaiDto> StatusPegawaiList
        {
            get { return _statusPegawaiList; }
            set { _statusPegawaiList = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai = string.Empty;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        #region Enable / Disable Filter

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

        private bool _isNamaKeluargaChecked;
        public bool IsNamaKeluargaChecked
        {
            get { return _isNamaKeluargaChecked; }
            set { _isNamaKeluargaChecked = value; OnPropertyChanged(); }
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

        private string _filterNamaKeluarga;
        public string FilterNamaKeluarga
        {
            get { return _filterNamaKeluarga; }
            set { _filterNamaKeluarga = value; OnPropertyChanged(); }
        }

        #endregion

        #region Dialog Tambah Keluarga
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

        private MasterKeluargaDto _selectedAnggotaKeluarga;
        public MasterKeluargaDto SelectedAnggotaKeluarga
        {
            get { return _selectedAnggotaKeluarga; }
            set { _selectedAnggotaKeluarga = value; OnPropertyChanged(); }
        }

        private MasterKeluargaDto _anggotaKeluargaForm;
        public MasterKeluargaDto AnggotaKeluargaForm
        {
            get { return _anggotaKeluargaForm; }
            set { _anggotaKeluargaForm = value; OnPropertyChanged(); }
        }

        private bool _isCariDariPegawai;
        public bool IsCariDariPegawai
        {
            get { return _isCariDariPegawai; }
            set { _isCariDariPegawai = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPekerjaanDto> _pekerjaanList;
        public ObservableCollection<MasterPekerjaanDto> PekerjaanList
        {
            get { return _pekerjaanList; }
            set { _pekerjaanList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisKelaminDto> _jenisKelaminList;
        public ObservableCollection<MasterJenisKelaminDto> JenisKelaminList
        {
            get { return _jenisKelaminList; }
            set { _jenisKelaminList = value; OnPropertyChanged(); }
        }

        #endregion
    }

    public class MasterKeluargaWpf : MasterKeluargaDto
    {
        public string NoNamaPegawai { get => $"NIK : {NoPegawai} - Nama Pegawai : {NamaPegawai}"; }
    }
}
