using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.Material;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut
{
    public class MaterialViewModel : ViewModelBase
    {
        public MaterialViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {

            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitEditCommand = new OnSubmitEditCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnResetFilterCommand = new OnResetFilterCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteFormCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitEditCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnSubmitCommand { get; set; }
        public ICommand OnExportCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnResetFilterCommand { get; }

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

        private ObservableCollection<MasterMaterialDto> _masterMaterialList = new ObservableCollection<MasterMaterialDto>();
        public ObservableCollection<MasterMaterialDto> MasterMaterialList
        {
            get { return _masterMaterialList; }
            set
            {
                _masterMaterialList = value;
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

        private MasterMaterialDto _selectedData;
        public MasterMaterialDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterMaterialDto _materialForm;
        public MasterMaterialDto MaterialForm
        {
            get { return _materialForm; }
            set
            {
                _materialForm = value;
                OnPropertyChanged();
            }
        }

        private MasterMaterialDto _materialFilter = new MasterMaterialDto();
        public MasterMaterialDto MaterialFilter
        {
            get { return _materialFilter; }
            set
            {
                _materialFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isPreviousButtonEnabled;
        public bool IsPreviousButtonEnabled
        {
            get { return _isPreviousButtonEnabled; }
            set
            {
                _isPreviousButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabled;
        public bool IsNextButtonEnabled
        {
            get { return _isNextButtonEnabled; }
            set
            {
                _isNextButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisible;
        public bool IsLeftPageNumberFillerVisible
        {
            get { return _isLeftPageNumberFillerVisible; }
            set
            {
                _isLeftPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisible;
        public bool IsRightPageNumberFillerVisible
        {
            get { return _isRightPageNumberFillerVisible; }
            set
            {
                _isRightPageNumberFillerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActive;
        public bool IsLeftPageNumberActive
        {
            get { return _isLeftPageNumberActive; }
            set
            {
                _isLeftPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActive;
        public bool IsRightPageNumberActive
        {
            get { return _isRightPageNumberActive; }
            set
            {
                _isRightPageNumberActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisible;
        public bool IsMiddlePageNumberVisible
        {
            get { return _isMiddlePageNumberVisible; }
            set
            {
                _isMiddlePageNumberVisible = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalPage;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }

        private int _totalRecord;
        public int TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
                OnPropertyChanged();
            }
        }

        private int _limit = 50;
        public int Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                OnPropertyChanged();
                OnFilterCommand.Execute(null);
            }
        }

        private bool _isKodeMaterialChecked;
        public bool IsKodeMaterialChecked
        {
            get { return _isKodeMaterialChecked; }
            set
            {
                _isKodeMaterialChecked = value;
                if (!value)
                {
                    var temp = MaterialFilter;
                    temp.KodeMaterial = null;
                    MaterialFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNamaMaterialChecked;
        public bool IsNamaMaterialChecked
        {
            get { return _isNamaMaterialChecked; }
            set
            {
                _isNamaMaterialChecked = value;
                if (!value)
                {
                    var temp = MaterialFilter;
                    temp.NamaMaterial = null;
                    MaterialFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<string, bool>> ListDataKategori
        {
            get
            {
                var ListOption = new string[] { "Standard", "Limbah" };

                var data = new ObservableCollection<KeyValuePair<string, bool>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<string, bool>(item, item == "Limbah"));

                return data;
            }
        }

        private KeyValuePair<string, bool> _selectedDataKategori = new KeyValuePair<string, bool>("Standard", false);
        public KeyValuePair<string, bool> SelectedDataKategori
        {
            get { return _selectedDataKategori; }
            set
            {
                _selectedDataKategori = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var ListOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
            }
        }
    }
}
