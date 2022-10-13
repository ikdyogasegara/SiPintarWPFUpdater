using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialDesignThemes.Wpf;
using SiPintar.Commands;
using SiPintar.Helpers;
using SiPintar.Utilities;
using SiPintar.ViewModels.Billing.Supervisi;

namespace SiPintar.Views.Billing.Supervisi.RekeningAir
{
    /// <summary>
    /// Interaction logic for FotoMeterView.xaml
    /// </summary>
    ///

    public partial class FotoMeterView : UserControl
    {
        private Point _start;
        private Point _origin;
        private readonly Uri _fotoStanUri;
        private readonly Uri _fotoRumahUri;
        private readonly bool _isFotoRumah;

        public FotoMeterView(string bulan, string tahun, string nosamb, bool isFotoRumah = false)
        {
            InitializeComponent();
            _isFotoRumah = isFotoRumah;
            _fotoStanUri = new Uri(Path.Combine(AppSetting.LokasiFotoMeter, bulan + tahun, $"{nosamb}.jpg"), UriKind.Absolute);
            _fotoRumahUri = new Uri(Path.Combine(AppSetting.LokasiFotoMeter, bulan + tahun, $"{nosamb}R.jpg"), UriKind.Absolute);
            LoadImage();

            PreviewKeyUp += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CheckForm_PreviewKeyUp(object sender, KeyEventArgs e) => CheckButton();

        private void CheckButton()
        {

        }


        private void LoadImage()
        {
            try
            {
                FotoStan.Source = null;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(_isFotoRumah == false ? _fotoStanUri.OriginalString : _fotoRumahUri.OriginalString, UriKind.Absolute);
                bitmapImage.EndInit();
                FotoStan.Stretch = Stretch.Fill;
                FotoStan.Source = bitmapImage;
                imgRotateTransform.Angle = 0;
                BorderFotoStan.Height = FotoStan.Height;
            }
            catch (Exception e)
            {
                DialogHelper.ShowSnackbar(false, e.Message, "danger");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //var param = new Dictionary<string, dynamic>
            //{
            //    { "path", _fotoStanUri.AbsolutePath },
            //    { "path_thumbnails", fotoStanUriThum.AbsolutePath },
            //    { "angle", Convert.ToInt32(imgRotateTransform.Angle) }
            //};
            // _ = Task.Run(() => ((AsyncCommandBase)_vm.OnSaveFotoMeterCommand).ExecuteAsync(param));
        }

        private void FotoStan_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MouseWheel(imgCanvas, imgTransformGroup, e);
        }

        private void Img_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveMagnifier(imgCanvas, FotoStan, e);
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseDown(imgCanvas, FotoStan, imgTranslateTransform, e);
        }

        private void Img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseUp(FotoStan, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rotate(imgRotateTransform);
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn(imgTransformGroup);
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ZoomOut(imgTransformGroup);
        }
        private void BtnFTW_Click(object sender, RoutedEventArgs e)
        {
            FitToContentMagnifier(FotoStan, imgTransformGroup, imgCanvas);
        }

        public void Rotate(RotateTransform rt)
        {
            if (rt.Angle == 270)
                rt.Angle = 0;
            else
                rt.Angle += 90;

            rt.CenterX = 100;
            rt.CenterY = 100;
        }

        public static void ZoomIn(TransformGroup tg)
        {
            var st = (ScaleTransform)tg.Children[0];

            st.ScaleX *= 1.25;
            st.ScaleY *= 1.25;
        }

        public static void ZoomOut(TransformGroup tg)
        {
            var st = (ScaleTransform)tg.Children[0];
            st.ScaleX *= 0.75;
            st.ScaleY *= 0.75;
        }
        public static void FitToContentMagnifier(Image imgObject, TransformGroup tg, Canvas parentCanvas)
        {
            var st = (ScaleTransform)tg.Children[0];
            var tt = (TranslateTransform)tg.Children[1];

            tt.X = 0;
            tt.Y = 0;
            st.CenterX = 0;
            st.CenterY = 0;


            var auxX = parentCanvas.ActualWidth / imgObject.ActualWidth;
            var auxY = parentCanvas.ActualHeight / imgObject.ActualHeight;

            st.ScaleX = auxX > auxY ? auxY : auxX;
            st.ScaleY = auxX > auxY ? auxY : auxX;
        }

