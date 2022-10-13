using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.Retribusi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class RetribusiViewModel : ViewModelBase
    {
        public RetribusiViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnExportCommand { get; }

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

        private ObservableCollection<MasterRetribusiLainDto> _masterRetribusiList = new ObservableCollection<MasterRetribusiLainDto>();
        public ObservableCollection<MasterRetribusiLainDto> MasterRetribusiList
        {
            get { return _masterRetribusiList; }
            set
            {
                _masterRetribusiList = value;
                OnPropertyChanged();
            }
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

        private MasterRetribusiLainDto _selectedData;
        public MasterRetribusiLainDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterRetribusiLainDto _retribusiForm;
        public MasterRetribusiLainDto RetribusiForm
        {
            get { return _retribusiForm; }
            set
            {
                _retribusiForm = value;
                OnPropertyChanged();
            }
        }

        private string _namaRetribusiForm;
        public string NamaRetribusiForm
        {
            get => _namaRetribusiForm;
            set { _namaRetribusiForm = value; OnPropertyChanged(); }
        }

        private decimal? _biayaRetribusiForm = decimal.Zero;
        public decimal? BiayaRetribusiForm
        {
            get => _biayaRetribusiForm;
            set { _biayaRetribusiForm = value; OnPropertyChanged(); }
        }

        private string _kodeRetribusiForm;
        public string KodeRetribusiForm
        {
            get => _kodeRetribusiForm;
            set { _kodeRetribusiForm = value; OnPropertyChanged(); }
        }

    }
}
