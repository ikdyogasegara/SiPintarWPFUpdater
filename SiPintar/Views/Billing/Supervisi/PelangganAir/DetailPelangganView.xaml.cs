using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.PelangganAir
{
    public partial class DetailPelangganView : UserControl
    {
        private bool IsMapInitiated;

        public DetailPelangganView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            InfoDasar.IsChecked = true;
            IsMapInitiated = false;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }

        #region GMAP CONTROL
        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap);
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

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            InfoDasarSection.Visibility = current == "InfoDasar" ? Visibility.Visible : Visibility.Collapsed;
            LokasiSection.Visibility = current == "LokasiPelanggan" ? Visibility.Visible : Visibility.Collapsed;

            if (current == "LokasiPelanggan")
            {
                if (!IsMapInitiated)
                {
                    var _viewModel = (PelangganAirViewModel)DataContext;
                    if (_viewModel.SelectedData != null && _viewModel.SelectedData.Latitude != null && _viewModel.SelectedData.Longitude != null)
                        _ = Task.Run(() => MapConfigHelper.Initiate(MainMap));

                    IsMapInitiated = true;
                }

                SetMarker();
            }
        }

        private void SetMarker()
        {
            var _viewModel = (PelangganAirViewModel)DataContext;
            if (_viewModel.SelectedData != null && _viewModel.SelectedData.Latitude != null && _viewModel.SelectedData.Longitude != null)
            {
                var MarkerList = new List<MapMarkerObject>()
                {
                    new MapMarkerObject()
                    {
                        MarkerInformation = $"LOKASI PELANGGAN\n{_viewModel.SelectedData.Nama}",
                        Latitude = _viewModel.SelectedData.Latitude,
                        Longitude = _viewModel.SelectedData.Longitude
                    }
                };

                MapConfigHelper.SetMarker(MainMap, MarkerList);
            }
        }
    }
}
