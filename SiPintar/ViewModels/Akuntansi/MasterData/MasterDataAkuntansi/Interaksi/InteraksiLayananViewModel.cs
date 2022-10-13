using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi.InteraksiLayanan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi
{
    [ExcludeFromCodeCoverage]
    public class InteraksiLayananViewModel : ViewModelBase
    {
        public InteraksiLayananViewModel(InteraksiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
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


        private InteraksiViewModel _parent;
        public InteraksiViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private MasterInPelayananWpf _selectedData;
        public MasterInPelayananWpf SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterInPelayananWpf> _dataList;
        public ObservableCollection<MasterInPelayananWpf> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        #region DialogForm

        private MasterWilayahDto _selectedWilayah = new MasterWilayahDto();
        public MasterWilayahDto SelectedWilayah
        {
            get { return _selectedWilayah; }
            set
            {
                _selectedWilayah = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _selectedGolongan = new MasterGolonganDto();
        public MasterGolonganDto SelectedGolongan
        {
            get { return _selectedGolongan; }
            set
            {
                _selectedGolongan = value;
                OnPropertyChanged();
            }
        }


        private MasterJenisNonAirDto _selectedJenisNonAir = new();
        public MasterJenisNonAirDto SelectedJenisNonAir
        {
            get { return _selectedJenisNonAir; }
            set
            {
                _selectedJenisNonAir = value;
                OnPropertyChanged();
            }
        }
        private MasterPerkiraan3Dto _selectedPerkiraan3Debet = new();
        public MasterPerkiraan3Dto SelectedPerkiraan3Debet
        {
            get { return _selectedPerkiraan3Debet; }
            set
            {
                _selectedPerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _selectedPerkiraan3Kredit = new();
        public MasterPerkiraan3Dto SelectedPerkiraan3Kredit
        {
            get { return _selectedPerkiraan3Kredit; }
            set
            {
                _selectedPerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _selectedPerkiraan3NonAir = new();
        public MasterPerkiraan3Dto SelectedPerkiraan3NonAir
        {
            get { return _selectedPerkiraan3NonAir; }
            set
            {
                _selectedPerkiraan3NonAir = value;
                OnPropertyChanged();
            }
        }

        private KeyValuePair<string, string> _selectedPembentukRekAir;
        public KeyValuePair<string, string> SelectedPembentukRekAir
        {
            get => _selectedPembentukRekAir;
            set
            {
                _selectedPembentukRekAir = value;
                OnPropertyChanged();
            }
        }


        private KeyValuePair<string, string> _selectedPembentukRekNonAir;
        public KeyValuePair<string, string> SelectedPembentukRekNonAir
        {
            get => _selectedPembentukRekNonAir;
            set
            {
                _selectedPembentukRekNonAir = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private dynamic _tableSetting;
        public dynamic TableSetting
        {
            get { return _tableSetting; }
            set
            {
                _tableSetting = value;
                OnPropertyChanged();
            }
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
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

    #region MasterInPelayananWpf
    public class MasterInPelayananWpf : MasterInPelayananAirDto, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public int? IdInPelayananNonAir { get; set; }
        public int? IdJenisNonAir { get; set; }
        public string KodeJenisNonAir { get; set; }
        public string NamaJenisNonAir { get; set; }
        public int? IdPerkiraan3 { get; set; }
        public string KodePerkiraan3 { get; set; }
        public string NamaPerkiraan3 { get; set; }

        public string KodeWilayahWpf
        {
            get { return KodeWilayah; }
            set
            {
                KodeWilayah = value;
                OnPropertyChanged();
            }
        }

        public string NamaWilayahWpf
        {
            get { return NamaWilayah; }
            set
            {
                NamaWilayah = value;
                OnPropertyChanged();
            }
        }

        public string KodeGolonganWpf
        {
            get { return KodeGolongan; }
            set
            {
                KodeGolongan = value;
                OnPropertyChanged();
            }
        }

        public string NamaGolonganWpf
        {
            get { return NamaGolongan; }
            set
            {
                NamaGolongan = value;
                OnPropertyChanged();
            }
        }
        public string KodePerkiraan3DebetWpf
        {
            get { return KodePerkiraan3Debet; }
            set
            {
                KodePerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        public string NamaPerkiraan3DebetWpf
        {
            get { return NamaPerkiraan3Debet; }
            set
            {
                NamaPerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        public string KodePerkiraan3KreditWpf
        {
            get { return KodePerkiraan3Kredit; }
            set
            {
                KodePerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }

        public string NamaPerkiraan3KreditWpf
        {
            get { return NamaPerkiraan3Kredit; }
            set
            {
                NamaPerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }

        public bool? FlagPembentukRekairWpf
        {
            get { return FlagPembentukRekair; }
            set
            {
                FlagPembentukRekair = value;
                OnPropertyChanged();
            }
        }

        public string KeteranganWpf
        {
            get { return Keterangan; }
            set
            {
                Keterangan = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    #endregion
}
