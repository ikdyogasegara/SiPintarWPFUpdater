using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.Pendaftaran
{
    public partial class Step2AddFormView : UserControl
    {
        private bool _isMapInitiated;
        private string _currentLat;
        private string _currentLon;
        private string _currentAlamatMap;

        public Step2AddFormView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            if (!_isMapInitiated)
            {
                MapConfigHelper.Initiate(MainMap);
                MapConfigHelper.OnLoaded(MainMap, false, 16);
                CenterMarker.Visibility = Visibility.Visible;
                _isMapInitiated = true;
            }
        }

        #region GMAP CONTROL
        private void ZoomInButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMap.Zoom < MainMap.MaxZoom)
                MainMap.Zoom++;
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainMap.Zoom > MainMap.MinZoom)
                MainMap.Zoom--;
        }
        #endregion

        private void OpenMap_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PendaftaranViewModel)DataContext;
            _ = DialogHost.Show(new MapView(viewModel, RefreshMap), "InnerHublangRootDialog");
        }

        public bool RefreshMap()
        {
            if (DataContext is PendaftaranViewModel viewModel)
            {
                MapConfigHelper.SetCenter(MainMap, viewModel.FormData.Latitude, viewModel.FormData.Longitude);
            }
            return true;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }

        private void Search_Click(object sender = null, RoutedEventArgs e = null)
        {
            var viewModel = (PendaftaranViewModel)DataContext;

            if (string.IsNullOrEmpty(Lokasi.Text) || viewModel == null)
                return;

            _ = SearchAddressAsync(viewModel);
        }

        private async Task SearchAddressAsync(PendaftaranViewModel viewModel)
        {
            viewModel.IsLoadingMap = true;

            var result = await MapConfigHelper.GetLatLonByAddressAsync(Lokasi.Text);

            if (result != null && result.Length > 0)
            {
                _currentLat = result[0].Replace(',', '.');
                _currentLon = result[1].Replace(',', '.');

                MapConfigHelper.SetCenter(MainMap, _currentLat, _currentLon);
                CenterMarker.Visibility = Visibility.Visible;
                LonText.Text = _currentLon;
                LatText.Text = _currentLat;

                var alamatResult = await MapConfigHelper.GetAddressByLatLonAsync(_currentLat, _currentLon);
                AlamatMap.Text = alamatResult;
                _currentAlamatMap = alamatResult;

                SetCurrentPoint();
            }

            viewModel.IsLoadingMap = false;
        }

        private void MainMap_OnMapDrag()
        {
            if (DataContext is PendaftaranViewModel viewModel)
            {
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    CenterMarker.Visibility = Visibility.Visible;
                    var center = MainMap.Position;
                    _ = GetAddressNameAsync(center);
                };
            }
        }

        private async Task GetAddressNameAsync(PointLatLng center)
        {
            _currentLat = center.Lat.ToString().Replace(',', '.');
            _currentLon = center.Lng.ToString().Replace(',', '.');

            _currentAlamatMap = await MapConfigHelper.GetAddressByLatLonAsync(_currentLat, _currentLon);
            AlamatMap.Text = _currentAlamatMap;
            LonText.Text = _currentLon;
            LatText.Text = _currentLat;
            SetCurrentPoint();
        }

        private void SetCurrentPoint()
        {
            var viewModel = (PendaftaranViewModel)DataContext;
            viewModel.FormData.Latitude = _currentLat;
            viewModel.FormData.Longitude = _currentLon;
            viewModel.FormData.AlamatMap = _currentAlamatMap;
        }

        private void Lokasi_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }
    }
}
