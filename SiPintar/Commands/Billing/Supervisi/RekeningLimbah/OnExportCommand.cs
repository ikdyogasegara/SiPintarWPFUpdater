using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Commands.Billing.Supervisi.RekeningLimbah
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly RekeningLimbahViewModel _viewModel;
        public OnExportCommand(RekeningLimbahViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null || _viewModel.IsLoading)
                return;

            var param = (Dictionary<string, dynamic>)parameter;
            DataGrid Data = param["Data"];
            string FileType = param["FileType"];

            var SanitizedData = GetRawData(Data);

            string FileName = "Data Rekening Limbah";
            string TitlePage = "Data Rekening Limbah";

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

            await Task.FromResult(true);
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var Item = Data.ItemsSource as IList<RekeningLimbahDto>;
            var json = JsonConvert.SerializeObject(Item);
            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] ColumnsToBeDeleted = { "IdPdam", "FlagHapus", "WaktuUpdate", "IdUserRequest" };

            foreach (string ColName in ColumnsToBeDeleted)
            {
                if (data.Columns.Contains(ColName))
                    data.Columns.Remove(ColName);
            }

            return data;
        }
    }
}
