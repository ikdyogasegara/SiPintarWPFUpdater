using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.GolonganLimbah;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class GolonganLimbahViewModel : ViewModelBase
    {
        public GolonganLimbahViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
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

        private MasterTarifLimbahDto _selectedData;
        public MasterTarifLimbahDto SelectedData
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

        private ObservableCollection<MasterTarifLimbahDto> _masterGolonganList = new ObservableCollection<MasterTarifLimbahDto>();
        public ObservableCollection<MasterTarifLimbahDto> MasterGolonganList
        {
            get { return _masterGolonganList; }
            set
            {
                _masterGolonganList = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private MasterTarifLimbahDto _golonganLimbahForm;
        public MasterTarifLimbahDto GolonganFormLimbah
        {
            get { return _golonganLimbahForm; }
            set
            {
                _golonganLimbahForm = value;
                OnPropertyChanged();
            }
        }

        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged();
            }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listYear = new List<int>();
        public List<int> ListYear
        {
            get { return _listYear; }
            set
            {
                _listYear = value;
                OnPropertyChanged();
            }
        }

    }
}
