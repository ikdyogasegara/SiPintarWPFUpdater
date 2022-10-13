using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using LiveCharts;
using LiveCharts.Helpers;
using SiPintar.Commands.Bacameter.Produktivitas.PembacaanPerTanggal;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.Produktivitas
{
    [ExcludeFromCodeCoverage]
    public class PembacaanPerTanggalViewModel : ViewModelBase
    {
        public PembacaanPerTanggalViewModel(ProduktivitasViewModel parent, IRestApiClientModel restApi)
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
        private ObservableCollection<SummaryProduktifitasBacameterPembacaanPerTanggalDto> _pembacaanPerTgl { get; set; }
        public ObservableCollection<SummaryProduktifitasBacameterPembacaanPerTanggalDto> PembacaanPerTgl
        {
            get { return _pembacaanPerTgl; }
            set
            {
                _pembacaanPerTgl = value;
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
        private ChartValues<int> jumlahBacaSeries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> jumlahTaksirSeries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private ChartValues<int> jumlahKelainanSeries { get; set; }
        public ChartValues<int> JumlahBacaSeries
        {
            get { return jumlahBacaSeries; }
            set
            {
                jumlahBacaSeries = value;
                OnPropertyChanged();
            }
        }
        public ChartValues<int> JumlahTaksirSeries
        {
            get { return jumlahTaksirSeries; }
            set
            {
                jumlahTaksirSeries = value;
                OnPropertyChanged();
            }
        }
        public ChartValues<int> JumlahKelainanSeries
        {
            get { return jumlahKelainanSeries; }
            set
            {
                jumlahKelainanSeries = value;
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
        private bool jumlahBacaSeriesVisibility;
        private bool jumlahKelainanSeriesVisibility;
        private bool jumlahTaksirSeriesVisibility;
        public bool JumlahBacaSeriesVisibility
        {
            get { return jumlahBacaSeriesVisibility; }
            set
            {
                jumlahBacaSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool JumlahKelainanSeriesVisibility
        {
            get { return jumlahKelainanSeriesVisibility; }
            set
            {
                jumlahKelainanSeriesVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool JumlahTaksirSeriesVisibility
        {
            get { return jumlahTaksirSeriesVisibility; }
            set
            {
                jumlahTaksirSeriesVisibility = value;
                OnPropertyChanged();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private string plot { get; set; }
        public string Plot
        {
            get { return plot; }
            set
            {
                plot = value;
                OnPropertyChanged();
                OnPropertyChanged("CurrentPlot");
                OnPropertyChanged("CurrentPlotImage");
            }
        }
        public string CurrentPlot
        {
            get { return $"Grafik {Plot}"; }
        }
        public string CurrentPlotImage
        {
            get { return $"/SiPintar;component/Assets/Images/Chart/ic_{Plot.ToLower()}_chart.png"; }
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
            Labels = PembacaanPerTgl.Select(x => ((DateTime)x.Tanggal).ToString("dd MM yyyy")).ToArray();

            JumlahBacaSeries = PembacaanPerTgl.Select(x => (int)x.Baca).AsChartValues();
            JumlahTaksirSeries = PembacaanPerTgl.Select(x => (int)x.Taksir).AsChartValues();
            JumlahKelainanSeries = PembacaanPerTgl.Select(x => (int)x.Kelainan).AsChartValues();
        }
    }
}
