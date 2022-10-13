using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;


namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnSubmitBaPerbaruiDataSambBaruFormCommand : AsyncCommandBase
    {
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitBaPerbaruiDataSambBaruFormCommand(IRestApiClientModel restApi, bool isTest = false)
        {
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var body = parameter as Dictionary<string, dynamic>;

            var response = await _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/permohonan-non-pelanggan", null, body);

            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                {
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "distribusi");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "distribusi");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "distribusi");

            await Task.FromResult(_isTest);
        }
    }
}
