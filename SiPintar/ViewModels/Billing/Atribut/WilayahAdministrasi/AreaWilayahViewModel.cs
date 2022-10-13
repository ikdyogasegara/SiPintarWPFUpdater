using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.AreaWilayah;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi
{
    public class AreaWilayahViewModel : ViewModelBase
    {
        public AreaWilayahViewModel(WilayahAdministrasiViewModel parentViewModel, IRestApiClientModel _restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, _restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, _restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, _restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, _restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private string _section;
        public string Section
        {
            get => _section;
            set { _section = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new ObservableCollection<MasterWilayahDto>();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get => _wilayahList;
            set { _wilayahList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyWilayah"); }
        }

        private MasterWilayahDto _selectedWilayah;
        public MasterWilayahDto SelectedWilayah
        {
            get => _selectedWilayah;
            set
            {
                _selectedWilayah = value;
                OnPropertyChanged();
                OnPropertyChanged("IsWilayahSelected");

                SelectedArea = null;
                AreaList.Clear();
                _ = Task.Run(() => OnLoadCommand.Execute("area"));
            }
        }

        private bool _isLoadingWilayah;
        public bool IsLoadingWilayah
        {
            get => _isLoadingWilayah;
            set { _isLoadingWilayah = value; OnPropertyChanged(); }
        }

        private bool _isWilayahSelected;
        public bool IsWilayahSelected
        {
            get => _isWilayahSelected || SelectedWilayah != null;
            set { _isWilayahSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyWilayah;
        public bool IsEmptyWilayah
        {
            get => _isEmptyWilayah || WilayahList == null || WilayahList.Count == 0;
            set { _isEmptyWilayah = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterAreaDto> _areaList = new ObservableCollection<MasterAreaDto>();
        public ObservableCollection<MasterAreaDto> AreaList
        {
            get => _areaList;
            set
            {
                _areaList = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyArea");
            }
        }

        private MasterAreaDto _selectedArea;
        public MasterAreaDto SelectedArea
        {
            get => _selectedArea;
            set
            {
                _selectedArea = value;
                OnPropertyChanged();
                OnPropertyChanged("IsAreaSelected");

                SelectedRayon = null;
                RayonList.Clear();
                _ = Task.Run(() => OnLoadCommand.Execute("rayon"));
            }
        }

        private bool _isLoadingArea;
        public bool IsLoadingArea
        {
            get => _isLoadingArea;
            set { _isLoadingArea = value; OnPropertyChanged(); }
        }

        private bool _isAreaSelected;
        public bool IsAreaSelected
        {
            get => _isAreaSelected || SelectedArea != null;
            set { _isAreaSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyArea;
        public bool IsEmptyArea
        {
            get => _isEmptyArea || AreaList == null || AreaList.Count == 0;
            set { _isEmptyArea = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterRayonDto> _rayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get => _rayonList;
            set { _rayonList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyRayon"); }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get => _selectedRayon;
            set { _selectedRayon = value; OnPropertyChanged(); OnPropertyChanged("IsRayonSelected"); }
        }

        private bool _isLoadingRayon;
        public bool IsLoadingRayon
        {
            get => _isLoadingRayon;
            set { _isLoadingRayon = value; OnPropertyChanged(); }
        }

        private bool _isRayonSelected;
        public bool IsRayonSelected
        {
            get => _isRayonSelected || SelectedRayon != null;
            set { _isRayonSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyRayon;
        public bool IsEmptyRayon
        {
            get => _isEmptyRayon || RayonList == null || RayonList.Count == 0;
            set { _isEmptyRayon = value; OnPropertyChanged(); }
        }


        private string _kodeForm;
        public string KodeForm
        {
            get => _kodeForm;
            set { _kodeForm = value; OnPropertyChanged(); }
        }

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }
    }
}
