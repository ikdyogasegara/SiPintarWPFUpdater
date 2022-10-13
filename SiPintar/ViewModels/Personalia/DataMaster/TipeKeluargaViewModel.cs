using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Personalia.DataMaster.TipeKeluarga;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Personalia.DataMaster
{
    public class TipeKeluargaViewModel : ViewModelBase
    {
        public TipeKeluargaViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnToggleFilterCommand = new OnToggleFilterCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, restApi, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnToggleFilterCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitFormCommand { get; }
        public ICommand OnSubmitDeleteFormCommand { get; }

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

        private ObservableCollection<MasterTipeKeluargaDto> _tipeKeluargaList;
        public ObservableCollection<MasterTipeKeluargaDto> TipeKeluargaList
        {
            get { return _tipeKeluargaList; }
            set { _tipeKeluargaList = value; OnPropertyChanged(); }
        }

        private MasterTipeKeluargaDto _selectedData;
        public MasterTipeKeluargaDto SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private string _formKodeTipeKeluarga;
        public string FormKodeTipeKeluarga
        {
            get { return _formKodeTipeKeluarga; }
            set { _formKodeTipeKeluarga = value; OnPropertyChanged(); }
        }

        private bool? _formFlagKawin;
        public bool? FormFlagKawin
        {
            get { return _formFlagKawin; }
            set { _formFlagKawin = value; OnPropertyChanged(); }
        }

        private string _formUraian;
        public string FormUraian
        {
            get { return _formUraian; }
            set { _formUraian = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPersenPangan;
        public bool? FormFlagPersenPangan
        {
            get { return _formFlagPersenPangan; }
            set { _formFlagPersenPangan = value; OnPropertyChanged(); }
        }

        private decimal? _formNominalPangan;
        public decimal? FormNominalPangan
        {
            get { return _formNominalPangan; }
            set { _formNominalPangan = value; OnPropertyChanged(); }
        }

        private bool? _formFlagPersenKel;
        public bool? FormFlagPersenKel
        {
            get { return _formFlagPersenKel; }
            set { _formFlagPersenKel = value; OnPropertyChanged(); }
        }

        private decimal? _formNominalKel;
        public decimal? FormNominalKel
        {
            get { return _formNominalKel; }
            set { _formNominalKel = value; OnPropertyChanged(); }
        }

        private bool _isKodeTipeKeluargaChecked;
        public bool IsKodeTipeKeluargaChecked
        {
            get { return _isKodeTipeKeluargaChecked; }
            set { _isKodeTipeKeluargaChecked = value; OnPropertyChanged(); }
        }

        private bool _isUraianChecked;
        public bool IsUraianChecked
        {
            get { return _isUraianChecked; }
            set { _isUraianChecked = value; OnPropertyChanged(); }
        }

        private string _filterKodeTipeKeluarga;
        public string FilterKodeTipeKeluarga
        {
            get { return _filterKodeTipeKeluarga; }
            set { _filterKodeTipeKeluarga = value; OnPropertyChanged(); }
        }

        private string _filterUraian;
        public string FilterUraian
        {
            get { return _filterUraian; }
            set { _filterUraian = value; OnPropertyChanged(); }
        }

    }
}
