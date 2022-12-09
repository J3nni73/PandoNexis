using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    /// <summary>
    ///     Extensions
    /// </summary>
    internal static class Extension
    {
        private static readonly Regex _numberRegexp = new Regex(@"[^0-9\.]", RegexOptions.Compiled);

        public static DateTime ConvertToDateTime(this string inputValue)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(inputValue, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeValue);
            return dateTimeValue;
        }
    }
}
