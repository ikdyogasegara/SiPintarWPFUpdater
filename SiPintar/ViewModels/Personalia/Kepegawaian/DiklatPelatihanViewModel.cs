using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Kepegawaian.DiklatPelatihan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class DiklatPelatihanViewModel : ViewModelBase
    {
        public DiklatPelatihanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSelectPegawaiCommand = new OnSelectPegawaiCommand(this, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnOpenDeleteDetailFormCommand = new OnOpenDeleteDetailFormCommand(this, isTest);
            OnDeleteDetailFormCommand = new OnDeleteDetailFormCommand(this, isTest);
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
        public ICommand OnSelectPegawaiCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnOpenDeleteDetailFormCommand { get; }
        public ICommand OnDeleteDetailFormCommand { get; }
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

        private ObservableCollection<DiklatWpf> _diklatPelatihanListTable;
        public ObservableCollection<DiklatWpf> DiklatPelatihanListTable
        {
            get { return _diklatPelatihanListTable; }
            set { _diklatPelatihanListTable = value; OnPropertyChanged(); }
        }

        private DiklatWpf _selectedDataTable;
        public DiklatWpf SelectedDataTable
        {
            get { return _selectedDataTable; }
            set { _selectedDataTable = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DiklatDto> _diklatPelatihanList;
        public ObservableCollection<DiklatDto> DiklatPelatihanList
        {
            get { return _diklatPelatihanList; }
            set { _diklatPelatihanList = value; OnPropertyChanged(); }
        }

        private DiklatDto _selectedData;
        public DiklatDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private DiklatDetailDto _selectedDetailData;
        public DiklatDetailDto SelectedDetailData
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

        private string _formNoSertifikat;
        public string FormNoSertifikat
        {
            get { return _formNoSertifikat; }
            set { _formNoSertifikat = value; OnPropertyChanged(); }
        }

        private DateTime? _formTglAwal;
        public DateTime? FormTglAwal
        {
            get { return _formTglAwal; }
            set { _formTglAwal = value; OnPropertyChanged(); }
        }

        private DateTime? _formTglAkhir;
        public DateTime? FormTglAkhir
        {
            get { return _formTglAkhir; }
            set { _formTglAkhir = value; OnPropertyChanged(); }
        }

        private string _formUraian;
        public string FormUraian
        {
            get { return _formUraian; }
            set { _formUraian = value; OnPropertyChanged(); }
        }

        private string _formTempat;
        public string FormTempat
        {
            get { return _formTempat; }
            set { _formTempat = value; OnPropertyChanged(); }
        }

        private string _formPenyelenggara;
        public string FormPenyelenggara
        {
            get { return _formPenyelenggara; }
            set { _formPenyelenggara = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DiklatDetailDto> _formDetailList;
        public ObservableCollection<DiklatDetailDto> FormDetailList
        {
            get { return _formDetailList; }
            set { _formDetailList = value; OnPropertyChanged(); }
        }

        private DiklatDetailDto _formDetailData;
        public DiklatDetailDto FormDetailData
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

        private bool _isNoSertifikatChecked;
        public bool IsNoSertifikatChecked
        {
            get { return _isNoSertifikatChecked; }
            set { _isNoSertifikatChecked = value; OnPropertyChanged(); }
        }

        private bool _isUraianChecked;
        public bool IsUraianChecked
        {
            get { return _isUraianChecked; }
            set { _isUraianChecked = value; OnPropertyChanged(); }
        }

        private bool _isTempatChecked;
        public bool IsTempatChecked
        {
            get { return _isTempatChecked; }
            set { _isTempatChecked = value; OnPropertyChanged(); }
        }

        private bool _isPenyelenggaraChecked;
        public bool IsPenyelenggaraChecked
        {
            get { return _isPenyelenggaraChecked; }
            set { _isPenyelenggaraChecked = value; OnPropertyChanged(); }
        }

        private bool _isTglAwalChecked;
        public bool IsTglAwalChecked
        {
            get { return _isTglAwalChecked; }
            set { _isTglAwalChecked = value; OnPropertyChanged(); }
        }

        private bool _isTglAkhirChecked;
        public bool IsTglAkhirChecked
        {
            get { return _isTglAkhirChecked; }
            set { _isTglAkhirChecked = value; OnPropertyChanged(); }
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

        private string _filterNoSertifikat;
        public string FilterNoSertifikat
        {
            get { return _filterNoSertifikat; }
            set { _filterNoSertifikat = value; OnPropertyChanged(); }
        }

        private string _filterUraian;
        public string FilterUraian
        {
            get { return _filterUraian; }
            set { _filterUraian = value; OnPropertyChanged(); }
        }

        private string _filterTempat;
        public string FilterTempat
        {
            get { return _filterTempat; }
            set { _filterTempat = value; OnPropertyChanged(); }
        }

        private string _filterPenyelenggara;
        public string FilterPenyelenggara
        {
            get { return _filterPenyelenggara; }
            set { _filterPenyelenggara = value; OnPropertyChanged(); }
        }

        private DateTime? _filterTglAwal;
        public DateTime? FilterTglAwal
        {
            get { return _filterTglAwal; }
            set { _filterTglAwal = value; OnPropertyChanged(); }
        }

        private DateTime? _filterTglAkhir;
        public DateTime? FilterTglAkhir
        {
            get { return _filterTglAkhir; }
            set { _filterTglAkhir = value; OnPropertyChanged(); }
        }
        #endregion
    }

    public class DiklatWpf
    {
        public int? IdDiklat { get; set; }
        public string NamaPegawai { get; set; }
        public string Uraian { get; set; }
        public string Tempat { get; set; }
        public string Penyelenggara { get; set; }
        public DateTime? TglAwal { get; set; }
        public DateTime? TglAkhir { get; set; }
        public int? Tahun { get; set; }
        public string NoSertifikat { get; set; }
    }
}
