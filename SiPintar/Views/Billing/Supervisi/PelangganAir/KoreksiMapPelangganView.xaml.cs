using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganAir
{
    public partial class KoreksiMapPelangganView : UserControl
    {
        private string currentLat;
        private string currentLon;

        public KoreksiMapPelangganView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            _ = Task.Run(() => MapConfigHelper.Initiate(MainMap));

            CenterMarker.Visibility = Visibility.Collapsed;
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap);

            CenterMarker.Visibility = Visibility.Collapsed;

            var _viewModel = (PelangganAirViewModel)DataContext;
            if (_viewModel.PelangganForm != null)
            {
                string CurrentAddress = null;
                CurrentAddress += _viewModel.PelangganForm.Alamat != null ? _viewModel.PelangganForm.Alamat + ", " : "";
                CurrentAddress += _viewModel.PelangganForm.NamaKecamatan != null ? _viewModel.PelangganForm.NamaKecamatan + ", " : "";
                CurrentAddress += _viewModel.PelangganForm.NamaKelurahan != null ? _viewModel.PelangganForm.NamaKelurahan + ", " : "";

                Alamat.Text = CurrentAddress;

                if (_viewModel.PelangganForm.Latitude != null && _viewModel.PelangganForm.Longitude != null)
                {
                    currentLat = _viewModel.PelangganForm.Latitude.Replace(',', '.');
                    currentLon = _viewModel.PelangganForm.Longitude.Replace(',', '.');

                    MapConfigHelper.SetCenter(MainMap, currentLat, currentLon);
                    CenterMarker.Visibility = Visibility.Visible;
                }
                else if (_viewModel.PelangganForm.Alamat != null)
                {
                    Search_Click();
                }
            }
        }

        private void ZoomIn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainMap.Zoom < MainMap.MaxZoom)
                MainMap.Zoom++;
        }

        private void ZoomOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainMap.Zoom > MainMap.MinZoom)
                MainMap.Zoom--;
        }
        #endregion

        private void Search_Click(object sender = null, RoutedEventArgs e = null)
        {
            var _viewModel = (PelangganAirViewModel)DataContext;

            if (string.IsNullOrEmpty(Alamat.Text) || _viewModel == null)
                return;

            _ = SearchAddressAsync(_viewModel);
        }

        private async Task SearchAddressAsync(PelangganAirViewModel _viewModel)
        {
            _viewModel.IsLoadingMap = true;

            var result = await MapConfigHelper.GetLatLonByAddressAsync(Alamat.Text);
            if (result != null && result.Length > 0)
            {
                currentLat = result[0].Replace(',', '.');
                currentLon = result[1].Replace(',', '.');

                MapConfigHelper.SetCenter(MainMap, currentLat, currentLon);
                CenterMarker.Visibility = Visibility.Visible;
            }

            _viewModel.IsLoadingMap = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PelangganAirViewModel)DataContext;

            if (_viewModel != null && currentLat != null && currentLon != null)
            {
                _viewModel.PelangganForm.Latitude = currentLat.Replace(',', '.');
                _viewModel.PelangganForm.Longitude = currentLon.Replace(',', '.');
                _viewModel.PelangganForm.Alamat = Alamat.Text;
            }

            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void MainMap_OnMapDrag()
        {
            CenterMarker.Visibility = Visibility.Visible;
            var center = MainMap.Position;

            _ = GetAddressNameAsync(center);
        }

        private async Task GetAddressNameAsync(PointLatLng center)
        {
            currentLat = center.Lat.ToString().Replace(',', '.');
            currentLon = center.Lng.ToString().Replace(',', '.');

            var result = await MapConfigHelper.GetAddressByLatLonAsync(currentLat, currentLon);
            if (result != null)
            {
                Alamat.Text = result;
            }
        }
    }
}
