using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SiPintar.ViewModels.Hublang.Pelayanan.KoreksiRekeningAir;

namespace SiPintar.Views.Hublang.Pelayanan.KoreksiRekeningAir.PermohonanKoreksi
{
    public partial class FotoView : UserControl
    {
        private readonly PermohonanKoreksiViewModel _viewModel;

        // Zoom
        private readonly double zoomMax = 5;
        private readonly double zoomMin = 0.5;
        private readonly double zoomSpeed = 0.001;
        private double _zoom = 1;

        // Image
        private Image _currentImage;
        private Point _clickPosition;

        public FotoView(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
            _viewModel = (PermohonanKoreksiViewModel)DataContext;
            CheckImage();
        }

        private void CheckImage()
        {
            CanvasDraw.Children.Clear();

            // Init image
            _currentImage = new Image { Source = _viewModel.PreviewFile };

            var left = (CanvasDraw.ActualWidth - _currentImage.ActualWidth) / 2;
            var top = (CanvasDraw.ActualHeight - _currentImage.ActualHeight) / 2;
            Canvas.SetLeft(_currentImage, left); // Set image position to top left.
            Canvas.SetTop(_currentImage, top);
            CanvasDraw.Children.Add(_currentImage); // Add image to canvas.

            var test = CanvasDraw;
        }

        // Zoom on Mouse wheel
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            _zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (_zoom < zoomMin) { _zoom = zoomMin; } // Limit Min Scale
            if (_zoom > zoomMax) { _zoom = zoomMax; } // Limit Max Scale

            var mousePos = e.GetPosition(CanvasDraw);

            if (_zoom > 1)
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom); // transform Canvas size
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CanvasDraw.Cursor = Cursors.Hand;
            _clickPosition = e.GetPosition(CanvasDraw); // get click position
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
            {
                var mousePos = e.GetPosition(CanvasZoom); // get absolute mouse position
                Canvas.SetLeft(CanvasDraw, mousePos.X - _clickPosition.X); // move canvas
                Canvas.SetTop(CanvasDraw, mousePos.Y - _clickPosition.Y);
            }
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            _zoom += 0.1;
            if (_zoom < zoomMin) { _zoom = zoomMin; } // Limit Min Scale
            if (_zoom > zoomMax) { _zoom = zoomMax; } // Limit Max Scale

            if (_zoom > 1)
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom, CanvasDraw.ActualWidth / 2, CanvasDraw.ActualHeight / 2);
            }
            else
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom);
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            _zoom -= 0.1;
            if (_zoom < zoomMin) { _zoom = zoomMin; } // Limit Min Scale
            if (_zoom > zoomMax) { _zoom = zoomMax; } // Limit Max Scale

            if (_zoom > 1)
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom, CanvasDraw.ActualWidth / 2, CanvasDraw.ActualHeight / 2);
            }
            else
            {
                CanvasDraw.RenderTransform = new ScaleTransform(_zoom, _zoom);
            }
        }
    }
}
