using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.GolonganLLTT;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class GolonganLlttViewModel : ViewModelBase
    {
        public GolonganLlttViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnExportCommand = new OnExportCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
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

        private MasterTarifLlttDto _selectedData;
        public MasterTarifLlttDto SelectedData
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

        private ObservableCollection<MasterTarifLlttDto> _masterGolonganList = new ObservableCollection<MasterTarifLlttDto>();
        public ObservableCollection<MasterTarifLlttDto> MasterGolonganList
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

        private MasterTarifLlttDto _golonganLimbahLltt;
        public MasterTarifLlttDto GolonganFormLltt
        {
            get { return _golonganLimbahLltt; }
            set
            {
                _golonganLimbahLltt = value;
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

    }
}
