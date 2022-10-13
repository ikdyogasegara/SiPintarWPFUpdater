using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.WilayahAdministrasi.Dma;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.WilayahAdministrasi
{
    public class DmaViewModel : ViewModelBase
    {
        public DmaViewModel(WilayahAdministrasiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnExportCommand { get; }

        private ObservableCollection<MasterDmaDto> _dmaList;
        public ObservableCollection<MasterDmaDto> DmaList
        {
            get => _dmaList;
            set { _dmaList = value; OnPropertyChanged(); }
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

        private MasterDmaDto _selectedData;
        public MasterDmaDto SelectedData
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

        private string _namaForm;
        public string NamaForm
        {
            get => _namaForm;
            set { _namaForm = value; OnPropertyChanged(); }
        }

        private string _kodeForm;
        public string KodeForm
        {
            get => _kodeForm;
            set { _kodeForm = value; OnPropertyChanged(); }
        }
    }
}
