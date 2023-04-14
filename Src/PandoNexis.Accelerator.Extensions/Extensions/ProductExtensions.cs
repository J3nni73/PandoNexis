using Litium.Products;
using System.Text;
using Litium.Runtime.DependencyInjection;
using System.Globalization;

namespace PandoNexis.Accelerator.Extensions.Extensions
{
    public static class ProductExtensions
    {
        private static readonly LazyService<VariantService> _variantService = new LazyService<VariantService>();
        private static readonly LazyService<BaseProductService> _baseProductService = new LazyService<BaseProductService>();
        public static string DescriptionExtended(this Variant variant)
        {
            var baseProduct = _baseProductService.Value.Get(variant.BaseProductSystemId);
            return baseProduct.DescriptionExtended(variant);
        }
        public static string DescriptionExtended(this BaseProduct baseProduct)
        {
            var result = new StringBuilder();
            result.Append("<p>");
            result.Append(baseProduct.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description, CultureInfo.CurrentCulture));
            result.Append("</p>");
            result.Append(baseProduct.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended, CultureInfo.CurrentCulture));
            return result.ToString();
        }
        static string DescriptionExtended(this BaseProduct baseProduct, Variant variant)
        {
            var result = new StringBuilder();
            if (variant != null)
            {
                result.Append("<p>");
                result.Append(variant.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description, CultureInfo.CurrentCulture));
                result.Append("</p>");
                result.Append(variant.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended, CultureInfo.CurrentCulture));
            }

            result.Append("<p>");
            result.Append(baseProduct.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.Description, CultureInfo.CurrentCulture));
            result.Append("</p>");
            result.Append(baseProduct.Fields.GetValue<string>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended, CultureInfo.CurrentCulture));

            return result.ToString();
        }
    }
}
