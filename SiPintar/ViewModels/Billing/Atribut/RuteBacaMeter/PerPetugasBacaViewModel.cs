using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter
{
    public class PerPetugasBacaViewModel : ViewModelBase
    {
        public PerPetugasBacaViewModel(RuteBacaMeterViewModel parent, IRestApiClientModel restApi)
        {
            _parent = parent;

            OnLoadCommand = new OnLoadCommand(this);
            OnRefreshCommand = OnLoadCommand;
            GetDataListCommand = new GetDataListCommand(this, restApi);
            GetJadwalListCommand = new GetJadwalListCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, restApi);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnExportCommand = new OnExportCommand(this);
            OnOpenAddRayonCommand = new OnOpenAddRayonCommand(this);
            OnSubmitAddRayonCommand = new OnSubmitAddRayonCommand(this);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand GetDataListCommand { get; }
        public ICommand GetJadwalListCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenEditFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnSubmitAddFormCommand { get; }
        public ICommand OnSubmitEditFormCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnOpenAddRayonCommand { get; }
        public ICommand OnSubmitAddRayonCommand { get; }



        private RuteBacaMeterViewModel _parent;
        public RuteBacaMeterViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        // grid petugas
        private bool _isPetugasSelected;
        public bool IsPetugasSelected
        {
            get => _isPetugasSelected;
            set { _isPetugasSelected = value; OnPropertyChanged(); }
        }

        private bool _isLoadingPetugas;
        public bool IsLoadingPetugas
        {
            get => _isLoadingPetugas;
            set { _isLoadingPetugas = value; OnPropertyChanged(); }
        }

        private bool _isEmptyPetugas;
        public bool IsEmptyPetugas
        {
            get => _isEmptyPetugas;
            set { _isEmptyPetugas = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPetugasBacaDto> _petugasList = new ObservableCollection<MasterPetugasBacaDto>();
        public ObservableCollection<MasterPetugasBacaDto> PetugasList
        {
            get { return _petugasList; }
            set
            {
                _petugasList = value;
                OnPropertyChanged();
            }
        }

        private MasterPetugasBacaDto _selectedPetugas;
        public MasterPetugasBacaDto SelectedPetugas
        {
            get { return _selectedPetugas; }
            set
            {
                _selectedPetugas = value;
                OnPropertyChanged();

                if (value != null)
                    GetJadwalListCommand.Execute(null);

                IsPetugasSelected = value != null;
            }
        }

        private bool _isPreviousButtonEnabledPetugas;
        public bool IsPreviousButtonEnabledPetugas
        {
            get { return _isPreviousButtonEnabledPetugas; }
            set
            {
                _isPreviousButtonEnabledPetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledPetugas;
        public bool IsNextButtonEnabledPetugas
        {
            get { return _isNextButtonEnabledPetugas; }
            set
            {
                _isNextButtonEnabledPetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisiblePetugas;
        public bool IsLeftPageNumberFillerVisiblePetugas
        {
            get { return _isLeftPageNumberFillerVisiblePetugas; }
            set
            {
                _isLeftPageNumberFillerVisiblePetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisiblePetugas;
        public bool IsRightPageNumberFillerVisiblePetugas
        {
            get { return _isRightPageNumberFillerVisiblePetugas; }
            set
            {
                _isRightPageNumberFillerVisiblePetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActivePetugas;
        public bool IsLeftPageNumberActivePetugas
        {
            get { return _isLeftPageNumberActivePetugas; }
            set
            {
                _isLeftPageNumberActivePetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActivePetugas;
        public bool IsRightPageNumberActivePetugas
        {
            get { return _isRightPageNumberActivePetugas; }
            set
            {
                _isRightPageNumberActivePetugas = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisiblePetugas;
        public bool IsMiddlePageNumberVisiblePetugas
        {
            get { return _isMiddlePageNumberVisiblePetugas; }
            set
            {
                _isMiddlePageNumberVisiblePetugas = value;
                OnPropertyChanged();
            }
        }

        private int _currentPagePetugas = 1;
        public int CurrentPagePetugas
        {
            get { return _currentPagePetugas; }
            set
            {
                _currentPagePetugas = value;
                OnPropertyChanged();
            }
        }

        private int _totalPagePetugas;
        public int TotalPagePetugas
        {
            get { return _totalPagePetugas; }
            set
            {
                _totalPagePetugas = value;
                OnPropertyChanged();
            }
        }

        private int _totalRecordPetugas;
        public int TotalRecordPetugas
        {
            get { return _totalRecordPetugas; }
            set
            {
                _totalRecordPetugas = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataPetugas = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitDataPetugas
        {
            get => _limitDataPetugas;
            set
            {
                _limitDataPetugas = value;
                OnPropertyChanged();

                GetDataListCommand.Execute(null);
            }
        }
        // end grid petugas

        // grid jadwal
        private bool _isJadwalSelected;
        public bool IsJadwalSelected
        {
            get => _isJadwalSelected;
            set { _isJadwalSelected = value; OnPropertyChanged(); }
        }

        private bool _isLoadingJadwal;
        public bool IsLoadingJadwal
        {
            get => _isLoadingJadwal;
            set { _isLoadingJadwal = value; OnPropertyChanged(); }
        }

        private bool _isEmptyJadwal;
        public bool IsEmptyJadwal
        {
            get => _isEmptyJadwal;
            set { _isEmptyJadwal = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterJadwalBacaDto> _JadwalList = new ObservableCollection<MasterJadwalBacaDto>();
        public ObservableCollection<MasterJadwalBacaDto> JadwalList
        {
            get { return _JadwalList; }
            set
            {
                _JadwalList = value;
                OnPropertyChanged();
            }
        }

        private MasterJadwalBacaDto _selectedJadwal;
        public MasterJadwalBacaDto SelectedJadwal
        {
            get { return _selectedJadwal; }
            set
            {
                _selectedJadwal = value;
                OnPropertyChanged();

                IsJadwalSelected = value != null;
            }
        }

        private bool _isPreviousButtonEnabledJadwal;
        public bool IsPreviousButtonEnabledJadwal
        {
            get { return _isPreviousButtonEnabledJadwal; }
            set
            {
                _isPreviousButtonEnabledJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledJadwal;
        public bool IsNextButtonEnabledJadwal
        {
            get { return _isNextButtonEnabledJadwal; }
            set
            {
                _isNextButtonEnabledJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisibleJadwal;
        public bool IsLeftPageNumberFillerVisibleJadwal
        {
            get { return _isLeftPageNumberFillerVisibleJadwal; }
            set
            {
                _isLeftPageNumberFillerVisibleJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisibleJadwal;
        public bool IsRightPageNumberFillerVisibleJadwal
        {
            get { return _isRightPageNumberFillerVisibleJadwal; }
            set
            {
                _isRightPageNumberFillerVisibleJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActiveJadwal;
        public bool IsLeftPageNumberActiveJadwal
        {
            get { return _isLeftPageNumberActiveJadwal; }
            set
            {
                _isLeftPageNumberActiveJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActiveJadwal;
        public bool IsRightPageNumberActiveJadwal
        {
            get { return _isRightPageNumberActiveJadwal; }
            set
            {
                _isRightPageNumberActiveJadwal = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisibleJadwal;
        public bool IsMiddlePageNumberVisibleJadwal
        {
            get { return _isMiddlePageNumberVisibleJadwal; }
            set
            {
                _isMiddlePageNumberVisibleJadwal = value;
                OnPropertyChanged();
            }
        }

        private int _currentPageJadwal = 1;
        public int CurrentPageJadwal
        {
            get { return _currentPageJadwal; }
            set
            {
                _currentPageJadwal = value;
                OnPropertyChanged();
            }
        }

        private int _totalPageJadwal;
        public int TotalPageJadwal
        {
            get { return _totalPageJadwal; }
            set
            {
                _totalPageJadwal = value;
                OnPropertyChanged();
            }
        }

        private int _totalRecordJadwal;
        public int TotalRecordJadwal
        {
            get { return _totalRecordJadwal; }
            set
            {
                _totalRecordJadwal = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataJadwal = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitDataJadwal
        {
            get => _limitDataJadwal;
            set
            {
                _limitDataJadwal = value;
                OnPropertyChanged();

                GetJadwalListCommand.Execute(null);
            }
        }
        // end grid jadwal

        // Form

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private bool _isEmptyRayonSearch;
        public bool IsEmptyRayonSearch
        {
            get => _isEmptyRayonSearch;
            set { _isEmptyRayonSearch = value; OnPropertyChanged(); }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get { return _selectedRayon; }
            set
            {
                _selectedRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isTglBacaChecked;
        public bool IsTglBacaChecked
        {
            get { return _isTglBacaChecked; }
            set
            {
                _isTglBacaChecked = value;
                OnPropertyChanged();

                if (!value)
                {
                    TanggalBacaForm = null;
                }
            }
        }

        private KeyValuePair<int, string>? _tanggalBacaForm;
        public KeyValuePair<int, string>? TanggalBacaForm
        {
            get { return _tanggalBacaForm; }
            set
            {
                _tanggalBacaForm = value;
                OnPropertyChanged();
            }
        }

        private string _searchKeywordForm;
        [ExcludeFromCodeCoverage]
        public string SearchKeywordForm
        {
            get { return _searchKeywordForm; }
            set
            {
                _searchKeywordForm = value;
                OnPropertyChanged();

                GetDataListCommand.Execute(null);
            }
        }

        private int _toleransiMinus;
        public int ToleransiMinus
        {
            get { return _toleransiMinus; }
            set
            {
                _toleransiMinus = value;
                OnPropertyChanged();
            }
        }

        private int _toleransiPlus;
        public int ToleransiPlus
        {
            get { return _toleransiPlus; }
            set
            {
                _toleransiPlus = value;
                OnPropertyChanged();
            }
        }

        // End Form

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
