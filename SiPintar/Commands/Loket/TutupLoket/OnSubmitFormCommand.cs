using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Loket;
using SiPintar.Views.Loket.TutupLoket;

namespace SiPintar.Commands.Loket.TutupLoket
{
    public class OnSubmitFormCommand : AsyncCommandBase
    {
        private readonly TutupLoketViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitFormCommand(TutupLoketViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewModel.IsTutupLoketToday)
            {
                await DialogHelper.ShowDialogGlobalViewAsync(
                    _isTest,
                    true,
                    "LoketRootDialog",
                    "Tutup Loket",
                    "Transaksi loket " + DateTime.Now.ToString("dddd, dd/MM/yyyy ") + "sudah ditutup!",
                    moduleName: "loket");
            }
            else
            {
                object load = await DialogHelper.ShowDialogGlobalLoadingAsync(_isTest, true, "LoketRootDialog",
                    "Mohon tunggu", "sedang memproses tindakan...");

                var body = new Dictionary<string, dynamic>
                {
                    { "TglPenerimaan", DateTime.Now },
                    { "IdLoket", Application.Current.Resources["IdLoket"]?.ToString() },
                    { "IdUser", Application.Current.Resources["IdUser"]?.ToString() },
                    { "Penerimaan", _viewModel.PenerimaanForm },
                    { "UangKecil", _viewModel.KasKecil }
                };

                if (_isTest)
                    body.Add("TestId", _viewModel.TestId);

                var response = await Task.Run(() => _restApi.PostAsync($"/api/{_restApi.GetApiVersion()}/tutup-loket", body));
                DialogHelper.CloseDialog(_isTest, true, "LoketRootDialog", load);

                if (!response.IsError)
                {
                    var result = response.Data;
                    if (result.Status)
                    {
                        await DialogHelper.ShowCustomDialogViewAsync(_isTest, true, "LoketRootDialog", GetSuccessDialog);
                        _viewModel.KasKecil = 0;
                    }
                    else
                        DialogHelper.ShowSnackbar(_isTest, "danger", result.Ui_msg, "loket");
                }
                else
                {
                    DialogHelper.ShowSnackbar(_isTest, "danger", response.Error.Message, "loket");
                }
            }
            _ = await Task.FromResult(_isTest);
        }


        private object GetSuccessDialog() => new SuccessDialog(_viewModel.PenerimaanForm, _viewModel.KasKecil);
    }
}
