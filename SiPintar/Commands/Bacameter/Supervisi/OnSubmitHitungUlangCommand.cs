using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Commands.Bacameter.Supervisi
{
    public class OnSubmitHitungUlangCommand : AsyncCommandBase
    {
        private readonly SupervisiViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitHitungUlangCommand(SupervisiViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.RekeningList == null || _viewModel.SelectedPeriode == null)
                return;

            try
            {
                var IdPelangganAir = new List<int>();

                var HasSelectedData = _viewModel.RekeningList.FirstOrDefault(i => i.IsSelected == true);
                foreach (var item in _viewModel.RekeningList)
                {
                    if (HasSelectedData != null)
                    {
                        if (item.IsSelected == true)
                            IdPelangganAir.Add((int)item.IdPelangganAir);
                    }
                    else
                    {
                        IdPelangganAir.Add((int)item.IdPelangganAir);
                    }
                }

                string SuccessMessage = null;
                string ErrorMessage = null;

                _viewModel.IsLoadingForm = true;

                DialogHelper.ShowLoading(_isTest, "BacameterRootDialog");

                var body = new Dictionary<string, dynamic>
                {
                    { "IdPeriode", _viewModel.SelectedPeriode.IdPeriode },
                    { "IdPelangganAir", IdPelangganAir.ToArray() }
                };

                if (_isTest)
                    body.Add("TestId", _viewModel.TestId);

                var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/rekening-air-hitung-ulang", null, body));
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
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }
    }
}
