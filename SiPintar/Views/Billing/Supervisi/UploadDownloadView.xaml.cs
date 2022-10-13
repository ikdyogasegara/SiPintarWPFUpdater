using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi
{
    public partial class UploadDownloadView : UserControl
    {
        private const int GWL_STYLE = -16;
        private const uint MF_BYCOMMAND = 0x00000000;
        private const uint MF_BYPOSITION = 0x00000400;
        private const uint SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern uint RemoveMenu(IntPtr hMenu, uint nPosition, uint wFlags);

        [DllImport("user32.dll")]
        private static extern bool DrawMenuBar(IntPtr hWnd);

        private UploadDownloadViewModel _viewModel;
        public UploadDownloadView()
        {
            InitializeComponent();
        }

        public void OnLoadComponent()
        {
            _viewModel = (UploadDownloadViewModel)DataContext;

            _viewModel.DisableCloseEvent += (State) =>
            {
                SetClosebutton(State);
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel = (UploadDownloadViewModel)DataContext;

            _viewModel.TagihanAir = true;
            _viewModel.TagihanLimbah = true;
            _viewModel.TagihanLltt = true;
            //_viewModel.SinkronParameter = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _viewModel = (UploadDownloadViewModel)DataContext;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _viewModel.ProgressVis = false;
                _viewModel.IsUploadDone = false;
                _viewModel.CanReUpload = false;
            });
        }

        public void SetClosebutton(bool State)
        {
            var wih = new WindowInteropHelper(Application.Current.MainWindow);
            IntPtr hWnd = wih.Handle;
            if (State)
            {
                _ = GetSystemMenu(hWnd, true);  // beachte true
                DrawMenuBar(hWnd);
            }
            else
            {
                IntPtr hMenu = GetSystemMenu(hWnd, false);
                RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
            }
        }

        private void Component_Loaded(object sender, RoutedEventArgs e)
        {
            OnLoadComponent();
        }
    }
}
