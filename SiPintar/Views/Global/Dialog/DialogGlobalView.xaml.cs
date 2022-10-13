using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Global.Dialog
{
    public partial class DialogGlobalView : UserControl
    {
        public DialogGlobalView(string header = "", string message = "", string type = "success", string buttonText = "Ok", bool hideButton = false, string moduleName = "main")
        {
            InitializeComponent();

            SetDisplay(header, message, type, buttonText, hideButton, moduleName);

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public void SetDisplay(string header, string message, string type, string buttonText, bool hideButton, string moduleName)
        {
            DialogHeader.Text = header;
            DialogMessage.Text = message;
            ButtonFirst.Content = buttonText;

            if (hideButton)
                ButtonFirst.Visibility = Visibility.Collapsed;

            switch (type)
            {
                case "success":
                    DialogImage.Source = new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_success.png", UriKind.RelativeOrAbsolute));
                    break;

                case "error":
                    DialogImage.Source = new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_error.png", UriKind.RelativeOrAbsolute));
                    break;

                case "warning":
                    DialogImage.Source = new BitmapImage(new Uri("/SiPintar;component/Assets/Images/ResponseIcon/ic_warning.png", UriKind.RelativeOrAbsolute));
                    break;

                default:
                    break;
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
                case "perencanaan":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppPerencanaan"];
                    break;
                case "distribusi":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppDistribusi"];
                    break;
                case "gudang":
                    HighlightColor.Background = (SolidColorBrush)Application.Current.Resources["BaseAppGudang"];
                    break;
                default:
                    break;
            }
        }
    }
}
