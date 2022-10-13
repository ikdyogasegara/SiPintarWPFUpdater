using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerRayon;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter
{
    public class PerRayonViewModel : ViewModelBase
    {
        public PerRayonViewModel(RuteBacaMeterViewModel parent, IRestApiClientModel restApi)
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
            OnOpenAddPetugasCommand = new OnOpenAddPetugasCommand(this);
            OnSubmitAddPetugasCommand = new OnSubmitAddPetugasCommand(this);
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
        public ICommand OnOpenAddPetugasCommand { get; }
        public ICommand OnSubmitAddPetugasCommand { get; }



        private RuteBacaMeterViewModel _parent;
        public RuteBacaMeterViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        // grid rayon
        private bool _isRayonSelected;
        public bool IsRayonSelected
        {
            get => _isRayonSelected;
            set { _isRayonSelected = value; OnPropertyChanged(); }
        }

        private bool _isLoadingRayon;
        public bool IsLoadingRayon
        {
            get => _isLoadingRayon;
            set { _isLoadingRayon = value; OnPropertyChanged(); }
        }

        private bool _isEmptyRayon;
        public bool IsEmptyRayon
        {
            get => _isEmptyRayon;
            set { _isEmptyRayon = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterRayonDto> _RayonList = new ObservableCollection<MasterRayonDto>();
        public ObservableCollection<MasterRayonDto> RayonList
        {
            get { return _RayonList; }
            set
            {
                _RayonList = value;
                OnPropertyChanged();
            }
        }

        private MasterRayonDto _selectedRayon;
        public MasterRayonDto SelectedRayon
        {
            get { return _selectedRayon; }
            set
            {
                _selectedRayon = value;
                OnPropertyChanged();

                if (value != null)
                    GetJadwalListCommand.Execute(null);

                IsRayonSelected = value != null;
            }
        }

        private bool _isPreviousButtonEnabledRayon;
        public bool IsPreviousButtonEnabledRayon
        {
            get { return _isPreviousButtonEnabledRayon; }
            set
            {
                _isPreviousButtonEnabledRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isNextButtonEnabledRayon;
        public bool IsNextButtonEnabledRayon
        {
            get { return _isNextButtonEnabledRayon; }
            set
            {
                _isNextButtonEnabledRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberFillerVisibleRayon;
        public bool IsLeftPageNumberFillerVisibleRayon
        {
            get { return _isLeftPageNumberFillerVisibleRayon; }
            set
            {
                _isLeftPageNumberFillerVisibleRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberFillerVisibleRayon;
        public bool IsRightPageNumberFillerVisibleRayon
        {
            get { return _isRightPageNumberFillerVisibleRayon; }
            set
            {
                _isRightPageNumberFillerVisibleRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPageNumberActiveRayon;
        public bool IsLeftPageNumberActiveRayon
        {
            get { return _isLeftPageNumberActiveRayon; }
            set
            {
                _isLeftPageNumberActiveRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPageNumberActiveRayon;
        public bool IsRightPageNumberActiveRayon
        {
            get { return _isRightPageNumberActiveRayon; }
            set
            {
                _isRightPageNumberActiveRayon = value;
                OnPropertyChanged();
            }
        }

        private bool _isMiddlePageNumberVisibleRayon;
        public bool IsMiddlePageNumberVisibleRayon
        {
            get { return _isMiddlePageNumberVisibleRayon; }
            set
            {
                _isMiddlePageNumberVisibleRayon = value;
                OnPropertyChanged();
            }
        }

        private int _currentPageRayon = 1;
        public int CurrentPageRayon
        {
            get { return _currentPageRayon; }
            set
            {
                _currentPageRayon = value;
                OnPropertyChanged();
            }
        }

        private int _totalPageRayon;
        public int TotalPageRayon
        {
            get { return _totalPageRayon; }
            set
            {
                _totalPageRayon = value;
                OnPropertyChanged();
            }
        }

        private int _totalRecordRayon;
        public int TotalRecordRayon
        {
            get { return _totalRecordRayon; }
            set
            {
                _totalRecordRayon = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<int, string> _limitDataRayon = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitDataRayon
        {
            get => _limitDataRayon;
            set
            {
                _limitDataRayon = value;
                OnPropertyChanged();

                GetDataListCommand.Execute(null);
            }
        }
        // end grid rayon

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

        private bool _isEmptyPetugasSearch;
        public bool IsEmptyPetugasSearch
        {
            get => _isEmptyPetugasSearch;
            set { _isEmptyPetugasSearch = value; OnPropertyChanged(); }
        }

        private MasterPetugasBacaDto _selectedPetugas;
        public MasterPetugasBacaDto SelectedPetugas
        {
            get { return _selectedPetugas; }
            set
            {
                _selectedPetugas = value;
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
        public string SearchKeywordForm
        {
            get { return _searchKeywordForm; }
            set
            {
                _searchKeywordForm = value;
                OnPropertyChanged();
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
