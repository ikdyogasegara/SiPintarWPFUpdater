using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using Newtonsoft.Json.Linq;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.ProsesSaldoHarian
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly ProsesSaldoHarianViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(ProsesSaldoHarianViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");

            _viewModel.IsLoadingForm = true;

            _viewModel.IsAdd = !_viewModel.ProsesSaldoHarianForm!.IdSetor.HasValue;

            var type = typeof(ParamSaldoSetorDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.ProsesSaldoHarianForm)!);
                }
            }

            RestApiResponse response;
            if (_viewModel.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/saldo-setor", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/saldo-setor", null!, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg!, "akuntansi");
                    _viewModel.OnRefreshCommand.Execute(null!);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg!, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message!, "akuntansi");

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
