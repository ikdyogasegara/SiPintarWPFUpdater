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

namespace SiPintar.Commands.Billing.Supervisi.PelangganL2T2
{
    public class OnSubmitEditCommand : AsyncCommandBase
    {
        private readonly PelangganL2T2ViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitEditCommand(PelangganL2T2ViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            string SuccessMessage = null;
            string ErrorMessage = null;

            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var body = new Dictionary<string, dynamic>
            {
                { "IdPelangganLltt", _viewModel.PelangganForm.IdPelangganLltt },
                { "NomorLltt", _viewModel.PelangganForm.NomorLltt },
                { "Nama", _viewModel.PelangganForm.Nama },
                { "Alamat", _viewModel.PelangganForm.Alamat },
                { "NoSamb", _viewModel.PelangganForm.NoSamb },
                { "IdTarifLltt", _viewModel.PelangganForm.IdTarifLltt },
                { "IdRayon", _viewModel.PelangganForm.IdRayon },
                { "IdKelurahan", _viewModel.PelangganForm.IdKelurahan },
                { "IdKolektif", _viewModel.PelangganForm.IdKolektif },
                { "IdStatus", _viewModel.PelangganForm.IdStatus },
                { "IdFlag", _viewModel.PelangganForm.IdFlag },
                { "NoHp", _viewModel.PelangganForm.NoHp },
                { "NoTelp", _viewModel.PelangganForm.NoTelp },
                { "NoKtp", _viewModel.PelangganForm.NoKtp },
                { "Email", _viewModel.PelangganForm.Email },
                { "Keterangan", _viewModel.PelangganForm.Keterangan }
            };

            var Response = await Task.Run(() => _restApi.PatchAsync($"/api/{ApiVersion}/master-pelanggan-lltt", null, body));
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

            ShowResult(SuccessMessage, ErrorMessage);

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
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
                Application.Current.Dispatcher.Invoke(delegate { LoadingDialog(); });
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
            _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", ErrorMessage, "error"), "BillingRootDialog");
        }

        [ExcludeFromCodeCoverage]
        private void ShowSuccess(string SuccessMessage)
        {
            var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (mainView != null)
                mainView.ShowSnackbar(SuccessMessage, "success");

            _viewModel.OnRefreshCommand.Execute(null);
        }
    }
}
