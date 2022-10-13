using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangKeluar;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class DaftarBarangKeluarViewModel : ViewModelBase
    {
        public readonly IRestApiClientModel _restApi;
        public DaftarBarangKeluarViewModel(ProsesTransaksiViewModel _parent, IRestApiClientModel restApi, bool _isTest = false)
        {
            _ = _parent;
            _restApi = restApi;
            OnLoadCommand = new OnLoadCommand(this, restApi, _isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, _isTest);
            OnGetDetailCommand = new OnGetDetailCommand(this, restApi, _isTest);
            OnOpenVerifikasiSemuaBarangCommand = new OnOpenVerifikasiSemuaBarangCommand(this, _isTest);
            OnVerifikasiSemuaBarangCommand = new OnVerifikasiSemuaBarangCommand(this, restApi, _isTest);
            OnOpenUnVerifikasiSemuaBarangCommand = new OnOpenUnVerifikasiSemuaBarangCommand(this, _isTest);
            OnUnVerifikasiSemuaBarangCommand = new OnUnVerifikasiSemuaBarangCommand(this, restApi, _isTest);
            OnOpenDeleteCommand = new OnOpenDeleteCommand(this, _isTest);
            OnDeleteCommand = new OnDeleteCommand(this, restApi, _isTest);
            OnOpenAddCommand = new OnOpenAddCommand(this, _isTest);
            OnGetDataBarangAddCommand = new OnGetDataBarangAddCommand(this, restApi, _isTest);
            OnAddCommand = new OnAddCommand(this, restApi, _isTest);
            OnOpenDeleteBarangCommand = new OnOpenDeleteBarangCommand(this, _isTest);
            OnDeleteBarangCommand = new OnDeleteBarangCommand(this, restApi, _isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnGetDetailCommand { get; }
        public ICommand OnOpenVerifikasiSemuaBarangCommand { get; }
        public ICommand OnVerifikasiSemuaBarangCommand { get; }
        public ICommand OnOpenUnVerifikasiSemuaBarangCommand { get; }
        public ICommand OnUnVerifikasiSemuaBarangCommand { get; }
        public ICommand OnOpenDeleteCommand { get; }
        public ICommand OnDeleteCommand { get; }
        public ICommand OnOpenAddCommand { get; }
        public ICommand OnAddCommand { get; }
        public ICommand OnGetDataBarangAddCommand { get; }
        public ICommand OnOpenDeleteBarangCommand { get; }
        public ICommand OnDeleteBarangCommand { get; }

        #region Filter
        private bool _isCheckedPeriode;
        public bool IsCheckedPeriode
        {
            get => _isCheckedPeriode;
            set
            {
                _isCheckedPeriode = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedNoTransaksi;
        public bool IsCheckedNoTransaksi
        {
            get => _isCheckedNoTransaksi;
            set
            {
                _isCheckedNoTransaksi = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedTglTransaksi;
        public bool IsCheckedTglTransaksi
        {
            get => _isCheckedTglTransaksi;
            set
            {
                _isCheckedTglTransaksi = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedKeterangan;
        public bool IsCheckedKeterangan
        {
            get => _isCheckedKeterangan;
            set
            {
                _isCheckedKeterangan = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedGudang;
        public bool IsCheckedGudang
        {
            get => _isCheckedGudang;
            set
            {
                _isCheckedGudang = value;
                OnPropertyChanged();
            }
        }

        private bool _isCheckedStatus;
        public bool IsCheckedStatus
        {
            get => _isCheckedStatus;
            set
            {
                _isCheckedStatus = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeGudangDto> _daftarPeriodeFilter = new ObservableCollection<MasterPeriodeGudangDto>();
        public ObservableCollection<MasterPeriodeGudangDto> DaftarPeriodeFilter
        {
            get => _daftarPeriodeFilter;
            set
            {
                _daftarPeriodeFilter = value;
                OnPropertyChanged();
            }
        }

        private MasterPeriodeGudangDto _selectedPeriodeFilter;
        public MasterPeriodeGudangDto SelectedPeriodeFilter
        {
            get => _selectedPeriodeFilter;
            set
            {
                _selectedPeriodeFilter = value;
                OnPropertyChanged();
            }
        }

        private string _noTransaksiFilter;
        public string NoTransaksiFilter
        {
            get => _noTransaksiFilter;
            set
            {
                _noTransaksiFilter = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglTransaksiAwalFilter;
        public DateTime? TglTransaksiAwalFilter
        {
            get => _tglTransaksiAwalFilter;
            set
            {
                _tglTransaksiAwalFilter = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _tglTransaksiAkhirFilter;
        public DateTime? TglTransaksiAkhirFilter
        {
            get => _tglTransaksiAkhirFilter;
            set
            {
                _tglTransaksiAkhirFilter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGudangDto> _daftarGudangFilter = new ObservableCollection<MasterGudangDto>();
        public ObservableCollection<MasterGudangDto> DaftarGudangFilter
        {
            get => _daftarGudangFilter;
            set
            {
                _daftarGudangFilter = value;
                OnPropertyChanged();
            }
        }

        private MasterGudangDto _selectedGudangFilter;
        public MasterGudangDto SelectedGudangFilter
        {
            get => _selectedGudangFilter;
            set
            {
                _selectedGudangFilter = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _daftarStatusFilter = new() { "Sudah Verifikasi", "Belum Verifikasi" };
        public ObservableCollection<string> DaftarStatusFilter
        {
            get => _daftarStatusFilter;
            set
            {
                _daftarStatusFilter = value;
                OnPropertyChanged();
            }
        }

        private string _selectedStatusFilter;
        public string SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                _selectedStatusFilter = value;
                OnPropertyChanged();
            }
        }

        private string _keteranganFilter;
        public string KeteranganFilter
        {
            get => _keteranganFilter;
            set
            {
                _keteranganFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _statusFilter;
        public bool StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ObservableCollection<BarangKeluarDto> _daftarBarangKeluar;
        public ObservableCollection<BarangKeluarDto> DaftarBarangKeluar
        {
            get => _daftarBarangKeluar;
            set
            {
                _daftarBarangKeluar = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyDaftarBarangKeluar");
            }
        }
        private BarangKeluarDto _selectedDaftarBarangKeluar;
        public BarangKeluarDto SelectedDaftarBarangKeluar
        {
            get => _selectedDaftarBarangKeluar;
            set
            {
                _selectedDaftarBarangKeluar = value;
                OnPropertyChanged();
                if (value != null)
                {
                    OnGetDetailCommand.Execute(null);
                }
            }
        }
        public bool IsEmptyDaftarBarangKeluar { get => _daftarBarangKeluar != null && _daftarBarangKeluar.Count == 0; }


        private ObservableCollection<BarangKeluarDetailDto> _daftarBarangKeluarDetail;
        public ObservableCollection<BarangKeluarDetailDto> DaftarBarangKeluarDetail
        {
            get => _daftarBarangKeluarDetail;
            set
            {
                _daftarBarangKeluarDetail = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyDaftarBarangKeluarDetail");
            }
        }
        private BarangKeluarDetailDto _selectedDaftarBarangKeluarDetail;
        public BarangKeluarDetailDto SelectedDaftarBarangKeluarDetail
        {
            get => _selectedDaftarBarangKeluarDetail;
            set
            {
                _selectedDaftarBarangKeluarDetail = value;
                OnPropertyChanged();
            }
        }
        public bool IsEmptyDaftarBarangKeluarDetail { get => _daftarBarangKeluarDetail != null && _daftarBarangKeluarDetail.Count == 0; }

        private ObservableCollection<MasterKeperluanDto> _daftarKeperluan;
        public ObservableCollection<MasterKeperluanDto> DaftarKeperluan
        {
            get => _daftarKeperluan;
            set
            {
                _daftarKeperluan = value;
                OnPropertyChanged();
            }
        }
        private MasterKeperluanDto _selectedDaftarKeperluan;
        public MasterKeperluanDto SelectedDaftarKeperluan
        {
            get => _selectedDaftarKeperluan;
            set
            {
                _selectedDaftarKeperluan = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingDetail;
        public bool IsLoadingDetail
        {
            get => _isLoadingDetail;
            set
            {
                _isLoadingDetail = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _pageSize = 1000000;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged();
            }
        }


        private MasterBarangDto _addBarang;
        public MasterBarangDto AddBarang
        {
            get => _addBarang;
            set
            {
                _addBarang = value;
                OnPropertyChanged();
            }
        }

        private DistribusiBarangStockDetailDto _addBarangWithStock;
        public DistribusiBarangStockDetailDto AddBarangWithStock
        {
            get => _addBarangWithStock;
            set
            {
                _addBarangWithStock = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyAddBarang");
            }
        }

        public bool IsEmptyAddBarang { get => AddBarangWithStock != null; }

    }
}
