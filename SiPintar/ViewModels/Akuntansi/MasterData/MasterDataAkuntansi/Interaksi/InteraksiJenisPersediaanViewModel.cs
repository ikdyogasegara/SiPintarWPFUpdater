using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiJenisPersediaan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    public class InteraksiJenisPersediaanViewModel : ViewModelBase
    {
        public InteraksiJenisPersediaanViewModel(InteraksiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parentViewModel;

            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, restApi, isTest);
            OnOpenSettingTableFormCommand = new OnOpenSettingTableFormCommand(this, isTest);
            OnSubmitSettingTableFormCommand = new OnSubmitSettingTableFormCommand(this, isTest);
            OnSubmitFormCommand = new OnSubmitFormCommand(this, restApi, isTest);

        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnOpenSettingTableFormCommand { get; set; }
        public ICommand OnSubmitSettingTableFormCommand { get; set; }
        public ICommand OnOpenAddFormCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnOpenDeleteFormCommand { get; set; }
        public ICommand OnSubmitDeleteFormCommand { get; set; }
        public ICommand OnSubmitFormCommand { get; set; }

        private InteraksiViewModel _parent = null!;
        public InteraksiViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private ParamMasterInPersediaanDto _inJenisPersediaanForm = new();
        public ParamMasterInPersediaanDto InJenisPersediaanForm
        {
            get => _inJenisPersediaanForm;
            set
            {
                _inJenisPersediaanForm = value;
                OnPropertyChanged();
            }
        }

        private MasterInPersediaanDto _selectedData = new();
        public MasterInPersediaanDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBarangDto _selectedJenis = new();
        public MasterJenisBarangDto SelectedJenis
        {
            get => _selectedJenis;
            set
            {
                _selectedJenis = value;
                OnPropertyChanged();
            }
        }

        private MasterKeperluanDto _selectedKeperluan = new();
        public MasterKeperluanDto SelectedKeperluan
        {
            get => _selectedKeperluan;
            set
            {
                _selectedKeperluan = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _selectedDebet = new();
        public MasterPerkiraan3Dto SelectedDebet
        {
            get => _selectedDebet;
            set
            {
                _selectedDebet = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _selectedKredit = new();
        public MasterPerkiraan3Dto SelectedKredit
        {
            get => _selectedKredit;
            set
            {
                _selectedKredit = value;
                OnPropertyChanged();
            }
        }

        private bool _isAktivaChecked;
        public bool IsAktivaChecked
        {
            get => _isAktivaChecked;
            set
            {
                _isAktivaChecked = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterInPersediaanDto> _dataList = new();
        public ObservableCollection<MasterInPersediaanDto> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private object _tableSetting = new();
        public object TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        private string _keterangan = string.Empty;
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                _keterangan = value;
                OnPropertyChanged();
            }
        }

        #region Pagination prop

        private bool _isOverLimit;
        public bool IsOverLimit
        {
            get { return _isOverLimit; }
            set
            {
                _isOverLimit = value;
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

        private int _totalPage = 1;
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<KeyValuePair<int, string>> ShowOptions
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(100, "100"),
                    new KeyValuePair<int, string>(200, "200"),
                    new KeyValuePair<int, string>(500, "500"),
                    new KeyValuePair<int, string>(1000, "1000"),
                    new KeyValuePair<int, string>(2000, "2000"),
                    new KeyValuePair<int, string>(3000, "3000"),
                    new KeyValuePair<int, string>(5000, "5000"),
                };
                return data;
            }
        }
        private KeyValuePair<int, string> _limitData = new(100, "100");
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
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

        private bool _isPageNumberVisible;
        public bool IsPageNumberVisible
        {
            get { return _isPageNumberVisible; }
            set
            {
                _isPageNumberVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Setting table
        [ExcludeFromCodeCoverage]
        public void TableColumnConfiguration(bool isTest)
        {
            if (isTest) return;

            var configIni = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\AppConfig\\Akuntansi\\master_data_akuntansi_interaksi_jenis_persediaan_config.ini";

            if (!File.Exists(configIni)) return;

            var parser = new IniFileParser.IniFileParser();
            var data = parser.ReadFile(configIni);

            TableSetting = new
            {
                KodeJenisBarang = data["show_table_column"]["KodeJenisBarang"] == "1",
                NamaJenisBarang = data["show_table_column"]["NamaJenisBarang"] == "1",
                KodeKeperluan = data["show_table_column"]["KodeKeperluan"] == "1",
                Keperluan = data["show_table_column"]["Keperluan"] == "1",
                KodeDebet = data["show_table_column"]["KodeDebet"] == "1",
                NamaDebet = data["show_table_column"]["NamaDebet"] == "1",
                KodeKredit = data["show_table_column"]["KodeKredit"] == "1",
                NamaKredit = data["show_table_column"]["NamaKredit"] == "1"
            };
        }
        #endregion
    }
}
