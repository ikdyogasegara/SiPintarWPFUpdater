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
    public static class IntegerValidationHelper
    {
        public static void IntegerValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
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
                Obj.Text = string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,###}", int.Parse(Obj.Text.Replace(".", "")));
        }

        public static string FormatStringId(string Param)
        {
            if (string.IsNullOrWhiteSpace(Param))
                return "";
            return string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,###}", int.Parse(Param.Replace(".", "")));
        }

        public static int FormatStringIdToInteger(string Param)
        {
            if (string.IsNullOrWhiteSpace(Param))
                return 0;
            return int.Parse(Param.Replace(".", ""));
        }
        public static string FormatIntegerToStringId(int? Param)
        {
            if (Param is null || Param is 0)
                return "0";
            return string.Format(CultureInfo.GetCultureInfo("id-ID"), "{0:#,###}", Param);
        }
    }
}
