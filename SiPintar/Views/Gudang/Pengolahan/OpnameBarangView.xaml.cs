using System.Windows;
using System.Windows.Controls;

namespace SiPintar.Views.Gudang.Pengolahan
{
    /// <summary>
    /// Interaction logic for OpnameBarangBulananView.xaml
    /// </summary>
    public partial class OpnameBarangView : UserControl
    {
        public OpnameBarangView()
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
    }
}
