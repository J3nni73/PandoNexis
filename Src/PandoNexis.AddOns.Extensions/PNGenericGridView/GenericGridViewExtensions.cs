using PandoNexis.AddOns.Extensions.PNGenericGridView.Services;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Products;
using System.Globalization;
using PandoNexis.AddOns.Extensions.PNGenericGridView.Constants;
using Litium.Accelerator.Builders.Framework;
using Litium.Sales;
using Litium;
using Microsoft.OpenApi.Extensions;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView
{
    public static class GenericGridViewExtensions
    {
        private const decimal footConverter = 35.325M;
        private const decimal freightConverter = 90;
        private const decimal usdRate = 9.1M;
        private const decimal customs = 1.05M;
        private const decimal salespriceAddon = 1.25M;
        private const decimal storePriceAddon = 3;
        private const decimal fobPercentage = 1.15M;
        private const decimal ddpPercentage = 1.30M;
        private const decimal distPercentage = 1.55M;
        private static readonly FieldDefinitionService _fieldDefinitionService = IoC.Resolve<FieldDefinitionService>();
        private static readonly GenericGridViewService _genericGridViewService = IoC.Resolve<GenericGridViewService>();

        public static string GetTagNameForGridView(this FieldDefinition fieldDefinition)
        {
            if (fieldDefinition.FieldType == "SpecialField")
            {
                return _genericGridViewService.GetTagName(fieldDefinition.Id);
            }
            return _fieldDefinitionService.Get<ProductArea>(fieldDefinition.Id).GetTagNameForGridView();
        }

        public static string GetValue(this Variant variant, string fieldId, bool multiCulture, IFieldDefinition fieldDefinition)
        {
            if (variant == null || fieldId == null)
                return null;
            if (fieldDefinition.FieldType == Constants.GenericGridView_FieldTypesConstants.SpecialField)
            {
                return GetSpecialFieldValue(variant, fieldId);
            }
            else
            {
                var value = fieldDefinition.IsMultiCulture ? variant.Fields[fieldId, CultureInfo.CurrentUICulture] : variant.Fields[fieldId];

                if (value == null)
                {
                    value = multiCulture ? variant.Fields[fieldId, CultureInfo.CurrentUICulture] : variant.Fields[fieldId];
                }
                if (fieldDefinition.FieldType == "TextOption")
                {
                    if (value != null)
                    {
                        var options = fieldDefinition.Option as TextOption;
                        return options?.Items?.FirstOrDefault(i => i.Value == value.ToString())?.Name?.FirstOrDefault(i => i.Key == "sv-SE").Value ?? value.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }

                }
                if (value is decimal decimalValue)
                {
                    return decimalValue.ToString("0.##").Replace(',', '.');
                }
                if (value == null)
                    return null;

                return value.ToString();
            }

        }
        public static string ToString(this object obj)
        {
            if (obj is decimal decimalObj)
            {
                return decimalObj.ToString("0.##");
            }
            return obj.ToString();
        }

        public static string GetGridViewFieldType(this string fieldType)
        {
            switch (fieldType)
            {
                case SystemFieldTypeConstants.Boolean:
                    return "bool";
                case SystemFieldTypeConstants.Date:
                case SystemFieldTypeConstants.DateTime:
                    return "date";
                case SystemFieldTypeConstants.Decimal:
                case SystemFieldTypeConstants.DecimalOption:
                case SystemFieldTypeConstants.Long:
                    return "decimal";
                case SystemFieldTypeConstants.Int:
                case SystemFieldTypeConstants.IntOption:
                    return "int";
                case GenericGridView_ProductFieldNameConstants.ProductImageUpload:
                    return "productimageupload";
                default:
                    return "string";
            }
        }

        public static string GetSpecialFieldValue(Variant variant, string fieldId)
        {
            var result = string.Empty;

            var ctnCbm = variant.Fields.GetValue<decimal>(GenericGridView_ProductFieldNameConstants.CtnCbm);
            var cubicFoot = ctnCbm * footConverter;
            var qtyCnt = variant.Fields.GetValue<decimal>(GenericGridView_ProductFieldNameConstants.QtyCtn);
            var unitPriceUSD = variant.Fields.GetValue<decimal>(GenericGridView_ProductFieldNameConstants.UnitPriceUSD);
            var shipping = qtyCnt == decimal.Zero ? decimal.Zero : cubicFoot * freightConverter / qtyCnt;
            var exchangeRate = unitPriceUSD * usdRate;
            var shippingAndExchangeRate = shipping + exchangeRate;
            var cost = shippingAndExchangeRate * customs;
            var salesPrice = cost * salespriceAddon;

            switch (fieldId)
            {
                case Constants.GenericGridView_ProductFieldNameConstants.CubicFoot:

                    result = cubicFoot.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.Shipping:

                    result = shipping.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.ExchangeRate:

                    result = exchangeRate.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.ShippingAndExchangeRate:
                    result = shippingAndExchangeRate.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.Cost:
                    result = cost.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.FOB:
                    result = (unitPriceUSD * fobPercentage).ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.DDP:
                    result = (cost * ddpPercentage).ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.DIST:
                    result = (cost * distPercentage).ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.SellingPriceBP:
                    result = salesPrice.ToString("0.##").Replace(',', '.');
                    break;
                case Constants.GenericGridView_ProductFieldNameConstants.StoreX3:
                    result = (salesPrice * storePriceAddon).ToString("0.##").Replace(',', '.');
                    break;
            }

            return result;
        }
    }
}
