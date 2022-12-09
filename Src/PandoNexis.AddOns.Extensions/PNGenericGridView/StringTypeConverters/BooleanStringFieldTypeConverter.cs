using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{

    [Service(Name = SystemFieldTypeConstants.Boolean)]
    internal class BooleanStringFieldTypeConverter : StringFieldTypeConverter
    {
        public override object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition)
        {
            if (value != null)
            {
                if (value == "1")
                    return true;

                bool result;
                if (bool.TryParse(value, out result))
                {
                    return result;
                }
            }
            return false;
        }
    }
}
