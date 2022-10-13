using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.Tunjangan.MasterTunjangan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.Tunjangan
{
    public class MasterTunjanganViewModel : ViewModelBase
    {
        public MasterTunjanganViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
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

        private ObservableCollection<MasterJenisTunjanganDto> _masterTunjanganList;
        public ObservableCollection<MasterJenisTunjanganDto> MasterTunjanganList
        {
            get { return _masterTunjanganList; }
            set { _masterTunjanganList = value; OnPropertyChanged(); }
        }

        private MasterJenisTunjanganDto _selectedData;
        public MasterJenisTunjanganDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private string _formKode;
        public string FormKode
        {
            get { return _formKode; }
            set { _formKode = value; OnPropertyChanged(); }
        }

        private string _formNamaTunjangan;
        public string FormNamaTunjangan
        {
            get { return _formNamaTunjangan; }
            set { _formNamaTunjangan = value; OnPropertyChanged(); }
        }

        private int? _formUrutan;
        public int? FormUrutan
        {
            get { return _formUrutan; }
            set { _formUrutan = value; OnPropertyChanged(); }
        }

        private string _formTipe;
        public string FormTipe
        {
            get { return _formTipe; }
            set { _formTipe = value; OnPropertyChanged(); }
        }

        #region Filter
        private bool _isKodeJenisTunjanganChecked;
        public bool IsKodeJenisTunjanganChecked
        {
            get { return _isKodeJenisTunjanganChecked; }
            set { _isKodeJenisTunjanganChecked = value; OnPropertyChanged(); }
        }

        private bool _isNamaJenisTunjanganChecked;
        public bool IsNamaJenisTunjanganChecked
        {
            get { return _isNamaJenisTunjanganChecked; }
            set { _isNamaJenisTunjanganChecked = value; OnPropertyChanged(); }
        }

        private bool _isTipeChecked;
        public bool IsTipeChecked
        {
            get { return _isTipeChecked; }
            set { _isTipeChecked = value; OnPropertyChanged(); }
        }

        private string _filterKodeJenisTunjangan;
        public string FilterKodeJenisTunjangan
        {
            get { return _filterKodeJenisTunjangan; }
            set { _filterKodeJenisTunjangan = value; OnPropertyChanged(); }
        }

        private string _filterNamaJenisTunjangan;
        public string FilterNamaJenisTunjangan
        {
            get { return _filterNamaJenisTunjangan; }
            set { _filterNamaJenisTunjangan = value; OnPropertyChanged(); }
        }

        private string _filterTipe;
        public string FilterTipe
        {
            get { return _filterTipe; }
            set { _filterTipe = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
