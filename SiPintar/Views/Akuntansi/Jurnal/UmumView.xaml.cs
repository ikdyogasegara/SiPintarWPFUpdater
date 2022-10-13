using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiPintar.Views.Akuntansi.Jurnal
{
    /// <summary>
    /// Interaction logic for UmumView.xaml
    /// </summary>
    public partial class UmumView : UserControl
    {
        public UmumView()
        {
            InitializeComponent();
            HideFilter_Click(null, null);
        }


        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
            ToolbarFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(240);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
            ToolbarFilter.Visibility = Visibility.Collapsed;
        }

        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGridJurnalUmum_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }
        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {

        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Tgl_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
