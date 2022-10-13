using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Kepemilikan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut
{
    public class KepemilikanViewModel : ViewModelBase
    {
        public KepemilikanViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterKepemilikanDto> _masterKepemilikanList;
        public ObservableCollection<MasterKepemilikanDto> MasterKepemilikanList
        {
            get => _masterKepemilikanList;
            set { _masterKepemilikanList = value; OnPropertyChanged(); }
        }

        private MasterKepemilikanDto _selectedData;
        public MasterKepemilikanDto SelectedData
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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                if (value)
                {
                    OnSubmitCommand = OnSubmitEditCommand;
                }
                else
                {
                    OnSubmitCommand = OnSubmitAddCommand;
                }
                OnPropertyChanged();
            }
        }

        private bool _isSelectedAll;
        public bool IsSelectedAll
        {
            get => _isSelectedAll;
            set { _isSelectedAll = value; OnPropertyChanged(); }
        }

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

        private string _kodeKepemilikanForm;
        public string KodeKepemilikanForm
        {
            get => _kodeKepemilikanForm;
            set { _kodeKepemilikanForm = value; OnPropertyChanged(); }
        }

        private string _namaKepemilikanForm;
        public string NamaKepemilikanForm
        {
            get => _namaKepemilikanForm;
            set { _namaKepemilikanForm = value; OnPropertyChanged(); }
        }
    }
}
