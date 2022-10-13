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

namespace SiPintar.Views.Akuntansi.Voucher
{
    /// <summary>
    /// Interaction logic for KuitansiView.xaml
    /// </summary>
    public partial class KuitansiView : UserControl
    {
        public KuitansiView()
        {
            InitializeComponent();

            RadioBuatVoucher.IsChecked = true;
        }

        private void RadioBuatVoucher_Checked(object sender, RoutedEventArgs e)
        {
            TanggalBayarAwal.IsEnabled = false;
            TanggalBayarAkhir.IsEnabled = false;
        }

        private void RadioBayarVoucher_Checked(object sender, RoutedEventArgs e)
        {
            TanggalBayarAwal.IsEnabled = true;
            TanggalBayarAkhir.IsEnabled = true;
        }
    }
}
