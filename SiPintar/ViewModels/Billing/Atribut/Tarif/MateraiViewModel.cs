using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.Tarif.Materai;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.Tarif
{
    public class MateraiViewModel : ViewModelBase
    {
        public MateraiViewModel(TarifViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);

        }

        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnSubmitEditCommand { get; }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MasterMeteraiDto> _dataMeterai = new ObservableCollection<MasterMeteraiDto>();
        public ObservableCollection<MasterMeteraiDto> DataMeterai
        {
            get { return _dataMeterai; }
            set
            {
                _dataMeterai = value;
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

        private bool _isEditFromDetail;
        public bool IsEditFromDetail
        {
            get => _isEditFromDetail;
            set { _isEditFromDetail = value; OnPropertyChanged(); }
        }

        private MasterMeteraiDto _selectedData;
        public MasterMeteraiDto SelectedData
        {
            get => _selectedData;
            set { _selectedData = value; OnPropertyChanged(); }
        }

        private MasterMeteraiDto _materaiForm;
        public MasterMeteraiDto MateraiForm
        {
            get => _materaiForm;
            set { _materaiForm = value; OnPropertyChanged(); }
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

    }
}
