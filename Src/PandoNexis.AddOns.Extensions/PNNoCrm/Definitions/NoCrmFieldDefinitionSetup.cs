using Litium.Customers;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNNoCrm.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoCrm.Definitions
{
    internal class NoCrmFieldDefinitionSetup : Litium.Accelerator.Definitions.FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public NoCrmFieldDefinitionSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                _fieldHelper.GetWebsiteTextOptionField(NoCrmProcessorConstants.NoCrmButtonNames, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetWebsiteMultiField(NoCrmProcessorConstants.NoCrmButtonLinks, SystemFieldTypeConstants.MultiField, true),
				_fieldHelper.GetWebsiteMultiField(NoCrmProcessorConstants.Body, SystemFieldTypeConstants.Editor),
            };
            return fields;
        }
    }
}
