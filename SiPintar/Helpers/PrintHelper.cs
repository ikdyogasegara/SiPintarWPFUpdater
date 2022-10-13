using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps.Packaging;
using AppBusiness.Data.DTOs;
using FastReport;
using FastReport.Data;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Other;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public class PrintHelper : IDisposable
    {
        #region Print Data with template

        private readonly string _templateInit = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
        private readonly string _templatePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template\\Laporan";
        public readonly string DesignerPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Libraries\\FastReport.Designer\\Designer.exe";
        public event EventHandler RunBeforeRender;
        public Report Report;

        public enum ObjectType
        {
            TextObject,
        }

        public enum PropertyType
        {
            Text, Height
        }

        private readonly List<AdditionalData> _additionalDataList = new List<AdditionalData>();

        public PrintHelper()
        {
            _additionalDataList.Clear();
        }

        public void PrintWithTemplate<T>(ObservableCollection<T> dataList, string templateName, EventHandler runBeforeRenderFunction = null)
        {
            var templateLocation = $"{_templatePath}\\{templateName}";

            var data = ObservableObject.ToDataSet(dataList);

            if (runBeforeRenderFunction != null)
                RunBeforeRender += runBeforeRenderFunction;

            ShowPreview(data, templateLocation);
        }

        public void ShowPreview(DataSet dataSet, string templatePath)
        {
            Prepare(dataSet, templatePath);

            var printWindow = new PrintPreviewWithTemplate();
            printWindow.ShowPage(Report);
        }

        public void Prepare(DataSet dataSet, string templatePath)
        {
            Report = new Report();
            Report.Load(templatePath);
            Report.RegisterData(dataSet);

            RunBeforeRender?.Invoke(Report, EventArgs.Empty);
            RenderAddData(Report);
            Report.Prepare();
        }

        public void RenderAddData(Report report)
        {
            foreach (var value in _additionalDataList)
            {
                switch (value.OType)
                {
                    case ObjectType.TextObject:
                        switch (value.PType)
                        {
                            case PropertyType.Text:
                                ((TextObject)report.FindObject(value.OField)).Text = value.OValue;
                                break;
                            case PropertyType.Height:
                                break;
                        }
                        break;
                }
            }
        }

        public void AddData(ObjectType oType, PropertyType pType, string oField, string oValue)
        {
            _additionalDataList.Add(new AdditionalData() { OType = oType, PType = pType, OField = oField, OValue = oValue });
        }

        #endregion

        #region Print Data with default table format (no template)

        public void PrintDataTable<T>(ObservableCollection<T> dataList, string title = null, string[] columnListToBeDelete = null, string paperSize = "A4", string orientation = "portrait")
        {
            var paperObject = PaperList().FirstOrDefault(i => i.PaperName == paperSize);

            var data = ObservableObject.ToDataTable(dataList);
            data = FilterDataTableColumn(data, columnListToBeDelete);

            var columnList = new List<string>();
            foreach (DataColumn column in data.Columns)
            {
                // create the cells in the first row, and add data into these cells circularly
                var columnName = Regex.Replace(column.ColumnName, "(\\B[A-Z])", " $1");
                columnList.Add(columnName);
            }

            var totalColumn = columnList.Count;

            // Flow Document properties
            var flowDoc = new FlowDocument();

            if (orientation == "landscape")
            {
                flowDoc.PageHeight = paperObject != null ? paperObject.Width : 595;
                flowDoc.PageWidth = paperObject != null ? paperObject.Height : 842;
                flowDoc.ColumnWidth = paperObject != null ? paperObject.Height : 842;
            }
            else
            {
                flowDoc.PageHeight = paperObject != null ? paperObject.Height : 842;
                flowDoc.PageWidth = paperObject != null ? paperObject.Width : 595;
                flowDoc.ColumnWidth = paperObject != null ? paperObject.Width : 595;
            }

            // Table
            var table = new Table();
            flowDoc.Blocks.Add(table);
            table.FontFamily = Application.Current.Resources["SarabunRegular"] as FontFamily;
            table.RowGroups.Add(new TableRowGroup());
            table.RowGroups[0].Rows.Add(new TableRow());

            var titleRow = table.RowGroups[0].Rows[0];
            titleRow.FontSize = 14;
            titleRow.FontWeight = FontWeights.Bold;

            titleRow.Cells.Add(new TableCell(new Paragraph(new Run(title))));
            titleRow.Cells[0].ColumnSpan = totalColumn;
            titleRow.Cells[0].Padding = new Thickness(0, 0, 0, 10);
            titleRow.Cells[0].TextAlignment = TextAlignment.Center;

            table.RowGroups[0].Rows.Add(new TableRow());

            // Header Row
            var headerRow = table.RowGroups[0].Rows[1];
            headerRow.FontSize = 6;
            headerRow.Background = Brushes.Silver;
            headerRow.FontWeight = FontWeights.Bold;

            for (var i = 0; i < totalColumn; i++)
            {
                headerRow.Cells.Add(new TableCell(new Paragraph(new Run(columnList[i]))));
                headerRow.Cells[i].Padding = new Thickness(5, 3, 5, 3);
                headerRow.Cells[i].TextAlignment = TextAlignment.Center;
            }

            // Content Row
            var rowCounter = 2;
            for (var i = 0; i < data.Rows.Count; i++)
            {
                table.RowGroups[0].Rows.Add(new TableRow());
                var contentRow = table.RowGroups[0].Rows[rowCounter];

                contentRow.FontSize = 6;
                contentRow.FontWeight = FontWeights.Normal;

                contentRow.Background = rowCounter % 2 != 0 ? Application.Current.Resources["CatskillWhite"] as Brush : Brushes.White;

                for (var j = 0; j < data.Columns.Count; j++)
                {
                    contentRow.Cells.Add(new TableCell(new Paragraph(new Run(data.Rows[i][j]?.ToString()))));
                }

                contentRow.Cells[2].TextAlignment = TextAlignment.Right;
                contentRow.Cells[3].TextAlignment = TextAlignment.Right;

                for (var k = 0; k < totalColumn; k++)
                {
                    contentRow.Cells[k].Padding = new Thickness(5, 0, 5, 0);
                }

                rowCounter++;
            }

            // Footer Row
            table.RowGroups[0].Rows.Add(new TableRow());
            var footerRow = table.RowGroups[0].Rows[rowCounter];
            footerRow.FontSize = 6;
            footerRow.Background = Brushes.Silver;
            footerRow.FontWeight = FontWeights.Bold;

            footerRow.Cells.Add(new TableCell(new Paragraph(new Run($"Menampilkan {string.Format("{0:N0}", data.Rows.Count)} data"))));

            footerRow.Cells[0].Padding = new Thickness(5, 0, 5, 0);
            footerRow.Cells[0].ColumnSpan = totalColumn;
            footerRow.Cells[0].TextAlignment = TextAlignment.Right;

            // show print preview
            ShowPreviewDataTable(flowDoc);
        }

        private void ShowPreviewDataTable(FlowDocument data)
        {
            var paginator = ((IDocumentPaginatorSource)data).DocumentPaginator;

            var dialog = new PrintDialog();
            paginator.PageSize = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

            var tempFileName = Path.GetRandomFileName();

            File.Delete(tempFileName);
            using var xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite);
            var writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(paginator);

            var previewWindow = new PrintPreviewNoTemplate { Document = xpsDocument.GetFixedDocumentSequence() };
            previewWindow.ShowDialog();
        }

        private DataTable FilterDataTableColumn(DataTable data, string[] columnListToBeDelete)
        {
            var result = data;

            if (columnListToBeDelete != null)
            {
                foreach (var colName in columnListToBeDelete)
                {
                    if (result.Columns.Contains(colName))
                        result.Columns.Remove(colName);
                }
            }

            return result;
        }

        public ObservableCollection<PaperObject> PaperList() => new ObservableCollection<PaperObject>() { new PaperObject() { PaperName = "A4", Height = 842, Width = 595 } };

        #endregion

        #region Print Data Kwitansi/BA/SPK/DSB

        public static DataTable CreateStructureDataTable(string name, ObservableCollection<KeyValuePair<string, Type>> structure)
        {
            var response = new DataTable(name);
            foreach (var item in structure)
            {
                response.Columns.Add(item.Key, item.Value);
            }

            return response;
        }

        public static DataTable CreateDataTable(DataTable dtTable, dynamic data, ObservableCollection<KeyValuePair<string, Type>> structure)
        {
            if (data is JArray)
            {
                foreach (JObject item in data)
                {
                    var row = dtTable.NewRow();
                    foreach (var val in structure)
                    {
                        if (item.GetValue(val.Key, StringComparison.OrdinalIgnoreCase) != null)
                        {
                            row[val.Key] = Convert.ChangeType(item.GetValue(val.Key, StringComparison.OrdinalIgnoreCase), val.Value);
                        }
                    }

                    dtTable.Rows.Add(row);
                }
            }
            else
            {
                var row = dtTable.NewRow();
                foreach (var val in structure)
                {
                    if (((JObject)data).GetValue(val.Key, StringComparison.OrdinalIgnoreCase) != null)
                    {
                        row[val.Key] = Convert.ChangeType(((JObject)data).GetValue(val.Key, StringComparison.OrdinalIgnoreCase), val.Value);
                    }
                }

                dtTable.Rows.Add(row);
            }

            return dtTable;
        }

        public static ObservableCollection<ParamTemplate> CreateStructureParam(ObservableCollection<KeyValuePair<string, Type>> structure)
        {
            var response = new ObservableCollection<ParamTemplate>();
            foreach (var item in structure)
            {
                response.Add(new ParamTemplate() { Name = item.Key, Tipe = item.Value, Value = null });
            }

            return response;
        }

        public static ObservableCollection<ParamTemplate> CreateParam(ObservableCollection<ParamTemplate> param, dynamic paramData)
        {
            if (paramData is JArray)
            {
                foreach (JObject data in paramData)
                {
                    foreach (var item in param)
                    {
                        if (data.GetValue(item.Name, StringComparison.OrdinalIgnoreCase) != null)
                        {
                            item.Value = Convert.ChangeType(data.GetValue(item.Name, StringComparison.OrdinalIgnoreCase), item.Tipe);
                        }
                    }
                }
            }
            else
            {
                if (paramData is JObject data)
                {
                    foreach (var item in param)
                    {
                        if (data.GetValue(item.Name, StringComparison.OrdinalIgnoreCase) != null)
                        {
                            item.Value = Convert.ChangeType(data.GetValue(item.Name, StringComparison.OrdinalIgnoreCase), item.Tipe);
                        }
                    }
                }
            }

            return param;
        }

        public static async Task<bool> PrepareTemplateAsync(string module, string templateName, CetakDto dataCetak, bool isTest = false, string title = "[Title]", string identifier = null, Func<string, string, string, string, bool, bool> errorHandling = null)
        {
            if (isTest)
                return true;
            await UpsertTemplateAsync(module, templateName, isTest);
            var pathTemplate = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
            var pathFile = $"{pathTemplate}\\{module}\\{templateName}.frx";
            var pathBase = $"{pathTemplate}\\Samples\\StarterTemplate.frx";
            if (File.Exists(pathFile) && File.Exists(pathBase))
            {
                try
                {
                    var reportBase = new Report();
                    reportBase.Load(pathBase);
                    var reportFile = new Report();
                    reportFile.Load(pathFile);
                    var pages = new ObservableCollection<ReportPage>();

                    reportBase.ScriptText = reportFile.ScriptText;

                    foreach (var item in reportFile.Pages)
                    {
                        pages.Add(item as ReportPage);
                    }
                    var totals = new Collection<Total>();
                    foreach (var item in reportFile.Dictionary.Totals)
                    {
                        totals.Add(item as Total);
                    }

                    reportBase.Pages.Clear();
                    foreach (var item in pages)
                    {
                        reportBase.Pages.Add(item);
                    }
                    reportBase.Dictionary.Totals.Clear();
                    foreach (var item in totals)
                    {
                        reportBase.Dictionary.Totals.Add(item);
                    }

                    var dataList = new ObservableCollection<DataTable>();
                    var param = dataCetak.ParamStructure is null ? new ObservableCollection<ParamTemplate>() : CreateStructureParam(dataCetak.ParamStructure);
                    foreach (var item in dataCetak.Data)
                    {
                        var dtTable = CreateStructureDataTable(item.Nama, item.DataStructure);
                        dataList.Add(dtTable);
                    }

                    var ct = 0;
                    foreach (var item in dataList)
                    {
                        ct++;
                        var saveName = item.TableName;
                        //check if exist
                        var check = (TableDataSource)reportBase.GetDataSource(saveName);
                        if (check is null)
                        {
                            var tds = (TableDataSource)reportBase.GetDataSource($"DSource{ct}");
                            if (tds != null)
                            {
                                tds.Enabled = true;
                                var ds = new DataSet();
                                item.TableName = $"DSource{ct}";
                                ds.Tables.Add(item);
                                reportBase.RegisterData(ds);
                                tds.Alias = saveName;
                            }
                        }
                        else
                        {
                            var ds = new DataSet();
                            item.TableName = $"DSource{ct}";
                            ds.Tables.Add(item);
                            reportBase.RegisterData(ds);
                        }
                    }

                    if (param != null)
                    {
                        reportBase.Parameters.Clear();
                        foreach (var item in param)
                        {
                            reportBase.Parameters.Add(new Parameter() { Name = item.Name, DataType = item.Tipe, Value = item.Value });
                        }
                    }

                    //add user login data to parameter
                    reportBase.Parameters.Add(new Parameter() { Name = "FullName", DataType = typeof(string) });
                    reportBase.Parameters.Add(new Parameter() { Name = "UserName", DataType = typeof(string) });
                    reportBase.Parameters.Add(new Parameter() { Name = "NoIdentitas", DataType = typeof(string) });
                    reportBase.Parameters.Add(new Parameter() { Name = "NamaLoketLogin", DataType = typeof(string) });

                    reportBase.Save(pathFile);
                }
                catch (Exception ex)
                {
                    if (errorHandling != null)
                    {
                        Serilog.Log.Error(ex.Message);
                        _ = errorHandling(identifier, module.ToLower(), title, "Template tidak valid!", isTest);
                    }
                    return false;
                }
            }
            return true;
        }

        public static async Task CetakAsync(string module, string templateName, CetakDto dataCetak, Func<ParamShowInfo, string, string, bool, bool> showInfo = null,
            bool isTest = false, string title = "[Title]", bool directPrint = false)
        {
            if (isTest)
                return;
            await UpsertTemplateAsync(module, templateName, isTest);
            var pathTemplate = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
            var pathFile = $"{pathTemplate}\\{module}\\{templateName}.frx";
            var pathBase = $"{pathTemplate}\\Samples\\StarterTemplate.frx";
            if (File.Exists(pathFile) && File.Exists(pathBase))
            {
                try
                {
                    var reportBase = new Report();
                    reportBase.Load(pathBase);
                    var reportFile = new Report();
                    reportFile.Load(pathFile);
                    var pages = new ObservableCollection<ReportPage>();
                    var rebindObj = new ObservableCollection<Dictionary<string, dynamic>>();

                    reportBase.ScriptText = reportFile.ScriptText;

                    foreach (var item in reportFile.Pages)
                    {
                        pages.Add(item as ReportPage);
                        var idxPage = reportFile.Pages.IndexOf(item as Base);
                        foreach (var em in ((ReportPage)item).AllObjects)
                        {
                            if (em is DataBand band)
                            {
                                var idxObj = ((ReportPage)item).AllObjects.IndexOf(em as DataBand);
                                rebindObj.Add(new Dictionary<string, dynamic>() { { "PageIdx", idxPage }, { "ObjIdx", idxObj }, { "DataSourceAlias", band.DataSource?.Alias }, });
                            }
                        }
                    }

                    reportBase.Pages.Clear();
                    foreach (var item in pages)
                    {
                        reportBase.Pages.Add(item);
                    }

                    var dataList = new ObservableCollection<DataTable>();
                    var param = CreateStructureParam(dataCetak.ParamStructure);
                    param = CreateParam(param, dataCetak.Param);
                    foreach (var item in dataCetak.Data)
                    {
                        var dtTable = CreateStructureDataTable(item.Nama, item.DataStructure);
                        dtTable = CreateDataTable(dtTable, item.Data, item.DataStructure);
                        dataList.Add(dtTable);
                    }

                    var ct = 0;
                    foreach (var item in dataList)
                    {
                        ct++;
                        var saveName = item.TableName;
                        //check if exist
                        var check = (TableDataSource)reportBase.GetDataSource(saveName);
                        if (check is null)
                        {
                            var tds = (TableDataSource)reportBase.GetDataSource($"DSource{ct}");
                            if (tds != null)
                            {
                                tds.Enabled = true;
                                var ds = new DataSet();
                                item.TableName = $"DSource{ct}";
                                ds.Tables.Add(item);
                                reportBase.RegisterData(ds);
                                tds.Alias = saveName;
                            }
                        }
                        else
                        {
                            var ds = new DataSet();
                            item.TableName = $"DSource{ct}";
                            ds.Tables.Add(item);
                            reportBase.RegisterData(ds);
                        }
                    }

                    if (param != null)
                    {
                        reportBase.Parameters.Clear();
                        foreach (var item in param)
                        {
                            reportBase.Parameters.Add(new Parameter() { Name = item.Name, DataType = item.Tipe, Value = item.Value });
                        }
                    }

                    foreach (var item in rebindObj)
                    {
                        var dband = ((ReportPage)reportBase.Pages[item["PageIdx"]]).AllObjects[item["ObjIdx"]] as DataBand;
                        if (item["DataSourceAlias"] != null)
                        {
                            dband.DataSource = reportBase.GetDataSource(item["DataSourceAlias"]);
                        }
                    }

                    reportBase.Save(pathFile);

                    reportBase.Prepare();
                    if (directPrint)
                    {
                        reportBase.PrintSettings.ShowDialog = false;
                        reportBase.PrintPrepared();
                    }
                    else
                    {
                        var printWindow = new PrintPreviewWithTemplate();
                        printWindow.ShowPage(reportBase);
                    }
                }
                catch (Exception ex)
                {
                    if (showInfo != null)
                    {
                        Console.WriteLine(ex.Message);
                        _ = showInfo(new ParamShowInfo() { Status = false, Message = "Template tidak valid." }, title, module.ToLower(), isTest);
                    }
                }
            }
        }

        public static async Task CetakAsync(string module, string templateName, CetakDto dataCetak, Func<string, string, string, string, bool, bool> errorHandling = null,
            bool isTest = false, string title = "[Title]", string identifier = null, bool directPrint = false, ParamMasterReportDataWpf filterData = null)
        {
            if (isTest)
                return;
            await UpsertTemplateAsync(module, templateName, isTest);
            var pathTemplate = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
            var pathFile = $"{pathTemplate}\\{module}\\{templateName}.frx";
            var pathBase = $"{pathTemplate}\\Samples\\StarterTemplate.frx";
            if (File.Exists(pathFile) && File.Exists(pathBase))
            {
                try
                {
                    var reportBase = new Report();
                    reportBase.Load(pathBase);
                    var reportFile = new Report();
                    reportFile.Load(pathFile);
                    var pages = new ObservableCollection<ReportPage>();
                    var totals = new Collection<Total>();

                    reportBase.ScriptText = reportFile.ScriptText;

                    foreach (var item in reportFile.Dictionary.Totals)
                    {
                        totals.Add(item as Total);
                    }
                    var rebindObj = new ObservableCollection<Dictionary<string, dynamic>>();
                    foreach (var item in reportFile.Pages)
                    {
                        pages.Add(item as ReportPage);
                        var idxPage = reportFile.Pages.IndexOf(item as Base);
                        foreach (var em in ((ReportPage)item).AllObjects)
                        {
                            if (em is DataBand band)
                            {
                                var idxObj = ((ReportPage)item).AllObjects.IndexOf(em as DataBand);
                                rebindObj.Add(new Dictionary<string, dynamic>() { { "PageIdx", idxPage }, { "ObjIdx", idxObj }, { "DataSourceAlias", band.DataSource?.Alias }, });
                            }
                        }
                    }

                    reportBase.Pages.Clear();
                    foreach (var item in pages)
                    {
                        reportBase.Pages.Add(item);
                    }
                    reportBase.Dictionary.Totals.Clear();
                    foreach (var item in totals)
                    {
                        reportBase.Dictionary.Totals.Add(item);
                    }

                    var dataList = new ObservableCollection<DataTable>();
                    var param = CreateStructureParam(dataCetak.ParamStructure);
                    param = CreateParam(param, dataCetak.Param);
                    foreach (var item in dataCetak.Data)
                    {
                        var dtTable = CreateStructureDataTable(item.Nama, item.DataStructure);
                        dtTable = CreateDataTable(dtTable, item.Data, item.DataStructure);
                        dataList.Add(dtTable);
                    }

                    var ct = 0;
                    foreach (var item in dataList)
                    {
                        ct++;
                        var saveName = item.TableName;
                        //check if exist
                        var check = (TableDataSource)reportBase.GetDataSource(saveName);
                        if (check is null)
                        {
                            var tds = (TableDataSource)reportBase.GetDataSource($"DSource{ct}");
                            if (tds != null)
                            {
                                tds.Enabled = true;
                                var ds = new DataSet();
                                item.TableName = $"DSource{ct}";
                                ds.Tables.Add(item);
                                reportBase.RegisterData(ds);
                                tds.Alias = saveName;
                            }
                        }
                        else
                        {
                            var ds = new DataSet();
                            item.TableName = $"DSource{ct}";
                            ds.Tables.Add(item);
                            reportBase.RegisterData(ds);
                        }
                    }

                    if (param != null)
                    {
                        reportBase.Parameters.Clear();
                        foreach (var item in param)
                        {
                            reportBase.Parameters.Add(new Parameter() { Name = item.Name, DataType = item.Tipe, Value = item.Value });
                        }
                    }

                    foreach (var item in rebindObj)
                    {
                        var dband = ((ReportPage)reportBase.Pages[item["PageIdx"]]).AllObjects[item["ObjIdx"]] as DataBand;
                        if (item["DataSourceAlias"] != null)
                        {
                            dband.DataSource = reportBase.GetDataSource(item["DataSourceAlias"]);
                        }
                    }

                    if (filterData != null)
                    {
                        foreach (var item in filterData.Filters)
                        {
                            if (item.ParamType == "list")
                            {
                                reportBase.SetParameterValue(item.ListDataDetail.FieldDisplayValue1, item.ListDisplay1);
                                reportBase.SetParameterValue(item.ListDataDetail.FieldDisplayValue2, item.ListDisplay2);
                            }
                            else if (item.ParamType == "custom_list")
                            {
                                reportBase.SetParameterValue($"kode{item.ListCustomDataDetail.Nama}", item.ListDisplay1);
                                reportBase.SetParameterValue($"nama{item.ListCustomDataDetail.Nama}", item.ListDisplay2);
                            }
                            else if (item.ParamType == "range")
                            {
                                if (item.DataType.Contains("date"))
                                {

                                    reportBase.SetParameterValue($"{item.Name.Replace(" ", "")}Awal", DateTime.ParseExact(item.Value1, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                                    reportBase.SetParameterValue($"{item.Name.Replace(" ", "")}Akhir", DateTime.ParseExact(item.Value2, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    reportBase.SetParameterValue($"{item.Name.Replace(" ", "")}Awal", item.Value1);
                                    reportBase.SetParameterValue($"{item.Name.Replace(" ", "")}Akhir", item.Value2);
                                }
                            }
                            else
                            {
                                if (item.DataType.Contains("date"))
                                {
                                    reportBase.SetParameterValue(item.Name.Replace(" ", ""), DateTime.ParseExact(item.Value1, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
                                }
                                else
                                {
                                    reportBase.SetParameterValue(item.Name.Replace(" ", ""), item.Value1);
                                }
                            }
                        }
                    }

                    //add user login data to parameter
                    reportBase.SetParameterValue("FullName", Application.Current.Resources["FullName"]?.ToString());
                    reportBase.SetParameterValue("UserName", Application.Current.Resources["UserName"]?.ToString());
                    reportBase.SetParameterValue("NoIdentitas", Application.Current.Resources["NoIdentitas"]?.ToString());
                    reportBase.SetParameterValue("NamaLoketLogin", Application.Current.Resources["NamaLoket"]?.ToString());

                    reportBase.Save(pathFile);

                    reportBase.Prepare();
                    if (directPrint)
                    {
                        reportBase.PrintSettings.ShowDialog = false;
                        reportBase.PrintPrepared();
                    }
                    else
                    {
                        var printWindow = new PrintPreviewWithTemplate();
                        printWindow.ShowPage(reportBase);
                    }
                }
                catch (Exception ex)
                {
                    if (errorHandling != null)
                    {
                        Serilog.Log.Error(ex.Message);
                        _ = errorHandling(identifier, module.ToLower(), title, "Template tidak valid!", isTest);
                    }
                }
            }
        }


        public static Task<bool> UpsertTemplateAsync(string module, string templateName, bool isTest = false)
        {
            var pathTemplate = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
            var pathFile = $"{pathTemplate}\\{module}\\{templateName}.frx";
            if (!Directory.Exists(Path.GetDirectoryName(pathFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
            }

            if (!File.Exists(pathFile))
            {
                File.Copy($"{pathTemplate}\\Samples\\StarterTemplate.frx", pathFile);
            }
            _ = isTest;
            return Task.FromResult(true);
        }

        #endregion

        public static void OpenFileInDesigner(string module, string fileName, bool isTest = false)
        {
            if (!isTest)
            {
                var pathTemplate = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Template";
                var filePath = $"{pathTemplate}\\{module}\\{fileName}.frx";
                var designerPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Libraries\\FastReport.Designer\\Designer.exe";
                Process.Start(designerPath, filePath);
            }
        }

        public static dynamic GetPropertiClass(dynamic dto)
        {
            var result = new ObservableCollection<KeyValuePair<string, Type>>();
            foreach (var p in dto.GetProperties())
            {
                result.Add(new KeyValuePair<string, Type>(p.Name, p.PropertyType));
            }

            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    [ExcludeFromCodeCoverage]
    public class ParamShowInfo
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ParamReportData
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class AdditionalData
    {
        public PrintHelper.ObjectType OType { get; set; }
        public PrintHelper.PropertyType PType { get; set; }
        public string OField { get; set; }
        public string OValue { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PaperObject
    {
        public string PaperName { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ParamTemplate
    {
        public string Name { get; set; }
        public Type Tipe { get; set; }
        public dynamic Value { get; set; }
    }
}
