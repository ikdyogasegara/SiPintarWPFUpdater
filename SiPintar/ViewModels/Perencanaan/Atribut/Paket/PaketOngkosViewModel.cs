using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Perencanaan.Atribut.Paket.Ongkos;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Perencanaan.Atribut.Paket
{
    [ExcludeFromCodeCoverage]
    public class PaketOngkosViewModel : ViewModelBase
    {
        public PaketOngkosViewModel(PaketViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddPaketFormCommand = new OnOpenAddPaketFormCommand(this);
            OnOpenAddItemFormCommand = new OnOpenAddItemFormCommand(this);
            OnOpenDeletePaketConfirmCommand = new OnOpenDeletePaketConfirmCommand(this);
            OnOpenDeleteItemConfirmCommand = new OnOpenDeleteItemConfirmCommand(this);
            OnOpenEditPaketFormCommand = new OnOpenEditPaketFormCommand(this);
            OnOpenEditItemFormCommand = new OnOpenEditItemFormCommand(this);
            OnSubmitAddPaketCommand = new OnSubmitAddPaketCommand(this, restApi);
            OnSubmitAddItemStep1Command = new OnSubmitAddItemStep1Command(this);
            OnSubmitAddItemStep2Command = new OnSubmitAddItemStep2Command(this, restApi);
            OnSubmitDeletePaketCommand = new OnSubmitDeletePaketCommand(this, restApi);
            OnSubmitDeleteItemCommand = new OnSubmitDeleteItemCommand(this, restApi);
            OnSubmitEditPaketCommand = new OnSubmitEditPaketCommand(this, restApi);
            OnSubmitEditItemCommand = new OnSubmitEditItemCommand(this, restApi);
            OnSearchItemCommand = new OnSearchItemCommand(this, restApi);
            OnResetSearchItemCommand = new OnResetSearchItemCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddPaketFormCommand { get; }
        public ICommand OnOpenAddItemFormCommand { get; }
        public ICommand OnOpenDeletePaketConfirmCommand { get; }
        public ICommand OnOpenDeleteItemConfirmCommand { get; }
        public ICommand OnOpenEditPaketFormCommand { get; }
        public ICommand OnOpenEditItemFormCommand { get; }
        public ICommand OnSubmitAddPaketCommand { get; }
        public ICommand OnSubmitAddItemStep1Command { get; }
        public ICommand OnSubmitAddItemStep2Command { get; }
        public ICommand OnSubmitDeletePaketCommand { get; }
        public ICommand OnSubmitDeleteItemCommand { get; }
        public ICommand OnSubmitEditPaketCommand { get; }
        public ICommand OnSubmitEditItemCommand { get; }
        public ICommand OnSearchItemCommand { get; }
        public ICommand OnResetSearchItemCommand { get; }

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

        private ObservableCollection<MasterPaketOngkosDto> _paketList = new ObservableCollection<MasterPaketOngkosDto>();
        public ObservableCollection<MasterPaketOngkosDto> PaketList
        {
            get => _paketList;
            set { _paketList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyPaket"); }
        }

        private MasterPaketOngkosDto _selectedPaket;
        public MasterPaketOngkosDto SelectedPaket
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

        private List<MasterPaketOngkosDetailDto> _rincianList = new List<MasterPaketOngkosDetailDto>();
        public List<MasterPaketOngkosDetailDto> RincianList
        {
            get => _rincianList;
            set { _rincianList = value; OnPropertyChanged(); OnPropertyChanged("IsEmptyRincian"); }
        }

        private MasterPaketOngkosDetailDto _selectedRincian;
        public MasterPaketOngkosDetailDto SelectedRincian
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

        // form item

        private string _namaItemForm;
        public string NamaItemForm
        {
            get => _namaItemForm;
            set { _namaItemForm = value; OnPropertyChanged(); }
        }

        private bool _isTriggerSearch;
        public bool IsTriggerSearch
        {
            get => _isTriggerSearch;
            set { _isTriggerSearch = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterOngkosDto> _searchList = new ObservableCollection<MasterOngkosDto>();
        public ObservableCollection<MasterOngkosDto> SearchList
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

        private MasterOngkosDto _selectedItem = new MasterOngkosDto();
        public MasterOngkosDto SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        private decimal? _kuantitasForm;
        public decimal? KuantitasForm
        {
            get => _kuantitasForm;
            set { _kuantitasForm = value; OnPropertyChanged(); }
        }

        // end form item

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
