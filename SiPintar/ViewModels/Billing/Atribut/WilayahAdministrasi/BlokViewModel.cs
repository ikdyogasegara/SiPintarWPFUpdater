using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Blok;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi
{
    public class BlokViewModel : ViewModelBase
    {
        public BlokViewModel(WilayahAdministrasiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnGetLokasiCommand = new OnGetLokasiCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnGetLokasiCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterBlokDto> _masterBlokList;
        public ObservableCollection<MasterBlokDto> MasterBlokList
        {
            get => _masterBlokList;
            set { _masterBlokList = value; OnPropertyChanged(); }
        }

        private MasterBlokDto _selectedData;
        public MasterBlokDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private string _kodeBlokForm;
        public string KodeBlokForm
        {
            get => _kodeBlokForm;
            set { _kodeBlokForm = value; OnPropertyChanged(); }
        }

        private string _namaBlokForm;
        public string NamaBlokForm
        {
            get => _namaBlokForm;
            set { _namaBlokForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList;
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); }
        }

        private MasterWilayahDto _wilayahForm;
        public MasterWilayahDto WilayahForm
        {
            get => _wilayahForm;
            set { _wilayahForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAreaDto> _areaList;
        public ObservableCollection<MasterAreaDto> AreaList
        {
            get => _areaList;
            set { _areaList = value; OnPropertyChanged(); }
        }

        private MasterAreaDto _areaForm;
        public MasterAreaDto AreaForm
        {
            get => _areaForm;
            set { _areaForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterRayonDto> _rayonList;
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get => _rayonList;
            set { _rayonList = value; OnPropertyChanged(); }
        }

        private MasterRayonDto _rayonForm;
        public MasterRayonDto RayonForm
        {
            get => _rayonForm;
            set { _rayonForm = value; OnPropertyChanged(); }
        }
    }
}
