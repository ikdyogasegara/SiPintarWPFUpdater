using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Gudang.ProsesTransaksi.DaftarBarangMasuk;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class DaftarBarangMasukViewModel : ViewModelBase
    {
        public readonly IRestApiClientModel RestApi;
        public DaftarBarangMasukViewModel(ProsesTransaksiViewModel parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = parent;
            RestApi = restApi;
            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnGetBarangDetailCommand = new OnGetBarangDetailCommand(this, restApi, isTest);
            OnOpenSetTanggalVoucherDialogCommand = new OnOpenSetTanggalVoucherDialogCommand(this, isTest);
            OnSetTanggalVoucherCommand = new OnSetTanggalVoucherCommand(this, restApi, isTest);
            OnDeleteDaftarBarangMasukCommand = new OnDeleteDaftarBarangMasukCommand(this, restApi, isTest);
            OnDeleteBarangMasukCommand = new OnDeleteBarangMasukCommand(this, restApi, isTest);
            OnOpenTambahBarangCommand = new OnOpenTambahBarangCommand(this, isTest);
            OnTambahBarangCommand = new OnTambahBarangCommand(this, restApi, isTest);
            OnOpenKoreksiBarangCommand = new OnOpenKoreksiBarangCommand(this, isTest);
            OnKoreksiBarangCommand = new OnKoreksiBarangCommand(this, restApi, isTest);
            OnCetakOrderPembelianCommand = new OnCetakCommand(restApi, "gudang", "barang-masuk-cetak", "Cetak Order Pembelian", ErrorHandlingCetak, isTest)
            {
                TemplateName = "Gudang-BarangMasuk-OrderPembelian"
            };
            OnCetakSpkCommand = new OnCetakCommand(restApi, "gudang", "barang-masuk-cetak", "Cetak Surat Perjanjian Kerja", ErrorHandlingCetak, isTest)
            {
                TemplateName = "Gudang-BarangMasuk-SuratPerjanjianKerja"
            };
            OnCetakLpbCommand = new OnCetakCommand(restApi, "gudang", "barang-masuk-cetak", "Cetak LPB", ErrorHandlingCetak, isTest)
            {
                TemplateName = "Gudang-BarangMasuk-LPB"
            };
            OnCetakDpbCommand = new OnCetakCommand(restApi, "gudang", "barang-masuk-cetak", "Cetak DPB", ErrorHandlingCetak, isTest)
            {
                TemplateName = "Gudang-BarangMasuk-DPB"
            };
        }

        #region Variable
        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnGetBarangDetailCommand { get; }
        public ICommand OnOpenSetTanggalVoucherDialogCommand { get; }
        public ICommand OnSetTanggalVoucherCommand { get; }
        public ICommand OnDeleteDaftarBarangMasukCommand { get; }
        public ICommand OnDeleteBarangMasukCommand { get; }
        public ICommand OnOpenTambahBarangCommand { get; }
        public ICommand OnOpenKoreksiBarangCommand { get; }
        public ICommand OnTambahBarangCommand { get; }
        public ICommand OnKoreksiBarangCommand { get; }
        public ICommand OnCetakOrderPembelianCommand { get; }
        public ICommand OnCetakSpkCommand { get; }
        public ICommand OnCetakLpbCommand { get; }
        public ICommand OnCetakDpbCommand { get; }

        private DaftarBarangMasukTab _activeTab = DaftarBarangMasukTab.PerNoTransaksi;
        public DaftarBarangMasukTab ActiveTab
        {
            get => _activeTab;
            set
            {
                _activeTab = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<BarangMasukDto> _daftarBarangMasuk;
        public ObservableCollection<BarangMasukDto> DaftarBarangMasuk
        {
            get => _daftarBarangMasuk;
            set
            {
                _daftarBarangMasuk = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyDaftarBarangMasuk");
            }
        }

        private BarangMasukDto _selectedDaftarBarangMasuk;
        public BarangMasukDto SelectedDaftarBarangMasuk
        {
            get => _selectedDaftarBarangMasuk;
            set
            {
                _selectedDaftarBarangMasuk = value;
                OnPropertyChanged();
                OnGetBarangDetailCommand.Execute(null);
            }
        }
        public bool IsEmptyDaftarBarangMasuk { get => _daftarBarangMasuk != null && _daftarBarangMasuk.Count == 0; }

        private ObservableCollection<BarangMasukDetailDto> _daftarBarangMasukDetail;
        public ObservableCollection<BarangMasukDetailDto> DaftarBarangMasukDetail
        {
            get => _daftarBarangMasukDetail;
            set
            {
                _daftarBarangMasukDetail = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmptyDaftarBarangMasukDetail");
            }
        }

        private BarangMasukDetailDto _selectedDaftarBarangMasukDetail;
        public BarangMasukDetailDto SelectedDaftarBarangMasukDetail
        {
            get => _selectedDaftarBarangMasukDetail;
            set
            {
                _selectedDaftarBarangMasukDetail = value;
                OnPropertyChanged();
            }
        }
        public bool IsEmptyDaftarBarangMasukDetail { get => SelectedDaftarBarangMasuk != null && _daftarBarangMasukDetail != null && _daftarBarangMasukDetail.Count == 0; }

        private ObservableCollection<MasterKategoriBarangMasukDto> _kategoriBarangMasuk;
        public ObservableCollection<MasterKategoriBarangMasukDto> KategoriBarangMasuk
        {
            get => _kategoriBarangMasuk;
            set
            {
                _kategoriBarangMasuk = value;
                OnPropertyChanged();
            }
        }

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
        #endregion

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
        #endregion

        public void ResetFilter()
        {
            IsCheckedPeriode = false;
            IsCheckedNoTransaksi = false;
            IsCheckedTglTransaksi = false;
            SelectedPeriodeFilter = null;
            NoTransaksiFilter = null;
            TglTransaksiAwalFilter = null;
            TglTransaksiAkhirFilter = null;
        }
    }

    public enum DaftarBarangMasukTab
    {
        PerNoTransaksi,
        DaftarBarang
    }
}
