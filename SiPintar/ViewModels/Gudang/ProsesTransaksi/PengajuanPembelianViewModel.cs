using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Gudang.ProsesTransaksi.PengajuanPembelian;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class PengajuanPembelianViewModel : ViewModelBase
    {
        public PengajuanPembelianViewModel(ProsesTransaksiViewModel? parent = null, IRestApiClientModel? restApi = null, bool isTest = false)
        {
            _ = parent;
            _isLoading = false;
            _hideDone = true;
            _filterWilayahEnabled = false;
            _filterNoPengajuanEnabled = false;
            _filterTglPengajuanEnabled = false;
            _isAdd = false;
            _isView = false;
            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi!, isTest);
            OnOpenPengajuanCommand = new OnOpenPengajuanCommand(this, restApi!, isTest);
            OnAddDataBarangPengajuanCommand = new OnAddDataBarangPengajuanCommand(this, restApi!, isTest);
            OnDeleteDataBarangPengajuanCommand = new OnDeleteDataBarangPengajuanCommand(this, isTest);
            OnSubmitPengajuanCommand = new OnSubmitPengajuanCommand(this, restApi!, isTest);
            OnOpenDeletePengajuanCommand = new OnOpenDeletePengajuanCommand(this, isTest);
            OnDeletePengajuanCommand = new OnDeletePengajuanCommand(this, restApi!, isTest);
            OnViewPengajuanCommand = new OnViewPengajuanCommand(this, restApi!, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnOpenPengajuanCommand { get; }
        public ICommand OnAddDataBarangPengajuanCommand { get; }
        public ICommand OnDeleteDataBarangPengajuanCommand { get; }
        public ICommand OnSubmitPengajuanCommand { get; }
        public ICommand OnOpenDeletePengajuanCommand { get; }
        public ICommand OnDeletePengajuanCommand { get; }
        public ICommand OnViewPengajuanCommand { get; }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set
            {
                _isAdd = value;
                OnPropertyChanged();
                if (value)
                {
                    IsView = !value;
                }
            }
        }

        private bool _isView;
        public bool IsView
        {
            get => _isView;
            set
            {
                _isView = value;
                OnPropertyChanged();
                if (value)
                {
                    IsAdd = !value;
                }
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

        private bool _hideDone;
        public bool HideDone
        {
            get => _hideDone;
            set
            {
                _hideDone = value;
                OnPropertyChanged();
                OnRefreshCommand.Execute(null!);
            }
        }

        private ObservableCollection<MasterWilayahGudangWpf>? _wilayahList;
        public ObservableCollection<MasterWilayahGudangWpf>? WilayahList
        {
            get => _wilayahList;
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadMasterWilayah));
            }
        }
        public bool LoadMasterWilayah { get => _wilayahList == null; }

        private ObservableCollection<MasterKategoriBarangMasukDto>? _kategoriBarangMasukList;
        public ObservableCollection<MasterKategoriBarangMasukDto>? KategoriBarangMasukList
        {
            get => _kategoriBarangMasukList;
            set
            {
                _kategoriBarangMasukList = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LoadKategoriBarangMasuk));
            }
        }
        public bool LoadKategoriBarangMasuk { get => _kategoriBarangMasukList == null; }


        private MasterWilayahGudangWpf? _filterWilayah;
        public MasterWilayahGudangWpf? FilterWilayah
        {
            get => _filterWilayah;
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }

        private bool _filterWilayahEnabled;
        public bool FilterWilayahEnabled
        {
            get => _filterWilayahEnabled;
            set
            {
                _filterWilayahEnabled = value;
                OnPropertyChanged();
            }
        }

        private string? _filterNoPengajuan;
        public string? FilterNoPengajuan
        {
            get => _filterNoPengajuan;
            set
            {
                _filterNoPengajuan = value;
                OnPropertyChanged();
            }
        }

        private bool _filterNoPengajuanEnabled;
        public bool FilterNoPengajuanEnabled
        {
            get => _filterNoPengajuanEnabled;
            set
            {
                _filterNoPengajuanEnabled = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTglPengajuanMulai;
        public DateTime? FilterTglPengajuanMulai
        {
            get => _filterTglPengajuanMulai;
            set
            {
                _filterTglPengajuanMulai = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _filterTglPengajuanSampai;
        public DateTime? FilterTglPengajuanSampai
        {
            get => _filterTglPengajuanSampai;
            set
            {
                _filterTglPengajuanSampai = value;
                OnPropertyChanged();
            }
        }

        private bool _filterTglPengajuanEnabled;
        public bool FilterTglPengajuanEnabled
        {
            get => _filterTglPengajuanEnabled;
            set
            {
                _filterTglPengajuanEnabled = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<PengajuanPembelianBarangDto>? _data;
        public ObservableCollection<PengajuanPembelianBarangDto>? Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged();
                OnPropertyChanged("IsEmpty");
            }
        }

        private PengajuanPembelianBarangDto? _selectedData;
        public PengajuanPembelianBarangDto? SelectedData
        {
            get { return _selectedData; }
            set
            {
                _selectedData = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty { get => _data != null && _data.Count == 0; }

        #region PengajuanBaru
        private string? _noDpbPengajuan;
        public string? NoDpbPengajuan
        {
            get => _noDpbPengajuan;
            set
            {
                _noDpbPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged("LoadNoDpb");
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }
        public bool LoadNoDpb { get => _noDpbPengajuan == null; }


        private MasterWilayahGudangWpf? _gudangPengajuan;
        public MasterWilayahGudangWpf? GudangPengajuan
        {
            get => _gudangPengajuan;
            set
            {
                _gudangPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }

        private MasterKategoriBarangMasukDto? _kategoriPengajuan;
        public MasterKategoriBarangMasukDto? KategoriPengajuan
        {
            get => _kategoriPengajuan;
            set
            {
                _kategoriPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }

        private DateTime? _tanggalPengajuan;
        public DateTime? TanggalPengajuan
        {
            get => _tanggalPengajuan;
            set
            {
                _tanggalPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }

        private string? _keteranganPengajuan;
        public string? KeteranganPengajuan
        {
            get => _keteranganPengajuan;
            set
            {
                _keteranganPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }

        private ObservableCollection<MasterBarangPengajuan>? _daftarBarangPengajuan;
        public ObservableCollection<MasterBarangPengajuan>? DaftarBarangPengajuan
        {
            get => _daftarBarangPengajuan;
            set
            {
                _daftarBarangPengajuan = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsValidPengajuan));
            }
        }

        private MasterBarangPengajuan? _selectedBarangPengajuan;
        public MasterBarangPengajuan? SelectedBarangPengajuan
        {
            get => _selectedBarangPengajuan;
            set
            {
                _selectedBarangPengajuan = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidPengajuan
        {
            get
            {
                return NoDpbPengajuan != null && GudangPengajuan != null
                    && KategoriPengajuan != null && TanggalPengajuan != null
                    && KeteranganPengajuan != null && KeteranganPengajuan.Trim() != ""
                    && DaftarBarangPengajuan != null && DaftarBarangPengajuan?.Count > 0;
            }
        }
        #endregion
    }

    public class MasterWilayahGudangWpf : MasterWilayahDto
    {
        public string GudangWilayah { get => $"Gudang {NamaWilayah}"; }
    }

    public class MasterBarangPengajuan : MasterBarangDto
    {
        private decimal? _totalQty;
        public decimal? TotalQty
        {
            get => _totalQty;
            set
            {
                _totalQty = value;
            }
        }
    }

}
