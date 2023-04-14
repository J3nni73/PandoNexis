using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Globalization;
using PandoNexis.AddOns.Extensions.PNStylingByChannel.Constants;

namespace PandoNexis.AddOns.Extensions.PNStylingByChannel.Definitions.Channels
{
    internal class ChannelFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<GlobalizationArea>(ChannelFieldNameConstants.AlternativeFavicon, SystemFieldTypeConstants.MediaPointerImage),
                new FieldDefinition<GlobalizationArea>(ChannelFieldNameConstants.AlternativeLogo, SystemFieldTypeConstants.MediaPointerImage),
                new FieldDefinition<GlobalizationArea>(ChannelFieldNameConstants.CssFileName, SystemFieldTypeConstants.Text)
                {
                    MultiCulture = false,
                },
            };
            return fields;
        }
    }
}
