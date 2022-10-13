using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;

namespace SiPintar.Commands.Global
{
    public class OnCetakCommand : AsyncCommandBase
    {
        public string TemplateName { get; set; }
        public bool IsCetak { get; set; } = true;
        public bool IsReport { get; set; }
        public bool IsPreview { get; set; } = true;
        public object CustomParam { get; set; }
        public CetakApiMethod Method { get; set; } = CetakApiMethod.GET;
        public string CustomIdentifier { get; set; }
        private readonly string _module;
        private string _linkApi;
        public string LinkApi { get => _linkApi; set { _linkApi = value; } }
        private readonly string _title;
        private readonly IRestApiClientModel _restApi;
        private readonly Func<string, string, string, string, bool, bool> _errorHandling;
        private readonly bool _isTest;

        public OnCetakCommand(IRestApiClientModel restApi, string module = "Global", string linkApi = null, string title = null,
            Func<string, string, string, string, bool, bool> errorHandling = null, bool isTest = false)
        {
            _module = module;
            _restApi = restApi;
            _linkApi = linkApi;
            _title = title;
            _errorHandling = errorHandling;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string errorMessage = null;
            errorMessage = string.IsNullOrWhiteSpace(_module) ? "Terjadi Kelasahan, Silahkan Hubungi Administrator. Code : Module Not Defined." : errorMessage;
            errorMessage = string.IsNullOrWhiteSpace(TemplateName) ? "Terjadi Kelasahan, Silahkan Hubungi Administrator. Code : Template Not Defined." : errorMessage;
            errorMessage = string.IsNullOrWhiteSpace(_linkApi) ? "Terjadi Kelasahan, Silahkan Hubungi Administrator. Code : Api Not Defined." : errorMessage;

            string dialogIdentifier = null;
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var moduleCapitalizeFirstWord = $"{_module[0].ToString().ToUpper()}{_module.AsSpan(1).ToString()}";
                dialogIdentifier = CustomIdentifier ?? $"{moduleCapitalizeFirstWord}RootDialog";
                object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                    dialogIdentifier, "Mohon tunggu", "sedang menyiapkan data...");

                Dictionary<string, dynamic> paramApi = null;
                if (parameter == null)
                {
                    paramApi = CustomParam as Dictionary<string, dynamic>;
                }
                else
                {
                    paramApi = parameter as Dictionary<string, dynamic>;
                }

                RestApiResponse response = null;
                switch (Method)
                {
                    case CetakApiMethod.POST:
                        response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/{_linkApi}", paramApi, isReport: IsReport));
                        break;
                    default:
                        response = await Task.Run(() => _restApi.GetAsync($"/api/{_restApi.GetApiVersion()}/{_linkApi}", paramApi, isReport: IsReport));
                        break;

                }

                DialogHelper.CloseDialog(_isTest, true, dialogIdentifier, dg);

                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status && result.Data != null)
                    {
                        var res = result.Data.ToObject<ObservableCollection<CetakDto>>();
                        if (res?.Count > 0)
                        {
                            if (IsCetak)
                            {
                                await PrintHelper.CetakAsync(moduleCapitalizeFirstWord, TemplateName.Replace(" ", ""), res[0],
                                errorHandling: _errorHandling, isTest: _isTest, title: _title, identifier: dialogIdentifier, directPrint: !IsPreview);
                            }
                            else
                            {
                                var checkTemplate = await PrintHelper.PrepareTemplateAsync(moduleCapitalizeFirstWord, TemplateName.Replace(" ", ""), res[0], _isTest, title: _title, identifier: dialogIdentifier,
                                    errorHandling: _errorHandling);
                                if (checkTemplate)
                                {
                                    PrintHelper.OpenFileInDesigner(moduleCapitalizeFirstWord, TemplateName.Replace(" ", ""), _isTest);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (_errorHandling != null)
                        {
                            _ = _errorHandling(dialogIdentifier, _module.ToLower(), _title, response.Data.Ui_msg, _isTest);
                        }
                    }
                }
                else
                {
                    if (_errorHandling != null)
                    {
                        _ = _errorHandling(dialogIdentifier, _module.ToLower(), _title, response.Error.Message, _isTest);
                    }
                }
            }
            else
            {
                if (_errorHandling != null)
                {
                    _ = _errorHandling(dialogIdentifier, _module.ToLower(), _title, errorMessage, _isTest);
                }
            }

            await Task.FromResult(_isTest);
        }
    }

    public enum CetakApiMethod
    {
        GET,
        POST
    }
}
