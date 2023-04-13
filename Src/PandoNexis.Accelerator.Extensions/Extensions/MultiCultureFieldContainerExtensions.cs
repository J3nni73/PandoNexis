using Litium.FieldFramework;
using System.Globalization;

namespace PandoNexis.Accelerator.Extensions.Extensions
{
    public static class MultiCultureFieldContainerExtensions
    {
        /// <summary>
        /// Tries to get value 3 times. 
        /// First try, with current culture.
        /// Second try, with en-US culture.
        /// Third try, with no culture. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this MultiCultureFieldContainer item, string id, CultureInfo culture = null)
        {
            var value = default(T);

            value = item.GetValue<T>(id, culture ?? CultureInfo.CurrentUICulture);
            if (!EqualityComparer<T>.Default.Equals(value, default))
            {
                if (value is string s)
                {
                    if (!string.IsNullOrEmpty(s))
                        return value;
                }
                else
                    return value;
            }

            value = item.GetValue<T>(id, new CultureInfo("en-US"));
            if (!EqualityComparer<T>.Default.Equals(value, default))
            {
                if (value is string s)
                {
                    if (!string.IsNullOrEmpty(s))
                        return value;
                }
                else
                    return value;
            }

            return item.GetValue<T>(id);
        }
    }
}
