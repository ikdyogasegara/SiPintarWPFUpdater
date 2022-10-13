using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiPintar.DependencyInjection;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels;
using SiPintar.Views;
using SiPintar.Views.Global.Other;

namespace SiPintar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class App : Application, IDisposable
    {
        private readonly string _configPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\config.env";
        private readonly string _folderPath = Path.Combine(Path.GetTempPath(), "PDAM-SiPintar-Net");

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        public static Dictionary<string, string> resources { get; set; }
        public static BackgroundProcessHelper BgProcessList { get; set; }

        private readonly IHost _host;

        private Window _loginWindow;
        private Window _mainWindow;
        private Window _appSettingWindow;

        private static ISplashScreen s_splashScreen;
        private ManualResetEvent _resetSplashCreated;
        private Thread _splashThread;
        private readonly LoadSettingService _loadSettingService = new LoadSettingService();

        public App()
        {
            _host = CreateHostBuilder().Build();
            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        private static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddViewModels()
                        .AddSerilogConfiguration();
                    // .AddHostedService<RabbitMqConsumerExtension>();
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _ = InitiateWindow(e);
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        private async Task InitiateWindow(StartupEventArgs e)
        {
            StartSplash();
            s_splashScreen.AddMessage("Memuat konfigurasi...");

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_CurrentDomain_UnhandledException);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(AppDomain_CurrentDomain_ProcessExit);

            base.OnStartup(e);
            _host.Start();

            CheckConfigEnv();

            s_splashScreen.AddMessage("Pembersihan data sementara...");

            if (Directory.Exists(_folderPath))
                Directory.Delete(_folderPath, true);

            #region download attribute list

            await LiteDBExtension.SetToAppConfigAsync("IdPdam", DotNetEnv.Env.GetString("ID_PDAM") ?? "0");

            var statusToken = await _loadSettingService.GetSpecialTokenAsync();

            if (statusToken == false)
            {
                MessageBox.Show("Aplikasi tidak berhasil mendapatkan token !");
            }

            await _loadSettingService.GetPengaturanAsync();

            await _loadSettingService.GetPdamAsync();

            var listDownloadAttribute = new List<string>()
            {
                "MasterGolongan",
                "MasterDiameter",
                "MasterRayon",
                "MasterArea",
                "MasterWilayah",
                "MasterKelurahan",
                "MasterKecamatan",
                "MasterCabang",
                "MasterUser",
                "MasterTipePermohonan",
                "MasterStatus",
                "MasterKolektif",
                "MasterFlag",
                "MasterKondisiMeter",
                "MasterAdministrasiLain",
                "MasterPemeliharaanLain",
                "MasterRetribusiLain",
                "MasterMeterai",
                "MasterBlok",
                "MasterMerekMeter",
                "MasterLoket",
                "MasterKelainan",
                "MasterPetugasBaca",
                "MasterPeriode",
                "MasterPekerjaan",
                "MasterTarifLimbah",
                "MasterTarifLltt",
                "MasterSumberAir",
                "MasterJenisBangunan",
                "MasterKepemilikan",
                "MasterPeruntukan",
                "MasterDma",
                "MasterDmz",
                "MasterJenisNonAir",
                "MasterTipePendaftaranSambungan",
                "MasterAlasanBatal",
                "MasterPegawai",
                "MasterMaterial",
                "MasterBagianMemintaBarang",
                "MasterBarang",
                "MasterPaketRab",
                "MasterJenisGantiMeter",
                "MasterTarifTangki",
                "MasterPeriodeAkuntansi",
                "MasterPerkiraan1",
                "MasterPerkiraan2",
                "MasterPerkiraan3",
                "MasterJenisBarang",
                "MasterKeperluan",
                "MasterParameterAkuntansi",
                "MasterPeriodeAkuntansi",
                "MasterPeriodeAkuntansiTriwulan",
                "MasterAkunEtap",
                "MasterPenyusutanAktiva",
                "MasterHarianKas",
                "AnggaranLabaRugiMaster",
                "PerputaranUangMaster",
                "MasterJenisVoucher",
                "NeracaMaster",
                "ArusKasMaster",
                "EkuitasMaster",
                "ReportFilterCustom",
            };

            listDownloadAttribute = new();

            decimal no = 0;

            listDownloadAttribute = new();

            foreach (var i in listDownloadAttribute)
            {
                no = no + 1;
                s_splashScreen.AddMessage($"Download {Convert.ToInt32((decimal)no / (decimal)listDownloadAttribute.Count * 100)} % ({i})");
                await UpdateListData.ProcessAsync(false, new List<string>() { i });
            }

            #endregion

            s_splashScreen.AddMessage("Memuat komponen...");

            _mainWindow = _host.Services.GetRequiredService<MainView>();
            _loginWindow = _host.Services.GetRequiredService<LoginView>();
            _appSettingWindow = _host.Services.GetRequiredService<AppConfigView>();

            s_splashScreen.AddMessage("Menyelesaikan. silakan tunggu...");

            _ = LiteDBExtension.SetupAppAsync();
            BgProcessList = new BackgroundProcessHelper();

            ShowLoginWindow();

            s_splashScreen.LoadComplete();

            await Task.FromResult(true);
        }

        public void ShowLoginWindow()
        {
            Current.MainWindow = _loginWindow;
            _loginWindow.Topmost = true;
            _loginWindow.Show();
            _loginWindow.Activate();
            _loginWindow.Topmost = false;
            _mainWindow.Hide();

            ((LoginViewModel)_loginWindow.DataContext).UpdateState();

            if (!OpenedWindow.ContainsKey("login"))
                OpenedWindow.Add("login", _loginWindow);

            // close child modules
            foreach (var item in OpenedWindow)
            {
                if (item.Key != "login" && item.Key != "main")
                    item.Value.Close();
            }
        }

        public void ShowMainWindow()
        {
            ((MainViewModel)_mainWindow.DataContext).NavigationItems = ((MainViewModel)_mainWindow.DataContext).GetNavigationItems();

            Current.MainWindow = _mainWindow;
            _mainWindow.Topmost = true;
            _mainWindow.Show();
            _mainWindow.Activate();
            _mainWindow.Topmost = false;
            _loginWindow.Hide();
            ((LoginView)_loginWindow).ResetForm();

            ((MainViewModel)_mainWindow.DataContext).UpdateState();

            if (!OpenedWindow.ContainsKey("main"))
                OpenedWindow.Add("main", _mainWindow);
        }

        private void StartSplash()
        {
            _resetSplashCreated = new ManualResetEvent(false);

            _splashThread = new Thread(ShowSplash);
            _splashThread.SetApartmentState(ApartmentState.STA);
            _splashThread.IsBackground = true;
            _splashThread.Name = "Splash Screen";
            _splashThread.Start();

            _resetSplashCreated.WaitOne();
        }

        private void ShowSplash()
        {
            var animatedSplashScreenWindow = new SplashScreenView();
            s_splashScreen = animatedSplashScreenWindow;

            animatedSplashScreenWindow.Show();

            _resetSplashCreated.Set();
            System.Windows.Threading.Dispatcher.Run();
        }

        private static void AppDomain_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Aplikasi mengalami masalah, silahkan periksa log aplikasi atau hubungi Tim Teknis terkait!");
            Serilog.Log.Logger.Error(e.ExceptionObject.ToString());
        }

        private void AppDomain_CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            var lifetime = _host.Services.GetService<IHostLifetime>() as IDisposable;
            lifetime?.Dispose();
        }

        public void DisplayAppSettingWindow(object dataContext)
        {
            ((AppConfigView)_appSettingWindow).ShowHandlerDialog(dataContext);
        }

        public static Dictionary<string, dynamic> OpenedWindow = new Dictionary<string, dynamic>();

        private void CheckConfigEnv()
        {
            var data = DotNetEnv.Env.Load(_configPath);

            if (!data.Any())
            {
                MessageBox.Show("Aplikasi mengalami masalah, pastikan terdapat file config.env se-folder dengan Sipintar.exe!");
                Current.Shutdown();
            }
        }

        public static void AddToBackgroundProcess(BackgroundProcessHelper.ProcessObject data)
        {
            BgProcessList.AddToQueue(data);
        }

        public static void ClearAllBackgroundProcess()
        {
            BgProcessList.ClearAllQueue();
        }

        public static List<BackgroundProcessHelper.ProcessObject> GetBackgroundProcessList()
        {
            return BgProcessList.QueueList;
        }

        public static void BackgroundProcessRun(IRestApiClientModel restApi)
        {
            _ = BgProcessList.RunAsync(restApi);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
