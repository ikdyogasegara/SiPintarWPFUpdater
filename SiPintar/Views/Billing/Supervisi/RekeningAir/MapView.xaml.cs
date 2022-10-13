using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class MapView : UserControl
    {
        private readonly string _currentLat;
        private readonly string _currentLon;

        private bool _isMapInitiated;
        private readonly Func<bool> _func;

        public MapView(object dataContext, Func<bool> func = null)
        {
            InitializeComponent();
            DataContext = dataContext;
            var viewModel = (RekeningAirViewModel)DataContext;
            _func = func;

            _isMapInitiated = false;

            _currentLat = viewModel.SelectedData.Latitude ?? "0";
            _currentLon = viewModel.SelectedData.Longitude ?? "0";
        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap, true, 14);

            if (!_isMapInitiated)
            {
                var viewModel = (RekeningAirViewModel)DataContext;
                if (viewModel.SelectedData is { Latitude: { }, Longitude: { } })
                    MapConfigHelper.Initiate(MainMap);

                _isMapInitiated = true;
            }

            MainMap.Height = 500;
            SetMarker();
        }

        private void SetMarker()
        {
            var markerList = new List<MapMarkerObject>();
            var viewModel = (RekeningAirViewModel)DataContext;

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

            if (viewModel.SelectedData is { Latitude: { }, Longitude: { } })
            {
                markerList.Add(new MapMarkerObject()
                {
                    MarkerInformation = $"Pelanggan\n- Nama : {viewModel.SelectedData.IdPelangganAir}",
                    Latitude = viewModel.SelectedData.Latitude,
                    Longitude = viewModel.SelectedData.Longitude,
                });

                MapConfigHelper.SetCenter(MainMap, viewModel.SelectedData.Latitude, viewModel.SelectedData.Longitude);
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

        private void MainMap_OnMapDrag()
        {
        }
    }
}
