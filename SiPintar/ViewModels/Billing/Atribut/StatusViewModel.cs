using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Status;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class StatusViewModel : ViewModelBase
    {
        public StatusViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
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
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }

        private ObservableCollection<MasterStatusDto> _statusList;
        public ObservableCollection<MasterStatusDto> StatusList
        {
            get => _statusList;
            set { _statusList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private MasterStatusDto _selectedData;
        public MasterStatusDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
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

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }

        private bool _includeRekeningAirForm;
        public bool IncludeRekeningAirForm
        {
            get => _includeRekeningAirForm;
            set { _includeRekeningAirForm = value; OnPropertyChanged(); }
        }

        private bool _includeRekeningLimbahForm;
        public bool IncludeRekeningLimbahForm
        {
            get => _includeRekeningLimbahForm;
            set { _includeRekeningLimbahForm = value; OnPropertyChanged(); }
        }

        private bool _includeRekeningLLTTForm;
        public bool IncludeRekeningLLTTForm
        {
            get => _includeRekeningLLTTForm;
            set { _includeRekeningLLTTForm = value; OnPropertyChanged(); }
        }

        private bool _tanpaBiayaAirForm;
        public bool TanpaBiayaAirForm
        {
            get => _tanpaBiayaAirForm;
            set { _tanpaBiayaAirForm = value; OnPropertyChanged(); }
        }
    }
}
