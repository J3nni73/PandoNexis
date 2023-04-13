using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNStylingByChannel.Constants;

namespace PandoNexis.AddOns.Extensions.PNStylingByChannel.Definitions.Channels
{
    internal class ChannelTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetChannelField(ChannelTemplateIdConstants.DefaultChannelFieldTemplate,  "Alternative Styling",ChannelFieldNameConstants.CssFileName),
                GetChannelField(ChannelTemplateIdConstants.DefaultChannelFieldTemplate,  "Alternative Styling",ChannelFieldNameConstants.AlternativeLogo),
                GetChannelField(ChannelTemplateIdConstants.DefaultChannelFieldTemplate,  "Alternative Styling",ChannelFieldNameConstants.AlternativeFavicon),
            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
           return null;
        }
    }
}
