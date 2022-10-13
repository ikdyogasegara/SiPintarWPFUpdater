using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.Pemeliharaan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class PemeliharaanViewModel : ViewModelBase
    {
        public PemeliharaanViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnExportCommand { get; }


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

        private string _kodePemeliharaanForm;
        public string KodePemeliharaanForm
        {
            get => _kodePemeliharaanForm;
            set { _kodePemeliharaanForm = value; OnPropertyChanged(); }
        }

        private string _namaPemeliharaanForm;
        public string NamaPemeliharaanForm
        {
            get => _namaPemeliharaanForm;
            set { _namaPemeliharaanForm = value; OnPropertyChanged(); }
        }

        private decimal? _biayaPemeliharaanForm = decimal.Zero;
        public decimal? BiayaPemeliharaanForm
        {
            get => _biayaPemeliharaanForm;
            set { _biayaPemeliharaanForm = value; OnPropertyChanged(); }
        }


        private ObservableCollection<MasterPemeliharaanLainDto> _data = new ObservableCollection<MasterPemeliharaanLainDto>();
        public ObservableCollection<MasterPemeliharaanLainDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private MasterPemeliharaanLainDto _selectedData;
        public MasterPemeliharaanLainDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
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


    }
}
