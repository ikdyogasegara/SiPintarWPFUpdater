using System;
using System.Globalization;
using System.Windows.Data;

namespace SiPintar.Converters
{
    public class StatusPermohonanToBrushConverter : IValueConverter
    {
        public bool SpkPerencanaan { get; set; }

        private readonly string BelumSpk = "#FFFFFF";
        private readonly string SudahSpk = "#F7ECD8";
        private readonly string SudahRab = "#DDCCF4";
        private readonly string SudahBa = "#CCEDFF";

        private readonly string LblBelumSpk = "#1E7B49";
        private readonly string LblSudahSpk = "#BC722D";
        private readonly string LblSudahRab = "#B263D7";
        private readonly string LblSudahBa = "#3CB1F2";

        private readonly string BgBelumSpk = "#C4FBD0";
        private readonly string BgSudahSpk = "#F7ECD8";
        private readonly string BgSudahRab = "#DDCCF4";
        private readonly string BgSudahBa = "#CCEDFF";

        public bool isLabel { get; set; }
        public bool isBackground { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Data = value as int?;
            if (isLabel)
            {
                if (SpkPerencanaan)
                {
                    return Data < 3 ? LblBelumSpk : LblSudahRab;
                }
                else
                {
                    return Data switch
                    {
                        3 => LblSudahBa,
                        2 => LblSudahRab,
                        1 => LblSudahSpk,
                        _ => LblBelumSpk
                    };
                }
            }
            else if (isBackground)
            {
                if (SpkPerencanaan)
                {
                    return Data < 3 ? BgBelumSpk : BgSudahRab;
                }
                else
                {
                    return Data switch
                    {
                        3 => BgSudahBa,
                        2 => BgSudahRab,
                        1 => BgSudahSpk,
                        _ => BgBelumSpk
                    };
                }
            }
            else
            {
                if (SpkPerencanaan)
                {
                    return Data < 3 ? BelumSpk : SudahRab;
                }
                else
                {
                    return Data switch
                    {
                        3 => SudahBa,
                        2 => SudahRab,
                        1 => SudahSpk,
                        _ => BelumSpk
                    };
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
