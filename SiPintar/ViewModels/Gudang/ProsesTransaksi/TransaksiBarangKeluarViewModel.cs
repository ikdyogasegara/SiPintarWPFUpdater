using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Gudang.ProsesTransaksi.TransaksiBarangKeluar;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Gudang.ProsesTransaksi
{
    public class TransaksiBarangKeluarViewModel : ViewModelBase
    {
        public delegate void OnEnterKodeBarangHandler();
        public delegate void OnAddBarangToListPengajuanHandler();
        public delegate void OnBarangTambahEnterHandler();
        public event OnEnterKodeBarangHandler OnEnterKodeBarangEvent;
        public event OnAddBarangToListPengajuanHandler OnAddBarangToListPengajuanEvent;
        public event OnBarangTambahEnterHandler OnBarangTambahEnterEvent;

        public TransaksiBarangKeluarViewModel(ProsesTransaksiViewModel _parent, IRestApiClientModel restApi, bool isTest = false)
        {
            _ = _parent;
            OnLoadCommand = new OnLoadCommand(this, restApi, isTest);
            OnOpenPengajuanFormCommand = new OnOpenPengajuanFormCommand(this, restApi, isTest);
            OnGenerateNoTransaksiCommand = new OnGenerateNoTransaksiCommand(this, restApi, isTest);
            OnGenerateNoRegistrasiCommand = new OnGenerateNoRegistrasiCommand(this, restApi, isTest);
            OnGetBarangDetailCommand = new OnGetBarangDetailCommand(this, restApi, isTest);
            OnAddBarangToListFormCommand = new OnAddBarangToListFormCommand(this, restApi, isTest);
            OnDeleteBarangListForm = new OnDeleteBarangListForm(this, isTest);
            OnAddBarangPaketToListFormCommand = new OnAddBarangPaketToListFormCommand(this, restApi, isTest);
            OnOpenCariBarangCommand = new OnOpenCariBarangCommand(this, isTest);
            OnOpenCariBarangTambahCommand = new OnOpenCariBarangTambahCommand(this, isTest);
            OnGetCariBarangListCommand = new OnGetCariBarangListCommand(this, restApi, isTest);
            OnOpenSubmitPengajuanPengeluaranBarangCommand = new OnOpenSubmitPengajuanPengeluaranBarangCommand(this, isTest);
            OnSubmitPengajuanPengeluaranBarangCommand = new OnSubmitPengajuanPengeluaranBarangCommand(this, restApi, isTest);
            OnGetDataDetailCommand = new OnGetDataDetailCommand(this, restApi, isTest);
            OnOpenTambahBarangCommand = new OnOpenTambahBarangCommand(this, isTest);
            OnGetBarangTambahDetailCommand = new OnGetBarangTambahDetailCommand(this, restApi, isTest);
            OnTambahBarangKePengajuanCommand = new OnTambahBarangKePengajuanCommand(this, restApi, isTest);
            OnOpenKoreksiDataDetailCommand = new OnOpenKoreksiDataDetailCommand(this, isTest);
            OnSubmitKoreksiDataDetailCommand = new OnSubmitKoreksiDataDetailCommand(this, restApi, isTest);
            OnOpenDeleteDataDetailCommand = new OnOpenDeleteDataDetailCommand(this, isTest);
            OnSubmitDeleteDataDetailCommand = new OnSubmitDeleteDataDetailCommand(this, restApi, isTest);
            OnOpenProsesDppbCommand = new OnOpenProsesDppbCommand(this, isTest);
            OnGenerateNoBppCommand = new OnGenerateNoBppCommand(this, restApi, isTest);
            OnOpenConfirmationProsesCommand = new OnOpenConfirmationProsesCommand(this, isTest);
            OnSubmitProsesCommand = new OnSubmitProsesCommand(this, restApi, isTest);
            OnGetDataSelesaiDetailCommand = new OnGetDataSelesaiDetailCommand(this, restApi, isTest);
            OnFilterDataSelesaiCommand = new OnFilterDataSelesaiCommand(this, restApi, isTest);
            OnOpenHapusPengajuanCommand = new OnOpenHapusPengajuanCommand(this, isTest);
            OnSubmitHapusPengajuanCommand = new OnSubmitHapusPengajuanCommand(this, restApi, isTest);
            OnOpenKoreksiDataPengajuanCommand = new OnOpenKoreksiDataPengajuanCommand(this, isTest);
            OnOpenConfirmKoreksiDataPengajuanCommand = new OnOpenConfirmKoreksiDataPengajuanCommand(this, isTest);
            OnSubmitKoreksiDataPengajuanCommand = new OnSubmitKoreksiDataPengajuanCommand(this, restApi, isTest);
            OnCetakPengajuanCommand = new OnCetakCommand(restApi, "gudang", "supervisi-pengajuan-pengeluaran-barang-cetak", "Cetak Pengajuan", ErrorHandlingCetak, isTest);
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenPengajuanFormCommand { get; }
        public ICommand OnGenerateNoTransaksiCommand { get; }
        public ICommand OnGenerateNoRegistrasiCommand { get; }
        public ICommand OnGetBarangDetailCommand { get; }
        public ICommand OnAddBarangToListFormCommand { get; }
        public ICommand OnDeleteBarangListForm { get; }
        public ICommand OnAddBarangPaketToListFormCommand { get; }
        public ICommand OnOpenCariBarangCommand { get; }
        public ICommand OnOpenCariBarangTambahCommand { get; }
        public ICommand OnGetCariBarangListCommand { get; }
        public ICommand OnOpenSubmitPengajuanPengeluaranBarangCommand { get; }
        public ICommand OnSubmitPengajuanPengeluaranBarangCommand { get; }
        public ICommand OnGetDataDetailCommand { get; }
        public ICommand OnOpenTambahBarangCommand { get; }
        public ICommand OnGetBarangTambahDetailCommand { get; }
        public ICommand OnTambahBarangKePengajuanCommand { get; }
        public ICommand OnOpenKoreksiDataDetailCommand { get; }
        public ICommand OnSubmitKoreksiDataDetailCommand { get; }
        public ICommand OnOpenDeleteDataDetailCommand { get; }
        public ICommand OnSubmitDeleteDataDetailCommand { get; }
        public ICommand OnOpenProsesDppbCommand { get; }
        public ICommand OnGenerateNoBppCommand { get; }
        public ICommand OnOpenConfirmationProsesCommand { get; }
        public ICommand OnSubmitProsesCommand { get; }
        public ICommand OnGetDataSelesaiDetailCommand { get; }
        public ICommand OnFilterDataSelesaiCommand { get; }
        public ICommand OnOpenHapusPengajuanCommand { get; }
        public ICommand OnSubmitHapusPengajuanCommand { get; }
        public ICommand OnOpenKoreksiDataPengajuanCommand { get; }
        public ICommand OnOpenConfirmKoreksiDataPengajuanCommand { get; }
        public ICommand OnSubmitKoreksiDataPengajuanCommand { get; }
        public ICommand OnCetakPengajuanCommand { get; }

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

        private bool _isLoadin1;
        public bool IsLoading1
        {
            get => _isLoadin1;
            set
            {
                _isLoadin1 = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get => _isLoadingForm;
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingCari;
        public bool IsLoadingCari
        {
            get => _isLoadingCari;
            set
            {
                _isLoadingCari = value;
                OnPropertyChanged();
            }
        }

        private bool _isErrorCariKodeBarang;
        public bool IsErrorCariKodeBarang
        {
            get => _isErrorCariKodeBarang;
            set
            {
                _isErrorCariKodeBarang = value;
                OnPropertyChanged();
            }
        }

        private bool _isGeneratingNoBpp;
        public bool IsGeneratingNoBpp
        {
            get => _isGeneratingNoBpp;
            set
            {
                _isGeneratingNoBpp = value;
                OnPropertyChanged();
            }
        }

        private bool _isNoTransaksiChecked;
        public bool IsNoTransaksiChecked
        {
            get => _isNoTransaksiChecked;
            set
            {
                _isNoTransaksiChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isNoRegistrasiChecked;
        public bool IsNoRegistrasiChecked
        {
            get => _isNoRegistrasiChecked;
            set
            {
                _isNoRegistrasiChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isTanggalPengajuanChecked;
        public bool IsTanggalPengajuanChecked
        {
            get => _isTanggalPengajuanChecked;
            set
            {
                _isTanggalPengajuanChecked = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PengajuanPengeluaranBarangDto> _dataList;
        public ObservableCollection<PengajuanPengeluaranBarangDto> DataList
        {
            get => _dataList;
            set
            {
                _dataList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PengajuanPengeluaranBarangDto> _dataSelesaiList;
        public ObservableCollection<PengajuanPengeluaranBarangDto> DataSelesaiList
        {
            get => _dataSelesaiList;
            set
            {
                _dataSelesaiList = value;
                OnPropertyChanged();
            }
        }

        private PengajuanPengeluaranBarangDto _selectedData;
        public PengajuanPengeluaranBarangDto SelectedData
        {
            get => _selectedData;
            set
            {
                _selectedData = value;
                OnPropertyChanged();
                if (value != null)
                {
                    OnGetDataDetailCommand.Execute(null);
                }
            }
        }

        private PengajuanPengeluaranBarangDto _selectedDataSelesai;
        public PengajuanPengeluaranBarangDto SelectedDataSelesai
        {
            get => _selectedDataSelesai;
            set
            {
                _selectedDataSelesai = value;
                OnPropertyChanged();
                if (value != null)
                {
                    OnGetDataSelesaiDetailCommand.Execute(null);
                }
            }
        }

        private ObservableCollection<PengajuanPengeluaranBarangDetailDto> _dataDetailList;
        public ObservableCollection<PengajuanPengeluaranBarangDetailDto> DataDetailList
        {
            get => _dataDetailList;
            set
            {
                _dataDetailList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<PengajuanPengeluaranBarangDetailDto> _dataSelesaiDetailList;
        public ObservableCollection<PengajuanPengeluaranBarangDetailDto> DataSelesaiDetailList
        {
            get => _dataSelesaiDetailList;
            set
            {
                _dataSelesaiDetailList = value;
                OnPropertyChanged();
            }
        }

        private PengajuanPengeluaranBarangDetailDto _selectedDataDetail;
        public PengajuanPengeluaranBarangDetailDto SelectedDataDetail
        {
            get => _selectedDataDetail;
            set
            {
                _selectedDataDetail = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPengeluaranBarangWpf _form;
        public ParamPengajuanPengeluaranBarangWpf Form
        {
            get => _form;
            set
            {
                _form = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKategoriBarangKeluarDto> _kategoriList;
        public ObservableCollection<MasterKategoriBarangKeluarDto> KategoriList
        {
            get => _kategoriList;
            set
            {
                _kategoriList = value;
                OnPropertyChanged();
            }
        }

        private MasterKategoriBarangKeluarDto _selectedKategori;
        public MasterKategoriBarangKeluarDto SelectedKategori
        {
            get => _selectedKategori;
            set
            {
                _selectedKategori = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGudangDto> _gudangList;
        public ObservableCollection<MasterGudangDto> GudangList
        {
            get => _gudangList;
            set
            {
                _gudangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPeriodeGudangDto> _periodeList;
        public ObservableCollection<MasterPeriodeGudangDto> PeriodeList
        {
            get => _periodeList;
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBarangPaketDto> _paketBarangList;
        public ObservableCollection<MasterBarangPaketDto> PaketBarangList
        {
            get => _paketBarangList;
            set
            {
                _paketBarangList = value;
                OnPropertyChanged();
            }
        }

        private MasterGudangDto _selectedGudang;
        public MasterGudangDto SelectedGudang
        {
            get => _selectedGudang;
            set
            {
                _selectedGudang = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangPaketDto _selectedPaketBarang;
        public MasterBarangPaketDto SelectedPaketBarang
        {
            get => _selectedPaketBarang;
            set
            {
                _selectedPaketBarang = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBagianMemintaBarangDto> _bagianMemintaBarangList;
        public ObservableCollection<MasterBagianMemintaBarangDto> BagianMemintaBarangList
        {
            get => _bagianMemintaBarangList;
            set
            {
                _bagianMemintaBarangList = value;
                OnPropertyChanged();
            }
        }

        private MasterBagianMemintaBarangDto _selectedBagianMemintaBarangList;
        public MasterBagianMemintaBarangDto SelectedBagianMemintaBarang
        {
            get => _selectedBagianMemintaBarangList;
            set
            {
                _selectedBagianMemintaBarangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterBarangWpf> _barangList;
        public ObservableCollection<MasterBarangWpf> BarangList
        {
            get => _barangList;
            set
            {
                _barangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ListBarangDistribusiBarangDto> _barangPilihList;
        public ObservableCollection<ListBarangDistribusiBarangDto> BarangPilihList
        {
            get => _barangPilihList;
            set
            {
                _barangPilihList = value;
                OnPropertyChanged();
            }
        }

        private ListBarangDistribusiBarangDto _selectedbarangPilihList;
        public ListBarangDistribusiBarangDto SelectedBarangPilihList
        {
            get => _selectedbarangPilihList;
            set
            {
                _selectedbarangPilihList = value;
                OnPropertyChanged();
            }
        }

        private string _barangPilihCari;
        public string BarangPilihCari
        {
            get => _barangPilihCari;
            set
            {
                _barangPilihCari = value;
                OnPropertyChanged();
            }
        }

        private MasterBarangWpf _cariBarangForm = new MasterBarangWpf();
        public MasterBarangWpf CariBarangForm
        {
            get => _cariBarangForm;
            set
            {
                _cariBarangForm = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPengeluaranBarangDetailWpf _selectedBarangFormPengeluaran;
        public ParamPengajuanPengeluaranBarangDetailWpf SelectedBarangFormPengeluaran
        {
            get => _selectedBarangFormPengeluaran;
            set
            {
                _selectedBarangFormPengeluaran = value;
                OnPropertyChanged();
            }
        }

        private ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarWpf _formProses;
        public ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarWpf FormProses
        {
            get => _formProses;
            set
            {
                _formProses = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorTransaksi;
        public string FilterNomorTransaksi
        {
            get => _filterNomorTransaksi;
            set
            {
                _filterNomorTransaksi = value;
                OnPropertyChanged();
            }
        }

        private string _filterNomorRegistrasi;
        public string FilterNomorRegistrasi
        {
            get => _filterNomorRegistrasi;
            set
            {
                _filterNomorRegistrasi = value;
                OnPropertyChanged();
            }
        }

        private DateTime _filterTanggalPengajuanMulai = DateTime.Now;
        public DateTime FilterTanggalPengajuanMulai
        {
            get => _filterTanggalPengajuanMulai;
            set
            {
                _filterTanggalPengajuanMulai = value;
                OnPropertyChanged();
            }
        }

        private DateTime _filterTanggalPengajuanSelesai = DateTime.Now;
        public DateTime FilterTanggalPengajuanSelesai
        {
            get => _filterTanggalPengajuanSelesai;
            set
            {
                _filterTanggalPengajuanSelesai = value;
                OnPropertyChanged();
            }
        }

        public void InvokeEventOnEnterKodeBarang()
        {
            OnEnterKodeBarangEvent?.Invoke();
        }
        public void InvokeEventOnAddBarangToListPengajuan()
        {
            OnAddBarangToListPengajuanEvent?.Invoke();
        }
        public void InvokeEventOnBarangTambahEnter()
        {
            OnBarangTambahEnterEvent?.Invoke();
        }
    }


    public class ParamPengajuanPengeluaranBarangWpf : ParamPengajuanPengeluaranBarangDto, INotifyPropertyChanged
    {
        public string NomorPengajuanPengeluaranWpf
        {
            get => NomorPengajuanPengeluaran;
            set
            {
                NomorPengajuanPengeluaran = value;
                OnPropertyChanged();
            }
        }

        public string NomorRegistrasiWpf
        {
            get => NomorRegistrasi;
            set
            {
                NomorRegistrasi = value;
                OnPropertyChanged();
            }
        }

        private string _kategori;
        public string KategoriWpf
        {
            get => _kategori;
            set
            {
                _kategori = value;
                OnPropertyChanged();
            }
        }

        private string _kodeGudang;
        public string KodeGudangWpf
        {
            get => _kodeGudang;
            set
            {
                _kodeGudang = value;
                OnPropertyChanged();
            }
        }

        private string _namaGudang;
        public string NamaGudangWpf
        {
            get => _namaGudang;
            set
            {
                _namaGudang = value;
                OnPropertyChanged();
            }
        }

        private string _namaBagianMeminta;
        public string NamaBagianMemintaWpf
        {
            get => _namaBagianMeminta;
            set
            {
                _namaBagianMeminta = value;
                OnPropertyChanged();
            }
        }

        public DateTime? TanggalPengajuanWpf
        {
            get
            {
                TglPengajuan ??= DateTime.Now;
                return TglPengajuan;
            }
            set
            {
                TglPengajuan = value;
                OnPropertyChanged();
            }
        }

        public string DiGunakanUntukWpf
        {
            get => DiGunakanUntuk;
            set
            {
                DiGunakanUntuk = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf> _detail;
        public ObservableCollection<ParamPengajuanPengeluaranBarangDetailWpf> DetailWpf
        {
            get => _detail;
            set
            {
                _detail = value;
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged Members
        [ExcludeFromCodeCoverage]
        public event PropertyChangedEventHandler PropertyChanged;

        [ExcludeFromCodeCoverage]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class ParamPengajuanPengeluaranBarangDetailWpf : ParamPengajuanPengeluaranBarangDetailDto
    {
        public string Wilayah { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public string Satuan { get; set; }
    }

    public class MasterBarangWpf : MasterBarangDto
    {
        public decimal Qty { get; set; } = decimal.Zero;
    }

    public class ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarWpf : ParamPengajuanPengeluaranBarangProsesKeDaftarBarangKeluarDto, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        [ExcludeFromCodeCoverage]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string NomorBppWpf
        {
            get => NomorBpp;
            set
            {
                NomorBpp = value;
                OnPropertyChanged();
            }
        }

    }
}
