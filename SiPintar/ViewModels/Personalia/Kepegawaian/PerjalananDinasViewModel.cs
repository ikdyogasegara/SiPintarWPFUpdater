using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Kepegawaian.PerjalananDinas;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Kepegawaian
{
    public class PerjalananDinasViewModel : ViewModelBase
    {
        public PerjalananDinasViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnOpenSelectPegawaiCommand = new OnOpenSelectPegawaiCommand(this, restApi, isTest);
            OnSearchPegawaiCommand = new OnSearchPegawaiCommand(this, restApi, isTest);
            OnOpenAddPesertaFormCommand = new OnOpenAddPesertaFormCommand(this, isTest);
            OnOpenEditPesertaFormCommand = new OnOpenEditPesertaFormCommand(this, isTest);
            OnOpenDeletePesertaFormCommand = new OnOpenDeletePesertaFormCommand(this, isTest);
            OnOpenAddPesertaBiayaFormCommand = new OnOpenAddPesertaBiayaFormCommand(this, restApi, isTest);
            OnOpenEditPesertaBiayaFormCommand = new OnOpenEditPesertaBiayaFormCommand(this, restApi, isTest);
            OnOpenDeletePesertaBiayaFormCommand = new OnOpenDeletePesertaBiayaFormCommand(this, isTest);
            OnSavePesertaFormCommand = new OnSavePesertaFormCommand(this, isTest);
            OnDeletePesertaFormCommand = new OnDeletePesertaFormCommand(this, isTest);
            OnSavePesertaBiayaFormCommand = new OnSavePesertaBiayaFormCommand(this, isTest);
            OnDeletePesertaBiayaFormCommand = new OnDeletePesertaBiayaFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenAddPesertaFormCommand { get; }
        public ICommand OnOpenEditPesertaFormCommand { get; }
        public ICommand OnOpenDeletePesertaFormCommand { get; }
        public ICommand OnOpenAddPesertaBiayaFormCommand { get; }
        public ICommand OnOpenEditPesertaBiayaFormCommand { get; }
        public ICommand OnOpenDeletePesertaBiayaFormCommand { get; }
        public ICommand OnOpenSelectPegawaiCommand { get; }
        public ICommand OnSearchPegawaiCommand { get; }
        public ICommand OnSavePesertaFormCommand { get; }
        public ICommand OnDeletePesertaFormCommand { get; }
        public ICommand OnSavePesertaBiayaFormCommand { get; }
        public ICommand OnDeletePesertaBiayaFormCommand { get; }
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

        private bool _isEditPeserta;
        public bool IsEditPeserta
        {
            get { return _isEditPeserta; }
            set { _isEditPeserta = value; OnPropertyChanged(); }
        }

        private bool _isEditPesertaBiaya;
        public bool IsEditPesertaBiaya
        {
            get { return _isEditPesertaBiaya; }
            set { _isEditPesertaBiaya = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SppdDto> _perjalananDinasList;
        public ObservableCollection<SppdDto> PerjalananDinasList
        {
            get { return _perjalananDinasList; }
            set { _perjalananDinasList = value; OnPropertyChanged(); }
        }

        private SppdDto _selectedData;
        public SppdDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private SppdPesertaDto _selectedDataPeserta;
        public SppdPesertaDto SelectedDataPeserta
        {
            get { return _selectedDataPeserta; }
            set { _selectedDataPeserta = value; OnPropertyChanged(); }
        }

        private SppdPesertaBiayaDto _selectedDataPesertaBiaya;
        public SppdPesertaBiayaDto SelectedDataPesertaBiaya
        {
            get { return _selectedDataPesertaBiaya; }
            set { _selectedDataPesertaBiaya = value; OnPropertyChanged(); }
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

        private string _formSpt;
        public string FormSpt
        {
            get { return _formSpt; }
            set { _formSpt = value; OnPropertyChanged(); }
        }

        private string _formSpt1;
        public string FormSpt1
        {
            get { return _formSpt1; }
            set { _formSpt1 = value; OnPropertyChanged(); }
        }

        private string _formSpt2;
        public string FormSpt2
        {
            get { return _formSpt2; }
            set { _formSpt2 = value; OnPropertyChanged(); }
        }

        private string _formSpt3;
        public string FormSpt3
        {
            get { return _formSpt3; }
            set { _formSpt3 = value; OnPropertyChanged(); }
        }

        private string _formSpt4;
        public string FormSpt4
        {
            get { return _formSpt4; }
            set { _formSpt4 = value; OnPropertyChanged(); }
        }

        private string _formSpt5;
        public string FormSpt5
        {
            get { return _formSpt5; }
            set { _formSpt5 = value; OnPropertyChanged(); }
        }

        private string _formSppd;
        public string FormSppd
        {
            get { return _formSppd; }
            set { _formSppd = value; OnPropertyChanged(); }
        }

        private string _formSppd1;
        public string FormSppd1
        {
            get { return _formSppd1; }
            set { _formSppd1 = value; OnPropertyChanged(); }
        }

        private string _formSppd2;
        public string FormSppd2
        {
            get { return _formSppd2; }
            set { _formSppd2 = value; OnPropertyChanged(); }
        }

        private string _formSppd3;
        public string FormSppd3
        {
            get { return _formSppd3; }
            set { _formSppd3 = value; OnPropertyChanged(); }
        }

        private string _formSppd4;
        public string FormSppd4
        {
            get { return _formSppd4; }
            set { _formSppd4 = value; OnPropertyChanged(); }
        }

        private string _formSppd5;
        public string FormSppd5
        {
            get { return _formSppd5; }
            set { _formSppd5 = value; OnPropertyChanged(); }
        }

        private string _formDasar;
        public string FormDasar
        {
            get { return _formDasar; }
            set { _formDasar = value; OnPropertyChanged(); }
        }

        private string _formKeperluan;
        public string FormKeperluan
        {
            get { return _formKeperluan; }
            set { _formKeperluan = value; OnPropertyChanged(); }
        }

        private string _formTempatBerangkat;
        public string FormTempatBerangkat
        {
            get { return _formTempatBerangkat; }
            set { _formTempatBerangkat = value; OnPropertyChanged(); }
        }

        private string _formTempatTujuan;
        public string FormTempatTujuan
        {
            get { return _formTempatTujuan; }
            set { _formTempatTujuan = value; OnPropertyChanged(); }
        }

        private DateTime? _formTglBerangkat;
        public DateTime? FormTglBerangkat
        {
            get { return _formTglBerangkat; }
            set
            {
                _formTglBerangkat = value;
                FormLamaDinas = value.HasValue && FormTglKembali.HasValue ? Math.Abs(FormTglKembali.Value.Subtract(value.Value).Days) + 1 : 0;
                OnPropertyChanged();
            }
        }

        private DateTime? _formTglKembali;
        public DateTime? FormTglKembali
        {
            get { return _formTglKembali; }
            set
            {
                _formTglKembali = value;
                FormLamaDinas = FormTglBerangkat.HasValue && value.HasValue ? Math.Abs(value.Value.Subtract(FormTglBerangkat.Value).Days) + 1 : 0;
                OnPropertyChanged();
            }
        }

        private int? _formLamaDinas;
        public int? FormLamaDinas
        {
            get { return _formLamaDinas; }
            set { _formLamaDinas = value; OnPropertyChanged(); }
        }

        private string _formTransportasi;
        public string FormTransportasi
        {
            get { return _formTransportasi; }
            set { _formTransportasi = value; OnPropertyChanged(); }
        }

        private string _formBebanAnggaran;
        public string FormBebanAnggaran
        {
            get { return _formBebanAnggaran; }
            set { _formBebanAnggaran = value; OnPropertyChanged(); }
        }

        private string _formKeterangan;
        public string FormKeterangan
        {
            get { return _formKeterangan; }
            set { _formKeterangan = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SppdPesertaWpf> _formPesertaList;
        public ObservableCollection<SppdPesertaWpf> FormPesertaList
        {
            get { return _formPesertaList; }
            set { _formPesertaList = value; OnPropertyChanged(); }
        }

        private SppdPesertaWpf _formPesertaData;
        public SppdPesertaWpf FormPesertaData
        {
            get { return _formPesertaData; }
            set { _formPesertaData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SppdPesertaBiayaDto> _formPesertaBiayaList;
        public ObservableCollection<SppdPesertaBiayaDto> FormPesertaBiayaList
        {
            get { return _formPesertaBiayaList; }
            set { _formPesertaBiayaList = value; OnPropertyChanged(); }
        }

        private int? _formBiayaSppd;
        public int? FormBiayaSppd
        {
            get { return _formBiayaSppd; }
            set { _formBiayaSppd = value; OnPropertyChanged(); }
        }

        private string _formDeskripsi;
        public string FormDeskripsi
        {
            get { return _formDeskripsi; }
            set { _formDeskripsi = value; OnPropertyChanged(); }
        }

        private string _formKeteranganBiaya;
        public string FormKeteranganBiaya
        {
            get { return _formKeteranganBiaya; }
            set { _formKeteranganBiaya = value; OnPropertyChanged(); }
        }

        private decimal? _formBiaya = 0;
        public decimal? FormBiaya
        {
            get { return _formBiaya; }
            set
            {
                _formBiaya = value;
                FormJumlah = value * FormQty;
                OnPropertyChanged();
            }
        }

        private int? _formQty = 0;
        public int? FormQty
        {
            get { return _formQty; }
            set
            {
                _formQty = value;
                FormJumlah = FormBiaya * value;
                OnPropertyChanged();
            }
        }

        private decimal? _formJumlah = 0;
        public decimal? FormJumlah
        {
            get { return _formJumlah; }
            set { _formJumlah = value; OnPropertyChanged(); }
        }

        private string _filterFormNamaPegawai;
        public string FilterFormNamaPegawai
        {
            get { return _filterFormNamaPegawai; }
            set { _filterFormNamaPegawai = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterSppdBiayaDto> _sppdBiayaFormList;
        public ObservableCollection<MasterSppdBiayaDto> SppdBiayaFormList
        {
            get { return _sppdBiayaFormList; }
            set { _sppdBiayaFormList = value; OnPropertyChanged(); }
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

        private bool _isNoSptChecked;
        public bool IsNoSptChecked
        {
            get { return _isNoSptChecked; }
            set { _isNoSptChecked = value; OnPropertyChanged(); }
        }

        private bool _isNoSppdChecked;
        public bool IsNoSppdChecked
        {
            get { return _isNoSppdChecked; }
            set { _isNoSppdChecked = value; OnPropertyChanged(); }
        }

        private bool _isKeperluanChecked;
        public bool IsKeperluanChecked
        {
            get { return _isKeperluanChecked; }
            set { _isKeperluanChecked = value; OnPropertyChanged(); }
        }

        private bool _isTempatTujuanChecked;
        public bool IsTempatTujuanChecked
        {
            get { return _isTempatTujuanChecked; }
            set { _isTempatTujuanChecked = value; OnPropertyChanged(); }
        }

        private bool _isTglBerangkatChecked;
        public bool IsTglBerangkatChecked
        {
            get { return _isTglBerangkatChecked; }
            set { _isTglBerangkatChecked = value; OnPropertyChanged(); }
        }

        private bool _isTglKembaliChecked;
        public bool IsTglKembaliChecked
        {
            get { return _isTglKembaliChecked; }
            set { _isTglKembaliChecked = value; OnPropertyChanged(); }
        }

        private bool _isBebanAnggaranChecked;
        public bool IsBebanAnggaranChecked
        {
            get { return _isBebanAnggaranChecked; }
            set { _isBebanAnggaranChecked = value; OnPropertyChanged(); }
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

        private string _filterNoSpt;
        public string FilterNoSpt
        {
            get { return _filterNoSpt; }
            set { _filterNoSpt = value; OnPropertyChanged(); }
        }

        private string _filterNoSppd;
        public string FilterNoSppd
        {
            get { return _filterNoSppd; }
            set { _filterNoSppd = value; OnPropertyChanged(); }
        }

        private string _filterKeperluan;
        public string FilterKeperluan
        {
            get { return _filterKeperluan; }
            set { _filterKeperluan = value; OnPropertyChanged(); }
        }

        private string _filterTempatTujuan;
        public string FilterTempatTujuan
        {
            get { return _filterTempatTujuan; }
            set { _filterTempatTujuan = value; OnPropertyChanged(); }
        }

        private DateTime? _filterTglBerangkat;
        public DateTime? FilterTglBerangkat
        {
            get { return _filterTglBerangkat; }
            set { _filterTglBerangkat = value; OnPropertyChanged(); }
        }

        private DateTime? _filterTglKembali;
        public DateTime? FilterTglKembali
        {
            get { return _filterTglKembali; }
            set { _filterTglKembali = value; OnPropertyChanged(); }
        }

        private string _filterBebanAnggaran;
        public string FilterBebanAnggaran
        {
            get { return _filterBebanAnggaran; }
            set { _filterBebanAnggaran = value; OnPropertyChanged(); }
        }
        #endregion

    }

    public class SppdPesertaWpf : SppdPesertaDto
    {
        public decimal? TotalBiaya { get => Biaya.Sum(b => b.Jumlah); }
    }
}
