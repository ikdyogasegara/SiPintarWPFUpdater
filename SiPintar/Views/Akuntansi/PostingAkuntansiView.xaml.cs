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

namespace SiPintar.Views.Akuntansi
{
    /// <summary>
    /// Interaction logic for PostingAkuntansiView.xaml
    /// </summary>
    public partial class PostingAkuntansiView : UserControl
    {
        public PostingAkuntansiView()
        {
            InitializeComponent();
        }

        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BatalButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            ProgressBar.Visibility = Visibility.Collapsed;
        }
    }
}
