using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using LiveCharts;
using LiveCharts.Helpers;
using SiPintar.Commands.Billing.Produktivitas.Pelanggan;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Billing.Produktivitas
{
    [ExcludeFromCodeCoverage]
    public class PelangganViewModel : ViewModelBase
    {
        public PelangganViewModel(ProduktivitasViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }


        private ProduktivitasViewModel _parent { get; set; }
        public ProduktivitasViewModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ObservableCollection<SummaryProduktifitasBacameterPelangganDto> produktivitasPelanggan { get; set; }
        public ObservableCollection<SummaryProduktifitasBacameterPelangganDto> ProduktivitasPelanggan
        {
            get { return produktivitasPelanggan; }
            set
            {
                produktivitasPelanggan = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private string[] labels { get; set; }
        public string[] Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                OnPropertyChanged("Labels");
            }
        }
        public Func<double, string> Formatter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> jumlahPelangganSeries { get; set; }
        public ChartValues<int> JumlahPelangganSeries
        {
            get { return jumlahPelangganSeries; }
            set
            {
                jumlahPelangganSeries = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> terbacaSeries { get; set; }
        public ChartValues<int> TerbacaSeries
        {
            get { return terbacaSeries; }
            set
            {
                terbacaSeries = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> fotoMeterSeries { get; set; }
        public ChartValues<int> FotoMeterSeries
        {
            get { return fotoMeterSeries; }
            set
            {
                fotoMeterSeries = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> fotoRumahSeries { get; set; }
        public ChartValues<int> FotoRumahSeries
        {
            get { return fotoRumahSeries; }
            set
            {
                fotoRumahSeries = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> videoSeries { get; set; }
        public ChartValues<int> VideoSeries
        {
            get { return videoSeries; }
            set
            {
                videoSeries = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> gpsSeries { get; set; }
        public ChartValues<int> GpsSeries
        {
            get { return gpsSeries; }
            set
            {
                gpsSeries = value;
                OnPropertyChanged();
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                OnPropertyChanged();
                OnPropertyChanged("IsNotLoading");
            }
        }
        public bool IsNotLoading { get { return !IsLoading; } }
        private Visibility chartVisibility;
        public Visibility ChartVisibility
        {
            get { return chartVisibility; }
            set
            {
                chartVisibility = value;
                OnPropertyChanged();
                OnPropertyChanged("AlertVisibility");
            }
        }

        private bool isDataEmpty;
        public bool IsDataEmpty
        {
            get { return isDataEmpty; }
            set
            {
                isDataEmpty = value;
                OnPropertyChanged();
                OnPropertyChanged("IsDataNotEmpty");
            }
        }

        public bool IsDataNotEmpty
        {
            get { return !IsDataEmpty; }
        }

        public Visibility AlertVisibility
        {
            get { return ChartVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible; }
        }
        private bool jumlahPelangganSeriesVisibility;
        private bool terbacaSeriesVisibility;
        private bool fotoMeterSeriesVisibility;
        private bool fotoRumahSeriesVisibility;
        private bool videoSeriesVisibility;
        private bool gpsSeriesVisibility;
        public bool JumlahPelangganSeriesVisibility
        {
            get { return jumlahPelangganSeriesVisibility; }
            set
            {
                jumlahPelangganSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool TerbacaSeriesVisibility
        {
            get { return terbacaSeriesVisibility; }
            set
            {
                terbacaSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool FotoMeterSeriesVisibility
        {
            get { return fotoMeterSeriesVisibility; }
            set
            {
                fotoMeterSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool FotoRumahSeriesVisibility
        {
            get { return fotoRumahSeriesVisibility; }
            set
            {
                fotoRumahSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool VideoSeriesVisibility
        {
            get { return videoSeriesVisibility; }
            set
            {
                videoSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool GpsSeriesVisibility
        {
            get { return gpsSeriesVisibility; }
            set
            {
                gpsSeriesVisibility = value;
                OnPropertyChanged();
            }
        }

        private string plot { get; set; }
        public string Plot
        {
            get { return plot; }
            set
            {
                plot = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentPlotImage");
                OnPropertyChanged("CurrentPlot");

            }
        }
        public string CurrentPlot
        {
            get { return $"Grafik {Plot}"; }
        }
        public string CurrentPlotImage
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Plot))
                {
                    return $"/SiPintar;component/Assets/Images/Chart/ic_{Plot.ToLower()}_chart.png";
                }
                else
                {
                    return $"/SiPintar;component/Assets/Images/Chart/ic_kolom_chart.png";
                }
            }
        }

        private ZoomingOptions _zoomingModeX;
        public ZoomingOptions ZoomingModeX
        {
            get { return _zoomingModeX; }
            set
            {
                _zoomingModeX = value;
                OnPropertyChanged();
            }
        }
        private ZoomingOptions _zoomingModeY;
        public ZoomingOptions ZoomingModeY
        {
            get { return _zoomingModeY; }
            set
            {
                _zoomingModeY = value;
                OnPropertyChanged();
            }
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }

        public void LoadSeries()
        {
            Labels = ProduktivitasPelanggan.Select(x => x.Bulan).ToArray();

            JumlahPelangganSeries = ProduktivitasPelanggan.Select(x => (int)x.TotalPelanggan).AsChartValues();
            TerbacaSeries = ProduktivitasPelanggan.Select(x => (int)x.SudahBaca).AsChartValues();
            FotoMeterSeries = ProduktivitasPelanggan.Select(x => (int)x.FotoMeter).AsChartValues();
            FotoRumahSeries = ProduktivitasPelanggan.Select(x => (int)x.FotoRumah).AsChartValues();
            VideoSeries = ProduktivitasPelanggan.Select(x => (int)x.Video).AsChartValues();
            GpsSeries = ProduktivitasPelanggan.Select(x => (int)x.Gps).AsChartValues();
        }
    }
}
