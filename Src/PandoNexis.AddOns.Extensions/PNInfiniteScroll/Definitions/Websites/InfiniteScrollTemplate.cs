using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.PNInfiniteScroll.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNInfiniteScroll.Definitions.Websites
{
    internal class InfiniteScrollTemplate : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetWebsiteField("AcceleratorWebsite", "ProductLists", PNInfiniteScrollConstants.PaginationType)
            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            return null;
        }
    }
}
