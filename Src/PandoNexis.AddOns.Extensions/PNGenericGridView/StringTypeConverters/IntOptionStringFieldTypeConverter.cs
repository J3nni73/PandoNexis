using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Runtime.DependencyInjection;
using System.Collections.Generic;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(Name = SystemFieldTypeConstants.IntOption)]
    internal class IntOptionStringFieldTypeConverter : StringFieldTypeConverter
    {
        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            if (value != null)
            {
                var options = fieldDefinition.Option as IntOption;

                var values = value.Split(',');

                if (options != null && options.MultiSelect == false)
                {
                    int result;
                    if (int.TryParse(value, out result))
                    {
                        return result;
                    }
                }
                else
                {
                    List<int> valueList = new List<int>();
                    foreach (var val in values)
                    {
                        int result;
                        if (int.TryParse(val, out result))
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
