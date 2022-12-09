using PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(Name = SystemFieldTypeConstants.DateTime)]
    internal class DateTimeStringFieldTypeConverter : StringFieldTypeConverter
    {
        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            return value.ConvertToDateTime();
        }
    }
}
