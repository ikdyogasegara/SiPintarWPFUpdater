using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.RumusVolume;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut
{
    public class RumusVolumeViewModel : ViewModelBase
    {
        public RumusVolumeViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnRefreshCommand = new OnRefreshCommand(this);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmCommand = new OnOpenDeleteConfirmCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnResetFilterCommand = new OnResetFilterCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnResetFilterCommand { get; }

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

        private bool _isEdit;
        public bool IsEdit
        {
            get => _isEdit;
            set { _isEdit = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterRumusVolumeOngkosDto> _dataList = new ObservableCollection<MasterRumusVolumeOngkosDto>();
        public ObservableCollection<MasterRumusVolumeOngkosDto> DataList
        {
            get => _dataList;
            set { _dataList = value; OnPropertyChanged(); OnPropertyChanged("IsEmpty"); }
        }

        private MasterRumusVolumeOngkosDto _selectedData;
        public MasterRumusVolumeOngkosDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
                OnPropertyChanged("IsDataSelected");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isDataSelected;
        public bool IsDataSelected
        {
            get => _isDataSelected || SelectedData != null;
            set { _isDataSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty || DataList == null || DataList.Count == 0;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private MasterRumusVolumeOngkosDto _filterData = new MasterRumusVolumeOngkosDto() { FlagHapus = false };
        public MasterRumusVolumeOngkosDto FilterData
        {
            get => _filterData;
            set
            {
                _filterData = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilterKodeOngkosChecked;
        public bool IsFilterKodeOngkosChecked
        {
            get { return _isFilterKodeOngkosChecked; }
            set
            {
                _isFilterKodeOngkosChecked = value;
                if (!value)
                {
                    var temp = FilterData;
                    temp.KodeOngkos = null;
                    FilterData = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isFilterNamaOngkosChecked;
        public bool IsFilterNamaOngkosChecked
        {
            get { return _isFilterNamaOngkosChecked; }
            set
            {
                _isFilterNamaOngkosChecked = value;
                if (!value)
                {
                    var temp = FilterData;
                    temp.NamaOngkos = null;
                    FilterData = temp;
                }
                OnPropertyChanged();
            }
        }

        private MasterRumusVolumeOngkosDto _formData = new MasterRumusVolumeOngkosDto();
        public MasterRumusVolumeOngkosDto FormData
        {
            get => _formData;
            set
            {
                _formData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterOngkosDto> _ongkosList;
        public ObservableCollection<MasterOngkosDto> OngkosList
        {
            get => _ongkosList;
            set
            {
                _ongkosList = value;
                OnPropertyChanged();
            }
        }

        private MasterOngkosDto _selectedOngkos;
        public MasterOngkosDto SelectedOngkos
        {
            get => _selectedOngkos;
            set
            {
                _selectedOngkos = value;
                OnPropertyChanged();

                FormData.IdOngkos = _selectedOngkos?.IdOngkos;
            }
        }

        private bool _isBatas1Active = true;
        public bool IsBatas1Active
        {
            get { return _isBatas1Active; }
            set
            {
                _isBatas1Active = value;
                if (!value)
                {
                    var temp = FormData;
                    temp.Bb1 = 0;
                    temp.Ba1 = 0;
                    temp.Volum1 = 0;
                    FormData = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBatas2Active = true;
        public bool IsBatas2Active
        {
            get { return _isBatas2Active; }
            set
            {
                _isBatas2Active = value;
                if (!value)
                {
                    var temp = FormData;
                    temp.Bb2 = 0;
                    temp.Ba2 = 0;
                    temp.Volum2 = 0;
                    FormData = temp;
                }
                else
                {
                    var temp = FormData;
                    temp.Bb2 = temp.Ba1;
                    temp.Ba2 = temp.Bb2 + 1;
                    temp.Volum2 = 0;
                    FormData = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBatas3Active = true;
        public bool IsBatas3Active
        {
            get { return _isBatas3Active; }
            set
            {
                _isBatas3Active = value;
                if (!value)
                {
                    var temp = FormData;
                    temp.Bb3 = 0;
                    temp.Ba3 = 0;
                    temp.Volum3 = 0;
                    FormData = temp;
                }
                else
                {
                    var temp = FormData;
                    temp.Bb3 = temp.Ba2;
                    temp.Ba3 = temp.Bb3 + 1;
                    temp.Volum2 = 0;
                    FormData = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBatas4Active = true;
        public bool IsBatas4Active
        {
            get { return _isBatas4Active; }
            set
            {
                _isBatas4Active = value;
                if (!value)
                {
                    var temp = FormData;
                    temp.Bb4 = 0;
                    temp.Ba4 = 0;
                    temp.Volum4 = 0;
                    FormData = temp;
                }
                else
                {
                    var temp = FormData;
                    temp.Bb4 = temp.Ba3;
                    temp.Ba4 = temp.Bb4 + 1;
                    temp.Volum2 = 0;
                    FormData = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isBatas5Active = true;
        public bool IsBatas5Active
        {
            get { return _isBatas5Active; }
            set
            {
                _isBatas5Active = value;
                if (!value)
                {
                    var temp = FormData;
                    temp.Bb5 = 0;
                    temp.Ba5 = 0;
                    temp.Volum5 = 0;
                    FormData = temp;
                }
                else
                {
                    var temp = FormData;
                    temp.Bb5 = temp.Ba4;
                    temp.Ba5 = temp.Bb5 + 1;
                    temp.Volum2 = 0;
                    FormData = temp;
                }
                OnPropertyChanged();
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
