using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.Globalization;
using Solution.Extensions.Constants;

namespace Solution.Extensions.Definitions.Websites
{
    internal class ChannelTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var template = new ChannelFieldTemplate(Solution.Extensions.Constants.ChannelTemplateIdConstants.TestTemplateID)
            {
                FieldGroups = new[]
                {
                    new FieldTemplateFieldGroup()
                    {
                        Id = "General",
                        Collapsed = false,
                        Fields =
                        {
                            ChannelTemplateIdConstants.TestField
                        }
                    },
                }
            };
            return new List<FieldTemplate>() { template };
        }
    }
}
