using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;

namespace SiPintar.Views.Global.Dialog
{
    /// <summary>
    /// Interaction logic for DialogGlobalProgressBarView.xaml
    /// </summary>
    public partial class DialogGlobalProgressBarView : UserControl
    {
        private readonly ICommand _onWorkerCompleteCommand;
        private readonly BackgroundWorker _backgroundWorker;
        private readonly string _identfier;
        private readonly string _modulName;

        public DialogGlobalProgressBarView(
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
            InitializeComponent();

            DataContext = dataContext;

            _backgroundWorker = backgroundWorker;
            _onWorkerCompleteCommand = onWorkerCompleteCommand;
            _modulName = moduleName;

            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);

            _identfier = string.Concat(moduleName[0].ToString().ToUpper(), moduleName.AsSpan(1)) + "RootDialog";

            SetDisplay(header, message, cancelButtonText, hideCancel, hideSembunyikan, moduleName);

            if (!hideSembunyikan)
                PreviewKeyUp += new KeyEventHandler(HandleEsc);

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void SetDisplay(string header, string message, string cancelButtonText, bool hideCancel, bool hideSembunyikan, string moduleName)
        {
            DialogHeader.Text = header;
            DialogMessage.Text = message;
            CancelButton.Content = cancelButtonText;

            if (hideCancel)
                CancelButton.Visibility = Visibility.Collapsed;

            if (hideSembunyikan)
            {
                CloseButton.Visibility = Visibility.Collapsed;
                SembunyikanButton.Visibility = Visibility.Collapsed;
            }


            switch (moduleName)
            {
                case "bacameter":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppBacameter"];
                    break;
                case "billing":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppBilling"];
                    break;
                case "loket":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppLoket"];
                    break;
                case "hublang":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppHublang"];
                    break;
                case "gudang":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppGudang"];
                    break;
                case "perencanaan":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppPerencanaan"];
                    break;
                case "distribusi":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppDistribusi"];
                    break;
                case "akuntansi":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppAkuntansi"];
                    break;
                default:
                    break;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (_backgroundWorker != null)
            {
                if (_backgroundWorker.WorkerSupportsCancellation)
                {
                    _backgroundWorker.CancelAsync();
                }
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBarStatus.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_backgroundWorker != null)
            {
                if (e.Cancelled)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else if (e.Error != null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", e.Error.Message, "error"), _identfier);
                }
                else
                {
                    string[] res = e.Result as string[];

                    DialogHost.CloseDialogCommand.Execute(null, null);
                    if (res[0] == "error")
                    {
                        _ = DialogHost.Show(new DialogGlobalView("Terjadi Kesalahan", res[1], "error"), _identfier);
                    }
                    else
                    {
                        DialogHelper.ShowSnackbar(false, "success", res[1], _modulName);
                        _ = ((AsyncCommandBase)_onWorkerCompleteCommand).ExecuteAsync(null);
                    }
                }
            }

        }
    }
}
