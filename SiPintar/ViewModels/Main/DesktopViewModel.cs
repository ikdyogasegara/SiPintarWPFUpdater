using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SiPintar.Commands.Main.Desktop;

namespace SiPintar.ViewModels.Main
{
    public class DesktopViewModel : ViewModelBase
    {
        private readonly MainViewModel _parent;
        public ICommand OnLoadCommand { get; }
        public ICommand OnOpenDesktopAppCommand { get; }
        public ICommand OnRetryErrorCommand { get => _parent.OnLoadCommand; }

        public DesktopViewModel(MainViewModel parent)
        {
            _parent = parent;
            OnLoadCommand = new OnLoadCommand(this);
            OnOpenDesktopAppCommand = new OnOpenDesktopAppCommand(parent);
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

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set
            {
                _isError = value;
                OnPropertyChanged();
            }
        }


        private List<string> _moduleAkses = new List<string>();
        public List<string> ModuleAkses
        {
            get => _moduleAkses;
            set
            {
                _moduleAkses = value;
                OnPropertyChanged();
            }
        }

        private List<List<object>> _listModule;
        public List<List<object>> ListModule
        {
            get => _listModule;
            set
            {
                _listModule = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
        public List<object> GetModuleItems()
        {
            var IdLoket = Application.Current.Resources["IdModuleLoket"]?.ToString();
            var IdBilling = Application.Current.Resources["IdModuleBilling"]?.ToString();
            var IdBacameter = Application.Current.Resources["IdModuleBacameter"]?.ToString();
            var IdHublang = Application.Current.Resources["IdModuleHublang"]?.ToString();
            var IdPerencanaan = Application.Current.Resources["IdModulePerencanaan"]?.ToString();
            var IdDistribusi = Application.Current.Resources["IdModuleDistribusi"]?.ToString();
            var IdGudang = Application.Current.Resources["IdModuleGudang"]?.ToString();
            var IdPersonalia = Application.Current.Resources["IdModulePersonalia"]?.ToString();
            var IdModuleAkuntansiKeuangan = Application.Current.Resources["IdModuleAkuntansikeuangan"]?.ToString();

            var list = new List<object>();

            if (ModuleAkses.Contains(IdLoket))
            {
                list.Add(new
                {
                    Key = "loket",
                    Title = "Loket",
                    Logo = "/SiPintar;component/Assets/Images/Application/loket_logo_3x.png",
                    Description = "Aplikasi pembayaran tagihan pelanggan air, non air & angsuran.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdBilling))
            {
                list.Add(new
                {
                    Key = "billing",
                    Title = "Billing",
                    Logo = "/SiPintar;component/Assets/Images/Application/billing_logo_3x.png",
                    Description = "Mempermudah pengolahan rekening hingga pencatatan laporan jadi lebih efektif.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdBacameter))
            {
                list.Add(new
                {
                    Key = "bacameter",
                    Title = "Bacameter",
                    Logo = "/SiPintar;component/Assets/Images/Application/bacameter_logo_3x.png",
                    Description = "Sistem untuk mengolah dan monitoring data bacameter.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdHublang))
            {
                list.Add(new
                {
                    Key = "hublang",
                    Title = "Hubungan Pelanggan",
                    Logo = "/SiPintar;component/Assets/Images/Application/hublang_logo_3x.png",
                    Description = "Membantu proses pendaftaran sambungan & pengaduan layanan menjadi lebih mudah.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdPerencanaan))
            {
                list.Add(new
                {
                    Key = "perencanaan",
                    Title = "Perencanaan",
                    Logo = "/SiPintar;component/Assets/Images/Application/perencanaan_logo_3x.png",
                    Description = "Pengelolaan RAB dan SPKO lebih efisien dengan modul Perencanaan.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdDistribusi))
            {
                list.Add(new
                {
                    Key = "distribusi",
                    Title = "Distribusi",
                    Logo = "/SiPintar;component/Assets/Images/Application/distribusi_logo_3x.png",
                    Description = "Sistem terintegrasi yang digunakan untuk pekerjaan lapangan.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdGudang))
            {
                list.Add(new
                {
                    Key = "gudang",
                    Title = "Gudang",
                    Logo = "/SiPintar;component/Assets/Images/Application/gudang_logo_3x.png",
                    Description = "Sistem andalan untuk  manajemen inventori, keluar/masuk barang, dan proses administratif lainnya semua aktifitas di Gudang PDAM.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdPersonalia))
            {
                list.Add(new
                {
                    Key = "personalia",
                    Title = "Personalia",
                    Logo = "/SiPintar;component/Assets/Images/Application/personalia_logo_3x.png",
                    Description = "Sistem andalan untuk pengelolaan data karyawan, perhitungan gaji, hingga pelaporan kinerja SDM PDAM.",
                    Version = "0.1.0"
                });
            }

            if (ModuleAkses.Contains(IdModuleAkuntansiKeuangan))
            {
                list.Add(new
                {
                    Key = "akuntansi",
                    Title = "Akuntansi & Keuangan",
                    Logo = "/SiPintar;component/Assets/Images/Application/akuntansi_logo_3x.png",
                    Description = "Efisiensi analisis Neraca Perusahaan, Laporan Laba Rugi dan Laporan Arus Kas di PDAM dengan Modul Akuntansi.",
                    Version = "0.1.0"
                });
            }

            list.Add(new
            {
                Key = "laporan",
                Title = "Laporan",
                Logo = "/SiPintar;component/Assets/Images/Application/report_logo_3x.png",
                Description = "Aplikasi reporting manajemen data yang ada di aplikasi PDAM Pintar.",
                Version = "0.1.0"
            });

            return list;
        }
    }
}
