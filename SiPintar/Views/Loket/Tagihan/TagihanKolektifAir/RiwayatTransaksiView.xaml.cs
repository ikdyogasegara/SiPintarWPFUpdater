using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SiPintar.Commands;
using SiPintar.Commands.Global;
using SiPintar.Helpers;
using SiPintar.ViewModels.Loket.Tagihan.TagihanKolektifAir;
using SiPintar.Views.Loket.Dialog;

namespace SiPintar.Views.Loket.Tagihan.TagihanKolektifAir
{
    public partial class RiwayatTransaksiView : UserControl
    {
        public RiwayatTransaksiView()
        {
            InitializeComponent();
            PanelGridRiwayat.Visibility = Visibility.Collapsed;
            FilterSection.Visibility = Visibility.Collapsed;
        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.CurrentPage > 1)
                {
                    vm.CurrentPage--;
                    SearchData();
                }
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.CurrentPage < vm.TotalPage)
                {
                    vm.CurrentPage++;
                    SearchData();
                }
            }
        }

        private void HideFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterWrapper.Width = new GridLength(0);
            FilterSection.Visibility = Visibility.Collapsed;
            ToggleShowFilter.Visibility = Visibility.Visible;
        }

        private void ShowFilter_Click(object sender = null, RoutedEventArgs e = null)
        {
            FilterWrapper.Width = new GridLength(210);
            FilterSection.Visibility = Visibility.Visible;
            ToggleShowFilter.Visibility = Visibility.Collapsed;
        }

        private void OpenFotoMeter_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel { SelectedData: { } } vm)
            {
                var bulan = vm.SelectedData.KodePeriode.ToString().Right(2);
                var tahun = vm.SelectedData.KodePeriode.ToString().Mid(2, 2);

                _ = DialogHost.Show(new FotoMeterView(bulan, tahun, vm.SelectedData.NoSamb), "LoketRootDialog");
            }
        }

        private void Cetak_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel { SelectedData: { } } vm)
            {
                var bodyCetak = new Dictionary<string, dynamic> { { "IdUser", Application.Current.Resources["IdUser"]?.ToString() }, { "IncludeData", true }, };

                switch (vm.FilterTipeTransaksi.Tipe)
                {
                    case "Pembayaran Rek. Air":
                        bodyCetak.Add("RekeningAir", new List<BayarRekeningAir>() { new BayarRekeningAir() { IdPelangganAir = vm.SelectedData.IdPelangganAir, IdPeriode = vm.SelectedData.IdPeriode }, });
                        break;
                    case "Pembayaran Rek. Limbah":
                        bodyCetak.Add("RekeningLimbah", new List<BayarRekeningLimbah>() { new BayarRekeningLimbah() { IdPelangganLimbah = vm.SelectedData.IdPelangganLimbah, IdPeriode = vm.SelectedData.IdPeriode }, });
                        break;
                    case "Pembayaran Rek. L2T2":
                        bodyCetak.Add("RekeningLltt", new List<BayarRekeningLltt>() { new BayarRekeningLltt() { IdPelangganLltt = vm.SelectedData.IdPelangganLltt, IdPeriode = vm.SelectedData.IdPeriode }, });
                        break;
                    case "Pembayaran Rek. Non Air":
                        bodyCetak.Add("RekeningNonAir", new List<BayarRekeningNonAir>() { new BayarRekeningNonAir() { IdNonAir = vm.SelectedData.IdNonAir, }, });
                        break;
                }

                ((OnCetakCommand)vm.OnCetakCommand).IsCetak = true;
                ((OnCetakCommand)vm.OnCetakCommand).IsPreview = true;
                ((OnCetakCommand)vm.OnCetakCommand).Method = CetakApiMethod.POST;
                ((OnCetakCommand)vm.OnCetakCommand).TemplateName = "KwitansiTagihan";
                _ = ((AsyncCommandBase)vm.OnCetakCommand).ExecuteAsync(bodyCetak);
                ((OnCetakCommand)vm.OnCetakCommand).Method = CetakApiMethod.GET;
            }
        }

        private void Batalkan_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel { SelectedData: { } } vm)
            {
                if (vm.SelectedData.WaktuTransaksi == null)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                        false,
                        true,
                        "LoketRootDialog",
                        "Waktu Transaksi Tidak Ditemukan",
                        $"Waktu Transaksi Tidak Ditemukan Pada Data!",
                        "warning",
                        moduleName: "loket");
                }
                else if (vm.SelectedData.WaktuTransaksi.Value.Date != DateTime.Now.Date && !vm.ParentPage.IsNamaRoleAdmin)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                        false,
                        true,
                        "LoketRootDialog",
                        "Waktu Transaksi Tidak Valid",
                        $"Waktu transaksi harus sama dengan tanggal saat ini !",
                        "warning",
                        moduleName: "loket");
                }
                else
                {
                    _ = DialogHost.Show(new BatalkanFormView(vm), "LoketRootDialog");
                }
            }
        }

        private void BatalkanPerNoLpp_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel { SelectedData: { } } vm)
            {
                if (vm.SelectedData.WaktuTransaksi == null)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                        false,
                        true,
                        "LoketRootDialog",
                        "Waktu Transaksi Tidak Ditemukan",
                        $"Waktu Transaksi Tidak Ditemukan Pada Data!",
                        "warning",
                        moduleName: "loket");
                }
                else if (vm.SelectedData.WaktuTransaksi.Value.Date != DateTime.Now.Date && !vm.ParentPage.IsNamaRoleAdmin)
                {
                    _ = DialogHelper.ShowDialogGlobalViewAsync(
                        false,
                        true,
                        "LoketRootDialog",
                        "Waktu Transaksi Tidak Valid",
                        $"Waktu transaksi harus sama dengan tanggal saat ini !",
                        "warning",
                        moduleName: "loket");
                }
                else
                {
                    _ = DialogHost.Show(new BatalkanPerNoLppFormView(vm, vm.SelectedData.NomorTransaksi, DateTime.Now), "LoketRootDialog");
                }
            }
        }

        #region Export Function

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = FindResource("ExportMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            _ = (RiwayatTransaksiViewModel)DataGridRiwayat.DataContext;
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridRiwayat);

            var fileName = $"Riwayat Transaksi";
            var titlePage = $"Data Riwayat Transaksi";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "LoketRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, "LoketRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, "LoketRootDialog");
                    break;
                default:
                    break;
            }
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var item = Data.ItemsSource as IList<RiwayatPelunasanDanPembatalanWpf>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings()
            {
                ContractResolver = new IgnorePropertiesResolver(
                    new[] { "ParameterWpf", "Parameter", "NonAirReguler", "SPK", "BeritaAcara", "Pelaksana", "RAB" })
            });

            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] columnsToBeDeleted = { "IdPdam" };

            var list = new List<string>(columnsToBeDeleted.ToList());

            foreach (var i in data.Columns)
            {
                if (AppExtensionsHelpers.Right(i.ToString(), 3) == "Wpf")
                {
                    list.Add(i.ToString());
                }
            }

            columnsToBeDeleted = list.ToArray();

            foreach (var colName in columnsToBeDeleted)
            {
                if (data.Columns.Contains(colName))
                {
                    data.Columns.Remove(colName);
                }
            }

            return data;
        }

        public class IgnorePropertiesResolver : DefaultContractResolver
        {
            private readonly HashSet<string> _ignoreProps;

            public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
            {
                _ignoreProps = new HashSet<string>(propNamesToIgnore);
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                if (_ignoreProps.Contains(property.PropertyName))
                {
                    property.ShouldSerialize = _ => false;
                }

                return property;
            }
        }

        #endregion

        private void Kembali_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                vm.ParentPage.IsFullPage = false;

                if (vm.ParentPage.PageSebelumRiwayat == "DetailTagihan")
                {
                    if (vm.Parent.ListSelectedPelangganAir != null && vm.Parent.ListSelectedPelangganAir.Count > 0)
                    {
                        vm.Parent.UpdatePage("DetailTagihanByIdPelangganAir", vm.ParentPage.TanggalTransaksi);
                    }

                    if (vm.Parent.ListSelectedNonAir != null && vm.Parent.ListSelectedNonAir.Count > 0)
                    {
                        vm.Parent.UpdatePage("DetailTagihanByIdNonAir", vm.ParentPage.TanggalTransaksi);
                    }
                }
                else
                {
                    vm.Parent.ListSelectedPelangganAir = null;
                    vm.Parent.ListSelectedNonAir = null;
                    vm.Parent.UpdatePage("SearchPage");
                }
            }
        }

        private void TipeTransaksi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.IdPelangganAir != null || vm.IdNonAir != null)
                {
                    Refresh_Click(null, null);
                }
            }
        }

        private void RiwayatByIdPelangganAir_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.SelectedPelangganAir != null)
                {
                    vm.IdNonAir = null;
                    vm.IdPelangganAir = vm.SelectedPelangganAir.IdPelangganAir;
                    InfoPelanggan.Text = vm.SelectedPelangganAir.Nama;
                    PanelGridRiwayat.Visibility = Visibility.Visible;
                    FilterSection.Visibility = Visibility.Visible;
                    Refresh_Click(null, null);
                }
            }
        }

        private void RiwayatByIdNonAir_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.SelectedNonAir != null)
                {
                    vm.IdNonAir = vm.SelectedNonAir.IdNonAir;
                    vm.IdPelangganAir = vm.SelectedNonAir.IdPelangganAir;
                    InfoPelanggan.Text = vm.SelectedNonAir.Nama;
                    PanelGridRiwayat.Visibility = Visibility.Visible;
                    FilterSection.Visibility = Visibility.Visible;
                    Refresh_Click(null, null);
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                vm.CurrentPage = 1;
                SearchData();
            }
        }

        private void SearchData()
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                _ = ((AsyncCommandBase)vm.OnRefreshCommand).ExecuteAsync(null);
            }
        }

        private void DataGrid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (DataContext is RiwayatTransaksiViewModel vm)
            {
                if (vm.SelectedData is null)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
