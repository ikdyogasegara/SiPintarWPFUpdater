using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Billing.Atribut.Tarif;

namespace SiPintar.Views.Billing.Atribut.Tarif
{
    public partial class RetribusiView : UserControl
    {
        public RetribusiView()
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
            var _viewModel = (RetribusiViewModel)DataGridRetribusi.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridRetribusi },
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }
    }
}
