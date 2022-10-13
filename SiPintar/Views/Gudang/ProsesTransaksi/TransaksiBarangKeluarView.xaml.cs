using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.ViewModels.Gudang.ProsesTransaksi;

namespace SiPintar.Views.Gudang.ProsesTransaksi
{
    /// <summary>
    /// Interaction logic for TransaksiBarangKeluarView.xaml
    /// </summary>
    public partial class TransaksiBarangKeluarView : UserControl
    {
        public TransaksiBarangKeluarView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is TransaksiBarangKeluarViewModel)
            {
                HideFilter_Click(null, null);
            }
        }

        private void SubMenu_Checked(object sender, RoutedEventArgs e)
        {
            var current = ((RadioButton)sender).Name;

            if (SupervisiPengajuanSec != null)
            {
                ActionControlSupervisi.Visibility = SupervisiPengajuanSec.Visibility = current == "SupervisiPengajuan" ? Visibility.Visible : Visibility.Collapsed;
                if (current == "SupervisiPengajuan")
                {
                    HideFilter_Click(null, null);
                    if (DataContext is TransaksiBarangKeluarViewModel vm)
                    {
                        _ = ((AsyncCommandBase)vm.OnLoadCommand).ExecuteAsync(null);
                    }
                }
            }
            if (BarangTerprosesSec != null)
            {
                ActionControlDataTerproses.Visibility = BarangTerprosesSec.Visibility = current == "BarangTerproses" ? Visibility.Visible : Visibility.Collapsed;
                if (DataContext is TransaksiBarangKeluarViewModel vm)
                {
                    vm.OnFilterDataSelesaiCommand.Execute(null);
                }
            }
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
            if (DataContext is TransaksiBarangKeluarViewModel vm)
            {
                vm.FilterNomorTransaksi = null;
                vm.FilterNomorRegistrasi = null;
                vm.FilterTanggalPengajuanMulai = DateTime.Now;
                vm.FilterTanggalPengajuanSelesai = DateTime.Now;
                vm.IsNoTransaksiChecked = false;
                vm.IsNoRegistrasiChecked = false;
                vm.IsTanggalPengajuanChecked = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataContext is TransaksiBarangKeluarViewModel vm && vm.SelectedData != null)
            {
                var bodyCetak = new Dictionary<string, dynamic>()
                {
                    { "IdPengajuanPengeluaran", vm.SelectedData.IdPengajuanPengeluaran }
                };
                ((OnCetakCommand)vm.OnCetakPengajuanCommand).IsCetak = true;
                ((OnCetakCommand)vm.OnCetakPengajuanCommand).IsPreview = true;
                ((OnCetakCommand)vm.OnCetakPengajuanCommand).TemplateName = "DaftarPengajuanPengeluaranBarang";
                _ = ((AsyncCommandBase)vm.OnCetakPengajuanCommand).ExecuteAsync(bodyCetak);
            }
            else
            {
                _ = DialogHelper.ShowDialogGlobalViewAsync(false, true, "GudangRootDialog", "Cetak DPPB", "Data belum dipilih!", "warning", moduleName: "gudang");
            }
        }
    }
}
