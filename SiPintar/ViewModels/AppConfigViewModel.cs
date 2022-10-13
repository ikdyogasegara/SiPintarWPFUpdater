using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.Commands.AppConfig;
using SiPintar.Utilities;

namespace SiPintar.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AppConfigViewModel : ViewModelBase
    {
        public AppConfigViewModel(IRestApiClientModel restApi)
        {
            OnSaveCommand = new OnSaveCommand(this, restApi);
            OnLoadConfigCommand = new OnLoadConfigCommand(this);
            OnBrowseReportPathCommand = new OnBrowseReportPathCommand(this);
            OnCekKoneksiCloudCommand = new OnCekKoneksiCloudCommand(this);
            OnCekKoneksiLocalCommand = new OnCekKoneksiLocalCommand(this);
            OnCekKoneksiCloudReportCommand = new OnCekKoneksiCloudCommand(this, true);
            OnCekKoneksiLocalReportCommand = new OnCekKoneksiLocalCommand(this, true);
        }

        public ICommand OnSaveCommand { get; }
        public ICommand OnLoadConfigCommand { get; }
        public ICommand OnBrowseReportPathCommand { get; }
        public ICommand OnCekKoneksiCloudCommand { get; }
        public ICommand OnCekKoneksiLocalCommand { get; }
        public ICommand OnCekKoneksiCloudReportCommand { get; }
        public ICommand OnCekKoneksiLocalReportCommand { get; }


        public async Task SetInitConfigAsync() => await ((AsyncCommandBase)OnLoadConfigCommand).ExecuteAsync(null);

        private object _menuSetting;
        public object MenuSetting
        {
            get { return _menuSetting; }
            set
            {
                _menuSetting = value;
                OnPropertyChanged();
            }
        }

        private string _appVersion = string.Empty;
        public string AppVersion
        {
            get => _appVersion;
            set { _appVersion = value; OnPropertyChanged(); }
        }

        private bool _checkReportSameSetting;
        public bool CheckReportSameSetting
        {
            get => _checkReportSameSetting;
            set { _checkReportSameSetting = value; OnPropertyChanged(); }
        }

        private string _apiCloudUrl = string.Empty;
        public string ApiCloudUrl
        {
            get => _apiCloudUrl;
            set { _apiCloudUrl = value; OnPropertyChanged(); }
        }

        private string _apiLocalUrl = string.Empty;
        public string ApiLocalUrl
        {
            get => _apiLocalUrl;
            set { _apiLocalUrl = value; OnPropertyChanged(); }
        }

        private string _apiCloudReportUrl = string.Empty;
        public string ApiCloudReportUrl
        {
            get => _apiCloudReportUrl;
            set { _apiCloudReportUrl = value; OnPropertyChanged(); }
        }

        private string _apiLocalReportUrl = string.Empty;
        public string ApiLocalReportUrl
        {
            get => _apiLocalReportUrl;
            set { _apiLocalReportUrl = value; OnPropertyChanged(); }
        }

        private string _apiLoketReport = string.Empty;
        public string ApiLoketReport
        {
            get => _apiLoketReport;
            set { _apiLoketReport = value; OnPropertyChanged(); }
        }

        private string _apiBillingReport = string.Empty;
        public string ApiBillingReport
        {
            get => _apiBillingReport;
            set { _apiBillingReport = value; OnPropertyChanged(); }
        }

        private string _apiBacameterReport = string.Empty;
        public string ApiBacameterReport
        {
            get => _apiBacameterReport;
            set { _apiBacameterReport = value; OnPropertyChanged(); }
        }

        private string _apiHublangReport = string.Empty;
        public string ApiHublangReport
        {
            get => _apiHublangReport;
            set { _apiHublangReport = value; OnPropertyChanged(); }
        }

        private string _apiPerencanaanReport = string.Empty;
        public string ApiPerencanaanReport
        {
            get => _apiPerencanaanReport;
            set { _apiPerencanaanReport = value; OnPropertyChanged(); }
        }

        private string _apiDistribusiReport = string.Empty;
        public string ApiDistribusiReport
        {
            get => _apiDistribusiReport;
            set { _apiDistribusiReport = value; OnPropertyChanged(); }
        }

        private string _apiAkuntansiKeuanganReport = string.Empty;
        public string ApiAkuntansiKeuanganReport
        {
            get => _apiAkuntansiKeuanganReport;
            set { _apiAkuntansiKeuanganReport = value; OnPropertyChanged(); }
        }

        private string _apiLoket = string.Empty;
        public string ApiLoket
        {
            get => _apiLoket;
            set { _apiLoket = value; OnPropertyChanged(); }
        }

        private string _apiBilling = string.Empty;
        public string ApiBilling
        {
            get => _apiBilling;
            set { _apiBilling = value; OnPropertyChanged(); }
        }

        private string _apiBacameter = string.Empty;
        public string ApiBacameter
        {
            get => _apiBacameter;
            set { _apiBacameter = value; OnPropertyChanged(); }
        }

        private string _apiHublang = string.Empty;
        public string ApiHublang
        {
            get => _apiHublang;
            set { _apiHublang = value; OnPropertyChanged(); }
        }

        private string _apiPerencanaan = string.Empty;
        public string ApiPerencanaan
        {
            get => _apiPerencanaan;
            set { _apiPerencanaan = value; OnPropertyChanged(); }
        }

        private string _apiDistribusi = string.Empty;
        public string ApiDistribusi
        {
            get => _apiDistribusi;
            set { _apiDistribusi = value; OnPropertyChanged(); }
        }

        private string _apiAkuntansiKeuangan = string.Empty;
        public string ApiAkuntansiKeuangan
        {
            get => _apiAkuntansiKeuangan;
            set { _apiAkuntansiKeuangan = value; OnPropertyChanged(); }
        }

        private string _reportAppPath = string.Empty;
        public string ReportAppPath
        {
            get => _reportAppPath;
            set { _reportAppPath = value; OnPropertyChanged(); }
        }
    }
}
