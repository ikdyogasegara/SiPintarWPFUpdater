using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels;
using SiPintar.ViewModels.Laporan;

namespace SiPintar.Commands.Laporan.LaporanModule
{
    public class OnShowReportCommand : AsyncCommandBase
    {
        private readonly LaporanModuleViewModel _vm;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private CetakDto _currentData;
        private int PerBatch
        {
            get
            {
                return Convert.ToInt32(Application.Current.Resources["PerBatch"] ?? "1000");
            }
        }

        public OnShowReportCommand(LaporanModuleViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _vm = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _vm.UpdateProgress(0, _isTest);
            _vm.IsLoadingForm = true;
            var param = parameter as ParamMasterReportDataWpf;
            var filters = new List<ParamMasterReportDataFilterDto>();

            foreach (var item in param.Filters)
            {
                filters.Add(new ParamMasterReportDataFilterDto()
                {
                    IdFilter = item.IdParam,
                    Value1 = item.Value1,
                    Value2 = item.Value2,
                });
            }
            var paramSend = new Dictionary<string, dynamic>
            {
                { "IdModel", param.IdModel },
                { "Filters", filters },
                { "IdSort", param.IdSort },
                { "Page", 1 },
                { "PerBatch", PerBatch },
                { "Template", false },
            };
            _currentData = null;
            var unduhData = await GetDataAsync(_vm.Progress, paramSend, _vm.UpdateProgress, delegate (string msg)
            {
                _vm.UpdateProgress(0, _isTest);
                _ = DialogHelper.ShowDialogGlobalViewAsync(_isTest, true, "DialogFormReportDialog", "Cetak Laporan", msg, "error", moduleName: "main");
                return true;
            });
            if (unduhData)
            {
                var moduleName = $"{_vm.SelectedModule[0].ToString().ToUpper()}{_vm.SelectedModule[1..]}";
                var namaReport = moduleName + "\\" + _vm.DataReport.FirstOrDefault(x => x.IdModel == param.IdModel).Name;
                await PrintHelper.CetakAsync("Laporan", namaReport.Replace(" ", ""), _currentData,
                                    errorHandling: _vm.ErrorHandlingCetak, isTest: _isTest, title: "Cetak Laporan", identifier: "DialogFormReportDialog", directPrint: false, filterData: param);
            }
            _vm.IsLoadingForm = false;
            _ = Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<bool> GetDataAsync(int progress, Dictionary<string, dynamic> paramSend, Func<int, bool, bool> updateProgress = null, Func<string, bool> errorCallback = null)
        {
            var response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/master-report", paramSend, isReport: true);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    var temp = result.Data.ToObject<ObservableCollection<CetakDto>>();
                    if (temp.Count > 0 && temp[0] is CetakDto data)
                    {
                        var page = (int)paramSend.FirstOrDefault(x => x.Key == "Page").Value;
                        var totalPage = data.TotalPage;
                        if (updateProgress != null)
                        {
                            _ = updateProgress((int)Math.Floor(Convert.ToDecimal(page) / totalPage * 100), _isTest);
                        }
                        try
                        {
                            PrepareData(data);
                        }
                        catch (Exception ex)
                        {
                            Serilog.Log.Error(ex.Message);
                            return false;
                        }
                        if (page < totalPage)
                        {
                            await GetDataAsync(progress, new Dictionary<string, dynamic>
                            {
                                { "IdModel", paramSend.FirstOrDefault(x => x.Key == "IdModel").Value },
                                { "Filters", paramSend.FirstOrDefault(x => x.Key == "Filters").Value },
                                { "IdSort", paramSend.FirstOrDefault(x => x.Key == "IdSort").Value },
                                { "Page", (int)paramSend.FirstOrDefault(x => x.Key == "Page").Value + 1 },
                                { "PerBatch", PerBatch },
                                { "Template", false },
                            }, updateProgress, errorCallback);
                        }
                    }
                }
                else
                {
                    Serilog.Log.Error(result.Ui_msg);
                    if (errorCallback is Func<string, bool> callBack) { _ = callBack(result.Ui_msg); }
                    return false;
                }
            }
            else
            {
                Serilog.Log.Error(response.Error?.Message);
                if (errorCallback is Func<string, bool> callBack) { _ = callBack(response.Error?.Message); }
                return false;
            }
            return true;
        }

        public void PrepareData(CetakDto newData)
        {
            if (_currentData is null)
            {
                _currentData = newData;
            }
            else
            {
                foreach (var item in newData.Data)
                {
                    var dSource = _currentData.Data.FirstOrDefault(x => x.Nama == item.Nama);
                    if (dSource != null && dSource.Data is JArray currentData)
                    {
                        currentData.Merge(item.Data, new JsonMergeSettings() { MergeArrayHandling = MergeArrayHandling.Union });
                        dSource.Data = currentData;
                    }
                }
            }
        }
    }
}
