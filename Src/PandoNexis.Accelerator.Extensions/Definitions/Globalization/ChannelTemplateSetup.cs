using Litium.FieldFramework;
using Litium.Globalization;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.Accelerator.Extensions.Constants;

namespace PandoNexis.AddOns.Extensions.PNWebsiteSelector.Definitions.Channels
{
    internal class ChannelTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetChannelField(ChannelTemplateIdConstants.DefaultChannelFieldTemplate,  "General",SystemFieldDefinitionConstants.Name),
            };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new ChannelFieldTemplate(ChannelTemplateIdConstants.DefaultChannelFieldTemplate)
            {                
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Website",
                            Collapsed = false,
                        },
                    },
            };
            return template;
        }
    }
}
