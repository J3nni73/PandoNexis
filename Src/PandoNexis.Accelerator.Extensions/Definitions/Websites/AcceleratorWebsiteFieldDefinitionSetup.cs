using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.FieldFramework.FieldTypes;
using Litium.Accelerator.Definitions;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.Accelerator.Extensions.Definitions.Websites
{
    internal class AcceleratorWebsiteFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<WebsiteArea>(WebsiteFieldNameConstants.DefaultOpenGraphImage, SystemFieldTypeConstants.MediaPointerImage)
                {
                    MultiCulture = false,
                },
            };
            return fields;
        }
    }
}
