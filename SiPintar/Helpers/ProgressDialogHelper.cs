using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SiPintar.Utilities;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public class ProgressDialogHelper
    {
        public static Task ShowProgressDialogWorkerAsync(
            IRestApiClientModel restApi,
            string method,
            string linkApi,
            Dictionary<string, dynamic> body = null,
            bool isTest = false,
            bool dispatcher = false,
            string identfier = "main",
            string header = "",
            string message = "",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool hideSembunyikan = false,
            string moduleName = "main",
            object viewModel = null,
            ICommand onWorkerCompleteCommand = null)
        {
            DialogHelper.CloseDialog(isTest, true, identfier);

            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler((sender, e) =>
            {
                var worker = sender as BackgroundWorker;

                Task<RestApiResponse> response = null;

                switch (method)
                {
                    case "PATCH":
                        response = Task.Run(() => restApi.PatchAsync($"/api/{restApi.GetApiVersion()}/{linkApi}", null, body));
                        break;
                    case "POST":
                        response = Task.Run(() => restApi.PostAsync($"/api/{restApi.GetApiVersion()}/{linkApi}", body));
                        break;
                    default:
                        break;
                }

                int i = 1;
                while (true)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {
                        if (response.IsCompleted)
                        {
                            worker.ReportProgress(100);
                            Thread.Sleep(500);
                            break;
                        }

                        if (i == 10) i = 9;

                        Thread.Sleep(500);
                        worker.ReportProgress(i * 10);
                        i++;
                    }
                }

                if (!response.Result.IsError)
                {
                    var Result = response.Result.Data;
                    if (Result.Status)
                        e.Result = new string[] { "success", response.Result.Data.Ui_msg };
                    else
                        e.Result = new string[] { "error", response.Result.Data.Ui_msg };
                }
                else
                {
                    e.Result = new string[] { "error", response.Result.Error.Message };
                }
            });


            if (backgroundWorker.IsBusy != true)
                backgroundWorker.RunWorkerAsync();

            _ = DialogHelper.ShowDialogGlobalProgressBarViewAsync(
                    isTest,
                    dispatcher,
                    identfier,
                    header,
                    message,
                    cancelButtonText,
                    hideCancel,
                    hideSembunyikan,
                    moduleName,
                    viewModel,
                    backgroundWorker,
                    onWorkerCompleteCommand);


            return Task.FromResult(isTest);
        }

        public static Task ShowProgressDialogWorkerCustomAsync(
            DoWorkEventHandler doWork,
            bool isTest = false,
            bool dispatcher = false,
            string identfier = "main",
            string header = "",
            string message = "",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool hideSembunyikan = false,
            string moduleName = "main",
            object viewModel = null,
            ICommand onWorkerCompleteCommand = null)
        {
            DialogHelper.CloseDialog(isTest, true, identfier);

            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(doWork);


            if (backgroundWorker.IsBusy != true)
                backgroundWorker.RunWorkerAsync();

            _ = DialogHelper.ShowDialogGlobalProgressBarViewAsync(
                    isTest,
                    dispatcher,
                    identfier,
                    header,
                    message,
                    cancelButtonText,
                    hideCancel,
                    hideSembunyikan,
                    moduleName,
                    viewModel,
                    backgroundWorker,
                    onWorkerCompleteCommand);


            return Task.FromResult(isTest);
        }
    }
}
