using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan.KelompokKodePerkiraan2;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.KelompokKodePerkiraan
{
    public class KelompokKodePerkiraan2ViewModel : ViewModelBase
    {
        public KelompokKodePerkiraan2ViewModel(KelompokKodePerkiraanViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
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
        public ICommand OnOpenAddFormCommand { get; set; }
        public ICommand OnOpenEditFormCommand { get; set; }
        public ICommand OnOpenDeleteFormCommand { get; set; }
        public ICommand OnSubmitDeleteFormCommand { get; set; }
        public ICommand OnOpenSettingTableFormCommand { get; set; }
        public ICommand OnSubmitSettingTableFormCommand { get; set; }
        public ICommand OnSubmitFormCommand { get; set; }


        private KelompokKodePerkiraanViewModel _parent;
        public KelompokKodePerkiraanViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private object _tableSetting;
        public object TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan2Wpf> _dataList;
        public ObservableCollection<MasterPerkiraan2Wpf> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private ParamMasterPerkiraan2Dto _formPerkiraan;
        public ParamMasterPerkiraan2Dto FormPerkiraan
        {
            get => _formPerkiraan;
            set
            {
                _formPerkiraan = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan2Wpf _selectedData;
        public MasterPerkiraan2Wpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan1Dto _selectedDataPerkiraan1;
        public MasterPerkiraan1Dto SelectedDataPerkiraan1
        {
            get => _selectedDataPerkiraan1;
            set
            {
                _selectedDataPerkiraan1 = value;
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(100, "100");
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
    }

    public class MasterPerkiraan2Wpf : MasterPerkiraan2Dto, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public string KodePerkiraan2Wpf
        {
            get { return KodePerkiraan2; }
            set
            {
                KodePerkiraan2 = value;
                OnPropertyChanged();
            }
        }

        public string NamaPerkiraan2Wpf
        {
            get { return NamaPerkiraan2; }
            set
            {
                NamaPerkiraan2 = value;
                OnPropertyChanged();
            }
        }
        public int? IdPerkiraan1Wpf
        {
            get { return IdPerkiraan1; }
            set
            {
                IdPerkiraan1 = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
