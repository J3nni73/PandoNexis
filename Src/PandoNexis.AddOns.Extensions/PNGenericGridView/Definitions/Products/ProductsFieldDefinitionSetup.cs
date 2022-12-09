using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using Litium.Products;

namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Definitions.Products
{
    internal class ProductsFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended, SystemFieldTypeConstants.Editor)
                {
                    CanBeGridColumn = false,
                    CanBeGridFilter = false,
                    MultiCulture = true,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.Constants.ProductFieldNameConstants.ProductType, SystemFieldTypeConstants.TextOption)
                {
                    CanBeGridColumn = false,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.AddOns.Extensions.Constants.ProductFieldNameConstants.InventoryStatus, SystemFieldTypeConstants.TextOption)
                {
                    CanBeGridColumn = false,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                }
            };

            return fields;
        }
    }

}
