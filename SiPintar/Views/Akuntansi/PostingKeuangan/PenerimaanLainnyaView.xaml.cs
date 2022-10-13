using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppBusiness.Data.DTOs;
using AppBusiness.Data.Helpers;
using Newtonsoft.Json;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Views.Akuntansi.PostingKeuangan
{
    public partial class PenerimaanLainnyaView : UserControl
    {
        public PenerimaanLainnyaView()
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

        private void DataGridUser_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void OnPrevPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PenerimaanLainnyaViewModel vm)
            {
                vm.CurrentPage--;
                vm.OnRefreshCommand.Execute(null);
            }
        }

        private void OnNextPageClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is PenerimaanLainnyaViewModel vm)
            {
                vm.CurrentPage++;
                vm.OnRefreshCommand.Execute(null);
            }
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Tgl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var cm = (FindResource("ExportMenu") as ContextMenu)!;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridPenerimaanLainnya);

            var fileName = $"Penerimaan Lainnya";
            var titlePage = $"Data Penerimaan Lainnya";

            switch (fileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(sanitizedData, fileName, titlePage, "AkuntansiRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(sanitizedData, fileName, "AkuntansiRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(sanitizedData, fileName, "AkuntansiRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(sanitizedData, fileName, "AkuntansiRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(sanitizedData, fileName, "AkuntansiRootDialog");
                    break;
                default:
                    break;
            }
        }

        private static object GetRawData(DataGrid dataGrid)
        {
            var item = dataGrid.ItemsSource as IList<DaftarPenerimaanKasDataGridDto>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings());
            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable))!;

            string[] columnsToBeDeleted = {
                "IdPdam",
                "IdDaftarPenerimaanKas",
                "IdPeriode",
                "IdWilayah",
                "IdPerkiraan3Debet",
                "IdPerkiraan3Kredit",
                "Kategori",
                "SumberData",
                "FlagHapus",
                "WaktuUpdate",
                "StatusPostingText",
            };

            foreach (var colName in columnsToBeDeleted)
            {
                if (data.Columns.Contains(colName))
                {
                    data.Columns.Remove(colName);
                }
            }

            return data;
        }
    }
}

