using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    public partial class VideoView : UserControl
    {
        private readonly RekeningAirViewModel _viewModel;

        public VideoView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (RekeningAirViewModel)DataContext;

            PreviewKeyUp += new KeyEventHandler(HandleEsc);

            InitiateVideo();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void InitiateVideo()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += Timer_Tick;
            timer.Start();

            Video.Play();
            //Video.Pause();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Video.Source != null)
            {
                if (Video.NaturalDuration.HasTimeSpan)
                {
                    var videoPosition = Video.Position;
                    var videoDuration = Video.NaturalDuration.TimeSpan;
                    VideoIndicator.Width = Math.Min((double)videoPosition.Ticks / (double)videoDuration.Ticks * BorderMedia.Width, BorderMedia.Width);
                    Status.Text = $"{videoPosition.ToString(@"mm\:ss")} / {videoDuration.ToString(@"mm\:ss")}";
                    if (videoPosition == videoDuration)
                    {
                        Video.Stop();
                        VideoActionButton.IsChecked = false;
                    }
                }
            }
            else
            {
                Status.Text = "--/--";
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Video.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Video.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Video.Stop();
        }

        private void VideoActionButton_Checked(object sender, RoutedEventArgs e)
        {
            Video.Play();
        }

        private void VideoActionButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Video.Pause();
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            // _viewModel.OnDownloadVideoCommand.Execute(Video);
        }
    }
}
