using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GMap.NET.WindowsPresentation;

namespace SiPintar.Views.Global.Other
{
    public partial class MapMarkerView
    {
        private readonly Popup _popup;
        private readonly Label Label;
        private readonly GMapMarker _marker;

        public MapMarkerView(GMapMarker marker, string title, string markerType = "red")
        {
            InitializeComponent();

            _marker = marker;

            _popup = new Popup();
            Label = new Label();

            Loaded += CustomMarkerDemo_Loaded;
            SizeChanged += CustomMarkerDemo_SizeChanged;
            MouseEnter += MarkerControl_MouseEnter;
            MouseLeave += MarkerControl_MouseLeave;
            MouseLeftButtonUp += CustomMarkerDemo_MouseLeftButtonUp;
            MouseLeftButtonDown += CustomMarkerDemo_MouseLeftButtonDown;

            _popup.Placement = PlacementMode.Mouse;
            {
                Label.Background = Brushes.White;
                Label.Foreground = GetPopupColor(markerType);
                Label.BorderBrush = GetPopupColor(markerType);
                Label.BorderThickness = new Thickness(0.5);
                Label.Padding = new Thickness(5);
                Label.Margin = new Thickness(3);
                Label.FontSize = 15;
                Label.Content = title;
                Label.FontFamily = new FontFamily("Arial");
            }
            _popup.Child = Label;

            Icon.Source = GetIcon(markerType);
        }

        private BitmapImage GetIcon(string markerType)
        {
            return markerType switch
            {
                "blue" => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_blue.png", UriKind.Relative)),
                "green" => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_green.png", UriKind.Relative)),
                "pink" => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_pink.png", UriKind.Relative)),
                "purple" => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_purple.png", UriKind.Relative)),
                "yellow" => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_yellow.png", UriKind.Relative)),
                _ => new BitmapImage(new Uri(@"/SiPintar;component/Assets/Images/Map/ic_marker_red.png", UriKind.Relative)),
            };
        }

        private SolidColorBrush GetPopupColor(string markerType)
        {
            return markerType switch
            {
                "blue" => (SolidColorBrush)Application.Current.Resources["SecondaryBlue"],
                "green" => (SolidColorBrush)Application.Current.Resources["SuccessSalem"],
                "pink" => (SolidColorBrush)Application.Current.Resources["Modul"],
                "purple" => (SolidColorBrush)Application.Current.Resources["CeruleanHover"],
                "yellow" => (SolidColorBrush)Application.Current.Resources["WarningSunshade"],
                _ => (SolidColorBrush)Application.Current.Resources["Modul"],
            };
        }

        private void CustomMarkerDemo_Loaded(object sender, RoutedEventArgs e)
        {
            if (Icon.Source.CanFreeze)
            {
                Icon.Source.Freeze();
            }
        }

        private void CustomMarkerDemo_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _marker.Offset = new Point(-e.NewSize.Width / 2, -e.NewSize.Height);
        }

        private void CustomMarkerDemo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {
                Mouse.Capture((IInputElement)this);
            }
        }

        private void CustomMarkerDemo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Mouse.Capture(null);
            }
        }

        private void MarkerControl_MouseLeave(object sender, MouseEventArgs e)
        {
            _marker.ZIndex -= 10000;
            _popup.IsOpen = false;
        }

        private void MarkerControl_MouseEnter(object sender, MouseEventArgs e)
        {
            _marker.ZIndex += 10000;
            _popup.IsOpen = true;
        }
    }
}
