using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Meteran.MerkMeter;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Meteran
{
    public class MerkMeterViewModel : ViewModelBase
    {
        public MerkMeterViewModel(MeteranViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterMerekMeterDto> _merkMeterList;
        public ObservableCollection<MasterMerekMeterDto> MerkMeterList
        {
            get => _merkMeterList;
            set { _merkMeterList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
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

        private MasterMerekMeterDto _selectedData;
        public MasterMerekMeterDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private string _kodeForm;
        public string KodeForm
        {
            get => _kodeForm;
            set { _kodeForm = value; OnPropertyChanged(); }
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
    }
}
