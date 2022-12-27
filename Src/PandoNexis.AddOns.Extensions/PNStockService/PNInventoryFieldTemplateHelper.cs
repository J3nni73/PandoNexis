using Litium.FieldFramework;
using Litium.Products;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Services.PNStockService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNStockService
{
    internal class PNInventoryFieldTemplateHelper : FieldTemplateHelper
    {   private readonly DisplayTemplateService _displayTemplateService;

        public PNInventoryFieldTemplateHelper(DisplayTemplateService displayTemplateService)
        {
            _displayTemplateService = displayTemplateService;
        }

        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>()
            {
                GetProductField("ProductWithVariants",FieldTemplateHelperConstants.ProductFieldGroups, "General",PNInventoryConstants.IgnoreInventory),
            }; 
      

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {

            return null;
        }
    }
}
