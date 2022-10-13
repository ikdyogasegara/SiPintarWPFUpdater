using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SiPintar.ViewModels.Billing.Bantuan;

namespace SiPintar.Views.Billing.Bantuan
{

    public partial class FAQView : UserControl
    {

        public FAQView()
        {
            InitializeComponent();
        }

        private void Searching_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            var _viewModel = (FaqViewModel)DataContext;

            _viewModel.SearchCommand.Execute(null);
        }
    }

    public static class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html",
            typeof(string),
            typeof(BrowserBehavior),
            new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string)d.GetValue(HtmlProperty);
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string HtmlToRender = e.NewValue as string;
            HtmlToRender = ReplaceImagePathAuto(HtmlToRender);

            if (d is WebBrowser wb)
                wb.NavigateToString(HtmlToRender);
        }

        public static readonly string FILE_URL_PREFIX = "file://";
        public static readonly string PATH_SEPARATOR = "/";
        public static string ReplaceImagePath(string html, string path)
        {
            return html
                .Replace("../", string.Empty)
                .Replace("../", string.Empty)
                .Replace("./", string.Empty)
                .Replace("Assets/", path + "FAQ/Assets/");
        }
        public static string ReplaceImagePathAuto(string html)
        {
            string executableDirectoryName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string replaceWith = FILE_URL_PREFIX
                + executableDirectoryName
                + PATH_SEPARATOR;

            return ReplaceImagePath(html, replaceWith);
        }
    }
}
