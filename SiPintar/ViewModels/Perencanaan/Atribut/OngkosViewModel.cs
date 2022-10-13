using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.Ongkos;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut
{
    public class OngkosViewModel : ViewModelBase
    {
        public OngkosViewModel(AtributViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi);
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

        private bool _isEnablePerhitungan;
        public bool IsEnablePerhitungan
        {
            get => _isEnablePerhitungan;
            set { _isEnablePerhitungan = value; OnPropertyChanged(); }
        }

        private bool _isEnableBiaya = true;
        public bool IsEnableBiaya
        {
            get => _isEnableBiaya;
            set { _isEnableBiaya = value; OnPropertyChanged(); }
        }

        private List<MasterOngkosDto> _masterOngkosList = new List<MasterOngkosDto>();
        public List<MasterOngkosDto> MasterOngkosList
        {
            get { return _masterOngkosList; }
            set
            {
                _masterOngkosList = value;
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

        private MasterOngkosDto _selectedData;
        public MasterOngkosDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterOngkosDto _ongkosForm;
        public MasterOngkosDto OngkosForm
        {
            get { return _ongkosForm; }
            set
            {
                _ongkosForm = value;
                OnPropertyChanged();
            }
        }

        private MasterOngkosDto _ongkosFilter = new MasterOngkosDto();
        public MasterOngkosDto OngkosFilter
        {
            get { return _ongkosFilter; }
            set
            {
                _ongkosFilter = value;
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

        private bool _isKodeOngkosChecked;
        public bool IsKodeOngkosChecked
        {
            get { return _isKodeOngkosChecked; }
            set
            {
                _isKodeOngkosChecked = value;
                if (!value)
                {
                    var temp = OngkosFilter;
                    temp.KodeOngkos = null;
                    OngkosFilter = temp;
                }
                OnPropertyChanged();
            }
        }

        private bool _isNamaOngkosChecked;
        public bool IsNamaOngkosChecked
        {
            get { return _isNamaOngkosChecked; }
            set
            {
                _isNamaOngkosChecked = value;
                if (!value)
                {
                    var temp = OngkosFilter;
                    temp.NamaOngkos = null;
                    OngkosFilter = temp;
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

        public ObservableCollection<KeyValuePair<int, string>> ListPostBiaya
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(1, "pemasangan"),
                    new KeyValuePair<int, string>(2, "pendaftaran"),
                    new KeyValuePair<int, string>(3, "meterai"),
                    new KeyValuePair<int, string>(4, "jaminan"),
                    new KeyValuePair<int, string>(5, "tanggungan"),
                    new KeyValuePair<int, string>(6, "perencanaan"),
                };

                return data;
            }
        }

        private KeyValuePair<int, string> _selectedDataPostBiaya;
        public KeyValuePair<int, string> SelectedDataPostBiaya
        {
            get { return _selectedDataPostBiaya; }
            set
            {
                _selectedDataPostBiaya = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<KeyValuePair<int, string>> ListTipe
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(1, "Ongkos"),
                    new KeyValuePair<int, string>(2, "Administrasi"),
                    new KeyValuePair<int, string>(3, "Pekerjaan Persiapan"),
                    new KeyValuePair<int, string>(4, "Pekerjaan Pemasangan"),
                    new KeyValuePair<int, string>(5, "Pekerjaan Tapping & Crossing"),
                    new KeyValuePair<int, string>(6, "Pekerjaan Bongkar/Pasang"),
                    new KeyValuePair<int, string>(7, "Pekerjaan Lain-Lain"),
                    new KeyValuePair<int, string>(8, "Potongan"),
                    new KeyValuePair<int, string>(9, "Upah Kerja"),
                    new KeyValuePair<int, string>(10, "Biaya Rangkaian"),
                    new KeyValuePair<int, string>(10, "Biaya Perencanaan"),
                };

                return data;
            }
        }

        private KeyValuePair<int, string> _selectedDataTipe;
        public KeyValuePair<int, string> SelectedDataTipe
        {
            get { return _selectedDataTipe; }
            set
            {
                _selectedDataTipe = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<string, bool>> ListPerhitungan
        {
            get
            {
                var ListOption = new string[] { "Reguler", "Persentase" };

                var data = new ObservableCollection<KeyValuePair<string, bool>>();

                foreach (var item in ListOption)
                    data.Add(new KeyValuePair<string, bool>(item, item == "Persentase"));

                return data;
            }
        }

        private KeyValuePair<string, bool> _selectedDataPerhitungan;
        public KeyValuePair<string, bool> SelectedDataPerhitungan
        {
            get { return _selectedDataPerhitungan; }
            set
            {
                _selectedDataPerhitungan = value;

                if (SelectedDataPerhitungan.Key == "Reguler")
                {
                    IsEnablePerhitungan = false;
                    IsEnableBiaya = true;
                    SelectedDataPaketMaterial = new MasterPaketMaterialDto();
                }
                else
                {
                    IsEnablePerhitungan = true;
                    IsEnableBiaya = false;
                }

                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPaketMaterialDto> _paketMaterialList = new ObservableCollection<MasterPaketMaterialDto>();
        public ObservableCollection<MasterPaketMaterialDto> PaketMaterialList
        {
            get { return _paketMaterialList; }
            set
            {
                _paketMaterialList = value;
                OnPropertyChanged();
            }
        }

        private MasterPaketMaterialDto _selectedDataPaketMaterial;
        public MasterPaketMaterialDto SelectedDataPaketMaterial
        {
            get { return _selectedDataPaketMaterial; }
            set
            {
                _selectedDataPaketMaterial = value;
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
