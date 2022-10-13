using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels;

namespace SiPintar.Commands
{
    [ExcludeFromCodeCoverage]
    public abstract class AsyncCommandBase : ICommand
    {
        public readonly string[] ExecptionLabelReport = new string[]
        {
            "IdUserRequest",
            "WaktuUpdate",
            "IdPdam",
            "NamaPdam",
            "FlagHapus",
            "Key",
        };

        public string ErrorMessage { get; set; }

        private bool _isExecuting;
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public virtual void Execute(object parameter)
        {
            IsExecuting = true;

            _ = ExecuteAsync(parameter);

            IsExecuting = false;
        }

        public abstract Task ExecuteAsync(object parameter);

        public async Task<ObservableCollection<ParamPermohonanListWpf>> GetParamPermohonanDataAsync(MasterTipePermohonanDto pilihTipePermohonan, IRestApiClientModel restApi, bool isTest = false)
        {
            var responseData = new ObservableCollection<ParamPermohonanListWpf>();
            var dataParamList = pilihTipePermohonan.DetailParameter.Where(x => x.IdListData != null).ToList();
            foreach (var item in dataParamList)
            {
                var temp = new ParamPermohonanListWpf()
                {
                    Name = item.Parameter,
                    Data1 = new List<ParamPermohonanWpf>(),
                    Data2 = new List<ParamPermohonanWpf>()
                };
                var response = await Task.Run(() => restApi.GetAsync($"/api/{restApi.GetApiVersion()}/{item.EndPoint}", null));
                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status && result.Data != null)
                    {
                        var tempData = result.Data.ToObject<ObservableCollection<JObject>>();
                        foreach (var s in tempData)
                        {
                            temp.Data1.Add(new ParamPermohonanWpf()
                            {
                                Key = s.GetValue(item.FieldKey, StringComparison.OrdinalIgnoreCase).ToString(),
                                Value = s.GetValue(item.FieldDisplayValue1, StringComparison.OrdinalIgnoreCase).ToString()
                            });
                            temp.Data2.Add(new ParamPermohonanWpf()
                            {
                                Key = s.GetValue(item.FieldKey, StringComparison.OrdinalIgnoreCase).ToString(),
                                Value = s.GetValue(item.FieldDisplayValue2, StringComparison.OrdinalIgnoreCase).ToString()
                            });
                        }
                    }
                }
                responseData.Add(temp);
            }
            _ = isTest;
            return responseData;
        }

        public bool ShowInfoCetak(ParamShowInfo param, string title = "[Title]", string module = "main", bool isTest = false)
        {
            _ = DialogHelper.ShowDialogGlobalViewAsync(
                isTest,
                true,
                "HublangRootDialog",
                header: title,
                param.Message,
                param.Status ? "success" : "error",
                "Oke",
                moduleName: module);
            return true;
        }

        public async Task<ObservableCollection<MasterReportMainGroupDto>> GetDataReportGroupAsync(IRestApiClientModel restApi, string apiVersion)
        {
            ObservableCollection<MasterReportMainGroupDto> dataReportGroup = null;
            var param = new Dictionary<string, dynamic>();
            var response = await restApi.GetAsync($"/api/{apiVersion}/master-report/get-group", param, isReport: true);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    dataReportGroup = result.Data.ToObject<ObservableCollection<MasterReportMainGroupDto>>();
                }
                else
                {
                    ErrorMessage = response.Data.Ui_msg;
                }
            }
            else
            {
                ErrorMessage = response.Error.Message;
            }
            return dataReportGroup ?? new ObservableCollection<MasterReportMainGroupDto>();
        }
    }
}
