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
    /// <summary>
    /// Interaction logic for DialogGlobal3MessagesView.xaml
    /// </summary>
    public partial class DialogGlobal3MessagesView : UserControl
    {
        private readonly ICommand _yesAction;
        private readonly ICommand _cancelAction;
        private readonly bool _isBackgroundProcessOnConfirm;
        public DialogGlobal3MessagesView(
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
            InitializeComponent();

            _yesAction = yesButtonCommand;
            _cancelAction = cancelButtonCommand;
            _isBackgroundProcessOnConfirm = isBackgroundProcessOnConfirm;

            SetDisplay(header, message, message1, message2, type, yesButtonText, cancelButtonText, hideCancel, moduleName);

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void SetDisplay(string header, string message, string message1, string message2, string type, string yesButtonText, string cancelButtonText, bool hideCancel, string moduleName)
        {
            DialogHeader.Text = header;
            DialogMessage.Text = message;
            DialogMessage1.Text = message1;
            DialogMessage2.Text = message2;
            YesButton.Content = yesButtonText;
            CancelButton.Content = cancelButtonText;

            DialogMessage.Visibility = string.IsNullOrWhiteSpace(message) ? Visibility.Collapsed : Visibility.Visible;
            DialogMessage1.Visibility = string.IsNullOrWhiteSpace(message1) ? Visibility.Collapsed : Visibility.Visible;
            DialogMessage2.Visibility = string.IsNullOrWhiteSpace(message2) ? Visibility.Collapsed : Visibility.Visible;

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
                default:
                    break;
            }
        }

        private void YesButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isBackgroundProcessOnConfirm)
            {
                _yesAction.Execute(null);
            }
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                _ = Task.Run(() => ((AsyncCommandBase)_yesAction).ExecuteAsync(null));
            }
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
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
