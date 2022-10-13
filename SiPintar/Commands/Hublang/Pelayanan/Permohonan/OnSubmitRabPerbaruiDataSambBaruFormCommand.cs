using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;


namespace SiPintar.Commands.Hublang.Pelayanan.Permohonan
{
    public class OnSubmitRabPerbaruiDataSambBaruFormCommand : AsyncCommandBase
    {
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitRabPerbaruiDataSambBaruFormCommand(IRestApiClientModel restApi, bool isTest = false)
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
                    DialogHelper.ShowSnackbar(_isTest, "success", result.Ui_msg, "perencanaan");
                }
                else
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Data.Ui_msg, "perencanaan");
            }
            else
                DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "perencanaan");

            await Task.FromResult(_isTest);
        }
    }
}
