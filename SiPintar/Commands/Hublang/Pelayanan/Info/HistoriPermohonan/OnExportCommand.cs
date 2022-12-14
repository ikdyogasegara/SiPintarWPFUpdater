using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Commands.Hublang.Pelayanan.Info.HistoriPermohonan
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly HistoriPermohonanViewModel _viewModel;
        public OnExportCommand(HistoriPermohonanViewModel viewModel)
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

            string FileName = "Histori Permohonan";
            string TitlePage = "Data histori permohonan";

            switch (FileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(Data, FileName, TitlePage, "HublangRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(Data, FileName, "HublangRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(Data, FileName, "HublangRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(Data, FileName, "HublangRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(Data, FileName, "HublangRootDialog");
                    break;
                default:
                    break;
            }

            await Task.FromResult(true);
        }

    }
}
