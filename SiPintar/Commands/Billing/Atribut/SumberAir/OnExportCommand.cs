﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using SiPintar.Helpers;
using SiPintar.ViewModels.Billing.Atribut;

namespace SiPintar.Commands.Billing.Atribut.SumberAir
{
    [ExcludeFromCodeCoverage]
    public class OnExportCommand : AsyncCommandBase
    {
        private readonly SumberAirViewModel _viewModel;
        public OnExportCommand(SumberAirViewModel viewModel)
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

            string FileName = "Master Sumber Air";
            string TitlePage = "Data Master Sumber Air";

            switch (FileType)
            {
                case "html":
                    _ = ExportHelper.ExportHtml(Data, FileName, TitlePage, "BillingRootDialog");
                    break;
                case "xlsx":
                    _ = ExportHelper.ExportXlsx(Data, FileName, "BillingRootDialog");
                    break;
                case "xls":
                    _ = ExportHelper.ExportXls(Data, FileName, "BillingRootDialog");
                    break;
                case "xml":
                    _ = ExportHelper.ExportXml(Data, FileName, "BillingRootDialog");
                    break;
                case "csv":
                    _ = ExportHelper.ExportCsv(Data, FileName, "BillingRootDialog");
                    break;
                default:
                    break;
            }

            await Task.FromResult(true);
        }

    }
}
