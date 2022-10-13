using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class ExportHelper
    {
        private static bool IsLoadingState;
        private static string _IdentifierDialog;

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        public static async Task ExportXlsx(object dataToExport, string fileName, string IdentifierDialog)
        {
            try
            {
                _IdentifierDialog = IdentifierDialog;
                SaveFileDialog confirm = DetermineExportLocation(fileName, "xlsx");

                if (confirm.ShowDialog() == true)
                {
                    IsLoading(true);

                    string saveAsLocation = confirm.FileName;

                    var dataTable = dataToExport as DataTable;

                    // Convert DataGrid To DataTable
                    if (dataToExport is DataGrid dataGrid)
                        dataTable = ConvertDataGridToDataTable(dataGrid);

                    // create workbook
                    var workbook = new XSSFWorkbook();
                    // the table named mySheet
                    var sheet = (XSSFSheet)workbook.CreateSheet();
                    // create the first row
                    var dataRow = (XSSFRow)sheet.CreateRow(0);

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        // create the cells in the first row, and add data into these cells circularly
                        var ColumnName = Regex.Replace(column.ColumnName, "(\\B[A-Z])", " $1");
                        dataRow.CreateCell(column.Ordinal).SetCellValue(ColumnName);
                    }
                    // create rows on the basis of data from datatable(not including table header), and add data into cells in every row
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        dataRow = (XSSFRow)sheet.CreateRow(i + 1);
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            dataRow.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
                        }
                    }
                    using (var ms = new MemoryStream())
                    {
                        using var fs = new FileStream(saveAsLocation, FileMode.Create, FileAccess.Write);
                        workbook.Write(fs);
                    }

                    IsLoading(false);

                    await DialogHost.Show(
                        new DialogGlobalView(
                            "Export File",
                            "Data berhasil di export ke file excel 2007 (xlsx)",
                            "success",
                            "Buka Lokasi File"
                         ), IdentifierDialog
                    );

                    // Open Directory after success
                    OpenDirectory(saveAsLocation, confirm.SafeFileName);
                }
            }
            catch (Exception)
            {
                IsLoading(false);

                await DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan export file. Pastikan tidak ada aplikasi yang menggunakan file bersangkutan (jika melakukan overwrite).",
                        "warning"
                    ), IdentifierDialog
                 );
            }
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        public static async Task ExportXls(object dataToExport, string fileName, string IdentifierDialog)
        {
            try
            {
                _IdentifierDialog = IdentifierDialog;
                SaveFileDialog confirm = DetermineExportLocation(fileName, "xls");

                if (confirm.ShowDialog() == true)
                {
                    IsLoading(true);

                    string saveAsLocation = confirm.FileName;

                    var dataTable = dataToExport as DataTable;

                    // Convert DataGrid To DataTable
                    if (dataToExport is DataGrid dataGrid)
                        dataTable = ConvertDataGridToDataTable(dataGrid);

                    var workbook = new HSSFWorkbook();

                    var sheet = workbook.CreateSheet();

                    int ColumnsCount = dataTable.Columns.Count;
                    int RowsCount = dataTable.Rows.Count;

                    // column headings
                    var headerRow = sheet.CreateRow(0);
                    for (int i = 0; i < ColumnsCount; i++)
                    {
                        var ColumnName = Regex.Replace(dataTable.Columns[i].ColumnName, "(\\B[A-Z])", " $1");

                        var cell = headerRow.CreateCell(i);
                        cell.SetCellValue(ColumnName);
                    }

                    for (int i = 0; i < RowsCount; i++)
                    {
                        var rowIndex = i + 1;
                        var row = sheet.CreateRow(rowIndex);
                        for (int j = 0; j < ColumnsCount; j++)
                        {
                            var cell = row.CreateCell(j);
                            cell.SetCellValue(dataTable.Rows[i][j].ToString());
                        }
                    }

                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        using var fs = new FileStream(saveAsLocation, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(fs);
                    }

                    IsLoading(false);

                    await DialogHost.Show(
                        new DialogGlobalView(
                            "Export File",
                            "Data berhasil di export ke file excel (xls)",
                            "success",
                            "Buka Lokasi File"
                         ), IdentifierDialog
                    );

                    // Open Directory after success
                    OpenDirectory(saveAsLocation, confirm.SafeFileName);
                }
            }
            catch (Exception)
            {
                IsLoading(false);

                await DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan export file. Pastikan tidak ada aplikasi yang menggunakan file bersangkutan (jika melakukan overwrite).",
                        "warning"
                    ), IdentifierDialog
                 );
            }
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        public static async Task ExportHtml(object dataToExport, string fileName, string titlePage, string IdentifierDialog)
        {
            try
            {
                _IdentifierDialog = IdentifierDialog;
                SaveFileDialog confirm = DetermineExportLocation(fileName, "html");

                if (confirm.ShowDialog() == true)
                {
                    IsLoading(true);

                    string saveAsLocation = confirm.FileName;

                    var dataTable = dataToExport as DataTable;

                    // Convert DataGrid To DataTable
                    if (dataToExport is DataGrid dataGrid)
                        dataTable = ConvertDataGridToDataTable(dataGrid);


                    // Format HTML
                    var strHTMLBuilder = new StringBuilder();
                    strHTMLBuilder.Append("<html>");
                    strHTMLBuilder.Append("<head>");
                    strHTMLBuilder.Append("</head>");
                    strHTMLBuilder.Append("<body>");
                    strHTMLBuilder.Append("<div style='text-align: left; margin-bottom: 20px;'>");
                    strHTMLBuilder.Append($"<h4 style='font-family:Montserrat;'>{titlePage}</h4>");
                    strHTMLBuilder.Append("</div>");
                    strHTMLBuilder.Append("<table border='0px' cellpadding='1' cellspacing='1' style='font-family: Sarabun; font-size:12px;'>");

                    strHTMLBuilder.Append("<tr style='background-color: #ebebeb; font-family:Montserrat; font-size:10px; font-weight: bold;'>");
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        var ColumnName = Regex.Replace(item.ColumnName, "(\\B[A-Z])", " $1");

                        strHTMLBuilder.Append("<td style='padding: 10px 10px 10px 10px;'>");
                        strHTMLBuilder.Append(ColumnName);
                        strHTMLBuilder.Append("</td>");
                    }
                    strHTMLBuilder.Append("</tr>");

                    int RowCounter = 0;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string AlternatingStyle = "";
                        if ((RowCounter % 2) != 0)
                        {
                            AlternatingStyle = "background-color: #eff3f8;";
                        }

                        strHTMLBuilder.Append("<tr>");
                        foreach (DataColumn item in dataTable.Columns)
                        {
                            strHTMLBuilder.Append($"<td style='padding: 0px 2px 0px 2px; {AlternatingStyle}'>");
                            strHTMLBuilder.Append(row[item.ColumnName].ToString());
                            strHTMLBuilder.Append("</td>");

                        }
                        strHTMLBuilder.Append("</tr>");
                        RowCounter++;
                    }

                    //Close tags.  
                    strHTMLBuilder.Append("</table>");
                    strHTMLBuilder.Append("</body>");
                    strHTMLBuilder.Append("</html>");

                    string HTMLContent = strHTMLBuilder.ToString();

                    File.WriteAllText(saveAsLocation, HTMLContent);

                    IsLoading(false);

                    await DialogHost.Show(
                        new DialogGlobalView(
                            "Export File",
                            "Data berhasil di export ke file web HTML",
                            "success",
                            "Buka Lokasi File"
                         ), IdentifierDialog
                    );

                    // Open Directory after success
                    OpenDirectory(saveAsLocation, confirm.SafeFileName);
                }
            }
            catch (Exception)
            {
                IsLoading(false);

                await DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan export file. Silahkan hubungi tim teknis terkait.",
                        "warning"
                    ), IdentifierDialog
                 );
            }
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        public static async Task ExportXml(object dataToExport, string fileName, string IdentifierDialog)
        {
            try
            {
                _IdentifierDialog = IdentifierDialog;
                SaveFileDialog confirm = DetermineExportLocation(fileName, "xml");

                if (confirm.ShowDialog() == true)
                {
                    IsLoading(true);

                    string saveAsLocation = confirm.FileName;

                    var dataTable = dataToExport as DataTable;

                    // Convert DataGrid To DataTable
                    if (dataToExport is DataGrid dataGrid)
                        dataTable = ConvertDataGridToDataTable(dataGrid);

                    var XMLContent = new DataSet();
                    XMLContent.Tables.Add(dataTable);

                    XMLContent.WriteXml(saveAsLocation);

                    IsLoading(false);

                    await DialogHost.Show(
                        new DialogGlobalView(
                            "Export File",
                            "Data berhasil di export ke file XML",
                            "success",
                            "Buka Lokasi File"
                         ), IdentifierDialog
                    );

                    // Open Directory after success
                    OpenDirectory(saveAsLocation, confirm.SafeFileName);
                }
            }
            catch (Exception)
            {
                IsLoading(false);

                await DialogHost.Show(
                    new DialogGlobalView(
                        "Terjadi Kesalahan",
                        "Tidak dapat melakukan export file. Silahkan hubungi tim teknis terkait.",
                        "warning"
                    ), IdentifierDialog
                 );
            }
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "<Pending>")]
        public static async Task ExportCsv(object dataToExport, string fileName, string IdentifierDialog)
        {
            _IdentifierDialog = IdentifierDialog;
            SaveFileDialog confirm = DetermineExportLocation(fileName, "csv");

            if (confirm.ShowDialog() == true)
            {
                string saveAsLocation = confirm.FileName;

                var dataTable = dataToExport as DataTable;

                // Convert DataGrid To DataTable
                if (dataToExport is DataGrid dataGrid)
                    dataTable = ConvertDataGridToDataTable(dataGrid);

                // Format
                var strBuilder = new StringBuilder();

                // header column
                int RowCounter = 0;
                foreach (DataColumn item in dataTable.Columns)
                {
                    var ColumnName = Regex.Replace(item.ColumnName, "(\\B[A-Z])", " $1");

                    strBuilder = AddCommaCsv(RowCounter, strBuilder);

                    strBuilder.Append(ColumnName);
                    RowCounter++;
                }
                strBuilder.Append('\n');

                // row data
                foreach (DataRow row in dataTable.Rows)
                {
                    RowCounter = 0;
                    foreach (DataColumn item in dataTable.Columns)
                    {
                        strBuilder = AddCommaCsv(RowCounter, strBuilder);

                        strBuilder.Append(row[item.ColumnName].ToString());
                        RowCounter++;
                    }
                    strBuilder.Append('\n');
                }

                string CSVContent = strBuilder.ToString();

                File.WriteAllText(saveAsLocation, CSVContent);

                IsLoading(false);

                await DialogHost.Show(
                    new DialogGlobalView(
                        "Export File",
                        "Data berhasil di export ke file CSV",
                        "success",
                        "Buka Lokasi File"
                        ), IdentifierDialog
                );

                // Open Directory after success
                OpenDirectory(saveAsLocation, confirm.SafeFileName);
            }
        }

        private static StringBuilder AddCommaCsv(int RowCounter, StringBuilder strBuilder)
        {
            if (RowCounter > 0)
                strBuilder.Append(',');

            return strBuilder;
        }

        #region Internal Func

        // Convert DataGrid To DataTable
        private static DataTable ConvertDataGridToDataTable(DataGrid dataToExport)
        {
            dataToExport.SelectAllCells();
            dataToExport.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataToExport);
            dataToExport.UnselectAllCells();
            string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" },
            StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            var dataTable = new DataTable();
            // 1st row must be column names; force upper case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dataTable.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dataTable.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dataTable.Rows.Add(Row);
            }

            return dataTable;
        }

        // File Export Location
        private static SaveFileDialog DetermineExportLocation(string FileName, string Extension)
        {
            string Filter = null;
            string Title = "Export File";

            switch (Extension)
            {
                case "xlsx":
                    Filter = "Excel 2007 File (xlsx)|*.xlsx";
                    Title = "Export File Excel (xlsx)";
                    break;
                case "xls":
                    Filter = "Excel File (xls)|*.xls";
                    Title = "Export File Excel (xls)";
                    break;
                case "csv":
                    Filter = "CSV File (csv)|*.csv";
                    Title = "Export File CSV (csv)";
                    break;
                case "html":
                    Filter = "HTML File (html)|*.html";
                    Title = "Export File HTML (html)";
                    break;
                case "xml":
                    Filter = "XML File (xml)|*.xml";
                    Title = "Export File XML (xml)";
                    break;
                default:
                    break;
            }

            // Directory
            string WindowsPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.System)).ToString();
            string SystemFolder = WindowsPath.Split(':')[0];

            string DirPath = Path.Combine(SystemFolder, ":", "Users", Environment.UserName, "Downloads");

            // Confirmation Save File Dialog
            var SaveDlg = new SaveFileDialog
            {
                Filter = Filter,
                Title = Title,
                InitialDirectory = null
            };
            SaveDlg.InitialDirectory = DirPath;
            SaveDlg.FileName = $"{FileName}.{Extension}";

            return SaveDlg;
        }

        // Function to Open Directory Location
        private static void OpenDirectory(string saveAsLocation, string FileName)
        {
            string Directory = saveAsLocation.Replace(FileName, string.Empty);

            var p = new Process
            {
                StartInfo = new ProcessStartInfo(Directory)
                {
                    UseShellExecute = true
                }
            };
            p.Start();
        }

        private static void IsLoading(bool IsShow)
        {
            if (IsShow)
            {
                _ = DialogHost.Show(new GlobalLoadingDialog(null, "Proses export sedang dijalankan", null), _IdentifierDialog);
                IsLoadingState = true;
            }
            else if (IsLoadingState)
            {
                DialogHost.Close(_IdentifierDialog);
                IsLoadingState = false;
            }
        }

        #endregion
    }
}
