using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SiPintar.Views.Global.Other
{
    public partial class SnackbarView : UserControl
    {
        public Func<SnackbarView, bool> CloseSnackbar;

        public SnackbarView()
        {
            InitializeComponent();
        }

        public void ShowHandlerDialog(string message, string type)
        {
            MessageTextBlock.Text = message;

            BgSnackbar.Background = type switch
            {
                "success" => (Brush)Application.Current.Resources["SuccessSofter"],
                "warning" => (Brush)Application.Current.Resources["WarningSofter"],
                "danger" => (Brush)Application.Current.Resources["PrimarySofter"],
                _ => (Brush)Application.Current.Resources["Astronaut"],
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CloseSnackbar != null)
            {
                _ = CloseSnackbar(this);
            }
        }
    }
}
