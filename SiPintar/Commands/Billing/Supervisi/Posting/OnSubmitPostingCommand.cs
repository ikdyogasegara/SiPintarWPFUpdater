using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.Posting
{
    public class OnSubmitPostingCommand : AsyncCommandBase
    {
        private readonly PostingViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        public OnSubmitPostingCommand(PostingViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _isTest = isTest;
            _restApi = restApi;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            if (_viewModel.IsPostingPelangganAir)
                await PostingPelangganAirAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.IsPostingPelangganLimbah)
                await PostingPelangganLimbahAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.IsPostingPelangganLltt)
                await PostingPelangganLlttAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.IsPostingRekeningAir)
                await PostingRekeningAirAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.IsPostingRekeningLimbah)
                await PostingRekeningLimbahAsync(SuccessMessage, ErrorMessage);

            if (_viewModel.IsPostingRekeningLltt)
                await PostingRekeningLlttAsync(SuccessMessage, ErrorMessage);


            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private async Task PostingPelangganAirAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                var Response = _restApi.PostAsync("/api/v1/master-pelanggan-air-posting", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;

            });

            ShowResult(SuccessMessage, ErrorMessage);
        }



        private async Task PostingPelangganLimbahAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                RestApiResponse Response = _restApi.PostAsync("/api/v1/master-pelanggan-limbah-posting", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;

            });

            ShowResult(SuccessMessage, ErrorMessage);
        }

        private async Task PostingPelangganLlttAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                RestApiResponse Response = _restApi.PostAsync("/api/v1/master-pelanggan-lltt-posting", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;

            });
            ShowResult(SuccessMessage, ErrorMessage);
        }

        private async Task PostingRekeningAirAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                RestApiResponse Response = _restApi.PostAsync("/api/v1/rekening-air-posting-drd-air", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;
            });

            ShowResult(SuccessMessage, ErrorMessage);
        }

        private async Task PostingRekeningLimbahAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                RestApiResponse Response = _restApi.PostAsync("/api/v1/rekening-limbah-posting-drd", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;
            });

            ShowResult(SuccessMessage, ErrorMessage);
        }
        private async Task PostingRekeningLlttAsync(string SuccessMessage, string ErrorMessage)
        {
            await Task.Run(() =>
            {
                var body = new Dictionary<string, dynamic>();
                body.Add("IdPeriode", _viewModel.SelectedDataPeriode.IdPeriode);
                body.Add("Catatan", _viewModel.Catatan);
                RestApiResponse Response = _restApi.PostAsync("/api/v1/rekening-lltt-posting-drd", body).Result;
                if (!Response.IsError)
                {
                    var Result = Response.Data;
                    if (Result.Status)
                        SuccessMessage = Response.Data.Ui_msg;
                    else
                        ErrorMessage = Response.Data.Ui_msg;
                }
                else
                    ErrorMessage = Response.Error.Message;
            });

            ShowResult(SuccessMessage, ErrorMessage);
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult(string SuccessMessage, string ErrorMessage)
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    CloseDialog();
                });

                if (ErrorMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowError(ErrorMessage);
                    });
                }
                else if (SuccessMessage != null)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        ShowSuccess(SuccessMessage);
                    });
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowLoading()
        {
            if (!_isTest)
                Application.Current.Dispatcher.Invoke(delegate
                {
                    LoadingDialog();
                });
        }

        [ExcludeFromCodeCoverage]
        private void LoadingDialog()
        {
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        [ExcludeFromCodeCoverage]
        private void ShowError(string ErrorMessage)
        {
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"));
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BillingView)App.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnLoadCommand.Execute(null);
        }
    }
}
