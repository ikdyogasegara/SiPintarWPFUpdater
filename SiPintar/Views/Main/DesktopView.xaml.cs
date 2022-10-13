using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using SiPintar.Utilities;

namespace SiPintar.Views.Main
{
    public partial class DesktopView : UserControl
    {
        public DesktopView()
        {
            InitializeComponent();

            VersionInformation.Text = AppVersion.Version;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var url = e.Uri.ToString();
            _ = Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
    }
}
