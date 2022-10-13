using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SiPintar.Commands;
using SiPintar.Utilities;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.ViewModels.Hublang
{
    public class PelayananViewModel : ViewModelBase
    {
        private readonly InfoViewModel _info;
        private readonly PendaftaranViewModel _pendaftaran;
        private readonly LanggananViewModel _langganan;
        private readonly PermohonanViewModel _permohonan;
        private readonly TagihanManualViewModel _tagihanManual;

        private readonly BaPengembalianViewModel _bAPengembalian;
        private readonly SimulasiTarifViewModel _simulasiTarifViewModel;
        private readonly KoreksiRekeningAirViewModel _koreksi;

        public PelayananViewModel(IRestApiClientModel restApi, bool isTest = false)
        {
            _info = new InfoViewModel(this, restApi, isTest);
            _pendaftaran = new PendaftaranViewModel(restApi, isTest);
            _langganan = new LanggananViewModel(restApi, isTest);
            _permohonan = new PermohonanViewModel(this, restApi, isTest);
            _tagihanManual = new TagihanManualViewModel(restApi, isTest);
            _bAPengembalian = new BaPengembalianViewModel(this, restApi, isTest);
            _simulasiTarifViewModel = new SimulasiTarifViewModel(this, restApi, isTest);
            _koreksi = new KoreksiRekeningAirViewModel(restApi);
        }

        private ViewModelBase _pageViewModel;

        public ViewModelBase PageViewModel
        {
            get { return _pageViewModel; }
            set
            {
                _pageViewModel = value;
                OnPropertyChanged();
            }
        }

        private List<HorizontalNavigationItem> _navigationItems;

        public List<HorizontalNavigationItem> NavigationItems
        {
            get { return _navigationItems; }
            set
            {
                _navigationItems = value;
                OnPropertyChanged();
            }
        }

        public void UpdatePage(string pageName)
        {
            PageViewModel = pageName switch
            {
                "Pendaftaran Samb. Air" => _pendaftaran,
                "Pendaftaran Samb. Limbah" => _pendaftaran,
                "Pendaftaran Samb. L2T2" => _pendaftaran,

                "Pelanggan Pelanggan Air" => _langganan,
                "Pelanggan Pelanggan Limbah" => _langganan,
                "Pelanggan Pelanggan L2T2" => _langganan,

                "Info" => _info,

                "Permohonan Pelanggan Air" => _permohonan,
                "Permohonan Pelanggan Limbah" => _permohonan,
                "Permohonan Pelanggan L2T2" => _permohonan,
                "Permohonan Non Pelanggan" => _permohonan,

                "Pengaduan Pelanggan Air" => _permohonan,
                "Pengaduan Pelanggan Limbah" => _permohonan,
                "Pengaduan Pelanggan L2T2" => _permohonan,
                "Pengaduan Non Pelanggan" => _permohonan,

                "Tagihan Manual" => _tagihanManual,
                "Berita Acara Pelanggan Air" => _permohonan,
                "Berita Acara Pelanggan Limbah" => _permohonan,
                "Berita Acara Pelanggan L2T2" => _permohonan,
                "Berita Acara Non Pelanggan" => _permohonan,
                "Berita Acara Pengembalian" => _bAPengembalian,
                "Simulasi Tarif" => _simulasiTarifViewModel,
                "Koreksi Rekening Air Permohonan" => _koreksi,
                "Koreksi Rekening Air Usulan Koreksi" => _koreksi,

                _ => null
            };

            _ = LoadPageCommandAsync(pageName);
        }

        public List<HorizontalNavigationItem> GetNavigationItems()
        {
            var nav = new List<HorizontalNavigationItem>
            {
                new HorizontalNavigationItem() {Label = "Pengaduan", IsSelected = true, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>() {new HorizontalNavigationItem() {Label = "Pelanggan Air", IsSelected = true}, new HorizontalNavigationItem() {Label = "Pelanggan Limbah"}, new HorizontalNavigationItem() {Label = "Pelanggan L2T2"}, new HorizontalNavigationItem() {Label = "Non Pelanggan"}}},
                new HorizontalNavigationItem() {Label = "Permohonan", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>() {new HorizontalNavigationItem() {Label = "Pelanggan Air"}, new HorizontalNavigationItem() {Label = "Pelanggan Limbah"}, new HorizontalNavigationItem() {Label = "Pelanggan L2T2"}, new HorizontalNavigationItem() {Label = "Non Pelanggan"}}},
                new HorizontalNavigationItem() {Label = "Pendaftaran", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                {
                    new HorizontalNavigationItem() {Label = "Samb. Air"},
                    new HorizontalNavigationItem() {Label = "Samb. Limbah"},
                    new HorizontalNavigationItem() {Label = "Samb. L2T2"},
                }},
                new HorizontalNavigationItem() {Label = "Pelanggan", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>() {new HorizontalNavigationItem() {Label = "Pelanggan Air"}, new HorizontalNavigationItem() {Label = "Pelanggan Limbah"}, new HorizontalNavigationItem() {Label = "Pelanggan L2T2"}}},
                new HorizontalNavigationItem() {Label = "Koreksi Rekening Air", IsSelected = false, ChildNavigation = new ObservableCollection<HorizontalNavigationItem>() {new HorizontalNavigationItem() {Label = "Permohonan"}, new HorizontalNavigationItem() {Label = "Usulan Koreksi"}}},

                new HorizontalNavigationItem()
                {
                    Label = "Berita Acara",
                    IsSelected = false,
                    ChildNavigation = new ObservableCollection<HorizontalNavigationItem>()
                    {
                        new HorizontalNavigationItem() {Label = "Pelanggan Air"},
                        new HorizontalNavigationItem() {Label = "Pelanggan Limbah",},
                        new HorizontalNavigationItem() {Label = "Pelanggan L2T2"},
                        new HorizontalNavigationItem() {Label = "Pengembalian"},
                        new HorizontalNavigationItem() {Label = "Non Pelanggan"},
                    }
                },
                new HorizontalNavigationItem() {Label = "Tagihan Manual", IsSelected = false},
                new HorizontalNavigationItem() {Label = "Simulasi Tarif", IsSelected = false},
                new HorizontalNavigationItem() {Label = "Info", IsSelected = false},

            };

            return nav;
        }

        private Task LoadPageCommandAsync(string pageName)
        {
            switch (pageName)
            {
                case "Permohonan Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Permohonan" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Permohonan Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Permohonan" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Permohonan Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Permohonan" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Permohonan Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Permohonan" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;

                case "Pengaduan Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Pengaduan" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Pengaduan Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Pengaduan" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Pengaduan Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Pengaduan" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Pengaduan Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Pengaduan" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;

                case "Pendaftaran Samb. Air":
                    _ = ((AsyncCommandBase)((PendaftaranViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("SAMBUNGAN_BARU_AIR");
                    break;
                case "Pendaftaran Samb. Limbah":
                    _ = ((AsyncCommandBase)((PendaftaranViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("SAMBUNGAN_BARU_LIMBAH");
                    break;
                case "Pendaftaran Samb. L2T2":
                    _ = ((AsyncCommandBase)((PendaftaranViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("SAMBUNGAN_BARU_LLTT");
                    break;
                case "Info":
                    ((InfoViewModel)PageViewModel).NavigationItems = ((InfoViewModel)PageViewModel).GetNavigationItems();
                    break;
                case "Pelanggan Pelanggan Air":
                    _ = ((AsyncCommandBase)((LanggananViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Pelanggan Air");
                    break;
                case "Pelanggan Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((LanggananViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Pelanggan Limbah");
                    break;
                case "Pelanggan Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((LanggananViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Pelanggan L2T2");
                    break;

                case "Berita Acara Pelanggan Air":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara View Only" }, { "SelectedJenisPelanggan", "Pelanggan Air" } });
                    break;
                case "Berita Acara Pelanggan Limbah":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara View Only" }, { "SelectedJenisPelanggan", "Pelanggan Limbah" } });
                    break;
                case "Berita Acara Pelanggan L2T2":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara View Only" }, { "SelectedJenisPelanggan", "Pelanggan L2T2" } });
                    break;
                case "Berita Acara Non Pelanggan":
                    _ = ((AsyncCommandBase)((PermohonanViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(new Dictionary<string, dynamic> { { "FiturName", "Berita Acara View Only" }, { "SelectedJenisPelanggan", "Non Pelanggan" } });
                    break;

                case "Berita Acara Pengembalian":
                    _ = ((AsyncCommandBase)((BaPengembalianViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                    break;
                case "Tagihan Manual":
                    _ = ((AsyncCommandBase)((TagihanManualViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                    break;
                case "Simulasi Tarif":
                    _ = ((AsyncCommandBase)((SimulasiTarifViewModel)PageViewModel).OnLoadCommand).ExecuteAsync(null);
                    break;
                case "Koreksi Rekening Air Permohonan":
                    _ = ((AsyncCommandBase)((KoreksiRekeningAirViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Permohonan");
                    break;
                case "Koreksi Rekening Air Usulan Koreksi":
                    ((KoreksiRekeningAirViewModel)PageViewModel).IsBilling = false;
                    _ = ((AsyncCommandBase)((KoreksiRekeningAirViewModel)PageViewModel).OnLoadCommand).ExecuteAsync("Usulan");
                    break;

                default:
                    _ = pageName;
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
