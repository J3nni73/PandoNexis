using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.FieldFramework.FieldTypes;
using Litium.Accelerator.Definitions;
using PandoNexis.AddOns.Extensions.Constants;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView._Solution.Definitions.Websites
{
    internal class AcceleratorWebsiteFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_PageFieldNameConstants.QuotaCategory, SystemFieldTypeConstants.Pointer)
                {
                    Option = new PointerOption { EntityType = PointerTypeConstants.ProductsCategory }
                },
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.FootConverter, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.FreightConverter, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.UsdRate, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.Customs, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.SalespriceAddon, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.FobPercentage, SystemFieldTypeConstants.Decimal),
                new FieldDefinition<WebsiteArea>(Constants.GenericGridView_WebsiteFieldNameConstants.DdpPercentage, SystemFieldTypeConstants.Decimal),
            };
            return fields;
        }
    }
}
