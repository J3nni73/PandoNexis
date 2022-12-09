using Litium.FieldFramework;
using System.Collections.Generic;
using System.Globalization;
using Litium.FieldFramework.FieldTypes;

namespace PandoNexis.Accelerator.Extensions.Extensions
{
    public static class FieldFrameworkExtensions
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
        public static T GetValueOrDefault<T>(this IFieldFramework item, string id, CultureInfo culture = null)
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

        public static IList<string> GetTranslations(this IFieldDefinition field, CultureInfo culture = null)
        {
            var options = field.Option as TextOption;
            IList<string> textOptions = new List<string>();

            if (options?.Items == null)
                return textOptions;

            foreach (var option in options?.Items)
            {
                if (option == null)
                    continue;

                if (option.Name.TryGetValue(culture?.Name ?? CultureInfo.CurrentUICulture.Name, out string translation) && !string.IsNullOrEmpty(translation))
                    textOptions.Add(translation);
            }

            return textOptions;
        }
    }
}
