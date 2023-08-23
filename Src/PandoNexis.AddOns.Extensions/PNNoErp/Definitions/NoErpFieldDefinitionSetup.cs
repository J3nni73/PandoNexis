using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
{
    internal class NoErpFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public NoErpFieldDefinitionSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {

            var fields = new List<FieldDefinition>
            {
                _fieldHelper.GetCustomerFieldDefinition(NoErpOrderAdminConstants.Authorization, SystemFieldTypeConstants.Text, false, true, true),
                _fieldHelper.GetCustomerFieldDefinition(NoErpOrderAdminConstants.BaseUrl, SystemFieldTypeConstants.Text, false, true, true),
                _fieldHelper.GetWebsiteTextOptionField(NoErpOrderAdminConstants.NoErpButtonNames, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsiteMultiField(NoErpOrderAdminConstants.NoErpButtonLinks, SystemFieldTypeConstants.MultiField, true),
            };
            return fields;
        }
    }
}
