using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.Utilities.Services.Filter;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Laporan;
using SiPintar.Views.Laporan.LaporanModule;

namespace SiPintar.Commands.Laporan.LaporanModule
{
    public class OnOpenItemCommand : AsyncCommandBase
    {
        private readonly LaporanModuleViewModel vm;
        private readonly bool _isTest;
        private readonly ObservableCollection<KeyValuePair<string, string>> _listFilterReport =
            new ObservableCollection<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("master-rayon", "MasterRayon"),
                new KeyValuePair<string, string>("master-area", "MasterArea"),
                new KeyValuePair<string, string>("master-wilayah", "MasterWilayah"),
                new KeyValuePair<string, string>("master-loket", "MasterLoket"),
                new KeyValuePair<string, string>("master-user", "MasterUser"),
                new KeyValuePair<string, string>("master-tarif-golongan", "MasterGolongan"),
                new KeyValuePair<string, string>("master-tarif-diameter", "MasterDiameter"),
                new KeyValuePair<string, string>("master-tarif-tangki", "MasterTarifTangki"),
                new KeyValuePair<string, string>("master-merek-meter", "MasterMerekMeter"),
                new KeyValuePair<string, string>("master-periode-rekening", "MasterPeriode"),
                new KeyValuePair<string, string>("master-cabang", "MasterCabang"),
                new KeyValuePair<string, string>("master-kecamatan", "MasterKecamatan"),
                new KeyValuePair<string, string>("master-kelurahan", "MasterKelurahan"),
                new KeyValuePair<string, string>("master-kolektif", "MasterKolektif"),
                new KeyValuePair<string, string>("master-flag", "MasterFlag"),
                new KeyValuePair<string, string>("master-jenis-non-air", "MasterJenisNonAir"),
                new KeyValuePair<string, string>("master-status", "MasterStatus"),
                new KeyValuePair<string, string>("master-blok", "MasterBlok"),
                new KeyValuePair<string, string>("master-perkiraan1", "MasterPerkiraan1"),
                new KeyValuePair<string, string>("master-perkiraan2", "MasterPerkiraan2"),
                new KeyValuePair<string, string>("master-perkiraan3", "MasterPerkiraan3"),
                new KeyValuePair<string, string>("master-periode-akuntansi", "MasterPeriodeAkuntansi"),
                new KeyValuePair<string, string>("master-periode-akuntansi-triwulan", "MasterPeriodeAkuntansiTriwulan"),
                new KeyValuePair<string, string>("report-filter-custom", "ReportFilterCustom"),
            };
        public int? IdReport { get; set; }

