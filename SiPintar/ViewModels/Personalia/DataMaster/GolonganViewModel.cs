using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.Golongan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class GolonganViewModel : ViewModelBase
    {
        public GolonganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnLoadGajiPokokCommand = new OnLoadGajiPokokCommand(this, restApi, isTest);
            OnOpenAddGajiPokokFormCommand = new OnOpenAddGajiPokokFormCommand(this, restApi, isTest);
            OnOpenEditGajiPokokFormCommand = new OnOpenEditGajiPokokFormCommand(this, restApi, isTest);
            OnOpenDeleteGajiPokokFormCommand = new OnOpenDeleteGajiPokokFormCommand(this, isTest);
            OnSubmitGajiPokokFormCommand = new OnSubmitGajiPokokFormCommand(this, restApi, isTest);
            OnSubmitDeleteGajiPokokFormCommand = new OnSubmitDeleteGajiPokokFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }
        public ICommand OnLoadGajiPokokCommand { get; }
        public ICommand OnOpenAddGajiPokokFormCommand { get; }
        public ICommand OnOpenEditGajiPokokFormCommand { get; }
        public ICommand OnOpenDeleteGajiPokokFormCommand { get; }
        public ICommand OnSubmitGajiPokokFormCommand { get; }
        public ICommand OnSubmitDeleteGajiPokokFormCommand { get; }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingGajiPokok = true;
        public bool IsLoadingGajiPokok
        {
            get { return _isLoadingGajiPokok; }
            set { _isLoadingGajiPokok = value; OnPropertyChanged(); }
        }

        private bool _isEmpty = true;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEmptyGajiPokok = true;
        public bool IsEmptyGajiPokok
        {
            get { return _isEmptyGajiPokok; }
            set { _isEmptyGajiPokok = value; OnPropertyChanged(); }
        }

        private bool _isEdit = true;
        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganPegawaiDto> _golonganList;
        public ObservableCollection<MasterGolonganPegawaiDto> GolonganList
        {
            get { return _golonganList; }
            set { _golonganList = value; OnPropertyChanged(); }
        }

        private MasterGolonganPegawaiDto _selectedData;
        public MasterGolonganPegawaiDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                GajiPokokPerGolonganList = value == null ? new ObservableCollection<MasterGajiPokokDto>() : new ObservableCollection<MasterGajiPokokDto>(GajiPokokList.Where(d => d.IdGolonganPegawai == value.IdGolonganPegawai).ToList());
                IsEmptyGajiPokok = !GajiPokokPerGolonganList.Any();
                SelectedDataGajiPokok = GajiPokokPerGolonganList.FirstOrDefault();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<int> _urutanList;
        public ObservableCollection<int> UrutanList
        {
            get { return _urutanList; }
            set { _urutanList = value; OnPropertyChanged(); }
        }

        private string _formKodeGolongan;
        public string FormKodeGolongan
        {
            get { return _formKodeGolongan; }
            set { _formKodeGolongan = value; OnPropertyChanged(); }
        }

        private string _formGolongan;
        public string FormGolongan
        {
            get { return _formGolongan; }
            set { _formGolongan = value; OnPropertyChanged(); }
        }

        private int? _formUrutan;
        public int? FormUrutan
        {
            get { return _formUrutan; }
            set { _formUrutan = value; OnPropertyChanged(); }
        }

        private int? _formTingkat;
        public int? FormTingkat
        {
            get { return _formTingkat; }
            set { _formTingkat = value; OnPropertyChanged(); }
        }

        private decimal? _formTunjangan;
        public decimal? FormTunjangan
        {
            get { return _formTunjangan; }
            set { _formTunjangan = value; OnPropertyChanged(); }
        }

        #region Gaji Pokok
        private ObservableCollection<MasterGajiPokokDto> _gajiPokokList;
        public ObservableCollection<MasterGajiPokokDto> GajiPokokList
        {
            get { return _gajiPokokList; }
            set { _gajiPokokList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGajiPokokDto> _gajiPokokPerGolonganList;
        public ObservableCollection<MasterGajiPokokDto> GajiPokokPerGolonganList
        {
            get { return _gajiPokokPerGolonganList; }
            set { _gajiPokokPerGolonganList = value; OnPropertyChanged(); }
        }

        private MasterGajiPokokDto _selectedDataGajiPokok;
        public MasterGajiPokokDto SelectedDataGajiPokok
        {
            get { return _selectedDataGajiPokok; }
            set { _selectedDataGajiPokok = value; OnPropertyChanged(); }
        }

        private int? _formMasaKerja;
        public int? FormMasaKerja
        {
            get { return _formMasaKerja; }
            set { _formMasaKerja = value; OnPropertyChanged(); }
        }

        private decimal? _formGaji;
        public decimal? FormGaji
        {
            get { return _formGaji; }
            set { _formGaji = value; OnPropertyChanged(); }
        }
        #endregion

    }
}
