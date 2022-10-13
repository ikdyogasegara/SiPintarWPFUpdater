using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;


namespace SiPintar.Views.Akuntansi.PostingKeuangan
{
    /// <summary>
    /// Interaction logic for PengeluaranLainnyaView.xaml
    /// </summary>
    public partial class PengeluaranLainnyaView : UserControl
    {
        public PengeluaranLainnyaView()
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

        private void DataGridPengeluaranLainnya_ContextMenuOpening(object sender, ContextMenuEventArgs e)
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
            var cm = (FindResource("ExportMenu") as ContextMenu)!;
            cm.PlacementTarget = sender as Button;
            cm.Placement = PlacementMode.Bottom;
            cm.IsOpen = true;
        }

        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            var fileType = ((MenuItem)sender).Tag.ToString();
            var sanitizedData = GetRawData(DataGridPengeluaranLainnya);

            var fileName = $"Pengeluaran Lainnya";
            var titlePage = $"Data Pengeluaran Lainnya";

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
            var item = dataGrid.ItemsSource as IList<DaftarBiayaKasDataGridDto>;
            var json = JsonConvert.SerializeObject(item, new JsonSerializerSettings());
            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable))!;

            string[] columnsToBeDeleted = {
                "IdPdam",
                "IdDaftarBiayaKas",
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

