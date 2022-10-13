using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SiPintar.Utilities;
using SiPintar.ViewModels;

namespace SiPintar.Views
{
    public partial class LoginView : Window
    {
        public LoginView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            VersionInformation.Text = AppVersion.Version;
            NamaPdam.Text = Application.Current.Resources["NamaPdam"].ToString();

            Closed += new EventHandler(MainWindow_Closed);
        }

        private void ShortcutKey(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    {
                        if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(PasswordTextBox.Password))
                        {
                            Login_Click();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void MouseWrapper(object sender, MouseEventArgs e)
        {
            CompWrapper.Focus();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = !string.IsNullOrWhiteSpace(PasswordTextBox.Password) ? Visibility.Hidden : Visibility.Visible;
            if (!string.IsNullOrEmpty(PasswordTextBox.Password))
                ((LoginViewModel)DataContext).ErrorPassword = null;
        }

        private void LupaPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PanelLupaPassword.Visibility = Visibility.Visible;
            PanelLogin.Visibility = Visibility.Collapsed;
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            PanelLupaPassword.Visibility = Visibility.Collapsed;
            PanelLogin.Visibility = Visibility.Visible;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).DisplayAppSettingWindow(DataContext);
        }

        public void ResetForm()
        {
            Username.Text = string.Empty;
            PasswordTextBox.Password = string.Empty;
        }

        private void Username_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(Username.Text))
                ((LoginViewModel)DataContext).ErrorUsername = null;
        }

        private void Login_Click(object sender = null, RoutedEventArgs e = null)
        {
            var viewModel = (LoginViewModel)DataContext;
            if (viewModel.OnLoginCommand.CanExecute(PasswordTextBox))
                _ = Task.Run(() => viewModel.OnLoginCommand.Execute(PasswordTextBox));
        }
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            App.OpenedWindow.Remove("login");
        }
    }
}
