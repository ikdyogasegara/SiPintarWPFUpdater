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

namespace SiPintar.Views.Akuntansi.MasterData.MasterDataAkuntansi
{
    /// <summary>
    /// Interaction logic for PengelompokanAktivaView.xaml
    /// </summary>
    public partial class PengelompokanAktivaView : UserControl
    {
        public PengelompokanAktivaView()
        {
            InitializeComponent();
        }

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            //var Vm = DataContext as KelompokKodePerkiraan1ViewModel;
            //if (Vm.PilihData is null)
            //    e.Handled = true;
        }


        private void ShowOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DataContext is KelompokKodePerkiraan1ViewModel Vm)
            //    _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            //var Vm = DataContext as KelompokKodePerkiraan1ViewModel;
            //if (Vm != null)
            //{
            //    Vm.CurrentPage--;
            //    _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            //}
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            //var Vm = DataContext as KelompokKodePerkiraan1ViewModel;
            //if (Vm != null)
            //{
            //    Vm.CurrentPage++;
            //    _ = ((AsyncCommandBase)Vm.OnRefreshCommand).ExecuteAsync(null);
            //}
        }
    }
}
