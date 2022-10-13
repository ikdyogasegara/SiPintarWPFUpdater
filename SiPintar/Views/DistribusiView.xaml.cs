using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using MaterialDesignThemes.Wpf;
using SiPintar.Helpers;
using SiPintar.ViewModels;
using SiPintar.Views.Global.Other;

namespace SiPintar.Views
{
    /// <summary>
    /// Interaction logic for DistribusiView.xaml
    /// </summary>
    public partial class DistribusiView : Window
    {
        public DistribusiView(object dataContext)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            InitializeComponent();
            DataContext = dataContext;

            RootWindow.Margin = new Thickness(7);
            Closed += new EventHandler(MainWindow_Closed);
            StateChanged += View_StateChanged;
        }

        private void View_StateChanged(object sender, EventArgs e)
        {
            RootWindow.Margin = WindowState == WindowState.Maximized ? new Thickness(7) : new Thickness(0);
        }

        public void OnPrepare()
        {
            var dC = DataContext as DistribusiViewModel;
            Onboard.DataContext = dC._onboardingViewModel;
        }

        public void ShowSnackbar(string message, string type, int timeout = 5000)
        {
            var Sb = new SnackbarView() { CloseSnackbar = CloseSb };
            if (Sb != null)
            {
                Sb.ShowHandlerDialog(message, type);
                SnackbarContainer.Children.Add(Sb);
                _ = Task.Run(async () =>
                {
                    await Task.Delay(timeout);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (SnackbarContainer.Children.IndexOf(Sb) >= 0)
                        {
                            SnackbarContainer.Children.RemoveAt(SnackbarContainer.Children.IndexOf(Sb));
                        }
                    });
                });
            }
        }

        private bool CloseSb(SnackbarView Sb)
        {
            if (SnackbarContainer.Children.IndexOf(Sb) >= 0)
            {
                SnackbarContainer.Children.RemoveAt(SnackbarContainer.Children.IndexOf(Sb));
            }
            return true;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ((DistribusiViewModel)DataContext).OnBackToMainMenuCommand.Execute("distribusi");
        }

        public void AddToBgProcess(BackgroundProcessHelper.ProcessObject data)
        {
            ((DistribusiViewModel)DataContext).OnAddToBgProcessCommand.Execute(data);
        }

        /// <summary>
        /// TitleBar_MouseDown - Drag if single-click, resize if double-click
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    DragMove();
                }
        }

        /// <summary>
        /// CloseButton_Clicked
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ((DistribusiViewModel)DataContext).OnBackToMainMenuCommand.Execute("distribusi");
        }

        /// <summary>
        /// MaximizedButton_Clicked
        /// </summary>
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        /// <summary>
        /// Minimized Button_Clicked
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Adjusts the WindowSize to correct parameters when Maximize button is clicked
        /// </summary>
        private void AdjustWindowSize()
        {
            var restoreIcon = RestoreIcon as PackIcon;
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                restoreIcon.Kind = PackIconKind.WindowMaximize;
            }
            else
            {
                WindowState = WindowState.Maximized;
                restoreIcon.Kind = PackIconKind.WindowRestore;
            }

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
