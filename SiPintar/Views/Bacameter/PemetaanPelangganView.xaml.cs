using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter
{
    public partial class PemetaanPelangganView : UserControl
    {
        public PemetaanPelangganView()
        {
            InitializeComponent();
            ShowFilter_Click();

            _ = Task.Run(() => MapConfigHelper.Initiate(MainMap));
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PemetaanPelangganViewModel)DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(250);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
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

        private void MainMap_Loaded(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.OnLoaded(MainMap);
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (PemetaanPelangganViewModel)DataContext;

            if (_viewModel != null && _viewModel.SelectedJenisPeta != null)
                _ = ApplyFilterAsync(_viewModel);
        }

        private async Task ApplyFilterAsync(PemetaanPelangganViewModel _viewModel)
        {
            MapConfigHelper.ClearMarker(MainMap);

            await ((AsyncCommandBase)_viewModel.OnFilterCommand).ExecuteAsync(null);

            if (_viewModel.MarkerList != null)
                MapConfigHelper.SetMarker(MainMap, _viewModel.MarkerList);
        }

        private void Reset_Filter_Click(object sender, RoutedEventArgs e)
        {
            MapConfigHelper.ClearMarker(MainMap);

            var _viewModel = (PemetaanPelangganViewModel)DataContext;

            if (_viewModel != null)
                _ = ((AsyncCommandBase)_viewModel.OnResetFilterCommand).ExecuteAsync(null);
        }
    }
}
