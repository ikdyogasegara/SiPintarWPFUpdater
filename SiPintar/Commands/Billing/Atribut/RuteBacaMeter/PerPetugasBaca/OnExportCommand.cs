using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Atribut.RuteBacaMeter;

namespace SiPintar.Commands.Billing.Atribut.RuteBacaMeter.PerPetugasBaca
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly PerPetugasBacaViewModel _viewModel;
        private readonly bool _isTest;

        public OnExportCommand(PerPetugasBacaViewModel viewModel, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //if (parameter == null || _viewModel.IsLoading || _viewModel.DataList == null || _viewModel.DataList.Count == 0)
            //    return;

            var param = (Dictionary<string, dynamic>)parameter;
            DataGrid Data = param["Data"];
            string FileType = param["FileType"];

            var SanitizedData = GetRawData(Data);

            string FileName = $"Rute Baca Petugas";
            string TitlePage = $"Rute Baca Petugas";

            switch (FileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(SanitizedData, FileName, TitlePage, "BillingRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(SanitizedData, FileName, "BillingRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(SanitizedData, FileName, "BillingRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(SanitizedData, FileName, "BillingRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(SanitizedData, FileName, "BillingRootDialog");
                    break;
                default:
                    break;
            }

            await Task.FromResult(_isTest);
        }

        private DataTable GetRawData(DataGrid Data)
        {
            DataTable result = null;

            try
            {
                var Item = Data.ItemsSource as IList<dynamic>;
                var json = JsonConvert.SerializeObject(Item);
                result = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

                string[] ColumnsToBeDeleted = { "IdPdam", "IdUserRequest" };

                foreach (string ColName in ColumnsToBeDeleted)
                {
                    if (result.Columns.Contains(ColName))
                        result.Columns.Remove(ColName);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return result;
        }
    }
}
