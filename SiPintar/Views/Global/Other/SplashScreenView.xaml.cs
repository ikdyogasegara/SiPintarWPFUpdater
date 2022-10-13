using System.Windows;
using SiPintar.Utilities;

namespace SiPintar.Views.Global.Other
{
    public partial class SplashScreenView : Window, ISplashScreen
    {
        public SplashScreenView()
        {
            InitializeComponent();
            VersionInformation.Text = AppVersion.Version;
        }

        public void AddMessage(string message)
        {
            Dispatcher.Invoke(delegate ()
            {
                UpdateMessageTextBox.Text = message;
            });
        }

        public void LoadComplete()
        {
            Dispatcher.InvokeShutdown();
        }
    }

    public interface ISplashScreen
    {
        void AddMessage(string message);
        void LoadComplete();
    }
}
