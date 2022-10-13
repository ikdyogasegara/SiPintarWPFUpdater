using System.Windows;
using System.Windows.Controls;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi
{
    /// <summary>
    /// Interaction logic for PengajuanPembelianView.xaml
    /// </summary>
    public partial class PengajuanPembelianView : UserControl
    {
        public PengajuanPembelianView()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PengajuanPembelianViewModel vm)
            {
                vm.FilterNoPengajuanEnabled = false;
                vm.FilterWilayahEnabled = false;
                vm.FilterTglPengajuanEnabled = false;
                vm.FilterNoPengajuan = null;
                vm.FilterWilayah = null;
                vm.FilterTglPengajuanMulai = null;
                vm.FilterTglPengajuanSampai = null;
            }
        }

        private void DataGridBarang_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataContext is PengajuanPembelianViewModel vm)
            {
                vm.OnViewPengajuanCommand.Execute(null);
            }
        }
    }
}
