using Litium.Websites;
using Litium.FieldFramework;
using Litium.Accelerator.Definitions;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;

namespace PandoNexis.Accelerator.Extensions.Definitions.Websites
{
    internal class AcceleratorWebsiteFieldDefinitionSetup : FieldDefinitionSetup
    {
        private readonly FieldHelper.FieldHelper _fieldHelper;

        public AcceleratorWebsiteFieldDefinitionSetup(FieldHelper.FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                _fieldHelper.GetWebsiteFieldDefinition(WebsiteFieldNameConstants.DefaultOpenGraphImage, SystemFieldTypeConstants.MediaPointerImage),
            };
            return fields;
        }
    }
}
