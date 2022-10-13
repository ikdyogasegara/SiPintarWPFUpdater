using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiPintar.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class UniqueNumberHelper
    {
        public static string Generate(string name = "no", DateTime? dateTime = null, int increment = 1)
        {
            return $"{Convert.ToString(increment, CultureInfo.InvariantCulture).PadLeft(3, '0')}/{name.ToUpper(CultureInfo.CurrentCulture)}/{NumberToRomanHelper.getRomanNumber(dateTime.Value.Month)}/{dateTime.Value.Year}";
        }
    }
}
