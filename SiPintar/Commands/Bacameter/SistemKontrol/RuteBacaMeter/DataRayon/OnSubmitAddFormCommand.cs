using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using AppBusiness.Data.DTOs;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Bacameter.SistemKontrol.RuteBacaMeter;

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.DataRayon
{
    public class OnSubmitAddFormCommand : AsyncCommandBase
    {
        private readonly DataRayonViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitAddFormCommand(DataRayonViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedRayon == null || _viewModel.SelectedPetugas == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "RuteBacaMeterSubDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdRayon", _viewModel.SelectedRayon.IdRayon },
                { "IdPetugasBaca", _viewModel.SelectedPetugas.IdPetugasBaca },
                { "ToleransiMinus", _viewModel.ToleransiMinus },
                { "ToleransiPlus", _viewModel.ToleransiPlus },
            };

            if (_viewModel.TanggalBacaForm != null)
                body.Add("TanggalMulaiBaca", _viewModel.TanggalBacaForm.Value.Value);

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-jadwal-baca", body));
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
                DialogHelper.ShowSuccessErrorDialog(ErrorMessage, SuccessMessage, _isTest, "RuteBacaMeterSubDialog",
                    App.OpenedWindow["bacameter"], true, _viewModel.OnRefreshCommand, true);

            if (SuccessMessage != null)
            {
                CloseDialog();

                _viewModel.JadwalList = new ObservableCollection<MasterJadwalBacaDto>();
                _viewModel.IsEmptyJadwal = true;
            }

            _viewModel.IsLoadingForm = false;
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    if (DialogHost.IsDialogOpen("BacameterRootDialog"))
                        DialogHost.Close("BacameterRootDialog");
                });
        }
    }
}
