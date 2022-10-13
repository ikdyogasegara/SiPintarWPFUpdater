using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Tunjangan.PengecualianTunjangan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Tunjangan
{
    public class PengecualianTunjanganViewModel : ViewModelBase
    {
        public PengecualianTunjanganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
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

        private ObservableCollection<PengecualianTunjanganDetailDto> _jenisTunjanganList;
        public ObservableCollection<PengecualianTunjanganDetailDto> JenisTunjanganList
        {
            get { return _jenisTunjanganList; }
            set { _jenisTunjanganList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PengecualianTunjanganWpf> _pengecualianTunjanganList;
        public ObservableCollection<PengecualianTunjanganWpf> PengecualianTunjanganList
        {
            get { return _pengecualianTunjanganList; }
            set { _pengecualianTunjanganList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPegawaiDto> _pegawaiList;
        public ObservableCollection<MasterPegawaiDto> PegawaiList
        {
            get { return _pegawaiList; }
            set { _pegawaiList = value; OnPropertyChanged(); }
        }

        private PengecualianTunjanganDto _selectedData;
        public PengecualianTunjanganDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MasterPegawaiDto _selectedDataPegawai;
        public MasterPegawaiDto SelectedDataPegawai
        {
            get { return _selectedDataPegawai; }
            set { _selectedDataPegawai = value; OnPropertyChanged(); }
        }

        private bool? _formFlagRutin;
        public bool? FormFlagRutin
        {
            get { return _formFlagRutin; }
            set { _formFlagRutin = value; OnPropertyChanged(); }
        }

        private int? _formKodePeriode;
        public int? FormKodePeriode
        {
            get { return _formKodePeriode; }
            set { _formKodePeriode = value; OnPropertyChanged(); }
        }

        private string _formPengecualian;
        public string FormPengecualian
        {
            get { return _formPengecualian; }
            set { _formPengecualian = value; OnPropertyChanged(); }
        }

        private string _formKeterangan;
        public string FormKeterangan
        {
            get { return _formKeterangan; }
            set { _formKeterangan = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        #region Form Combobox Data
        private ObservableCollection<PengecualianTunjanganDetailWpf> _jenisTunjanganListForm;
        public ObservableCollection<PengecualianTunjanganDetailWpf> JenisTunjanganListForm
        {
            get { return _jenisTunjanganListForm; }
            set { _jenisTunjanganListForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeDto> _periodeListForm;
        public ObservableCollection<MasterPeriodeDto> PeriodeListForm
        {
            get { return _periodeListForm; }
            set { _periodeListForm = value; OnPropertyChanged(); }
        }
        #endregion

        #region Filter
        private bool _isNikChecked;
        public bool IsNikChecked
        {
            get { return _isNikChecked; }
            set { _isNikChecked = value; OnPropertyChanged(); }
        }

        private bool _isNamaChecked;
        public bool IsNamaChecked
        {
            get { return _isNamaChecked; }
            set { _isNamaChecked = value; OnPropertyChanged(); }
        }

        private string _filterNik;
        public string FilterNik
        {
            get { return _filterNik; }
            set { _filterNik = value; OnPropertyChanged(); }
        }

        private string _filterNama;
        public string FilterNama
        {
            get { return _filterNama; }
            set { _filterNama = value; OnPropertyChanged(); }
        }

        #endregion
    }

    public class PengecualianTunjanganWpf : PengecualianTunjanganDto
    {
        public string KeteranganPeriode { get { return FlagRutin == true ? "RUTIN" : KodePeriode.ToString(); } }
        public string DetailNamaTunjangan { get; set; }
        public bool IsChecked { get; set; }
    }

    public class PengecualianTunjanganDetailWpf : PengecualianTunjanganDetailDto
    {
        public bool IsChecked { get; set; }
    }
}
