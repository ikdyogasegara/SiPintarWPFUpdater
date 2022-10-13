using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SiPintar.Helpers;

namespace SiPintar.Views.Global.Dialog
{
    /// <summary>
    /// Interaction logic for DialogInputQtyView.xaml
    /// </summary>
    public partial class DialogInputQtyView : UserControl
    {
        private readonly Func<decimal, bool>? _callbackDecimal;
        private readonly Func<int, bool>? _callbackInt;
        public DialogInputQtyView(Func<decimal, bool>? callbackDecimal, Func<int, bool>? callbackInt, string? moduleName, string? title, string? titleContent, string? subtitle, string? subtitleContent)
        {
            InitializeComponent();
            _callbackDecimal = callbackDecimal;
            _callbackInt = callbackInt;
            TotalQty.GotFocus += DecimalValidationHelper.Object_GotFocus;
            TotalQty.LostFocus += DecimalValidationHelper.Object_LostFocus;
            TotalQty.PreviewTextInput += DecimalValidationHelper.DecimalValidationTextBox;
            TotalQty.Text = "1";

            CheckSubmit();
            SetDisplay(moduleName, title, titleContent, subtitle, subtitleContent);
        }

        private void SetDisplay(string? moduleName, string? title, string? titleContent, string? subtitle, string? subtitleContent)
        {
            Title.Visibility = TitleContent.Visibility = Visibility.Collapsed;
            Subtitle.Visibility = SubtitleContent.Visibility = Visibility.Collapsed;

            if (title != null && titleContent != null)
            {
                Title.Text = title;
                TitleContent.Text = titleContent;
                Title.Visibility = TitleContent.Visibility = Visibility.Visible;
            }

            if (title != null && titleContent != null)
            {
                Subtitle.Text = subtitle;
                SubtitleContent.Text = subtitleContent;
                Subtitle.Visibility = SubtitleContent.Visibility = Visibility.Visible;
            }

            switch (moduleName)
            {
                case "bacameter":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppBacameter"];
                    break;
                case "billing":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppBilling"];
                    break;
                case "loket":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppLoket"];
                    break;
                case "hublang":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppHublang"];
                    break;
                case "gudang":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppGudang"];
                    break;
                case "perencanaan":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppPerencanaan"];
                    break;
                case "distribusi":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppDistribusi"];
                    break;
                case "akuntansi":
                    Header.Background = TextEsc.Foreground = (SolidColorBrush)Application.Current.Resources["BaseAppAkuntansi"];
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_callbackDecimal != null)
            {
                _ = _callbackDecimal(DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text));
            }
            if (_callbackInt != null)
            {
                _ = _callbackInt(IntegerValidationHelper.FormatStringIdToInteger(TotalQty.Text));
            }
        }

        private void CheckSubmit()
        {
            BtnSubmit.IsEnabled = DecimalValidationHelper.FormatStringIdToDecimal(TotalQty.Text) > 0;
        }

        private void TotalQty_TextChanged(object sender, TextChangedEventArgs e) => CheckSubmit();
    }
}
