using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using SiPintar.Helpers;
using SiPintar.ViewModels.Hublang.Pelayanan;

namespace SiPintar.Commands.Hublang.Pelayanan.BAPengembalian
{
    [ExcludeFromCodeCoverage]
    public class OnExportExcelCommand : AsyncCommandBase
    {
        private readonly BaPengembalianViewModel _viewModel;
        public OnExportExcelCommand(BaPengembalianViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null || _viewModel.IsLoading)
                return;

            var param = (Dictionary<string, dynamic>)parameter;
            DataGrid Data = param["Data"];

            var SanitizedData = GetRawData(Data);

            string FileName = "Data Berita Acara Pengembalian";
            _ = ExportHelper.ExportXls(SanitizedData, FileName, "HublangRootDialog");

            await Task.FromResult(true);
        }

        private DataTable GetRawData(DataGrid Data)
        {
            var Item = Data.ItemsSource as IList<RekeningAirPengembalianDto>;
            var json = JsonConvert.SerializeObject(Item);
            var data = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));

            string[] ColumnsToBeDeleted = {
                "IdPdam",
                "IdPengembalian",
                "IdDiameter",
                "KodeDiameter",
                "IdRayon",
                "KodeRayon",
                "IdArea",
                "KodeArea",
                "IdWilayah",
                "KodeWilayah",
                "IdKelurahan",
                "KodeKelurahan",
                "IdKecamatan",
                "KodeKecamatan",
                "IdCabang",
                "KodeCabang",
                "IdUser",
                "FlagHapus",
                "WaktuUpdate",
                "IdUserRequest",
                "IdGolongan"};

            foreach (string ColName in ColumnsToBeDeleted)
            {
                if (data.Columns.Contains(ColName))
                    data.Columns.Remove(ColName);
            }

            return data;
        }

    }
}
