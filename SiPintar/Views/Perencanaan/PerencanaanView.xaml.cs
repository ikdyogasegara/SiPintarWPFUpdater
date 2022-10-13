using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Perencanaan;

namespace SiPintar.Views.Perencanaan
{
    public partial class PerencanaanView : UserControl
    {
        public PerencanaanView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var viewModel = (PerencanaanViewModel)DataContext;

            var label = ((RadioButton)sender).Tag.ToString();
            if (label == "Surat Perintah Kerja (SPK)" || label == "Rencana Anggaran Belanja (RAB)" || label == "Surat Perintah Kerja OLD (SPK)")
            {
                var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
                cm.PlacementTarget = sender as UIElement;
                cm.Placement = PlacementMode.Bottom;
                cm.IsOpen = true;
            }
            else if (viewModel != null && label != null)
                viewModel.UpdatePage(label);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (PerencanaanViewModel)DataContext;

            var mi = sender as MenuItem;
            if (mi != null)
            {
                var Tag = mi.Tag.ToString();
                if (viewModel != null && Tag != null)
                    viewModel.UpdatePage(Tag);
            }
        }
    }
}
