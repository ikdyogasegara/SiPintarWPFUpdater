using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.Paket.Material;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut.Paket
{
    [ExcludeFromCodeCoverage]
    public class PaketMaterialViewModel : ViewModelBase
    {
        public PaketMaterialViewModel(PaketViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddPaketFormCommand = new OnOpenAddPaketFormCommand(this);
            OnOpenAddBarangFormCommand = new OnOpenAddBarangFormCommand(this);
            OnOpenDeletePaketConfirmCommand = new OnOpenDeletePaketConfirmCommand(this);
            OnOpenDeleteBarangConfirmCommand = new OnOpenDeleteBarangConfirmCommand(this);
            OnOpenEditPaketFormCommand = new OnOpenEditPaketFormCommand(this);
            OnOpenEditBarangFormCommand = new OnOpenEditBarangFormCommand(this);
            OnSubmitAddPaketCommand = new OnSubmitAddPaketCommand(this, restApi);
            OnSubmitAddBarangStep1Command = new OnSubmitAddBarangStep1Command(this);
            OnSubmitAddBarangStep2Command = new OnSubmitAddBarangStep2Command(this, restApi);
            OnSubmitDeletePaketCommand = new OnSubmitDeletePaketCommand(this, restApi);
            OnSubmitDeleteBarangCommand = new OnSubmitDeleteBarangCommand(this, restApi);
            OnSubmitEditPaketCommand = new OnSubmitEditPaketCommand(this, restApi);
            OnSubmitEditBarangCommand = new OnSubmitEditBarangCommand(this, restApi);
            OnSearchBarangCommand = new OnSearchBarangCommand(this, restApi);
            OnResetSearchBarangCommand = new OnResetSearchBarangCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddPaketFormCommand { get; }
        public ICommand OnOpenAddBarangFormCommand { get; }
        public ICommand OnOpenDeletePaketConfirmCommand { get; }
        public ICommand OnOpenDeleteBarangConfirmCommand { get; }
        public ICommand OnOpenEditPaketFormCommand { get; }
        public ICommand OnOpenEditBarangFormCommand { get; }
        public ICommand OnSubmitAddPaketCommand { get; }
        public ICommand OnSubmitAddBarangStep1Command { get; }
        public ICommand OnSubmitAddBarangStep2Command { get; }
        public ICommand OnSubmitDeletePaketCommand { get; }
        public ICommand OnSubmitDeleteBarangCommand { get; }
        public ICommand OnSubmitEditPaketCommand { get; }
        public ICommand OnSubmitEditBarangCommand { get; }
        public ICommand OnSearchBarangCommand { get; }
        public ICommand OnResetSearchBarangCommand { get; }

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

        private ObservableCollection<MasterPaketMaterialDto> _paketList = new ObservableCollection<MasterPaketMaterialDto>();
        public ObservableCollection<MasterPaketMaterialDto> PaketList
        {
            get => _paketList;
            set { _paketList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyPaket"); }
        }

        private MasterPaketMaterialDto _selectedPaket;
        public MasterPaketMaterialDto SelectedPaket
        {
            get => _selectedPaket;
            set
            {
                _selectedPaket = value;
                OnPropertyChanged();
                OnPropertyChanged("IsPaketSelected");

                SelectedRincian = null;
                _ = Task.Run(() => OnLoadCommand.Execute("rincian"));
            }
        }

        private bool _isLoadingPaket;
        public bool IsLoadingPaket
        {
            get => _isLoadingPaket;
            set { _isLoadingPaket = value; OnPropertyChanged(); }
        }

        private bool _isPaketSelected;
        public bool IsPaketSelected
        {
            get => _isPaketSelected || SelectedPaket != null;
            set { _isPaketSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyPaket;
        public bool IsEmptyPaket
        {
            get => _isEmptyPaket || PaketList == null || PaketList.Count == 0;
            set { _isEmptyPaket = value; OnPropertyChanged(); }
        }

        private List<MasterPaketMaterialDetailDto> _rincianList = new List<MasterPaketMaterialDetailDto>();
        public List<MasterPaketMaterialDetailDto> RincianList
        {
            get => _rincianList;
            set { _rincianList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyRincian"); }
        }

        private MasterPaketMaterialDetailDto _selectedRincian;
        public MasterPaketMaterialDetailDto SelectedRincian
        {
            get => _selectedRincian;
            set { _selectedRincian = value; OnPropertyChanged(); OnPropertyChanged("IsRincianSelected"); }
        }

        private bool _isLoadingRincian;
        public bool IsLoadingRincian
        {
            get => _isLoadingRincian;
            set { _isLoadingRincian = value; OnPropertyChanged(); }
        }

        private bool _isRincianSelected;
        public bool IsRincianSelected
        {
            get => _isRincianSelected || SelectedRincian != null;
            set { _isRincianSelected = value; OnPropertyChanged(); }
        }

        private bool _isEmptyRincian;
        public bool IsEmptyRincian
        {
            get => _isEmptyRincian || RincianList == null || RincianList.Count == 0;
            set { _isEmptyRincian = value; OnPropertyChanged(); }
        }

        // form paket

        private string _kodePaketForm;
        public string KodePaketForm
        {
            get => _kodePaketForm;
            set { _kodePaketForm = value; OnPropertyChanged(); }
        }

        private string _namaPaketForm;
        public string NamaPaketForm
        {
            get => _namaPaketForm;
            set { _namaPaketForm = value; OnPropertyChanged(); }
        }

        // end form paket

        // form barang

        private string _namaBarangForm;
        public string NamaBarangForm
        {
            get => _namaBarangForm;
            set { _namaBarangForm = value; OnPropertyChanged(); }
        }

        private bool _isTriggerSearch;
        public bool IsTriggerSearch
        {
            get => _isTriggerSearch;
            set { _isTriggerSearch = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterMaterialDto> _searchList = new ObservableCollection<MasterMaterialDto>();
        public ObservableCollection<MasterMaterialDto> SearchList
        {
            get => _searchList;
            set { _searchList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptySearch"); }
        }

        private bool _isEmptySearch;
        public bool IsEmptySearch
        {
            get => _isEmptySearch || SearchList == null || SearchList.Count == 0;
            set { _isEmptySearch = value; OnPropertyChanged(); }
        }

        private MasterMaterialDto _selectedBarang = new MasterMaterialDto();
        public MasterMaterialDto SelectedBarang
        {
            get => _selectedBarang;
            set { _selectedBarang = value; OnPropertyChanged(); }
        }

        private decimal? _kuantitasForm;
        public decimal? KuantitasForm
        {
            get => _kuantitasForm;
            set { _kuantitasForm = value; OnPropertyChanged(); }
        }

        // end form barang

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
