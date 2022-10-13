using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using AppBusiness.Data.DTOs;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using SiPintar.Commands.Bacameter.SistemKontrol.JadwalRuteBaca.PetugasBacaGrafik;
using SiPintar.Utilities;

namespace SiPintar.ViewModels.Bacameter.SistemKontrol.JadwalRuteBaca
{
    public class PetugasBacaGrafikViewModel : ViewModelBase
    {
        public PetugasBacaGrafikViewModel(JadwalRuteBacaViewModel parent, IRestApiClientModel restApi)
        {
            Parent = parent;

            OnLoadCommand = new OnLoadCommand(this, restApi);
        }

        public ICommand OnLoadCommand { get; }


        private JadwalRuteBacaViewModel _parent;
        public JadwalRuteBacaViewModel Parent
        {
            get => _parent;
            set { _parent = value; OnPropertyChanged(); }
        }

        private ObservableCollection<SummaryPetugasBacaDto> _petugas { get; set; }
        public ObservableCollection<SummaryPetugasBacaDto> PetugasList
        {
            get { return _petugas; }
            set
            {
                _petugas = value;
                OnPropertyChanged();
            }
        }

        private string[] labels { get; set; }
        public string[] Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                OnPropertyChanged();
            }
        }

        public Func<double, string> Formatter { get; set; }

        private ChartValues<int> _jumlahJadwalSeries { get; set; }
        public ChartValues<int> JumlahJadwalSeries
        {
            get { return _jumlahJadwalSeries; }
            set
            {
                _jumlahJadwalSeries = value;
                OnPropertyChanged();
            }
        }

        private ChartValues<ObservableValue> _seriesCollection { get; set; }
        public ChartValues<ObservableValue> SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        private CartesianMapper<ObservableValue> _mapperColumn { get; set; }
        public CartesianMapper<ObservableValue> MapperColumn
        {
            get { return _mapperColumn; }
            set
            {
                _mapperColumn = value;
                OnPropertyChanged();
            }
        }

        private CartesianMapper<ObservableValue> _mapperRow { get; set; }
        public CartesianMapper<ObservableValue> MapperRow
        {
            get { return _mapperRow; }
            set
            {
                _mapperRow = value;
                OnPropertyChanged();
            }
        }

        private CartesianMapper<ObservableValue> _mapperLine { get; set; }
        public CartesianMapper<ObservableValue> MapperLine
        {
            get { return _mapperLine; }
            set
            {
                _mapperLine = value;
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
            }
        }

        private bool isEmpty;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                OnPropertyChanged();
            }
        }

        private bool _jumlahJadwalSeriesVisibility;
        public bool JumlahJadwalSeriesVisibility
        {
            get { return _jumlahJadwalSeriesVisibility; }
            set
            {
                _jumlahJadwalSeriesVisibility = value;
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
                OnPropertyChanged("Plot");
                OnPropertyChanged("CurrentPlot");
                OnPropertyChanged("CurrentPlotImage");
            }
        }
        public string CurrentPlot
        {
            get { return $"Grafik {Plot}"; }
        }
        [ExcludeFromCodeCoverage]
        public string CurrentPlotImage
        {
            get
            {
                if (string.IsNullOrEmpty(Plot))
                    return $"/SiPintar;component/Assets/Images/Chart/ic_kolom_chart.png";

                return $"/SiPintar;component/Assets/Images/Chart/ic_{Plot.ToLower()}_chart.png";
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

        [ExcludeFromCodeCoverage]
        public Color[] ColorList
        {
            get
            {
                Color[] list =
                {
                    (Color)ColorConverter.ConvertFromString("#028ddb"),
                    (Color)ColorConverter.ConvertFromString("#5ab9b9"),
                    (Color)ColorConverter.ConvertFromString("#c7416a"),
                    (Color)ColorConverter.ConvertFromString("#4b8edd"),
                    (Color)ColorConverter.ConvertFromString("#d35c4d"),
                    (Color)ColorConverter.ConvertFromString("#706dc1"),
                    (Color)ColorConverter.ConvertFromString("#e6923d"),
                    (Color)ColorConverter.ConvertFromString("#b75d8e"),
                    (Color)ColorConverter.ConvertFromString("#3fa85f"),
                    (Color)ColorConverter.ConvertFromString("#028ddb"),
                };

                return list;
            }
        }

        [ExcludeFromCodeCoverage]
        public void LoadSeries()
        {
            if (PetugasList == null)
            {
                SeriesCollection = new ChartValues<ObservableValue> { };
                return;
            }

            var data = new ChartValues<ObservableValue> { };

            Labels = PetugasList.Select(x => x.PetugasBaca).ToArray();

            JumlahJadwalSeries = PetugasList.Select(x => (int)x.JumlahPelanggan).AsChartValues();

            foreach (var item in PetugasList)
            {
                data.Add(new ObservableValue(Convert.ToDouble(item.JumlahPelanggan)));
            }

            SeriesCollection = data;
        }

        private int _testId;
        public int TestId
        {
            get => _testId;
            set { _testId = value; OnPropertyChanged(); }
        }
    }
}
