using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SiPintar.Views.Global.Other
{
    public partial class RatingControlView : StackPanel
    {
        public RatingControlView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RatingValueProperty = DependencyProperty.Register(
            "RatingValue",
            typeof(int),
            typeof(RatingControlView),
            new PropertyMetadata(0, new PropertyChangedCallback(RatingValueChanged)));

        public int RatingValue
        {
            get
            {

                return (int)GetValue(RatingValueProperty);
            }
            set
            {
                if (value < 0)
                {
                    SetValue(RatingValueProperty, 0);
                }
                else if (value > 5)
                {
                    SetValue(RatingValueProperty, 5);
                }
                else
                {
                    SetValue(RatingValueProperty, value);
                }
            }
        }

        private static void RatingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var parent = sender as RatingControlView;
            var ratingValue = (int)e.NewValue;
            UIElementCollection children = parent.Children;

            ToggleButton button;
            for (var i = 0; i < ratingValue; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = true;
            }

            for (var i = ratingValue; i < children.Count; i++)
            {
                button = children[i] as ToggleButton;
                button.IsChecked = false;
            }
        }

        private void RatingButtonClickEventHandler(object sender, RoutedEventArgs e)
        {
            var button = sender as ToggleButton;
            RatingValue = int.Parse((string)button.Tag);
        }
    }
}
