using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SiPintar.ViewModels.Bacameter;

namespace SiPintar.Views.Bacameter.Supervisi
{
    public partial class FotoStanView : UserControl
    {
        private readonly SupervisiViewModel _viewModel;

        // Zoom
        private readonly double zoomMax = 5;
        private readonly double zoomMin = 0.5;
        private readonly double zoomSpeed = 0.001;
        private double zoom = 1;

        // Image
        private Image currentImage;
        private Point clickPosition;

        public FotoStanView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (SupervisiViewModel)DataContext;

            CheckImage();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckImage()
        {
            canvas_Draw.Children.Clear();

            // Init image
            currentImage = new Image { Source = _viewModel.FileFotoStan };

            double left = (canvas_Draw.ActualWidth - currentImage.ActualWidth) / 2;
            double top = (canvas_Draw.ActualHeight - currentImage.ActualHeight) / 2;
            Canvas.SetLeft(currentImage, left); // Set image position to top left.
            Canvas.SetTop(currentImage, top);
            canvas_Draw.Children.Add(currentImage); // Add image to canvas.
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
        }
    }
}
