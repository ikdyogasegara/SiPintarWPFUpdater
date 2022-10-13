using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Bacameter.SistemKontrol.DataPembacaan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol
{
    public class DataPembacaanViewModel : ViewModelBase
    {
        public DataPembacaanViewModel(IRestApiClientModel restApi)
        {
            OnLoadCommand = new OnLoadCommand(this);
            OnRefreshCommand = OnLoadCommand;
            GetDataListCommand = new GetDataListCommand(this, restApi);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this);
            OnOpenDeleteConfirmationCommand = new OnOpenDeleteConfirmationCommand(this);
            OnExportCommand = new OnExportCommand(this);
            OnCetakCommand = new OnCetakCommand(this);
            OnSubmitAddCommand = new OnSubmitAddCommand(this, restApi);
            OnSubmitDeleteCommand = new OnSubmitDeleteCommand(this, restApi);
            OnPreviousPageCommand = new OnPreviousPageCommand(this);
            OnNextPageCommand = new OnNextPageCommand(this);
            OnOpenChangeStatusConfirmCommand = new OnOpenChangeStatusConfirmCommand(this);
            OnSubmitChangeStatusCommand = new OnSubmitChangeStatusCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand GetDataListCommand { get; }
        public ICommand OnOpenAddFormCommand { get; }
        public ICommand OnOpenDeleteConfirmationCommand { get; }
        public ICommand OnExportCommand { get; }
        public ICommand OnCetakCommand { get; }
        public ICommand OnSubmitAddCommand { get; }
        public ICommand OnSubmitDeleteCommand { get; }
        public ICommand OnPreviousPageCommand { get; }
        public ICommand OnNextPageCommand { get; }
        public ICommand OnOpenChangeStatusConfirmCommand { get; }
        public ICommand OnSubmitChangeStatusCommand { get; }


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set { _isLoadingForm = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MasterPeriodeDto> _dataList = new ObservableCollection<MasterPeriodeDto>();
        public ObservableCollection<MasterPeriodeDto> DataList
        {
            get { return _dataList; }
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeDto _selectedData;
        public MasterPeriodeDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
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

        private long _totalRecord;
        public long TotalRecord
        {
            get { return _totalRecord; }
            set
            {
                _totalRecord = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(50, "50");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();

                GetDataListCommand.Execute(null);
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> BulanList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>();

                int index = 1;
                foreach (var item in new CultureInfo("id-ID").DateTimeFormat.MonthNames)
                {
                    string MonthIndex = index.ToString();
                    if (MonthIndex.Length < 2)
                        MonthIndex = "0" + MonthIndex;

                    if (!string.IsNullOrWhiteSpace(item))
                        data.Add(new KeyValuePair<string, string>(MonthIndex, item));
                    index++;
                }

                return data;
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> TahunList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>();

                int startYear = 2016;
                int endYear = DateTime.Now.Year + 1;
                for (int i = startYear; i <= endYear; i++)
                    data.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString()));

                return data;
            }
        }

        private KeyValuePair<string, string> _bulanForm;
        public KeyValuePair<string, string> BulanForm
        {
            get => _bulanForm;
            set
            {
                _bulanForm = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _tahunForm;
        public KeyValuePair<string, string> TahunForm
        {
            get => _tahunForm;
            set
            {
                _tahunForm = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda1Form;
        public DateTime? TglDenda1Form
        {
            get => _tglDenda1Form;
            set
            {
                _tglDenda1Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda2Form;
        public DateTime? TglDenda2Form
        {
            get => _tglDenda2Form;
            set
            {
                _tglDenda2Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda3Form;
        public DateTime? TglDenda3Form
        {
            get => _tglDenda3Form;
            set
            {
                _tglDenda3Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglDenda4Form;
        public DateTime? TglDenda4Form
        {
            get => _tglDenda4Form;
            set
            {
                _tglDenda4Form = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglMulaiDendaForm;
        public DateTime? TglMulaiDendaForm
        {
            get => _tglMulaiDendaForm;
            set
            {
                _tglMulaiDendaForm = value;
                OnPropertyChanged();
            }
        }

        private bool _agreementForm;
        public bool AgreementForm
        {
            get => _agreementForm;
            set { _agreementForm = value; OnPropertyChanged(); }
        }

        private string _statusSection;
        public string StatusSection
        {
            get => _statusSection;
            set { _statusSection = value; OnPropertyChanged(); }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
