using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SiPintar.Helpers
{
    //excude because have UI Part
    [ExcludeFromCodeCoverage]
    public static class DecimalValidationHelper
    {
        public static void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void Object_GotFocus(object sender, RoutedEventArgs e)
        {
            var Obj = sender as TextBox;
            Obj.Text = Obj.Text.Replace(".", "");
        }

        public static void Object_LostFocus(object sender, RoutedEventArgs e)
        {
            var Obj = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(Obj.Text))
            {
                try
                {
                    Obj.Text = string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,##0.##}", decimal.Parse(Obj.Text, CultureInfo.GetCultureInfo("id-ID")));
                }
                catch
                {
                    Obj.Text = "0";
                }
            }
        }

        public static string FormatStringId(string Param)
        {
            if (string.IsNullOrWhiteSpace(Param))
                return "";
            return string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,###.##}", decimal.Parse(Param, CultureInfo.GetCultureInfo("id-ID")));
        }

        public static decimal FormatStringIdToDecimal(string Param)
        {
            if (string.IsNullOrWhiteSpace(Param))
                return decimal.Zero;
            return decimal.Parse(Param, CultureInfo.GetCultureInfo("id-ID"));
        }
        public static string FormatDecimalToStringId(decimal? Param = null)
        {
            if (Param is null || Param is decimal.Zero)
                return "0";
            return string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,###.##}", Param);
        }
    }
}