        public OnOpenItemCommand(LaporanModuleViewModel viewModel, bool isTest = false)
        {
            vm = viewModel;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            IdReport = parameter as int?;
            if (IdReport.HasValue)
            {
                object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "LaporanRootDialog",
                    "Mohon tunggu", "sedang mempersiapkan data ...");
                await GetFilterDataAsync(vm.DataReport.FirstOrDefault(x => x.IdModel == IdReport));
                DialogHelper.CloseDialog(_isTest, true, "LaporanRootDialog", dg);
                _ = DialogHelper.ShowCustomDialogViewAsync(_isTest, false, "LaporanRootDialog", GetInstance);
            }
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task GetFilterDataAsync(ReportModelDto report)
        {
            if (report != null)
            {

                foreach (var item in report.Params)
                {
                    if (item.ParamType == "list" && (vm.DataReportFilter ?? new ObservableCollection<DataReportFilterList>()).FirstOrDefault(x => x.EndPoint == item.ListDataDetail.EndPoint) is null
                        && _listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value != null)
                    {
                        await UpdateListData.ProcessAsync(false, new List<string>()
                            {
                                _listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value
                            });

                        var temp1 = new ObservableCollection<KeyValuePair<int, string>>();
                        var temp2 = new ObservableCollection<KeyValuePair<int, string>>();
                        var type = typeof(MasterListGlobal);
                        var fields = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

                        var temp = JsonConvert.SerializeObject(fields.FirstOrDefault(x => x.Name == _listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value).GetValue(null));
                        var tempData = JsonConvert.DeserializeObject<ObservableCollection<JObject>>(temp);
                        foreach (var s in tempData)
                        {
                            temp1.Add(new KeyValuePair<int, string>(
                                (int)s.GetValue(item.ListDataDetail.FieldKey, StringComparison.OrdinalIgnoreCase),
                                s.GetValue(item.ListDataDetail.FieldDisplayValue1, StringComparison.OrdinalIgnoreCase).ToString()));
                            temp2.Add(new KeyValuePair<int, string>(
                                (int)s.GetValue(item.ListDataDetail.FieldKey, StringComparison.OrdinalIgnoreCase),
                                s.GetValue(item.ListDataDetail.FieldDisplayValue2, StringComparison.OrdinalIgnoreCase).ToString()));
                        }

                        (vm.DataReportFilter ?? new ObservableCollection<DataReportFilterList>()).Add(new DataReportFilterList()
                        {
                            EndPoint = item.ListDataDetail?.EndPoint,
                            Data1 = temp1,
                            Data2 = temp2
                        });
                    }

                    if (item.ParamType == "custom_list" && _listFilterReport.FirstOrDefault(x => x.Key == "report-filter-custom").Value != null)
                    {
                        item.ListCustomDataDetail ??= new ReportFilterCustomDto();
                        await UpdateListData.ProcessAsync(_isTest, new List<string>() { "ReportFilterCustom" });

                        var temp1 = new ObservableCollection<KeyValuePair<int, string>>();
                        var temp2 = new ObservableCollection<KeyValuePair<int, string>>();
                        var type = typeof(MasterListGlobal);
                        var fields = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

                        var temp = JsonConvert.SerializeObject(fields.FirstOrDefault(x => x.Name == "ReportFilterCustom").GetValue(null));
                        var tempData = JsonConvert.DeserializeObject<ObservableCollection<ReportFilterCustomDto>>(temp).FirstOrDefault(x => x.IdFilterCustom == item.IdListCustomData).ReportFilterCustomDetail;

                        foreach (var s in tempData)
                        {
                            temp1.Add(new KeyValuePair<int, string>((int)s.IdFilterCustomDetail!, s.Kode));
                            temp2.Add(new KeyValuePair<int, string>((int)s.IdFilterCustomDetail!, s.Nama));
                        }

                        (vm.DataReportFilter ?? new ObservableCollection<DataReportFilterList>()).Add(new DataReportFilterList()
                        {
                            EndPoint = $"report-filter-custom-{item.ListCustomDataDetail.Nama.ToLower()}",
                            Data1 = temp1,
                            Data2 = temp2
                        });
                    }

                    if (item.DataType == "periode" || item.DataType == "tahun")
                    {
                        item.ListDataDetail ??= new MasterTipePermohonanConfigListDataDto();
                        item.ListDataDetail.EndPoint = "master-periode-rekening";
                        item.ListDataDetail.FieldKey = "idperiode";
                        item.ListDataDetail.FieldDisplayValue1 = "kodeperiode";
                        item.ListDataDetail.FieldDisplayValue2 = "namaperiode";

                        if (_listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value != null)
                        {
                            await UpdateListData.ProcessAsync(false, new List<string>()
                            {
                                _listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value
                            });

                            var temp1 = new ObservableCollection<KeyValuePair<int, string>>();
                            var temp2 = new ObservableCollection<KeyValuePair<int, string>>();
                            var type = typeof(MasterListGlobal);
                            var fields = type.GetProperties(BindingFlags.Static | BindingFlags.Public);

                            var temp = JsonConvert.SerializeObject(fields.FirstOrDefault(x => x.Name == _listFilterReport.FirstOrDefault(x => x.Key == item.ListDataDetail.EndPoint).Value).GetValue(null));
                            var tempData = JsonConvert.DeserializeObject<ObservableCollection<JObject>>(temp);
                            foreach (var s in tempData)
                            {
                                temp1.Add(new KeyValuePair<int, string>(
                                    (int)s.GetValue(item.ListDataDetail.FieldKey, StringComparison.OrdinalIgnoreCase),
                                    s.GetValue(item.ListDataDetail.FieldDisplayValue1, StringComparison.OrdinalIgnoreCase).ToString()));
                                temp2.Add(new KeyValuePair<int, string>(
                                    (int)s.GetValue(item.ListDataDetail.FieldKey, StringComparison.OrdinalIgnoreCase),
                                    s.GetValue(item.ListDataDetail.FieldDisplayValue2, StringComparison.OrdinalIgnoreCase).ToString()));
                            }

                            (vm.DataReportFilter ?? new ObservableCollection<DataReportFilterList>()).Add(new DataReportFilterList()
                            {
                                EndPoint = item.ListDataDetail?.EndPoint,
                                Data1 = temp1,
                                Data2 = temp2
                            });
                        }
                    }

                }
            }
        }

        private object GetInstance() => new DialogFormReport(vm, vm.DataReport.FirstOrDefault(x => x.IdModel == IdReport), vm.DataReportFilter, vm.OnShowReportCommand, vm.OpenDesignerCommand);
    }
}
