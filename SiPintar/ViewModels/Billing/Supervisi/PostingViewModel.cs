using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Billing.Supervisi.Posting;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Supervisi
{
    public class PostingViewModel : ViewModelBase
    {
        public PostingViewModel(SupervisiViewModel parentViewModel, IRestApiClientModel restApi)
        {
            _ = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, restApi);
            OnConfirmPostingCommand = new OnConfirmPostingCommand(this);
            OnSubmitPostingCommand = new OnSubmitPostingCommand(this, restApi);
            OnFilterCommand = new OnFilterCommand(this, restApi);
            OnExportCommand = new OnExportCommand(this);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
        }


        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnConfirmPostingCommand { get; }
        public ICommand OnSubmitPostingCommand { get; }
        public ICommand OnFilterCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }

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

        private List<RiwayatPostingDto> _riwayatPosting;
        public List<RiwayatPostingDto> RiwayatPosting
        {
            get { return _riwayatPosting; }
            set
            {
                _riwayatPosting = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _masterUserDto;
        public ObservableCollection<MasterUserDto> MasterUserList
        {
            get { return _masterUserDto; }
            set
            {
                _masterUserDto = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _selectedUser;
        public MasterUserDto SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        private int? _selectedDataTahunPosting;
        public int? SelectedDataTahunPosting
        {
            get { return _selectedDataTahunPosting; }
            set
            {
                _selectedDataTahunPosting = value;
                OnPropertyChanged();
            }
        }

        private List<int> _listTahunPosting = new List<int>();
        public List<int> ListTahunPosting
        {
            get { return _listTahunPosting; }
            set
            {
                _listTahunPosting = value;
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

        private bool _isPostingRekeningAir = true;
        public bool IsPostingRekeningAir
        {
            get => _isPostingRekeningAir;
            set { _isPostingRekeningAir = value; OnPropertyChanged(); }
        }

        private bool _isPostingRekeningLimbah = true;
        public bool IsPostingRekeningLimbah
        {
            get => _isPostingRekeningLimbah;
            set { _isPostingRekeningLimbah = value; OnPropertyChanged(); }
        }

        private bool _isPostingRekeningLltt = true;
        public bool IsPostingRekeningLltt
        {
            get => _isPostingRekeningLltt;
            set { _isPostingRekeningLltt = value; OnPropertyChanged(); }
        }

        private bool _isPostingPelangganAir = true;
        public bool IsPostingPelangganAir
        {
            get => _isPostingPelangganAir;
            set { _isPostingPelangganAir = value; OnPropertyChanged(); }
        }

        private bool _isPostingPelangganLimbah = true;
        public bool IsPostingPelangganLimbah
        {
            get => _isPostingPelangganLimbah;
            set { _isPostingPelangganLimbah = value; OnPropertyChanged(); }
        }

        private bool _isPostingPelangganLltt = true;
        public bool IsPostingPelangganLltt
        {
            get => _isPostingPelangganLltt;
            set { _isPostingPelangganLltt = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeDto> _masterPeriodeList;
        public ObservableCollection<MasterPeriodeDto> MasterPeriodeList
        {
            get { return _masterPeriodeList; }
            set
            {
                _masterPeriodeList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedDataPeriode = new MasterPeriodeDto();
        public MasterPeriodeDto SelectedDataPeriode
        {
            get => _selectedDataPeriode;
            set { _selectedDataPeriode = value; OnPropertyChanged(); }
        }

        private List<MasterPeriodeDto> _masterTahunRekeningList;
        public List<MasterPeriodeDto> MasterTahunRekeningList
        {
            get { return _masterTahunRekeningList; }
            set
            {
                _masterTahunRekeningList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedDataTahunRekening;
        public MasterPeriodeDto SelectedDataTahunRekening
        {
            get => _selectedDataTahunRekening;
            set { _selectedDataTahunRekening = value; OnPropertyChanged(); }
        }

        private string _catatan;
        public string Catatan
        {
            get => _catatan;
            set { _catatan = value; OnPropertyChanged(); }
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


        private int _limit = 10;
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

        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var listOption = new int[] { 10, 20, 50, 100, 200, 300, 500 };

                var data = new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var item in listOption)
                    data.Add(new KeyValuePair<int, string>(item, item.ToString()));

                return data;
            }
        }

    }
}
