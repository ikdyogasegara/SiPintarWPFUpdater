using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using SiPintar.ViewModels.Distribusi;

namespace SiPintar.Views.Distribusi
{
    public partial class DistribusiView : UserControl
    {
        public DistribusiView()
        {
            InitializeComponent();
        }

        private void NavItemSelectedHandler(object sender, RoutedEventArgs e)
        {
            var _viewModel = (DistribusiViewModel)DataContext;

            string Label = ((RadioButton)sender).Tag.ToString();
            if (Label == "Berita Acara")
            {
                var cm = ContextMenuService.GetContextMenu(sender as DependencyObject);
                cm.PlacementTarget = sender as UIElement;
                cm.Placement = PlacementMode.Bottom;
                cm.IsOpen = true;
            }
            else if (_viewModel != null && Label != null)
                _viewModel.UpdatePage(Label);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var _viewModel = (DistribusiViewModel)DataContext;

            var mi = sender as MenuItem;
            if (mi != null)
            {
                string Tag = mi.Tag.ToString();
                if (_viewModel != null && Tag != null)
                    _viewModel.UpdatePage(Tag);
            }
        }
    }
}
