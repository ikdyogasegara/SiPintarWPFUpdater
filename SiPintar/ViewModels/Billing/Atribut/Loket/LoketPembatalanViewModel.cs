using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Loket.LoketPembatalan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Loket
{
    public class LoketPembatalanViewModel : ViewModelBase
    {
        public LoketPembatalanViewModel(LoketViewModel parentViewModel, IRestApiClientModel restApi)
        {
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);

            _ = parentViewModel;
        }

        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterAlasanBatalDto> _pembatalanLoketList;
        public ObservableCollection<MasterAlasanBatalDto> PembatalanLoketList
        {
            get => _pembatalanLoketList;
            set { _pembatalanLoketList = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private MasterAlasanBatalDto _selectedData;
        public MasterAlasanBatalDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
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

        private string _alasanForm;
        public string AlasanForm
        {
            get => _alasanForm;
            set { _alasanForm = value; OnPropertyChanged(); }
        }

    }
}
