using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.Pendidikan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class PendidikanViewModel : ViewModelBase
    {
        public PendidikanViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }

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

        private ObservableCollection<MasterPendidikanDto> _pendidikanList;
        public ObservableCollection<MasterPendidikanDto> PendidikanList
        {
            get { return _pendidikanList; }
            set { _pendidikanList = value; OnPropertyChanged(); }
        }

        private MasterPendidikanDto _selectedData;
        public MasterPendidikanDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterGolonganPegawaiDto> _golonganList;
        public ObservableCollection<MasterGolonganPegawaiDto> GolonganList
        {
            get { return _golonganList; }
            set { _golonganList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<int> _urutanList;
        public ObservableCollection<int> UrutanList
        {
            get { return _urutanList; }
            set { _urutanList = value; OnPropertyChanged(); }
        }

        private string _formPendidikan;
        public string FormPendidikan
        {
            get { return _formPendidikan; }
            set { _formPendidikan = value; OnPropertyChanged(); }
        }

        private int? _formUrutan;
        public int? FormUrutan
        {
            get { return _formUrutan; }
            set { _formUrutan = value; OnPropertyChanged(); }
        }

        private int? _formIdGolonganPegawaiMin;
        public int? FormIdGolonganPegawaiMin
        {
            get { return _formIdGolonganPegawaiMin; }
            set { _formIdGolonganPegawaiMin = value; OnPropertyChanged(); }
        }

        private int? _formIdGolonganPegawaiMax;
        public int? FormIdGolonganPegawaiMax
        {
            get { return _formIdGolonganPegawaiMax; }
            set { _formIdGolonganPegawaiMax = value; OnPropertyChanged(); }
        }

    }
}
