using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Views.Billing.Atribut
{
    public partial class KelainanView : UserControl
    {
        public KelainanView()
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
            var viewModel = (KelainanViewModel)DataGridContent.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridContent },
                { "FileType", fileType }
            };

            _ = ((AsyncCommandBase)viewModel.OnExportCommand).ExecuteAsync(param);
        }
    }
}
