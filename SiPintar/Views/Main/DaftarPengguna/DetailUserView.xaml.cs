using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace SiPintar.Views.Main.DaftarPengguna
{
    public partial class DetailUserView : UserControl
    {
        public DetailUserView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ((CheckBox)sender).IsEnabled = false;
        }
    }
}
