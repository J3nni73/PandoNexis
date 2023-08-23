using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Globalization;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.PNStylingByChannel.Constants;

namespace PandoNexis.AddOns.Extensions.PNStylingByChannel.Definitions.Channels
{
    internal class ChannelFieldDefinitionSetup : FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public ChannelFieldDefinitionSetup(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                _fieldHelper.GetGlobalizationFieldDefinition(ChannelFieldNameConstants.AlternativeFavicon, SystemFieldTypeConstants.MediaPointerImage),
                _fieldHelper.GetGlobalizationFieldDefinition(ChannelFieldNameConstants.AlternativeLogo, SystemFieldTypeConstants.MediaPointerImage),
                _fieldHelper.GetGlobalizationFieldDefinition(ChannelFieldNameConstants.CssFileName, SystemFieldTypeConstants.Text),
            };
            return fields;
        }
    }
}
