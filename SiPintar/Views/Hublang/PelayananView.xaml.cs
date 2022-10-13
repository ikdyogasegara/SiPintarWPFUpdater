using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Hublang;

namespace SiPintar.Views.Hublang
{
    public partial class PelayananView : UserControl
    {
        public PelayananView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var viewModel = (PelayananViewModel)DataContext;

            var label = ((RadioButton)sender).Tag.ToString();
            if (label == "Permohonan" || label == "Pengaduan" || label == "Berita Acara" || label == "Pelanggan" || label == "Pendaftaran" || label == "Koreksi Rekening Air" || label == "Surat Perintah Kerja (SPK)" || label == "Rencana Anggaran Biaya (RAB)")
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
            var viewModel = (PelayananViewModel)DataContext;

            if (sender is MenuItem mi)
            {
                var tag = mi.Tag.ToString();
                if (viewModel != null && tag != null)
                    viewModel.UpdatePage(tag);
            }
        }
    }
}
