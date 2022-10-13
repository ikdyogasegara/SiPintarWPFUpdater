using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan
{
    /// <summary>
    /// Interaction logic for ProsesPiutangBulananView.xaml
    /// </summary>
    public partial class ProsesPiutangBulananView : UserControl
    {

        public ProsesPiutangBulananView()
        {
            InitializeComponent();
        }


        private void Bulan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Tahun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void HideProgressBar(object sender, RoutedEventArgs e)
        //{
        //    ProgressBar.Visibility = Visibility.Collapsed;
        //}

        //private void OkButton_Click(object sender = null, RoutedEventArgs e = null)
        //{
        //    ProgressBar.Visibility = Visibility.Visible;

        //}

        private void BatalButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            ProgressBar.Visibility = Visibility.Collapsed;
        }
    }
}
