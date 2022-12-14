using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.Meteran;

namespace SiPintar.Views.Billing.Atribut.Meteran
{
    public partial class MerkMeterView : UserControl
    {
        public MerkMeterView()
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
            var _viewModel = (MerkMeterViewModel)DataGridMerkMeter.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridMerkMeter },
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }
    }
}
