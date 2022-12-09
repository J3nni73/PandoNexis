using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using System.Drawing;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Definitions.Websites
{
    internal class AcceleratorWebsiteTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var template = new WebsiteFieldTemplate("AcceleratorWebsite")
            {
                FieldGroups = new[]
                {
                    new FieldTemplateFieldGroup()
                    {
                        Id = "Quota",
                        Collapsed = false,
                        Fields =
                        {
                            Constants.PageFieldNameConstants.QuotaCategory
                        }
                    }
                }
            };
            return new List<FieldTemplate>() { template };
        }
    }
}
