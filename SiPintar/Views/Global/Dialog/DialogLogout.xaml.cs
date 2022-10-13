using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Global.Dialog
{
    public partial class DialogLogout : UserControl
    {
        public DialogLogout()
        {
            InitializeComponent();

            Title.Text = Application.Current.Resources["logout_confirmation_header"] != null ? Application.Current.Resources["logout_confirmation_header"].ToString() : Title.Text;
            Message.Text = Application.Current.Resources["logout_confirmation_message"] != null ? Application.Current.Resources["logout_confirmation_message"].ToString() : Message.Text;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
