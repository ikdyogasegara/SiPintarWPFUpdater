using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Helpers;
using SiPintar.ViewModels.Bacameter.SistemKontrol.TarifGolongan;

namespace SiPintar.Commands.Bacameter.SistemKontrol.TarifGolongan.Golongan
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly GolonganViewModel _viewModel;
        public OnExportCommand(GolonganViewModel viewModel)
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

            string FileName = "Master Tarif Golongan";
            string TitlePage = "Data Master Tarif Golongan";

            switch (FileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(Data, FileName, TitlePage, "BacameterRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(Data, FileName, "BacameterRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(Data, FileName, "BacameterRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(Data, FileName, "BacameterRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(Data, FileName, "BacameterRootDialog");
                    break;
                default:
                    break;
            }

            await Task.FromResult(true);
        }

    }
}
