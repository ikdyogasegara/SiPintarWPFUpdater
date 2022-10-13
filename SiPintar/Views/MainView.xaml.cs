using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using MaterialDesignExtensions.Controls;
using MaterialDesignExtensions.Model;
using MaterialDesignThemes.Wpf;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Dialog;
using Squirrel;

namespace SiPintar.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IDisposable
    {
        private NotifyIcon _ni;
        private UpdateManager _manager;

        public MainView(object dataContext)
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            AddVersionNumber();
            //CheckVersionUpdate();

            DataContext = dataContext;

            Closed += new EventHandler(MainWindow_Closed);

            NotifyHandle();
        }

        #region updater
        private void AddVersionNumber()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Dispatcher.Invoke(() =>
            {
                this.Title += $" v.{versionInfo.FileVersion}";
            });
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _manager = await UpdateManager
                    .GitHubUpdateManager(@"https://github.com/ikdyogasegara/SiPintarWPFUpdater");

                CurrentVersionTextBox.Text = _manager.CurrentlyInstalledVersion().ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Failed to check update ; " + exception.Message);
            }
        }

        private async void CheckVersionUpdate()
        {
            try
            {
                using var mgr = await UpdateManager.GitHubUpdateManager(
                    "https://github.com/ikdyogasegara/SiPintarWPFUpdater");
                CurrentVersionTextBox.Text = mgr.CurrentlyInstalledVersion().ToString();
                var release = await mgr.UpdateApp();
                System.Windows.MessageBox.Show($"Pembaruan Aplikasi Tersedia {release.Version}...Tutup dan jalankan ulang aplikasi");
                Process.Start(System.Windows.Application.ResourceAssembly.Location);
                System.Windows.Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to check update ; " + e.Message);
            }
        }

        private async void CheckForUpdatesButton_Click(object sender, RoutedEventArgs e)
        {
            var updateInfo = await _manager.CheckForUpdate();
            UpdateButton.IsEnabled = updateInfo.ReleasesToApply.Count > 0;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await _manager.UpdateApp();
            System.Windows.MessageBox.Show("Updated succesfuly!");
            Process.Start(System.Windows.Application.ResourceAssembly.Location);
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        private void NotifyHandle()
        {
            _ni = new NotifyIcon();

            var iconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/SiPintar;component/Assets/Images/Application/main_icon.ico")).Stream;
            _ni.Icon = new Icon(iconStream);
            _ni.BalloonTipTitle = "Si Pintar PDAM";
            _ni.BalloonTipText = "Double-Klik untuk membuka menu utama";
            _ni.Visible = true;
            _ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    var viewModel = (MainViewModel)DataContext;

                    Show();
                    viewModel.WindowState = WindowState.Normal;
                };

            _ni.BalloonTipClosed += (sender, e) =>
            {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
            };
        }

        protected override void OnStateChanged(EventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;

            if (WindowState == WindowState.Minimized && viewModel.IsMinimizeToTray == true)
                Hide();

            base.OnStateChanged(e);
        }

        private void NavigationItemSelectedHandler(object sender, NavigationItemSelectedEventArgs args)
        {
            var viewModel = (MainViewModel)DataContext;

            var nav = (FirstLevelNavigationItem)args.NavigationItem;
            //Debug.WriteLine("Get User Data From NavigationItemSelectedHandler") use to trace LiteDB
            if (viewModel != null && nav != null)
                viewModel.UpdatePage(nav.Label);
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            App.OpenedWindow.Remove("main");

            _ni.Icon.Dispose();
            _ni.Dispose();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            _ = DialogHost.Show(new DialogCloseWindow(), "MainRootDialog");
        }

        public void ShowSnackbar(string message, string type, int timeout = 5000)
        {
            // Snackbar.SetAccessor(this);
            Snackbar.Visibility = Visibility.Visible;
            Snackbar.ShowHandlerDialog(message, type);
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(timeout)
            };
            timer.Tick += (sender, args) =>
            {
                Snackbar.Visibility = Visibility.Collapsed;
                timer.Stop();
            };
            timer.Start();
        }

        public void CloseSnackbar()
        {
            Snackbar.Visibility = Visibility.Collapsed;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
            typeof(BindingProxy), new UIPropertyMetadata(null));
    }

}
