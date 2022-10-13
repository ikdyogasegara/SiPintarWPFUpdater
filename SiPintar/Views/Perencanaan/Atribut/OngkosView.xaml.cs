using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Views.Perencanaan.Atribut
{
    /// <summary>
    /// Interaction logic for OngkosView.xaml
    /// </summary>
    public partial class OngkosView : UserControl
    {
        public static bool ShowFilter = true; // filter section flag
        public static bool TempShowFilter = true;
        public OngkosView()
        {
            InitializeComponent();
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
            _ = (OngkosViewModel)DataGridOngkos.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();
            _ = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridOngkos },
                { "FileType", FileType }
            };

            //_ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }
        #region == Show/Hide Filter Section
        public void ShowHideFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            TriggerShowFilter();

            if (ShowFilter)
            {
                FilterOptionLabel.Text = "Tutup Filter";
                BtnFilter.Visibility = Visibility.Collapsed;
                FilterColumn.Width = GridLength.Auto;
                FilterOngkos.Visibility = Visibility.Visible;
            }
            else
            {
                FilterOptionLabel.Text = "Filter";
                BtnFilter.Visibility = Visibility.Visible;
                FilterColumn.Width = GridLength.Auto;
                FilterOngkos.Visibility = Visibility.Collapsed;
            }
        }
        public static void TriggerShowFilter()
        {
            TempShowFilter = ShowFilter;
            ShowFilter = !ShowFilter;
        }
        #endregion
    }
}
