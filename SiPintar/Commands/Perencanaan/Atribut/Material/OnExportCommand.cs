using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.ViewModels.Perencanaan.Atribut;

namespace SiPintar.Commands.Perencanaan.Atribut.Material
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly MaterialViewModel _viewModel;
        public OnExportCommand(MaterialViewModel viewModel)
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

            string FileName = "Data Material";
            string TitlePage = "Data Material";

            switch (FileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(SanitizedData, FileName, TitlePage, "PerencanaanRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(SanitizedData, FileName, "PerencanaanRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(SanitizedData, FileName, "PerencanaanRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(SanitizedData, FileName, "PerencanaanRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(SanitizedData, FileName, "PerencanaanRootDialog");
                    break;
                default:
                    break;
            }

            await Task.FromResult(true);
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var Item = Data.ItemsSource as IList<MasterMaterialDto>;
            var json = JsonConvert.SerializeObject(Item);
            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] ColumnsToBeDeleted = { "IdPdam", "IdMaterial", "FlagHapus", "WaktuUpdate", "IdUserRequest" };

            foreach (string ColName in ColumnsToBeDeleted)
            {
                if (data.Columns.Contains(ColName))
                    data.Columns.Remove(ColName);
            }

            return data;
        }
    }
}
