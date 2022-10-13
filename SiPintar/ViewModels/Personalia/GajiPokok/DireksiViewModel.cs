using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.GajiPokok.Direksi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.GajiPokok
{
    public class DireksiViewModel : ViewModelBase
    {
        public DireksiViewModel(IRestApiClientModel restApi, bool isTest = false)
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

        private ObservableCollection<MasterPegawaiGajiDireksiDto> _direksiList;
        public ObservableCollection<MasterPegawaiGajiDireksiDto> DireksiList
        {
            get { return _direksiList; }
            set { _direksiList = value; OnPropertyChanged(); }
        }

        private MasterPegawaiGajiDireksiDto _selectedData;
        public MasterPegawaiGajiDireksiDto SelectedData
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

        private bool _isBerdasarkanGajiPegawai;
        public bool IsBerdasarkanGajiPegawai
        {
            get { return _isBerdasarkanGajiPegawai; }
            set { _isBerdasarkanGajiPegawai = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedDataPegawaiForm;
        public MasterPegawaiDto SelectedDataPegawaiForm
        {
            get { return _selectedDataPegawaiForm; }
            set { _selectedDataPegawaiForm = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedDataPegawaiRefForm;
        public MasterPegawaiDto SelectedDataPegawaiRefForm
        {
            get { return _selectedDataPegawaiRefForm; }
            set { _selectedDataPegawaiRefForm = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPersen = false;
        public bool? FormFlagPersen
        {
            get { return _formFlagPersen; }
            set { _formFlagPersen = value; OnPropertyChanged(); }
        }

        private decimal? _formNominalPersen;
        public decimal? FormNominalPersen
        {
            get { return _formNominalPersen; }
            set { _formNominalPersen = value; OnPropertyChanged(); }
        }

        private decimal? _formNominalNonPersen;
        public decimal? FormNominalNonPersen
        {
            get { return _formNominalNonPersen; }
            set { _formNominalNonPersen = value; OnPropertyChanged(); }
        }

        private string _formBerdasarkan;
        public string FormBerdasarkan
        {
            get { return _formBerdasarkan; }
            set { _formBerdasarkan = value; OnPropertyChanged(); }
        }

        private string _formPersenDari;
        public string FormPersenDari
        {
            get { return _formPersenDari; }
            set { _formPersenDari = value; OnPropertyChanged(); }
        }

        private int? _formIdTunjangan;
        public int? FormIdTunjangan
        {
            get { return _formIdTunjangan; }
            set { _formIdTunjangan = value; OnPropertyChanged(); }
        }

        private int? _formIdPotongan;
        public int? FormIdPotongan
        {
            get { return _formIdPotongan; }
            set { _formIdPotongan = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        #region Form Combobox Data
        private ObservableCollection<MasterJenisTunjanganWpf> _tunjanganList;
        public ObservableCollection<MasterJenisTunjanganWpf> TunjanganList
        {
            get { return _tunjanganList; }
            set { _tunjanganList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJenisPotonganWpf> _potonganList;
        public ObservableCollection<MasterJenisPotonganWpf> PotonganList
        {
            get { return _potonganList; }
            set { _potonganList = value; OnPropertyChanged(); }
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

        #endregion
    }

    public class MasterJenisTunjanganWpf : MasterJenisTunjanganDto
    {
        public bool IsChecked { get; set; }
    }

    public class MasterJenisPotonganWpf : MasterJenisPotonganDto
    {
        public bool IsChecked { get; set; }
    }

}
