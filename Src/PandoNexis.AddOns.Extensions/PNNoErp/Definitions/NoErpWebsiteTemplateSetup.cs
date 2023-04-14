using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNNoErp.Constants;

namespace PandoNexis.AddOns.Extensions.PNNoErp.Definitions
{
    internal class AcceleratorWebsiteTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetWebsiteField("AcceleratorWebsite", NoErpOrderAdminConstants.NoErp, NoErpOrderAdminConstants.ButtonLinks),

            };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
           return null;
        }
    }
}
