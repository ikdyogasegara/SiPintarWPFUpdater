using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Commands.Billing.Supervisi.Periode
{
    [ExcludeFromCodeCoverage]
    public class OnSubmitAddAsBackgroundCommand : AsyncCommandBase
    {
        private readonly PeriodeViewModel _viewModel;
        private readonly IRestApiClientModel _restApi;
        private readonly bool _isTest;

        private readonly string ApiVersion = Application.Current.Resources["api_version"]?.ToString();

        public OnSubmitAddAsBackgroundCommand(PeriodeViewModel viewModel, IRestApiClientModel restApi, bool isTest = false)
        {
            _viewModel = viewModel;
            _restApi = restApi;
            _isTest = isTest;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.IsLoadingForm = true;

            ShowLoading();

            var body = new Dictionary<string, dynamic>
            {
                { "KodePeriode", _viewModel.TahunForm.Key + _viewModel.BulanForm.Key },
                { "TglMulaiTagih", ((DateTime)_viewModel.TglMulaiTagihForm).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda1", ((DateTime)_viewModel.TglDenda1Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda2", ((DateTime)_viewModel.TglDenda2Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda3", ((DateTime)_viewModel.TglDenda3Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDenda4", ((DateTime)_viewModel.TglDenda4Form).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "TglMulaiDendaPerBulan", ((DateTime)_viewModel.TglMulaiDendaForm).ToString("yyyy-MM-dd'T00:00:00Z'") },
                { "Status", true }
            };

            _ = Task.Run(() => _restApi.PostAsync($"/api/{ApiVersion}/master-periode-rekening", body));

            AddToBgProcess();

            ShowResult();

            _viewModel.IsLoadingForm = false;

            await Task.FromResult(_isTest);
        }

        private void AddToBgProcess()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    if (mainView != null)
                    {
                        var data = new BackgroundProcessHelper.ProcessObject()
                        {
                            SubmitTime = DateTime.Now,
                            UrlToCheck = $"/api/{ApiVersion}/master-periode-rekening",
                            ValueToCheck = new Dictionary<string, dynamic>()
                            {
                                { "KodePeriode", _viewModel.TahunForm.Key + _viewModel.BulanForm.Key }
                            },
                            ProcessDescription = $"Proses Pembuatan Periode {_viewModel.BulanForm.Value} {_viewModel.TahunForm.Value}",
                            ModuleSource = "billing"
                        };

                        mainView.AddToBgProcess(data);
                    }
                });
            }
        }

        [ExcludeFromCodeCoverage]
        private void ShowResult()
        {
            if (!_isTest)
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    CloseDialog();
                    _ = ShowConfirmationAsync();
                });
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
            double EstimationTimeInSecond = 80;
            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, "sekitar 3-5 menit", false, false, EstimationTimeInSecond), "PeriodeDrdDialog");
        }

        [ExcludeFromCodeCoverage]
        private void CloseDialog()
        {
            if (DialogHost.IsDialogOpen("PeriodeDrdDialog"))
                DialogHost.Close("PeriodeDrdDialog");
        }

        [ExcludeFromCodeCoverage]
        private async Task ShowConfirmationAsync()
        {
            await DialogHost.Show(
                new DialogGlobalView(
                    "Konfirmasi",
                    @"Proses pembuatan periode akan dilanjutkan di belakang layar.
Silakan periksa status proses dengan klik pada indikator di kiri bawah window.",
                    "success",
                    "Ok",
                    false,
                    "billing"
                ), "PeriodeDrdDialog");

            if (DialogHost.IsDialogOpen("BillingRootDialog"))
                DialogHost.Close("BillingRootDialog");
        }
    }
}
