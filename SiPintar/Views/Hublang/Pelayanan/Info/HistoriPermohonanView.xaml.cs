using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.Commands;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Views.Hublang.Pelayanan.Info
{
    public partial class HistoriPermohonanView : UserControl
    {
        public HistoriPermohonanView()
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
            var _viewModel = (HistoriPermohonanViewModel)DataGridHistoriPermohonan.DataContext;
            var FileType = ((MenuItem)sender).Tag.ToString();

            var param = new Dictionary<string, dynamic>()
            {
                { "Data", DataGridHistoriPermohonan },
                { "FileType", FileType }
            };

            _ = ((AsyncCommandBase)_viewModel.OnExportCommand).ExecuteAsync(param);
        }
    }
}
