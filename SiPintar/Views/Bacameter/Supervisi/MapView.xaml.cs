using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class MapView : UserControl
    {
        private bool IsMapInitiated;

        public MapView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
            IsMapInitiated = false;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap);

            if (!IsMapInitiated)
            {
                var _viewModel = (SupervisiViewModel)DataContext;
                if (_viewModel.SelectedData != null && _viewModel.SelectedData.Latitude != null && _viewModel.SelectedData.Longitude != null)
                    _ = Task.Run(() => MapConfigHelper.Initiate(MainMap));

                IsMapInitiated = true;
            }

            SetMarker();
        }

        private void SetMarker()
        {
            var _viewModel = (SupervisiViewModel)DataContext;
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.Latitude != null && _viewModel.SelectedData.Longitude != null)
            {
                var MarkerList = new List<MapMarkerObject>()
                {
                    new MapMarkerObject()
                    {
                        MarkerInformation = $"{_viewModel.SelectedData.Nama}",
                        Latitude = _viewModel.SelectedData.Latitude,
                        Longitude = _viewModel.SelectedData.Longitude
                    }
                };

                MapConfigHelper.SetMarker(MainMap, MarkerList);
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
    }
}
