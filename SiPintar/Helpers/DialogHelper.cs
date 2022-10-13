using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using SiPintar.Views;
using SiPintar.Views.Global.Dialog;
using SiPintar.Views.Global.Other;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class DialogHelper
    {
        public static void SnackbarPerencanaanHandler(string errorMessage, string successMessage, string errorSnackbar, string successSnackbar, bool isTest,
            string dialogidentifier, dynamic mainView)
        {
            if (!isTest)
            {
                if (errorMessage != null)
                {
                    _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, errorSnackbar), dialogidentifier);
                }
                else if (successMessage != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    if (mainView != null)
                        mainView.ShowSnackbar(successMessage, successSnackbar);

                }
            }
        }

        public static void ShowSuccessErrorDialog(string errorMessage, string successMessage,
            bool isTest, string identifierDialog, dynamic mainView, bool isMustCloseDialogAfterSuccess,
            ICommand commandAfterSuccess = null, bool isBgProcess = true)
        {
            if (!isTest)
            {
                // bg process = use dispatcher
                if (isBgProcess)
                {
                    if (isMustCloseDialogAfterSuccess)
                        Application.Current.Dispatcher.Invoke(delegate { DialogHost.Close(identifierDialog); });

                    if (!string.IsNullOrWhiteSpace(errorMessage))
                    {
                        Application.Current.Dispatcher.Invoke(delegate { _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), identifierDialog); });
                    }
                    else if (successMessage != null)
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            if (mainView != null)
                                mainView.ShowSnackbar(successMessage, "success");

                            if (commandAfterSuccess != null)
                                commandAfterSuccess.Execute(null);
                        });
                    }
                }
                // non bg process
                else
                {
                    if (isMustCloseDialogAfterSuccess)
                        DialogHost.Close(identifierDialog);

                    if (errorMessage != null)
                    {
                        _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", errorMessage, "error"), identifierDialog);
                    }
                    else if (successMessage != null)
                    {
                        if (mainView != null)
                            mainView.ShowSnackbar(successMessage, "success");

                        if (commandAfterSuccess != null)
                            commandAfterSuccess.Execute(null);
                    }
                }
            }
        }

        public static void ShowLoading(bool isTest, string identifierDialog, bool isBgProcess = true,
            bool isUsingCounter = false, double estimationTimeInSecond = 0,
            string textCounterInfo = "sekitar 3-5 menit")
        {
            if (!isTest)
            {
                if (isBgProcess)
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        if (!isUsingCounter)
                            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), identifierDialog);
                        else
                            _ = DialogHost.Show(new GlobalLoadingDialog(null, null, textCounterInfo, false, false, estimationTimeInSecond), identifierDialog);
                    });
                }
                else
                {
                    if (!isUsingCounter)
                        _ = DialogHost.Show(new GlobalLoadingDialog(null, null, null), identifierDialog);
                    else
                        _ = DialogHost.Show(new GlobalLoadingDialog(null, null, textCounterInfo, false, false, estimationTimeInSecond), identifierDialog);
                }
            }
        }

        public static Task<object> ShowDialogGlobalLoadingAsync(bool isTest = false, bool dispatcher = false, string identfier = "main", string header = "", string message1 = "", string message2 = "",
            bool isCircleLoading = true, bool isUnknowHowLongTime = true, double estimatedTimeInSecond = 20)
        {
            object response = null;
            if (!isTest)
            {
                if (dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => response = await DialogHost.Show(new GlobalLoadingDialog(header, message1, message2, isCircleLoading, isUnknowHowLongTime, estimatedTimeInSecond), identfier));
                else
                    response = DialogHost.Show(new GlobalLoadingDialog(header, message1, message2, isCircleLoading, isUnknowHowLongTime, estimatedTimeInSecond), identfier);
            }
            return Task.FromResult(response);
        }

        public static void CloseDialog(bool isTest, bool dispatcher = false, string Identifier = "main", object dialog = null)
        {
            if (!isTest)
            {
                if (dispatcher)
                    Application.Current.Dispatcher.Invoke(() => DialogHost.Close(Identifier, dialog));
                else
                    DialogHost.Close(Identifier, dialog);
            }
        }

        public static Task<object> ShowDialogGlobalViewAsync(bool isTest = false, bool dispatcher = false, string identfier = "main", string header = "", string message = "", string type = "success", string buttonText = "Ok", bool hideButton = false, string moduleName = "main",
            DialogClosingEventHandler closingEventHandler = null)
        {
            object response = null;
            if (!isTest)
            {
                if (dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => response = await DialogHost.Show(new DialogGlobalView(header, message, type, buttonText, hideButton, moduleName), identfier, closingEventHandler: closingEventHandler));
                else
                    response = DialogHost.Show(new DialogGlobalView(header, message, type, buttonText, hideButton, moduleName), identfier, closingEventHandler: closingEventHandler);
            }
            return Task.FromResult(response);
        }

        public static void ShowSnackbar(bool isTest = false, string type = "success", string msg = "", string moduleName = null, bool dispatcher = true)
        {
            if (!isTest)
            {
                if (dispatcher)
                {
                    switch (moduleName)
                    {
                        case "bacameter":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (BacameterView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "hublang":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "billing":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (BillingView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "loket":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (LoketView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "gudang":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (GudangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "perencanaan":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (PerencanaanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "distribusi":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (DistribusiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "personalia":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (PersonaliaView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "akuntansi":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "main":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (MainView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        case "laporan":
                            Application.Current.Dispatcher.Invoke(delegate
                            {
                                var mainView = (LaporanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                                if (mainView != null)
                                    mainView.ShowSnackbar(msg, type);
                            });
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (moduleName)
                    {
                        case "hublang":
                            var mainViewHublang = (HublangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewHublang != null)
                                mainViewHublang.ShowSnackbar(msg, type);
                            break;
                        case "loket":
                            var mainViewLoket = (LoketView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewLoket != null)
                                mainViewLoket.ShowSnackbar(msg, type);
                            break;
                        case "gudang":
                            var mainViewGudang = (GudangView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewGudang != null)
                                mainViewGudang.ShowSnackbar(msg, type);
                            break;
                        case "perencanaan":
                            var mainViewPerencanaan = (PerencanaanView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewPerencanaan != null)
                                mainViewPerencanaan.ShowSnackbar(msg, type);
                            break;
                        case "distribusi":
                            var mainViewDistribusi = (DistribusiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewDistribusi != null)
                                mainViewDistribusi.ShowSnackbar(msg, type);
                            break;
                        case "personalia":
                            var mainViewPersonalia = (PersonaliaView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewPersonalia != null)
                                mainViewPersonalia.ShowSnackbar(msg, type);
                            break;
                        case "akuntansi":
                            var mainViewAkuntansi = (AkuntansiView)Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                            if (mainViewAkuntansi != null)
                                mainViewAkuntansi.ShowSnackbar(msg, type);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public static Task<object> ShowDialogGlobalYesCancelViewAsync(
            bool isTest = false,
            bool dispatcher = false,
            string identfier = "main",
            string header = "",
            string message = "",
            string type = "success",
            ICommand yesButtonCommand = null,
            string yesButtonText = "Ya",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool isBackgroundProcessOnConfirm = false,
            string moduleName = "main",
            ICommand cancelButtonCommand = null,
            string highlightText = null,
            bool isEnabledCetakLaporan = false,
            Action yesAction = null)
        {
            object response = null;
            if (!isTest)
            {
                if (dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => response = await DialogHost.Show(new DialogGlobalYesCancelView(header,
                        message, type, yesButtonCommand, yesButtonText,
                        cancelButtonText, hideCancel, isBackgroundProcessOnConfirm, moduleName, cancelButtonCommand, highlightText, isEnabledCetakLaporan, yesAction), identfier));
                else
                    response = DialogHost.Show(new DialogGlobalYesCancelView(header,
                        message, type, yesButtonCommand, yesButtonText,
                        cancelButtonText, hideCancel, isBackgroundProcessOnConfirm, moduleName, cancelButtonCommand, highlightText, isEnabledCetakLaporan, yesAction), identfier);
            }
            return Task.FromResult(response);
        }


        public static Task<object> ShowDialogGlobal3MessagesViewAsync(
            bool isTest = false,
            bool Dispatcher = false,
            string Identfier = "main",
            string header = "",
            string message = "",
            string message1 = "",
            string message2 = "",
            string type = "success",
            ICommand yesButtonCommand = null,
            string yesButtonText = "Ya",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool isBackgroundProcessOnConfirm = false,
            string moduleName = "main",
            ICommand cancelButtonCommand = null)
        {
            object response = null;
            if (!isTest)
            {
                if (Dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => response = await DialogHost.Show(new DialogGlobal3MessagesView(header,
                        message, message1, message2, type, yesButtonCommand, yesButtonText,
                        cancelButtonText, hideCancel, isBackgroundProcessOnConfirm, moduleName, cancelButtonCommand), Identfier));
                else
                    response = DialogHost.Show(new DialogGlobal3MessagesView(header,
                        message, message1, message2, type, yesButtonCommand, yesButtonText,
                        cancelButtonText, hideCancel, isBackgroundProcessOnConfirm, moduleName, cancelButtonCommand), Identfier);
            }
            return Task.FromResult(response);
        }

        public static Task<object> ShowCustomDialogViewAsync(bool isTest = false, bool dispatcher = false, string identfier = "main", Func<object> getDialog = null)
        {
            object response = null;
            if (!isTest && getDialog != null)
            {
                if (dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => response = await DialogHost.Show(getDialog(), identfier));
                else
                    response = DialogHost.Show(getDialog(), identfier);
            }
            return Task.FromResult(response);
        }

        public static void ShowInvalidAkses(bool isTest = false)
        {
            if (!isTest)
            {
                _ = ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "BillingRootDialog",
                    "Hak Akses",
                    $"Hak Akses Tidak Diizinkan !!",
                    "warning",
                    moduleName: "billing");
            }
        }

        public static void ShowPeriodeIsClosed(bool isTest = false)
        {
            if (!isTest)
            {
                _ = ShowDialogGlobalViewAsync(
                    false,
                    true,
                    "BillingRootDialog",
                    "Periode Tutup",
                    $"Perubahan tidak diijinkan untuk periode rekening yang tutup !!",
                    "warning",
                    moduleName: "billing");
            }
        }


        public static Task<object> ShowDialogGlobalProgressBarViewAsync(
            bool isTest = false,
            bool Dispatcher = false,
            string Identfier = "main",
            string header = "",
            string message = "",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool hideSembunyikan = false,
            string moduleName = "main",
            object dataContext = null,
            BackgroundWorker backgroundWorker = null,
            ICommand onWorkerCompleteCommand = null,
            string successMessage = null,
            string errorMessage = null)
        {
            object Response = null;
            if (!isTest)
            {
                if (Dispatcher)
                    _ = Application.Current.Dispatcher.Invoke(
                        async () => Response = await DialogHost.Show(new DialogGlobalProgressBarView(header,
                        message, cancelButtonText, hideCancel, hideSembunyikan, moduleName, dataContext, backgroundWorker, onWorkerCompleteCommand, successMessage, errorMessage), Identfier));
                else
                    Response = DialogHost.Show(new DialogGlobalProgressBarView(header,
                        message, cancelButtonText, hideCancel, hideSembunyikan, moduleName, dataContext, backgroundWorker, onWorkerCompleteCommand, successMessage, errorMessage), Identfier);
            }
            return Task.FromResult(Response);
        }
    }
}