        public static new void MouseWheel(Canvas c, TransformGroup tg, MouseWheelEventArgs e)
        {
            var deltaValue = e.Delta;
            var tt = (TranslateTransform)tg.Children[1];
            var st = (ScaleTransform)tg.Children[0];
            //double xSpot = e.GetPosition(c).X;
            //double ySpot = e.GetPosition(c).Y;

            var x = e.GetPosition(c).X - tt.X;
            var y = e.GetPosition(c).Y - tt.Y;

            //Double centerX = st.CenterX * (st.ScaleX - 1);
            //Double centerY = st.CenterY * (st.ScaleY - 1);

            st.CenterX = x;
            st.CenterY = y;



            if (deltaValue > 0)
            {
                st.ScaleX *= 1.25;
                st.ScaleY *= 1.25;
            }
            else
            {

                st.ScaleX *= 0.75;
                st.ScaleY *= 0.75;
            }
        }

        public new void MouseDown(Canvas imgCanvas, Image imgObject, TranslateTransform tt, MouseButtonEventArgs e)
        {
            imgObject.CaptureMouse();
            _start = e.GetPosition(imgCanvas);
            _origin = new Point(tt.X, tt.Y);
        }

        public new void MouseMove(Canvas imgCanvas, Image imgObject, TranslateTransform tt, MouseEventArgs e)
        {
            if (!imgObject.IsMouseCaptured) return;

            Vector v = _start - e.GetPosition(imgCanvas);
            tt.X = _origin.X - v.X;
            tt.Y = _origin.Y - v.Y;
        }

        public new void MouseUp(Image imgObject, MouseButtonEventArgs e)
        {
            imgObject.ReleaseMouseCapture();
        }

        public void MouseMoveMagnifier(Canvas imgCanvas, Image imgObject, MouseEventArgs e)
        {
            Magnifier(imgCanvas, imgObject, e);

            if (!imgObject.IsMouseCaptured) return;
            var tt = (TranslateTransform)((TransformGroup)imgObject.RenderTransform).Children[1];
            Vector v = _start - e.GetPosition(imgCanvas);
            tt.X = _origin.X - v.X;
            tt.Y = _origin.Y - v.Y;


        }

        public void Magnifier(Canvas imgCanvas, Image imgObject, MouseEventArgs e)
        {
            var zoom = 3;

            //String txtDebug = String.Empty;
            //String txtZoom = String.Empty;

            //Size size = imgObject.RenderSize;
            var rt = (RotateTransform)imgObject.LayoutTransform;
            var tt = (TranslateTransform)((TransformGroup)imgObject.RenderTransform).Children[1];
            var st = (ScaleTransform)((System.Windows.Media.TransformGroup)imgObject.RenderTransform).Children[0];
            var x = e.GetPosition(imgCanvas).X - tt.X;
            var y = e.GetPosition(imgCanvas).Y - tt.Y;
            //Point pos = e.MouseDevice.GetPosition(imgCanvas);
            //var wnd = Canvas.GetTop(imgObject);

            var transformGroup = new TransformGroup();
            var scale = new ScaleTransform
            {
                ScaleX = st.ScaleX * zoom,
                ScaleY = st.ScaleY * zoom
            };

            var rotate = new RotateTransform
            {
                Angle = rt.Angle
            };

            var translate = new TranslateTransform();

            var centerX = st.CenterX * (st.ScaleX - 1);
            var centerY = st.CenterY * (st.ScaleY - 1);

            if (rt.Angle == 0)
            {
                translate.X = -(x + centerX) / st.ScaleX;
                translate.Y = -(y + centerY) / st.ScaleY;
                scale.CenterX = (x + centerX) / st.ScaleX;
                scale.CenterY = (y + centerY) / st.ScaleY;
            }
            if (rt.Angle == 90)
            {
                translate.X = -(x + centerX) / st.ScaleX;
                translate.Y = -(y + centerY) / st.ScaleY;
                translate.X += imgObject.ActualHeight * st.ScaleX * zoom;
                scale.CenterX = (x + centerX) / st.ScaleX;
                scale.CenterY = (y + centerY) / st.ScaleY;
            }

            if (rt.Angle == 180)
            {
                translate.X = -(x + centerX) / st.ScaleX;
                translate.Y = -(y + centerY) / st.ScaleY;
                translate.X += imgObject.ActualWidth * st.ScaleX * zoom;
                translate.Y += imgObject.ActualHeight * st.ScaleY * zoom;
                scale.CenterX = (x + centerX) / st.ScaleX;
                scale.CenterY = (y + centerY) / st.ScaleY;
            }

            if (rt.Angle == 270)
            {
                translate.X = -(x + centerX) / st.ScaleX;
                translate.Y = -(y + centerY) / st.ScaleY;
                translate.Y += imgObject.ActualWidth * st.ScaleX * zoom;
                scale.CenterX = (x + centerX) / st.ScaleX;
                scale.CenterY = (y + centerY) / st.ScaleY;
            }

            transformGroup.Children.Add(rotate);
            transformGroup.Children.Add(scale);
            transformGroup.Children.Add(translate);
        }
    }
}
