using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppBusiness.Data.DTOs;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Akuntansi.MasterData.MasterDataAkuntansi;

namespace SiPintar.Commands.Akuntansi.MasterData.MasterDataAkuntansi.AnggaranLabaRugi
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly AnggaranLabaRugiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(AnggaranLabaRugiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            object dg = DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true,
                "AkuntansiRootDialog", "Mohon tunggu",
                "sedang memproses tindakan...");

            _viewModel.IsLoadingForm = true;

            _viewModel.IsEdit = true;

            var type = typeof(AnggaranLabaRugiDto);
            var properties = type.GetProperties();
            var body = new Dictionary<string, dynamic>();
            foreach (var property in properties)
            {
                if (property.Name != "IdPdam" && property.Name != "IdUserRequest")
                {
                    body.Add(property.Name, property.GetValue(_viewModel.AnggaranLabaRugiForm)!);
                }
            }

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/anggaran-laba-rugi", null!, body);
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    await ((AsyncCommandBase)_viewModel.OnRefreshCommand).ExecuteAsync(null!);
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "akuntansi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "akuntansi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error?.Message!, "akuntansi");

            _viewModel.IsLoadingForm = false;

            DialogHelper.CloseDialog(_isTest, true, "AkuntansiRootDialog", dg);

            await Task.FromResult(_isTest);
        }
    }
}
