using System.Windows;
using System.Windows.Controls;

namespace SiPintar.Views.Gudang.MasterData
{
    /// <summary>
    /// Interaction logic for BarangHabisView.xaml
    /// </summary>
    public partial class BarangHabisView : UserControl
    {
        public BarangHabisView()
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

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
