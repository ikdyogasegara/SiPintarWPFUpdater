using System.Windows;
using System.Windows.Controls;

namespace SiPintar.Views.Gudang.ProsesTransaksi
{
    /// <summary>
    /// Interaction logic for DaftarBarangKeluarView.xaml
    /// </summary>
    public partial class DaftarBarangKeluarView : UserControl
    {
        public DaftarBarangKeluarView()
        {
            InitializeComponent();
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
    }
}
