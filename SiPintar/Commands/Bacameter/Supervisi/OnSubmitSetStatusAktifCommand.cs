using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitSetStatusAktifCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitSetStatusAktifCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.StatusList == null || _viewModel.StatusList.Count == 0)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

            var getNonAktif = _viewModel.StatusList.FirstOrDefault(i => i.NamaStatus.ToLower() == "non aktif" || i.NamaStatus.ToLower() == "tidak aktif");
            var getAktif = _viewModel.StatusList.FirstOrDefault(i => i.NamaStatus.ToLower() == "aktif");

            int IdNonAktif = getNonAktif != null ? (int)getNonAktif.IdStatus : 0;
            int IdAktif = getAktif != null ? (int)getAktif.IdStatus : 0;

            int TargetStatus = _viewModel.SelectedData.IdStatus == IdAktif ? IdNonAktif : IdAktif;

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganAir", _viewModel.SelectedData.IdPelangganAir},
                { "IdPeriode", _viewModel.SelectedData.IdPeriode },
                { "IdStatus", TargetStatus }
            };

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-air-update-non-administratif", null, body));
            if (!Response.IsError)
            {
                var Result = Response.Data;
                if (Result.Status)
                    SuccessMessage = Response.Data.Ui_msg;
                else
                    ErrorMessage = Response.Data.Ui_msg;
            }
            else
            {
                ErrorMessage = Response.Error.Message;
            }

            if (App.OpenedWindow.ContainsKey("bacameter"))
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "BacameterRootDialog",
                    App.OpenedWindow["bacameter"], true, _viewModel.OnRefreshCommand, true);

            _viewModel.IsLoadingForm = false;
        }
    }
}
