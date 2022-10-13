using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Global.Dialog
{
    public partial class DialogCloseWindow : UserControl
    {
        public DialogCloseWindow()
        {
            InitializeComponent();
            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            Application.Current.MainWindow.Close();
            Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
        }
    }
}
