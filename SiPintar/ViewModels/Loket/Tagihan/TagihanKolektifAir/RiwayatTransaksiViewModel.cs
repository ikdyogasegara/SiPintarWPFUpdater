using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands.Global;
using SiPintar.Commands.Loket.Tagihan.TagihanKolektifAir.RiwayatTransaksi;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir
{
    public class RiwayatTransaksiViewModel : ViewModelBase
    {
        public RiwayatTransaksiViewModel(TagihanKolektifAirViewModel parent, TagihanViewModel parentPage, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parent;
            ParentPage = parentPage;
            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnRefreshCommand = new OnRefreshCommand(this, restApi, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this, isTest);
            OnSubmitBatalkanPerNoLppCommand = new OnSubmitBatalkanPerNoLppCommand(this, restApi, isTest);
            OnSubmitBatalkanCommand = new OnSubmitBatalkanCommand(this, restApi, isTest);
            OnCetakCommand = new OnCetakCommand(restApi, "loket", "payment-cetak", "Cetak Tagihan", ErrorHandlingCetak, isTest);

            var tahun = new ObservableCollection<TahunDto>()
            {
                new TahunDto{Tahun = DateTime.Now.Year},
                new TahunDto{Tahun = DateTime.Now.AddYears(-1).Year},
                new TahunDto{Tahun = DateTime.Now.AddYears(-2).Year},
                new TahunDto{Tahun = DateTime.Now.AddYears(-3).Year},
                new TahunDto{Tahun = DateTime.Now.AddYears(-4).Year},
                new TahunDto{Tahun = DateTime.Now.AddYears(-5).Year},
            };

            TahunList = tahun;

            var tipe = new ObservableCollection<TipeTransaksiDto> { new TipeTransaksiDto() { Tipe = "Pembayaran Rek. Air" } };

            if (AppSetting.PelangganLimbah == true)
            {
                tipe.Add(new TipeTransaksiDto() { Tipe = "Pembayaran Rek. Limbah" });
            }

            if (AppSetting.PelangganLltt == true)
            {
                tipe.Add(new TipeTransaksiDto() { Tipe = "Pembayaran Rek. L2T2" });
            }

            tipe.Add(new TipeTransaksiDto() { Tipe = "Pembayaran Rek. Non Air" });

            TipeTransaksi = tipe;
        }

        public ICommand OnLoadCommand { get; }
        public ICommand OnRefreshCommand { get; }
        public ICommand OnResetFilterCommand { get; }
        public ICommand OnSubmitBatalkanCommand { get; }
        public ICommand OnSubmitBatalkanPerNoLppCommand { get; }
        public ICommand OnCetakCommand { get; }

        private TagihanKolektifAirViewModel _parent;
        public TagihanKolektifAirViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private TagihanViewModel _parentPage;
        public TagihanViewModel ParentPage
        {
            get => _parentPage;
            set { _parentPage = value; OnPropertyChanged(); }
        }

        private ViewModelBase _pageViewModel;
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        private string _endPoint;
        public string EndPoint
        {
            get => _endPoint;
            set { _endPoint = value; OnPropertyChanged(); }
        }

        private MasterPelangganAirWpf _selectedPelangganAir;
        public MasterPelangganAirWpf SelectedPelangganAir
        {
            get => _selectedPelangganAir;
            set { _selectedPelangganAir = value; OnPropertyChanged(); }
        }

        private RekeningNonAirWpf _selectedNonAir;
        public RekeningNonAirWpf SelectedNonAir
        {
            get => _selectedNonAir;
            set { _selectedNonAir = value; OnPropertyChanged(); }
        }

        private int? _idPelangganAir;
        public int? IdPelangganAir
        {
            get => _idPelangganAir;
            set { _idPelangganAir = value; OnPropertyChanged(); }
        }

        private int? _idNonAir;
        public int? IdNonAir
        {
            get => _idNonAir;
            set { _idNonAir = value; OnPropertyChanged(); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _isEmptyAir;
        public bool IsEmptyAir
        {
            get => _isEmptyAir;
            set { _isEmptyAir = value; OnPropertyChanged(); }
        }

        private bool _isEmptyNonAir;
        public bool IsEmptyNonAir
        {
            get => _isEmptyNonAir;
            set { _isEmptyNonAir = value; OnPropertyChanged(); }
        }

        private bool _isLoadingDaftarTransaksi;
        public bool IsLoadingDaftarTransaksi
        {
            get => _isLoadingDaftarTransaksi;
            set { _isLoadingDaftarTransaksi = value; OnPropertyChanged(); }
        }

        private bool _isEmptyDaftarTransaksi;
        public bool IsEmptyDaftarTransaksi
        {
            get => _isEmptyDaftarTransaksi;
            set { _isEmptyDaftarTransaksi = value; OnPropertyChanged(); }
        }

        private MasterPelangganAirDto _currentPelangganAir;
        public MasterPelangganAirDto CurrentPelangganAir
        {
            get => _currentPelangganAir;
            set { _currentPelangganAir = value; OnPropertyChanged(); }
        }

        private MasterPelangganLimbahDto _currentPelangganLimbah;
        public MasterPelangganLimbahDto CurrentPelangganLimbah
        {
            get => _currentPelangganLimbah;
            set { _currentPelangganLimbah = value; OnPropertyChanged(); }
        }

        private MasterPelangganLlttDto _currentPelangganLltt;
        public MasterPelangganLlttDto CurrentPelangganLltt
        {
            get => _currentPelangganLltt;
            set { _currentPelangganLltt = value; OnPropertyChanged(); }
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

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RiwayatPelunasanDanPembatalanWpf> _dataList = new ObservableCollection<RiwayatPelunasanDanPembatalanWpf>();
        public ObservableCollection<RiwayatPelunasanDanPembatalanWpf> DataList
        {
            get { return _dataList; }
            set { _dataList = value; OnPropertyChanged(); }
        }

        private RiwayatPelunasanDanPembatalanWpf _selectedData;
        public RiwayatPelunasanDanPembatalanWpf SelectedData
        {
            get { return _selectedData; }
            set { _selectedData = value; OnPropertyChanged(); }
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

        private KeyValuePair<int, string> _limitData = new KeyValuePair<int, string>(10, "10");
        [ExcludeFromCodeCoverage]
        public KeyValuePair<int, string> LimitData
        {
            get => _limitData;
            set
            {
                _limitData = value;
                OnPropertyChanged();
            }
        }

        private bool _isKasirChecked;
        public bool IsKasirChecked
        {
            get { return _isKasirChecked; }
            set
            {
                _isKasirChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoketChecked;
        public bool IsLoketChecked
        {
            get { return _isLoketChecked; }
            set
            {
                _isLoketChecked = value;
                OnPropertyChanged();
            }
        }

        private MasterUserDto _filterKasir;
        public MasterUserDto FilterKasir
        {
            get { return _filterKasir; }
            set
            {
                _filterKasir = value;
                OnPropertyChanged();
            }
        }

        private MasterLoketDto _filterLoket;
        public MasterLoketDto FilterLoket
        {
            get { return _filterLoket; }
            set
            {
                _filterLoket = value;
                OnPropertyChanged();
            }
        }

        private bool _isTahunChecked;
        public bool IsTahunChecked
        {
            get { return _isTahunChecked; }
            set
            {
                _isTahunChecked = value;
                OnPropertyChanged();
            }
        }


        private TahunDto _filterTahun;
        public TahunDto FilterTahun
        {
            get { return _filterTahun; }
            set
            {
                _filterTahun = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<TahunDto> _tahunList = new ObservableCollection<TahunDto>();
        public ObservableCollection<TahunDto> TahunList
        {
            get { return _tahunList; }
            set
            {
                _tahunList = value;
                OnPropertyChanged();
            }
        }



        private bool _isPeriodeChecked;
        public bool IsPeriodeChecked
        {
            get { return _isPeriodeChecked; }
            set
            {
                _isPeriodeChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKolektifChecked;
        public bool IsKolektifChecked
        {
            get { return _isKolektifChecked; }
            set
            {
                _isKolektifChecked = value;
                OnPropertyChanged();
            }
        }


        private MasterPeriodeDto _filterPeriode;
        public MasterPeriodeDto FilterPeriode
        {
            get { return _filterPeriode; }
            set
            {
                _filterPeriode = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<MasterPeriodeDto> _periodeList = new ObservableCollection<MasterPeriodeDto>();
        public ObservableCollection<MasterPeriodeDto> PeriodeList
        {
            get { return _periodeList; }
            set
            {
                _periodeList = value;
                OnPropertyChanged();
            }
        }


        private TipeTransaksiDto _filterTipeTransaksi;
        public TipeTransaksiDto FilterTipeTransaksi
        {
            get { return _filterTipeTransaksi; }
            set
            {
                _filterTipeTransaksi = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<TipeTransaksiDto> _tipeTransaksi = new ObservableCollection<TipeTransaksiDto>();
        public ObservableCollection<TipeTransaksiDto> TipeTransaksi
        {
            get { return _tipeTransaksi; }
            set
            {
                _tipeTransaksi = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterUserDto> _kasirList = new ObservableCollection<MasterUserDto>();
        public ObservableCollection<MasterUserDto> KasirList
        {
            get { return _kasirList; }
            set
            {
                _kasirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterLoketDto> _loketList = new ObservableCollection<MasterLoketDto>();
        public ObservableCollection<MasterLoketDto> LoketList
        {
            get { return _loketList; }
            set
            {
                _loketList = value;
                OnPropertyChanged();
            }
        }

        private MasterKolektifDto _filterKolektif;
        public MasterKolektifDto FilterKolektif
        {
            get { return _filterKolektif; }
            set
            {
                _filterKolektif = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKolektifDto> _kolektifList = new ObservableCollection<MasterKolektifDto>();
        public ObservableCollection<MasterKolektifDto> KolektifList
        {
            get { return _kolektifList; }
            set
            {
                _kolektifList = value;
                OnPropertyChanged();
            }
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

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RekeningPelunasanDanPembatalanWpf> _daftarTransaksi;
        public ObservableCollection<RekeningPelunasanDanPembatalanWpf> DaftarTransaksi
        {
            get => _daftarTransaksi;
            set { _daftarTransaksi = value; OnPropertyChanged(); }
        }

        public bool IsAllNoTransaksiSelected
        {
            get
            {
                if (DaftarTransaksi != null)
                {
                    var temp = DaftarTransaksi.Count(x => x.IsSelected);
                    return temp == DaftarTransaksi.Count;
                }
                return false;
            }
            set
            {
                if (DaftarTransaksi != null)
                {
                    foreach (var item in DaftarTransaksi)
                    {
                        item.IsSelected = value;
                    }
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAlasanBatalDto> _alasanList = new ObservableCollection<MasterAlasanBatalDto>();
        public ObservableCollection<MasterAlasanBatalDto> AlasanList
        {
            get { return _alasanList; }
            set { _alasanList = value; OnPropertyChanged(); }
        }

        private MasterAlasanBatalDto _selectedAlasan;
        public MasterAlasanBatalDto SelectedAlasan
        {
            get { return _selectedAlasan; }
            set { _selectedAlasan = value; OnPropertyChanged(); }
        }

        public void CheckUpdate()
        {
            OnPropertyChanged("IsAllNoTransaksiSelected");
        }
    }

    public class RekeningPelunasanDanPembatalanWpf : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }

        private string _nomorTransaksi;
        public string NomorTransaksi
        {
            get => _nomorTransaksi;
            set { _nomorTransaksi = value; OnPropertyChanged(); }
        }

        private int _jumlahTagihan;
        public int JumlahTagihan
        {
            get => _jumlahTagihan;
            set { _jumlahTagihan = value; OnPropertyChanged(); }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        [ExcludeFromCodeCoverage]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


    }

    [ExcludeFromCodeCoverage]
    public class TahunDto
    {
        public int Tahun { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class TipeTransaksiDto
    {
        public string Tipe { get; set; }
    }
}
