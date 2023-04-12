using Litium.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litium.Runtime.DependencyInjection;
using System.Globalization;
using System.Text.RegularExpressions;
using Litium.Common;

namespace PandoNexis.Accelerator.Extensions.Extensions
{
    public static class CategoryExtensions
    {
        public static string DescriptionExtended(this Category category)
        {
            return category.DescriptionExtended(CultureInfo.CurrentCulture);
        }

        static string DescriptionExtended(this Category category, CultureInfo culture)
        {
            var result = new StringBuilder();

            result.Append("<p>");
            result.Append(category.Fields.GetValue<string>(Constants.ProductFieldNameConstants.Description, culture));
            result.Append("</p>");
            result.Append(category.Fields.GetValue<string>(Constants.ProductFieldNameConstants.DescriptionExtended, culture));

            return result.ToString();
        }

        public static string GetDisplayName(this Category category)
        {
            return category.GetDisplayName(CultureInfo.CurrentCulture);
        }
        public static string GetDisplayName(this Category category, CultureInfo culture)
        {
            return category.Localizations[culture]?.Name ?? string.Empty;
        }

    }
}
