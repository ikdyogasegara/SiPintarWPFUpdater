using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.MasterData.Paket;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.MasterData
{
    public class PaketViewModel : VmBase
    {
        public PaketViewModel(MasterDataViewModel _parent, IRestApiClientModel _restApi, bool _isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, _restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, _restApi, _isTest);
            OnOpenAddFormCommand = new OnOpenAddFormCommand(this, _restApi, _isTest);
            OnSubmitAddFormCommand = new OnSubmitAddFormCommand(this, _restApi, _isTest);
            OnOpenEditFormCommand = new OnOpenEditFormCommand(this, _restApi, _isTest);
            OnSubmitEditFormCommand = new OnSubmitEditFormCommand(this, _restApi, _isTest);
            OnOpenDeleteFormCommand = new OnOpenDeleteFormCommand(this, _restApi, _isTest);
            OnSubmitDeleteFormCommand = new OnSubmitDeleteFormCommand(this, _restApi, _isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, _isTest);
            OnTambahBarangCommand = new OnTambahBarangCommand(this, _isTest);
            OnHapusBarangCommand = new OnHapusBarangCommand(this, _isTest);
        }

        public ICommand OnTambahBarangCommand;
        public ICommand OnHapusBarangCommand;

        private ObservableCollection<MasterBarangPaketDetailDto> _data;
        public ObservableCollection<MasterBarangPaketDetailDto> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        private ListCollectionView _dataGroup;
        public ListCollectionView DataGroup
        {
            get { return _dataGroup; }
            set
            {
                _dataGroup = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangPaketDto _selectedData;
        public MasterBarangPaketDto SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        private bool _isNamaPaketChecked;
        public bool IsNamaPaketChecked
        {
            get { return _isNamaPaketChecked; }
            set
            {
                _isNamaPaketChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isUnitChecked;
        public bool IsUnitChecked
        {
            get { return _isUnitChecked; }
            set
            {
                _isUnitChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKodeBarangChecked;
        public bool IsKodeBarangChecked
        {
            get { return _isKodeBarangChecked; }
            set
            {
                _isKodeBarangChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isDeskripsiChecked;
        public bool IsDeskripsiChecked
        {
            get { return _isDeskripsiChecked; }
            set
            {
                _isDeskripsiChecked = value;
                OnPropertyChanged();
            }
        }

        private string _filterNamaPaket;
        public string FilterNamaPaket
        {
            get { return _filterNamaPaket; }
            set
            {
                _filterNamaPaket = value;
                OnPropertyChanged();
            }
        }

        private MasterSatuanBarangDto _filterUnit;
        public MasterSatuanBarangDto FilterUnit
        {
            get { return _filterUnit; }
            set
            {
                _filterUnit = value;
                OnPropertyChanged();
            }
        }

        private string _filterKodeBarang;
        public string FilterKodeBarang
        {
            get { return _filterKodeBarang; }
            set
            {
                _filterKodeBarang = value;
                OnPropertyChanged();
            }
        }

        private string _filterDeskripsi;
        public string FilterDeskripsi
        {
            get { return _filterDeskripsi; }
            set
            {
                _filterDeskripsi = value;
                OnPropertyChanged();
            }
        }

        //List Filter Data
        private ObservableCollection<MasterSatuanBarangDto> _dataSatuan;
        public ObservableCollection<MasterSatuanBarangDto> DataSatuan
        {
            get { return _dataSatuan; }
            set
            {
                _dataSatuan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading1;
        public bool IsLoading1
        {
            get { return _isLoading1; }
            set
            {
                _isLoading1 = value;
                OnPropertyChanged();
            }
        }
        private bool _isEmpty1;
        public bool IsEmpty1
        {
            get { return _isEmpty1; }
            set
            {
                _isEmpty1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBarangPaketDetailDto> _data1;
        public ObservableCollection<MasterBarangPaketDetailDto> Data1
        {
            get { return _data1; }
            set
            {
                _data1 = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangPaketDetailDto _selectedData1;
        public MasterBarangPaketDetailDto SelectedData1
        {
            get { return _selectedData1; }
            set
            {
                _selectedData1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBarangDto> _daftarBarang;
        public ObservableCollection<MasterBarangDto> DaftarBarang
        {
            get { return _daftarBarang; }
            set
            {
                _daftarBarang = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangDto _selectedBarang;
        public MasterBarangDto SelectedBarang
        {
            get { return _selectedBarang; }
            set
            {
                _selectedBarang = value;
                OnPropertyChanged();
            }
        }

        private string _totalBarang;
        public string TotalBarang
        {
            get { return _totalBarang; }
            set
            {
                _totalBarang = value;
                OnPropertyChanged();
            }
        }
    }
}
