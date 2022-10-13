using System.Collections.Generic;
using System.Threading.Tasks;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitUnverifikasiCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;
        private string _successMessage;
        private string _errorMessage;

        public OnSubmitUnverifikasiCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter == null)
                return;

            var ignoreCheckbox = (bool)parameter;

            _successMessage = null;
            _errorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

            var idPelangganAir = new List<int?>();

            if (ignoreCheckbox)
            {
                idPelangganAir.Add(_viewModel.KoreksiForm.IdPelangganAir);
            }
            else
            {
                foreach (var item in _viewModel.RekeningList)
                {
                    if (item.IsSelected && !item.IsUnverifikasi)
                    {
                        idPelangganAir.Add(item.IdPelangganAir);
                    }
                }
            }

            if (idPelangganAir.Count > 0)
                await ProsesAsync(idPelangganAir, _viewModel.SelectedData.IdPeriode, _viewModel.KonfirmasiPassword);
            else
                _errorMessage = "Tidak ada data yang memenuhi syarat unverifikasi";

            if (App.OpenedWindow.ContainsKey("bacameter"))
                DialogHelper.ShowSuccessErrorDialog(_errorMessage, _successMessage, _isTest, "BacameterRootDialog",
                    App.OpenedWindow["bacameter"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;
        }

        private async Task ProsesAsync(List<int?> idPelangganAir, int? idPeriode, string password)
        {
            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", idPelangganAir.ToArray() },
                { "IdPeriode", idPeriode }
            };

            if (idPelangganAir.Count > 1)
                body.Add("PasswordUser", password);

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var response = await Task.Run(() => _restApi.PatchAsync($"/api/{_restApi.GetApiVersion()}/rekening-air-unverifikasi", null, body));
            if (!response.IsError)
            {
                var result = response.Data;
                if (result.Status)
                    _successMessage = response.Data.Ui_msg;
                else
                    _errorMessage = response.Data.Ui_msg;
            }
            else
            {
                _errorMessage = response.Error.Message;
            }
        }
    }
}
