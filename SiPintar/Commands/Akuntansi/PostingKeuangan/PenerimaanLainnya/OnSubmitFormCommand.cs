using System.Collections.Generic;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.PostingKeuangan;

namespace SiPintar.Commands.Akuntansi.PostingKeuangan.PenerimaanLainnya
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly PenerimaanLainnyaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(PenerimaanLainnyaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            var type = typeof(ParamDaftarPenerimaanKasDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.PenerimaanLainnyaForm)!);
                }
            }

            RestApiResponse response;
            if (_viewModel.IsAdd)
                response = await _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/penerimaan-lainnya", body);
            else
                response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/penerimaan-lainnya", null!, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "akuntansi");
                    _viewModel.OnRefreshCommand.Execute(null);
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message!, "akuntansi");

            DialogHelper.CloseDialog(_isTest, false, "AkuntansiRootDialog");

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
