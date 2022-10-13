using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Hublang.Pelayanan.Info;

namespace SiPintar.Views.Hublang.Pelayanan.Info
{
    public partial class FotoDenahView : UserControl
    {
        // Zoom
        private readonly double zoomMax = 5;
        private readonly double zoomMin = 0.5;
        private readonly double zoomSpeed = 0.001;
        private double zoom = 1;

        // Image
        private Image currentImage;
        private Point clickPosition;
        private readonly DenahViewModel _viewModel;
        private readonly List<BitmapImage> ListImage;
        private int CurrentIndex;

        public FotoDenahView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (DenahViewModel)DataContext;

            ListImage = _viewModel.FotoForm;
            CurrentIndex = 0;

            CheckImage();

            PreviewKeyUp += new KeyEventHandler(HandleKeyboard);
        }

        private void HandleKeyboard(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    break;
                case Key.Left:
                    Previous_Click();
                    break;
                case Key.Right:
                    Next_Click();
                    break;
                default:
                    break;
            }
        }

        private void Previous_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (CurrentIndex > 0)
                CurrentIndex--;

            CheckImage();
        }
        private void Next_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (CurrentIndex < (ListImage.Count - 1))
                CurrentIndex++;

            CheckImage();
        }

        private void CheckImage()
        {
            canvas_Draw.Children.Clear();

            // Init image
            currentImage = new Image
            {
                Source = ListImage[CurrentIndex]
            };

            double left = (canvas_Draw.ActualWidth - currentImage.ActualWidth) / 2;
            double top = (canvas_Draw.ActualHeight - currentImage.ActualHeight) / 2;
            Canvas.SetLeft(currentImage, left); // Set image position to top left.
            Canvas.SetTop(currentImage, top);
            canvas_Draw.Children.Add(currentImage); // Add image to canvas.

            PrevButton.IsEnabled = CurrentIndex > 0;
            NextButton.IsEnabled = CurrentIndex < (ListImage.Count - 1);

            PrevButton.Background = PrevButton.IsEnabled ? (SolidColorBrush)new BrushConverter().ConvertFrom("#a7ddf7") : (SolidColorBrush)new BrushConverter().ConvertFrom("#eeeff3");
            NextButton.Background = NextButton.IsEnabled ? (SolidColorBrush)new BrushConverter().ConvertFrom("#a7ddf7") : (SolidColorBrush)new BrushConverter().ConvertFrom("#eeeff3");
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OnDownloadFotoCommand.Execute(currentImage);
        }

        // Zoom on Mouse wheel
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            Point mousePos = e.GetPosition(canvas_Draw);

            if (zoom > 1)
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom); // transform Canvas size
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas_Draw.Cursor = Cursors.Hand;
            clickPosition = e.GetPosition(canvas_Draw); // get click position
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
            {
                Point mousePos = e.GetPosition(canvas_Zoom); // get absolute mouse position
                Canvas.SetLeft(canvas_Draw, mousePos.X - clickPosition.X); // move canvas
                Canvas.SetTop(canvas_Draw, mousePos.Y - clickPosition.Y);
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            zoom += 0.1;
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            if (zoom > 1)
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom, canvas_Draw.ActualWidth / 2, canvas_Draw.ActualHeight / 2);
            }
            else
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom);
            }

            // CheckZoomButton();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            zoom -= 0.1;
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            if (zoom > 1)
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom, canvas_Draw.ActualWidth / 2, canvas_Draw.ActualHeight / 2);
            }
            else
            {
                canvas_Draw.RenderTransform = new ScaleTransform(zoom, zoom);
            }

            // CheckZoomButton();
        }

    }
}
