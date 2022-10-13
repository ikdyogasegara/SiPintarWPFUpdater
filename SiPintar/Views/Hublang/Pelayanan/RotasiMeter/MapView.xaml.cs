using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using AppBusiness.Data.DTOs;
using GMap.NET;
using MaterialDesignThemes.Wpf;
using NPOI.HSSF.Util;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Views.Hublang.Pelayanan.RotasiMeter
{
    public partial class MapView : UserControl
    {
        private string _currentLat;
        private string _currentLon;

        private bool _isMapInitiated;
        private readonly RotasiMeterViewModel _viewModel;
        private readonly Func<bool> _func;

        public MapView(object dataContext, Func<bool> func = null)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RotasiMeterViewModel)DataContext;
            _func = func;

            _isMapInitiated = false;

            _currentLat = _viewModel.FormData.Latitude ?? "0";
            _currentLon = _viewModel.FormData.Longitude ?? "0";

            Alamat.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap, true, 14);

            if (!_isMapInitiated)
            {
                var _viewModel = (RotasiMeterViewModel)DataContext;
                if (_viewModel.FormData != null && _viewModel.FormData.Latitude != null && _viewModel.FormData.Longitude != null)
                    MapConfigHelper.Initiate(MainMap);

                _isMapInitiated = true;
            }

            if (_viewModel.IsFor == "detail")
            {
                MainMap.Height = 500;
                CenterMarker.Visibility = Visibility.Collapsed;
                SaveButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainMap.Height = 500;
                CenterMarker.Visibility = Visibility.Visible;
                MainMap.OnMapDrag += MainMap_OnMapDrag;
            }

            SetMarker();
        }

        private void SetMarker()
        {
            var markerList = new List<MapMarkerObject>();
            var _viewModel = (RotasiMeterViewModel)DataContext;

            // foreach (var i in _viewModel.MapPelangganAirList)
            // {
            //     markerList.Add(new MapMarkerObject()
            //     {
            //         MarkerInformation = $"Pelanggan\n- Nama : {i.Nama}\n- No.Samb : {i.NoSamb}\n- Gol : {i.KodeGolongan}",
            //         Latitude = i.Latitude,
            //         Longitude = i.Longitude,
            //
            //     });
            // }

            if (_viewModel.FormData != null && _viewModel.FormData.Latitude != null && _viewModel.FormData.Longitude != null)
            {
                markerList.Add(new MapMarkerObject()
                {
                    MarkerInformation = $"Pelanggan\n- Nama : {_viewModel.FormData.IdPelangganAir}",
                    Latitude = _viewModel.FormData.Latitude,
                    Longitude = _viewModel.FormData.Longitude,
                });

                MapConfigHelper.SetCenter(MainMap, _viewModel.FormData.Latitude, _viewModel.FormData.Longitude);
            }
            else
            {
                MapConfigHelper.SetCenter(MainMap, _currentLat, _currentLon);
            }


            MapConfigHelper.SetMarker(MainMap, markerList);
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

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click();
        }

        private void Search_Click(object sender = null, RoutedEventArgs e = null)
        {
            var _viewModel = (RotasiMeterViewModel)DataContext;

            if (string.IsNullOrEmpty(Alamat.Text) || _viewModel == null)
                return;

            _ = SearchAddressAsync(_viewModel);
        }

        private async Task SearchAddressAsync(RotasiMeterViewModel _viewModel)
        {
            _viewModel.IsLoadingForm = true;

            var result = await MapConfigHelper.GetLatLonByAddressAsync(Alamat.Text);
            if (result != null && result.Length > 0)
            {
                _currentLat = result[0].Replace(',', '.');
                _currentLon = result[1].Replace(',', '.');

                MapConfigHelper.SetCenter(MainMap, _currentLat, _currentLon);

                if (_viewModel.IsFor == "detail")
                {
                    CenterMarker.Visibility = Visibility.Collapsed;

                }
                else
                {
                    CenterMarker.Visibility = Visibility.Visible;

                }
            }

            _viewModel.IsLoadingForm = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (RotasiMeterViewModel)DataContext;

            if (_viewModel != null && _viewModel.FormData != null && _currentLat != null && _currentLon != null)
            {
                _viewModel.FormData.Latitude = _currentLat.Replace(',', '.');
                _viewModel.FormData.Longitude = _currentLon.Replace(',', '.');
                // _viewModel.FormData.AlamatMap = Alamat.Text;
            }

            _func?.Invoke();

            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void MainMap_OnMapDrag()
        {
            if (DataContext is RotasiMeterViewModel viewModel)
            {
                viewModel.IsLoadingForm = true;
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    CenterMarker.Visibility = Visibility.Visible;
                    var center = MainMap.Position;
                    _ = GetAddressNameAsync(center);
                    viewModel.IsLoadingForm = false;
                };
            }
        }

        private async Task GetAddressNameAsync(PointLatLng center)
        {
            _currentLat = center.Lat.ToString().Replace(',', '.');
            _currentLon = center.Lng.ToString().Replace(',', '.');

            var result = await MapConfigHelper.GetAddressByLatLonAsync(_currentLat, _currentLon);
            if (result != null)
            {
                Alamat.Text = result;
            }
        }
    }
}
