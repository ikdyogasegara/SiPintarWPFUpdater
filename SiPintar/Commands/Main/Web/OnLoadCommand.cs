using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Main;

namespace SiPintar.Commands.Main.Web
{
    public class OnLoadCommand : AsyncCommandBase
    {
        private readonly WebViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private readonly bool _isErrorTest;

        public OnLoadCommand(WebViewModel viewModel, IRestApiClientModel restApi, bool isTest = false, bool isErrorTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
            _isErrorTest = isErrorTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoading = true;
            _viewModel.IsError = false;

            var statusConfig = await GetConfigAsync();

            if (!statusConfig || _isErrorTest)
            {
                _viewModel.IsError = true;
                _viewModel.IsLoading = false;
            }
            else
            {
                var data = _viewModel.ModuleItems();

                int TotalData = data.Count;
                int TotalGrid = 2;
                int TotalList = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalData) / TotalGrid));

                var result = new List<List<object>>();
                for (int i = 0; i < TotalList; i++)
                {
                    var subdata = new List<object>();
                    for (int j = 0; j < TotalGrid; j++)
                    {
                        if (data.Count > 0)
                        {
                            subdata.Add(data[0]);
                            data.RemoveAt(0);
                        }
                    }

                    result.Add(subdata);
                }

                _viewModel.ListModule = result;
                _viewModel.IsLoading = false;
            }
            await Task.FromResult(_isTest);
        }

        [ExcludeFromCodeCoverage]
        private async Task<bool> GetConfigAsync()
        {
            string ErrorMessage = null;
            var ResultData = new ObservableCollection<PengaturanDto>();

            var param = new Dictionary<string, dynamic>();

            if (_isTest)
                param.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.GetAsync("/api/pengaturan", null, true));
            if (!Response.IsError && Response.HasData)
            {
                var Result = Response.Data;
                if (Result.Status)
                {
                    var data = Result.Data.ToObject<ObservableCollection<PengaturanDto>>();

                    var siConfigUrl = data.FirstOrDefault(i => i.Key == "siconfig_web_url");
                    var dashboardUrl = data.FirstOrDefault(i => i.Key == "dashboard_web_url");
                    var pdamInfoUrl = data.FirstOrDefault(i => i.Key == "pdam_info_web_url");

                    string SiConfigUrl = siConfigUrl != null ? siConfigUrl.Value : "";
                    string DashboardUrl = dashboardUrl != null ? dashboardUrl.Value : "";
                    string PdamInfoUrl = pdamInfoUrl != null ? pdamInfoUrl.Value : "";

                    SetToAppConfig("siconfig_url", SiConfigUrl);
                    SetToAppConfig("dashboard_url", DashboardUrl);
                    SetToAppConfig("pdam_info_url", PdamInfoUrl);
                    return true;
                }
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }
            DialogHelper.ShowSnackbar(_isTest, "danger", ErrorMessage, "main", true);
            return false;
        }

        [ExcludeFromCodeCoverage]
        private static void SetToAppConfig(string key, string value)
        {
            try
            {
                // set to app resources
                if (Application.Current != null)
                {
                    if (Application.Current.Resources.Contains(key))
                        Application.Current.Resources[key] = value;
                    else
                        Application.Current.Resources.Add(key, value);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("web: " + e);
            }
        }
    }
}
