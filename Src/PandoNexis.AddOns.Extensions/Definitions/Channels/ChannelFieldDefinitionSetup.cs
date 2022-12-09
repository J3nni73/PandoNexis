using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.FieldFramework.FieldTypes;
using Litium.Accelerator.Definitions;
using PandoNexis.AddOns.Extensions.Constants;
using Litium.Globalization;

namespace PandoNexis.AddOns.Extensions.Definitions.Websites
{
    internal class ChannelFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<GlobalizationArea>(ChannelTemplateIdConstants.TestField, SystemFieldTypeConstants.Text),

            };
            return fields;
        }
    }
}
