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

namespace SiPintar.Commands.Bacameter.SistemKontrol.RuteBacaMeter.PetugasBaca
{
    public class OnSubmitEditFormCommand : AsyncCommandBase
    {
        private readonly PetugasBacaViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditFormCommand(PetugasBacaViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.SelectedPetugas == null || _viewModel.SelectedJadwal == null)
                return;

            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            DialogHelper.ShowLoading(_isTest, "RuteBacaMeterSubDialog");

            var body = new Dictionary<string, dynamic>
            {
                { "IdJadwalBaca", _viewModel.SelectedJadwal.IdJadwalBaca },
                { "IdPetugasBaca", _viewModel.SelectedJadwal.IdPetugasBaca },
                { "ToleransiMinus", _viewModel.ToleransiMinus },
                { "ToleransiPlus", _viewModel.ToleransiPlus },
                { "IdRayon", _viewModel.SelectedJadwal.IdRayon },
            };

            if (_viewModel.TanggalBacaForm != null)
                body.Add("TanggalMulaiBaca", _viewModel.TanggalBacaForm.Value.Value);

            if (_isTest)
                body.Add("TestId", _viewModel.TestId);

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-jadwal-baca", null, body));
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
