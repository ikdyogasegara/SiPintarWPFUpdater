using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;

namespace SiPintar.Views.Global.Dialog
{
    public partial class DialogGlobalYesCancelView : UserControl
    {
        private readonly ICommand _yesAction;
        private readonly ICommand _cancelAction;
        private readonly bool _isBackgroundProcessOnConfirm;
        private readonly bool _isEnableCetakLaporan;
        private readonly Action _yesActionFunc;
        public DialogGlobalYesCancelView(
            string header = "",
            string message = "",
            string type = "success",
            ICommand yesButtonCommand = null,
            string yesButtonText = "Ya",
            string cancelButtonText = "Batal",
            bool hideCancel = false,
            bool isBackgroundProcessOnConfirm = false,
            string moduleName = "main",
            ICommand cancelButtonCommand = null, string highlightText = null, bool isEnableCetakLaporan = false,
            Action yesAction = null)
        {
            InitializeComponent();

            _yesAction = yesButtonCommand;
            _yesActionFunc = yesAction;
            _cancelAction = cancelButtonCommand;
            _isBackgroundProcessOnConfirm = isBackgroundProcessOnConfirm;
            _isEnableCetakLaporan = isEnableCetakLaporan;

            if (!string.IsNullOrWhiteSpace(highlightText))
            {
                DialogMessage.Visibility = Visibility.Collapsed;
                DialogMessageHighlight.Visibility = Visibility.Visible;
                var idx = message.IndexOf(highlightText);
                textStart.Text = message.Substring(0, idx);
                textHightlight.Text = highlightText;
                textEnd.Text = message.Substring(idx + highlightText.Length, message.Length - (idx + highlightText.Length));
            }
            if (isEnableCetakLaporan)
            {
                CetakLaporanContainer.Visibility = Visibility.Visible;
            }
            SetDisplay(header, message, type, yesButtonText, cancelButtonText, hideCancel, moduleName);

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void SetDisplay(string header, string message, string type, string yesButtonText, string cancelButtonText, bool hideCancel, string moduleName)
        {
            DialogHeader.Text = header;
            DialogMessage.Text = message;
            YesButton.Content = yesButtonText;
            CancelButton.Content = cancelButtonText;

            DialogImage.Source = type switch
            {
                "success" => new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_success.png", UriKind.RelativeOrAbsolute)),
                "error" => new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_error.png", UriKind.RelativeOrAbsolute)),
                "warning" => new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_warning.png", UriKind.RelativeOrAbsolute)),
                "confirmation" => new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_info.png", UriKind.RelativeOrAbsolute)),
                _ => new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/verification.png", UriKind.RelativeOrAbsolute)),
            };

            if (hideCancel)
                CancelButton.Visibility = Visibility.Collapsed;

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

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            bool? cetak = null;
            if (_isEnableCetakLaporan)
            {
                cetak = CetakLaporan.IsChecked;
            }
            if (!_isBackgroundProcessOnConfirm)
            {
                _yesAction?.Execute(cetak);
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                if (_yesAction != null)
                {
                    _ = Task.Run(() => ((AsyncCommandBase)_yesAction).ExecuteAsync(cetak));
                }
                if (_yesActionFunc != null)
                {
                    _ = Task.Run(() => _yesActionFunc?.Invoke());
                }
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cancelAction != null)
            {
                if (!_isBackgroundProcessOnConfirm)
                    _cancelAction.Execute(null);
                else
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    _ = Task.Run(() => ((AsyncCommandBase)_cancelAction).ExecuteAsync(null));
                }
            }
        }
    }
}
