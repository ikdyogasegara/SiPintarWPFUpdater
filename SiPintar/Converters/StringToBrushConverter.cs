using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        public string? FirstBrush { get; set; }
        public string? SecondBrush { get; set; }
        public string? ThirdBrush { get; set; }
        public string? FourthBrush { get; set; }
        public string? FifthBrush { get; set; }
        public string? SixBrush { get; set; }
        public string? SevenBrush { get; set; }
        public string? EightBrush { get; set; }
        public string? NineBrush { get; set; }



        public string? FirstString { get; set; }
        public string? SecondString { get; set; }
        public string? ThirdString { get; set; }
        public string? FourthString { get; set; }
        public string? FifthString { get; set; }
        public string? SixString { get; set; }
        public string? SevenString { get; set; }
        public string? EightString { get; set; }
        public string? NineString { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                string s when FirstString != null && FirstString.Split('|').Contains(s) => FirstBrush,
                string f when SecondString != null && SecondString.Split('|').Contains(f) => SecondBrush,
                string t when ThirdString != null && ThirdString.Split('|').Contains(t) => ThirdBrush,
                string h when FourthString != null && FourthString.Split('|').Contains(h) => FourthBrush,
                string i when FifthString != null && FifthString.Split('|').Contains(i) => FifthBrush,
                string i when SixString != null && SixString.Split('|').Contains(i) => SixBrush,
                string i when SevenString != null && SevenString.Split('|').Contains(i) => SevenBrush,
                string i when EightString != null && EightString.Split('|').Contains(i) => EightBrush,
                string i when NineString != null && NineString.Split('|').Contains(i) => NineBrush,

                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
