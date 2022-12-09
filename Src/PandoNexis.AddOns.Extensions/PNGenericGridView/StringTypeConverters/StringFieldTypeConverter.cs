using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;


namespace PandoNexis.AddOns.Extensions.PNGenericGridView.StringTypeConverters
{
    [Service(ServiceType = typeof(StringFieldTypeConverter), NamedService = true)]
    public abstract class StringFieldTypeConverter
    {
        /// <summary>
        ///     Formats the specified field definition.
        /// </summary>
        /// <param name="fieldDefinitionId">The field id.</param>
        /// <param name="value">The field value.</param>
        /// <returns></returns>
        public abstract object ConvertFromString(string fieldDefinitionId, string value, IFieldDefinition fieldDefinition);
    }
}