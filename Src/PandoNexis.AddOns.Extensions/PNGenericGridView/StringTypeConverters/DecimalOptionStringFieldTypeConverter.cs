using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Runtime.DependencyInjection;
using System.Collections.Generic;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(Name = SystemFieldTypeConstants.DecimalOption)]
    internal class DecimalOptionStringFieldTypeConverter : StringFieldTypeConverter
    {


        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            if (value != null)
            {
                value = value.Replace(".", ",");
                var options = fieldDefinition.Option as DecimalOption;

                var values = value.Split(',');

                if (options != null && options.MultiSelect == false)
                {
                    var result = decimal.MinValue;
                    if (decimal.TryParse(value, out result))
                    {
                        return result;
                    }
                }
                else
                {
                    List<decimal> valueList = new List<decimal>();
                    foreach (var val in values)
                    {
                        var result = decimal.MinValue;
                        if (decimal.TryParse(val, out result))
                        {
                            valueList.Add(result);
                        }
                    }
                    return valueList;
                }
            }
            return null;
        }
    }
}
