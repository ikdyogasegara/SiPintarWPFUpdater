using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands.Global;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi
{
    /// <summary>
    /// Interaction logic for DaftarBarangMasukView.xaml
    /// </summary>
    public partial class DaftarBarangMasukView : UserControl
    {
        public DaftarBarangMasukView()
        {
            InitializeComponent();
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            if (PerNoTransaksiSec != null)
            {
                ActionControlPerNoTransaksi.Visibility = PerNoTransaksiSec.Visibility = current == "PerNoTransaksi" ? Visibility.Visible : Visibility.Collapsed;
                if (current == "PerNoTransaksi")
                {
                    HideFilter_Click2(null, null);
                    if (DataContext is DaftarBarangMasukViewModel vm)
                    {
                        vm.ActiveTab = DaftarBarangMasukTab.PerNoTransaksi;
                        vm.OnRefreshCommand.Execute(null);
                    }
                }
            }
            if (DaftarBarangSec != null)
            {
                ActionControlDaftarBarang.Visibility = DaftarBarangSec.Visibility = current == "DaftarBarang" ? Visibility.Visible : Visibility.Collapsed;
                if (current == "DaftarBarang")
                {
                    HideFilter_Click1(null, null);
                    if (DataContext is DaftarBarangMasukViewModel vm)
                    {
                        vm.ActiveTab = DaftarBarangMasukTab.DaftarBarang;
                        vm.OnRefreshCommand.Execute(null);
                    }
                }
            }
        }

        private void HideFilter_Click1(object sender, RoutedEventArgs e)
        {
            FilterWrapper1.Width = new GridLength(0);
            FilterSection1.Visibility = Visibility.Collapsed;
            ToolbarFilter1.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click1(object sender, RoutedEventArgs e)
        {
            FilterWrapper1.Width = new GridLength(240);
            FilterSection1.Visibility = Visibility.Visible;
            ToolbarFilter1.Visibility = Visibility.Collapsed;
        }

        private void HideFilter_Click2(object sender, RoutedEventArgs e)
        {
            FilterWrapper2.Width = new GridLength(0);
            FilterSection2.Visibility = Visibility.Collapsed;
            ToolbarFilter2.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click2(object sender, RoutedEventArgs e)
        {
            FilterWrapper2.Width = new GridLength(240);
            FilterSection2.Visibility = Visibility.Visible;
            ToolbarFilter2.Visibility = Visibility.Collapsed;
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk == null)
            {
                e.Handled = true;
            }
        }
        private void ContextMenu_ContextMenuOpeningDetail(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasukDetail == null)
            {
                e.Handled = true;
            }
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm)
            {
                vm.ResetFilter();
            }
        }

        private void CetakOP_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 },
                    { "IsTemplate", false }
                };
                ((OnCetakCommand)vm.OnCetakOrderPembelianCommand).IsCetak = true;
                vm.OnCetakOrderPembelianCommand.Execute(param);
            }
        }

        private void CetakSPK_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 },
                    { "IsTemplate", false }
                };
                ((OnCetakCommand)vm.OnCetakSpkCommand).IsCetak = true;
                vm.OnCetakSpkCommand.Execute(param);
            }
        }

        private void CetakLPB_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 },
                    { "IsTemplate", false }
                };
                ((OnCetakCommand)vm.OnCetakLpbCommand).IsCetak = true;
                vm.OnCetakLpbCommand.Execute(param);
            }
        }

        private void CetakDPB_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 },
                    { "IsTemplate", false }
                };
                ((OnCetakCommand)vm.OnCetakDpbCommand).IsCetak = true;
                vm.OnCetakDpbCommand.Execute(param);
            }
        }

        private void DesignOP_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 }
                };
                ((OnCetakCommand)vm.OnCetakOrderPembelianCommand).IsCetak = false;
                vm.OnCetakOrderPembelianCommand.Execute(param);
            }
        }

        private void DesignSPK_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 }
                };
                ((OnCetakCommand)vm.OnCetakSpkCommand).IsCetak = false;
                vm.OnCetakSpkCommand.Execute(param);
            }
        }

        private void DesignLPB_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 }
                };
                ((OnCetakCommand)vm.OnCetakLpbCommand).IsCetak = false;
                vm.OnCetakLpbCommand.Execute(param);
            }
        }

        private void DesignDPB_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is DaftarBarangMasukViewModel vm && vm.SelectedDaftarBarangMasuk != null)
            {
                var param = new Dictionary<string, dynamic>
                {
                    { "IdBarangMasuk", vm.SelectedDaftarBarangMasuk.IdBarangMasuk??-1 }
                };
                ((OnCetakCommand)vm.OnCetakDpbCommand).IsCetak = false;
                vm.OnCetakDpbCommand.Execute(param);
            }
        }
    }
}
