using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(Name = SystemFieldTypeConstants.Decimal)]
    internal class DecimalStringFieldTypeConverter : StringFieldTypeConverter
    {
        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            if (value != null)
            {
                var result = decimal.MinValue;
                value = value.Replace(".", ",");
                if (decimal.TryParse(value, out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
