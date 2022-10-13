using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Gudang;

namespace SiPintar.Views.Gudang
{
    public partial class ProsesTransaksiView : UserControl
    {
        public ProsesTransaksiView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (ProsesTransaksiViewModel)DataContext;

            string? label = ((RadioButton)sender).Tag.ToString();

            if (label == "Pembelian")
            {
                var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
                cm.PlacementTarget = sender as UIElement;
                cm.Placement = PlacementMode.Bottom;
                cm.IsOpen = true;
            }
            else if (_viewModel != null && label != null)
                _viewModel.UpdatePage(label);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ProsesTransaksiViewModel)DataContext;

            if (sender is MenuItem mi)
            {
                var tag = mi.Tag.ToString();
                if (viewModel != null && tag != null)
                    viewModel.UpdatePage(tag);
            }
        }
    }
}
