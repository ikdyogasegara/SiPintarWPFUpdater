using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Flag;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class FlagViewModel : ViewModelBase
    {
        public FlagViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }

        private ObservableCollection<MasterFlagDto> _flagList;
        public ObservableCollection<MasterFlagDto> FlagList
        {
            get => _flagList;
            set { _flagList = value; OnPropertyChanged(); }
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

        private MasterFlagDto _selectedData;
        public MasterFlagDto SelectedData
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

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }
    }
}
