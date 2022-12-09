using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Runtime.DependencyInjection;
using System.Linq;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(Name = SystemFieldTypeConstants.TextOption)]
    internal class TextOptionStringFieldTypeConverter : StringFieldTypeConverter
    {
        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            var values = value.Split(',');
            var options = fieldDefinition.Option as TextOption;
            if (options != null && options.MultiSelect == false)
            {
                return value;
            }
            return values.ToList();
        }

    }
}
