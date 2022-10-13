using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.AdministrasiLain;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class AdministrasiLainViewModel : ViewModelBase
    {
        public AdministrasiLainViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }

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

        private string _kodeAdministrasiForm;
        public string KodeAdministrasiForm
        {
            get => _kodeAdministrasiForm;
            set { _kodeAdministrasiForm = value; OnPropertyChanged(); }
        }

        private string _namaAdministrasiForm;
        public string NamaAdministrasiForm
        {
            get => _namaAdministrasiForm;
            set { _namaAdministrasiForm = value; OnPropertyChanged(); }
        }

        private decimal? _biayaAdministrasiForm;
        public decimal? BiayaAdministrasiForm
        {
            get => _biayaAdministrasiForm;
            set { _biayaAdministrasiForm = value; OnPropertyChanged(); }
        }

        private MasterAdministrasiLainDto _selectedData;
        public MasterAdministrasiLainDto SelectedData
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

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }


        private string _totalData = "0";
        public string TotalData
        {
            get { return _totalData; }
            set
            {
                _totalData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAdministrasiLainDto> _dataAdministrasi;
        public ObservableCollection<MasterAdministrasiLainDto> DataAdministrasi
        {
            get => _dataAdministrasi;
            set { _dataAdministrasi = value; OnPropertyChanged(); }
        }


    }
}
