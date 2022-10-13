using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using SiPintar.Commands;
using SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi.Interaksi;

namespace SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi
{
    public class InteraksiViewModel : ViewModelBase
    {
        private readonly InteraksiLayananViewModel _interaksiLayanan;
        private readonly InteraksiPenyusutanViewModel _interaksiPenyusutan;
        private readonly InteraksiJenisPersediaanViewModel _interaksiJenisPersediaan;

        public InteraksiViewModel(MasterDataAkuntansiViewModel parentViewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            Parent = parentViewModel;

            _interaksiLayanan = new InteraksiLayananViewModel(this, restApi);
            _interaksiPenyusutan = new InteraksiPenyusutanViewModel(this, restApi);
            _interaksiJenisPersediaan = new InteraksiJenisPersediaanViewModel(this, restApi);

            OnLoadCommand = new OnLoadCommand(this, isTest);
            OnResetFilterCommand = new OnResetFilterCommand(this);
            OnRefreshCommand = new OnRefreshCommand(this, isTest);
        }

        public ICommand OnLoadCommand { get; set; }
        public ICommand OnResetFilterCommand { get; set; }
        public ICommand OnRefreshCommand { get; set; }

        private MasterDataAkuntansiViewModel _parent = null!;
        public MasterDataAkuntansiViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private string _selectedPage = string.Empty;
        public string SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                OnPropertyChanged();
            }
        }

        private string _header1 = string.Empty;
        public string Header1
        {
            get { return _header1; }
            set
            {
                _header1 = value;
                OnPropertyChanged();
            }
        }

        private string _header2 = string.Empty;
        public string Header2
        {
            get { return _header2; }
            set
            {
                _header2 = value;
                OnPropertyChanged();
            }
        }

        private bool _isInPelayananAir;
        public bool IsInPelayananAir
        {
            get { return _isInPelayananAir; }
            set
            {
                _isInPelayananAir = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<int, string>> JenisInteraksiPelayanan
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(0, "Interaksi Pelayanan Air"),
                    new KeyValuePair<int, string>(1, "Interaksi Pelayanan Non Air"),
                };
                return data;
            }
        }

        private KeyValuePair<int, string> _selectedJenisInteraksiPelayanan;
        public KeyValuePair<int, string> SelectedJenisInteraksiPelayanan
        {
            get => _selectedJenisInteraksiPelayanan;
            set
            {
                _selectedJenisInteraksiPelayanan = value;
                if (value.Key == 0)
                {
                    Url = "master-inpelayanan-air";
                    IsInPelayananAir = true;

                }
                else if (value.Key == 1)
                {
                    Url = "master-inpelayanan-non-air";
                    IsInPelayananAir = false;
                }
                else
                {
                    Url = "";
                    IsInPelayananAir = false;
                }

                OnPropertyChanged();
            }
        }


        private string _url = string.Empty;
        public string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        #region Filter Display Toggle
        private bool _isFilterInLayanan;
        public bool IsFilterInLayanan
        {
            get => _isFilterInLayanan;
            set
            {
                _isFilterInLayanan = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilterInPersediaan;
        public bool IsFilterInPersediaan
        {
            get => _isFilterInPersediaan;
            set
            {
                _isFilterInPersediaan = value;
                OnPropertyChanged();
            }
        }

        private bool _isFilterInPenyusutan;
        public bool IsFilterInPenyusutan
        {
            get => _isFilterInPenyusutan;
            set
            {
                _isFilterInPenyusutan = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ViewModelBase _pageViewModel = new();
        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set { _pageViewModel = value; OnPropertyChanged(); }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Interaksi Layanan & Kd. Perkiraan (Air/Non)" => _interaksiLayanan,
                "Inter. Jenis Persediaan Kode Perkiraan" => _interaksiJenisPersediaan,
                "Inter. Penyusutan & Kode Perkiraan Biaya" => _interaksiPenyusutan,
                _ => throw new ArgumentException("The PageName does not have a ViewModel.", nameof(pageName))
            };

            ResetDisplayFilter();
            LoadPageCommand(pageName);
        }

        private void LoadPageCommand(string pageName)
        {
            _ = Task.Run(() =>
            {
                switch (pageName)
                {
                    case "Interaksi Layanan & Kd. Perkiraan (Air/Non)":
                        Header1 = "Interaksi Layanan Kd.Perkiraan(Air / Non)";
                        Header2 = "Pengaturan data Interaksi Layanan & Kd. Perkiraan (Air/Non)";
                        IsFilterInLayanan = true;
                        _ = ((AsyncCommandBase)((InteraksiLayananViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null!);
                        break;
                    case "Inter. Jenis Persediaan Kode Perkiraan":
                        Header1 = "Interaksi Jenis Persediaan Kode Perkiraan";
                        Header2 = "Pengaturan data Interaksi Jenis Persediaan Kode Perkiraan";
                        IsFilterInPersediaan = true;
                        _ = ((AsyncCommandBase)((InteraksiJenisPersediaanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null!);

                        break;
                    case "Inter. Penyusutan & Kode Perkiraan Biaya":
                        Header1 = "Interaksi Penyusutan & Kode Perkiraan Biaya";
                        Header2 = "Pengaturan data Interaksi Penyusutan & Kode Perkiraan Biaya";
                        IsFilterInPenyusutan = true;
                        _ = ((AsyncCommandBase)((InteraksiPenyusutanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null!);

                        break;
                    default:
                        break;
                }
            });
        }

        private void ResetDisplayFilter()
        {
            IsFilterInLayanan = false;
            IsFilterInPersediaan = false;
            IsFilterInPenyusutan = false;

            #region Reset filter InPersediaan
            IsJenisChecked = false;
            IsKeperluanChecked = false;
            IsDebetChecked = false;
            IsKreditChecked = false;
            #endregion

            #region Reset filter InPenyusutan
            IsAkunAktivaChecked = false;
            IsAkunAkmPenyusutanChecked = false;
            IsAkunBiayaChecked = false;
            #endregion

        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraan3List = new();
        public ObservableCollection<MasterPerkiraan3Dto> Perkiraan3List
        {
            get { return _perkiraan3List; }
            set
            {
                _perkiraan3List = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterAkunEtapDto> _akunEtapList = new ObservableCollection<MasterAkunEtapDto>();
        public ObservableCollection<MasterAkunEtapDto> AkunEtapList
        {
            get { return _akunEtapList; }
            set
            {
                _akunEtapList = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _perkiraan3 = new();
        public MasterPerkiraan3Dto Perkiraan3
        {
            get { return _perkiraan3; }
            set
            {
                _perkiraan3 = value;
                OnPropertyChanged();
            }
        }

        private MasterAkunEtapDto _akunEtap = new();
        public MasterAkunEtapDto AkunEtap
        {
            get { return _akunEtap; }
            set
            {
                _akunEtap = value;
                OnPropertyChanged();
            }
        }

        #region InPelaynan Air / Non Air

        private ObservableCollection<MasterJenisNonAirDto> _jenisNonAirList = new();
        public ObservableCollection<MasterJenisNonAirDto> JenisNonAirList
        {
            get { return _jenisNonAirList; }
            set
            {
                _jenisNonAirList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraan3DebetList = new();
        public ObservableCollection<MasterPerkiraan3Dto> Perkiraan3DebetList
        {
            get { return _perkiraan3DebetList; }
            set
            {
                _perkiraan3DebetList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraan3KreditList = new();
        public ObservableCollection<MasterPerkiraan3Dto> Perkiraan3KreditList
        {
            get { return _perkiraan3KreditList; }
            set
            {
                _perkiraan3KreditList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraan3NonAirList = new();
        public ObservableCollection<MasterPerkiraan3Dto> Perkiraan3NonAirList
        {
            get { return _perkiraan3NonAirList; }
            set
            {
                _perkiraan3NonAirList = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region InJenisPersediaan
        private ObservableCollection<MasterJenisBarangDto> _jenisBarangList = new ObservableCollection<MasterJenisBarangDto>();
        public ObservableCollection<MasterJenisBarangDto> JenisBarangList
        {
            get { return _jenisBarangList; }
            set
            {
                _jenisBarangList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterKeperluanDto> _keperluanList = new();
        public ObservableCollection<MasterKeperluanDto> KeperluanList
        {
            get => _keperluanList;
            set
            {
                _keperluanList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraanDebet = new();
        public ObservableCollection<MasterPerkiraan3Dto> PerkiraanDebet
        {
            get => _perkiraanDebet;
            set
            {
                _perkiraanDebet = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _perkiraanKredit = new();
        public ObservableCollection<MasterPerkiraan3Dto> PerkiraanKredit
        {
            get => _perkiraanKredit;
            set
            {
                _perkiraanKredit = value;
                OnPropertyChanged();
            }
        }

        private bool _isJenisChecked;
        public bool IsJenisChecked
        {
            get => _isJenisChecked;
            set
            {
                _isJenisChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKeperluanChecked;
        public bool IsKeperluanChecked
        {
            get => _isKeperluanChecked;
            set
            {
                _isKeperluanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isDebetChecked;
        public bool IsDebetChecked
        {
            get => _isDebetChecked;
            set
            {
                _isDebetChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isKreditChecked;
        public bool IsKreditChecked
        {
            get => _isKreditChecked;
            set
            {
                _isKreditChecked = value;
                OnPropertyChanged();
            }
        }

        private MasterJenisBarangDto _filterJenis = new();
        public MasterJenisBarangDto FilterJenis
        {
            get => _filterJenis;
            set
            {
                _filterJenis = value;
                OnPropertyChanged();
            }
        }

        private MasterKeperluanDto _filterKeperluan = new();
        public MasterKeperluanDto FilterKeperluan
        {
            get => _filterKeperluan;
            set
            {
                _filterKeperluan = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterDebet = new();
        public MasterPerkiraan3Dto FilterDebet
        {
            get => _filterDebet;
            set
            {
                _filterDebet = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterKredit = new();
        public MasterPerkiraan3Dto FilterKredit
        {
            get => _filterKredit;
            set
            {
                _filterKredit = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region InPenyusutan
        private ObservableCollection<MasterPerkiraan3Dto> _akunAktivaList = new();
        public ObservableCollection<MasterPerkiraan3Dto> AkunAktivaList
        {
            get { return _akunAktivaList; }
            set
            {
                _akunAktivaList = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _akunAktiva = new();
        public MasterPerkiraan3Dto AkunAktiva
        {
            get { return _akunAktiva; }
            set
            {
                _akunAktiva = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _akunBiayaList = new();
        public ObservableCollection<MasterPerkiraan3Dto> AkunBiayaList
        {
            get { return _akunBiayaList; }
            set
            {
                _akunBiayaList = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _akunBiaya = new();
        public MasterPerkiraan3Dto AkunBiaya
        {
            get { return _akunBiaya; }
            set
            {
                _akunBiaya = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterPerkiraan3Dto> _akunAkumPenyusutanList = new();
        public ObservableCollection<MasterPerkiraan3Dto> AkunAkumPenyusutanList
        {
            get { return _akunAkumPenyusutanList; }
            set
            {
                _akunAkumPenyusutanList = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _akunAkumPenyusutan = new();
        public MasterPerkiraan3Dto AkunAkumPenyusutan
        {
            get { return _akunAkumPenyusutan; }
            set
            {
                _akunAkumPenyusutan = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private ObservableCollection<MasterPerkiraan1Dto> _perkiraan1List = new();
        public ObservableCollection<MasterPerkiraan1Dto> Perkiraan1List
        {
            get { return _perkiraan1List; }
            set
            {
                _perkiraan1List = value;
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

        private bool _isAkunAktivaChecked;
        public bool IsAkunAktivaChecked
        {
            get { return _isAkunAktivaChecked; }
            set
            {
                _isAkunAktivaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAkunAkmPenyusutanChecked;
        public bool IsAkunAkmPenyusutanChecked
        {
            get { return _isAkunAkmPenyusutanChecked; }
            set
            {
                _isAkunAkmPenyusutanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isAkunBiayaChecked;
        public bool IsAkunBiayaChecked
        {
            get { return _isAkunBiayaChecked; }
            set
            {
                _isAkunBiayaChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isWilayahChecked;
        public bool IsWilayahChecked
        {
            get { return _isWilayahChecked; }
            set
            {
                _isWilayahChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isJenisNonAirChecked;
        public bool IsJenisNonAirChecked
        {
            get { return _isJenisNonAirChecked; }
            set
            {
                _isJenisNonAirChecked = value;
                OnPropertyChanged();
            }
        }


        private bool _isGolonganChecked;
        public bool IsGolonganChecked
        {
            get { return _isGolonganChecked; }
            set
            {
                _isGolonganChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isPerkiraanDebetChecked;
        public bool IsPerkiraanDebetChecked
        {
            get { return _isPerkiraanDebetChecked; }
            set
            {
                _isPerkiraanDebetChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isPerkiraanKreditChecked;
        public bool IsPerkiraanKreditChecked
        {
            get { return _isPerkiraanKreditChecked; }
            set
            {
                _isPerkiraanKreditChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isPerkiraanChecked;
        public bool IsPerkiraanChecked
        {
            get { return _isPerkiraanChecked; }
            set
            {
                _isPerkiraanChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isFlagChecked;
        public bool IsFlagChecked
        {
            get { return _isFlagChecked; }
            set
            {
                _isFlagChecked = value;
                OnPropertyChanged();
            }
        }

        private bool _isPembentukRekairChecked;
        public bool IsPembentukRekairChecked
        {
            get { return _isPembentukRekairChecked; }
            set
            {
                _isPembentukRekairChecked = value;
                OnPropertyChanged();
            }
        }



        #region filter data list
        private ObservableCollection<MasterPerkiraan2Dto> _akunAktivaListSecond = new();
        public ObservableCollection<MasterPerkiraan2Dto> AkunAktivaListSecond
        {
            get { return _akunAktivaListSecond; }
            set
            {
                _akunAktivaListSecond = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan2Dto _filterAkunAktiva = new();
        public MasterPerkiraan2Dto FilterAkunAktiva
        {
            get => _filterAkunAktiva;
            set
            {
                _filterAkunAktiva = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterAkunAkmPenyusutan = new();
        public MasterPerkiraan3Dto FilterAkunAkmPenyusutan
        {
            get => _filterAkunAkmPenyusutan;
            set
            {
                _filterAkunAkmPenyusutan = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterAkunBiaya = new();
        public MasterPerkiraan3Dto FilterAkunBiaya
        {
            get => _filterAkunBiaya;
            set
            {
                _filterAkunBiaya = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterWilayahDto> _wilayahList = new();
        public ObservableCollection<MasterWilayahDto> WilayahList
        {
            get { return _wilayahList; }
            set
            {
                _wilayahList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> PembentukRekNonAirList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Jurnal Rekening Sambungan Baru", "jrsb"),
                    new KeyValuePair<string, string>("Jurnal Rekening Non Air", "jrna"),
                    new KeyValuePair<string, string>("Pendapatan Cicilan", "pdpcicil"),
                    new KeyValuePair<string, string>("Rencana Anggaran Biaya", "rab"),
                    new KeyValuePair<string, string>("Pendaftaran", "pendaftaran"),
                    new KeyValuePair<string, string>("Formulir", "formulir"),
                    new KeyValuePair<string, string>("Opname", "opname"),
                    new KeyValuePair<string, string>("Ganti Water Meter", "gantiwm"),
                    new KeyValuePair<string, string>("Tera Water Meter", "terawm"),
                    new KeyValuePair<string, string>("Ubah Golongan", "ubahgol"),
                    new KeyValuePair<string, string>("Denda", "denda"),
                    new KeyValuePair<string, string>("Denda Non Air", "dendana"),
                    new KeyValuePair<string, string>("Pelanggaran", "pelanggaran"),
                    new KeyValuePair<string, string>("Instalasi Limbah", "instlimbah"),
                    new KeyValuePair<string, string>("Sedot Tinja", "sedottinja"),
                    new KeyValuePair<string, string>("Lainnya Sambungan Rumah", "lainnyasr"),
                    new KeyValuePair<string, string>("Lainnya", "lainnya"),
                    new KeyValuePair<string, string>("Materai", "meterai"),
                    new KeyValuePair<string, string>("Tangki", "tangki"),
                    new KeyValuePair<string, string>("Sambungan Kembali", "sambkembali"),
                    new KeyValuePair<string, string>("Hapus Secara Akuntansi", "hpsakun"),
                    new KeyValuePair<string, string>("Perbaikan", "perbaikan"),
                    new KeyValuePair<string, string>("BBM Tangki", "bbmtangki"),

                };
                return data;
            }
        }

        public ObservableCollection<KeyValuePair<string, string>> PembentukRekAirList
        {
            get
            {
                var data = new ObservableCollection<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Rekening Air", "rekair"),
                    new KeyValuePair<string, string>("Biaya Pemakaian", "biayapemakaian"),
                    new KeyValuePair<string, string>("Administrasi", "administrasi"),
                    new KeyValuePair<string, string>("Administrasi Lain", "administrasilain"),
                    new KeyValuePair<string, string>("Pemeliharaan", "pemeliharaan"),
                    new KeyValuePair<string, string>("Pemeliharaan Lain", "pemeliharaanlain"),
                    new KeyValuePair<string, string>("Retribusi", "retribusi"),
                    new KeyValuePair<string, string>("Retribusi Lain", "retribusilain"),
                    new KeyValuePair<string, string>("Materai", "materai"),
                };
                return data;
            }
        }

        private KeyValuePair<string, string> _filterPembentukRekAir;
        public KeyValuePair<string, string> FilterPembentukRekAir
        {
            get => _filterPembentukRekAir;
            set
            {
                _filterPembentukRekAir = value;
                OnPropertyChanged();
            }
        }


        private KeyValuePair<string, string> _filterPembentukRekNonAir;
        public KeyValuePair<string, string> FilterPembentukRekNonAir
        {
            get => _filterPembentukRekNonAir;
            set
            {
                _filterPembentukRekNonAir = value;
                OnPropertyChanged();
            }
        }


        private MasterWilayahDto _filterWilayah = new MasterWilayahDto();
        public MasterWilayahDto FilterWilayah
        {
            get { return _filterWilayah; }
            set
            {
                _filterWilayah = value;
                OnPropertyChanged();
            }
        }
        private MasterJenisNonAirDto _filterJenisNonAir = new MasterJenisNonAirDto();
        public MasterJenisNonAirDto FilterJenisNonAir
        {
            get { return _filterJenisNonAir; }
            set
            {
                _filterJenisNonAir = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MasterGolonganDto> _golonganList = new();
        public ObservableCollection<MasterGolonganDto> GolonganList
        {
            get { return _golonganList; }
            set
            {
                _golonganList = value;
                OnPropertyChanged();
            }
        }

        private MasterGolonganDto _filterGolongan = new();
        public MasterGolonganDto FilterGolongan
        {
            get { return _filterGolongan; }
            set
            {
                _filterGolongan = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterPerkiraan3Debet = new();
        public MasterPerkiraan3Dto FilterPerkiraan3Debet
        {
            get { return _filterPerkiraan3Debet; }
            set
            {
                _filterPerkiraan3Debet = value;
                OnPropertyChanged();
            }
        }

        private MasterPerkiraan3Dto _filterPerkiraan3Kredit = new();
        public MasterPerkiraan3Dto FilterPerkiraan3Kredit
        {
            get { return _filterPerkiraan3Kredit; }
            set
            {
                _filterPerkiraan3Kredit = value;
                OnPropertyChanged();
            }
        }
        private MasterPerkiraan3Dto _filterPerkiraan3 = new();
        public MasterPerkiraan3Dto FilterPerkiraan3
        {
            get { return _filterPerkiraan3; }
            set
            {
                _filterPerkiraan3 = value;
                OnPropertyChanged();
            }
        }

        private bool? _flagPembentukRekair;
        public bool? FlagPembentukRekair
        {
            get { return _flagPembentukRekair; }
            set
            {
                _flagPembentukRekair = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private ParamMasterInPelayananFilterDto _interaksiLayananFilter = new();
        public ParamMasterInPelayananFilterDto InteraksiLayananFilter
        {
            get { return _interaksiLayananFilter; }
            set
            {
                _interaksiLayananFilter = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingForm;
        public bool IsLoadingForm
        {
            get { return _isLoadingForm; }
            set
            {
                _isLoadingForm = value;
                OnPropertyChanged();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged();
            }
        }
    }

}
