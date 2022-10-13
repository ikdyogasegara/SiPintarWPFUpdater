using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter
{
    public partial class ProduktivitasView : UserControl
    {
        public ProduktivitasView()
        {
            InitializeComponent();
            ShowFilter_Click();
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
            var _viewModel = (ProduktivitasViewModel)MainContent.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }

        private void Tgl_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = false;
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
            FilterWrapper.Width = new GridLength(195);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void JenisProduktivitas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _viewModel = (ProduktivitasViewModel)MainContent.DataContext;
            var nav = (ComboBox)sender;

            if (_viewModel != null && nav != null)
                _viewModel.UpdatePage(nav.SelectedItem.ToString());
        }

        private void Periode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var _viewModel = (ProduktivitasViewModel)MainContent.DataContext;
            //var nav = (ComboBox)sender;

            //if (_viewModel != null && nav != null)
            //    _viewModel.UpdatePage(_viewModel.JenisProduktivitas);
        }
    }
}
