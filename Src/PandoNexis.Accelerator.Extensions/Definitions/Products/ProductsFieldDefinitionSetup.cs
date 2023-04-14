using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Products;

namespace PandoNexis.Accelerator.Extensions.Definitions.Products
{
    internal class ProductsFieldDefinitionSetup : FieldDefinitionSetup
    {
        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                new FieldDefinition<ProductArea>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.DescriptionExtended, SystemFieldTypeConstants.Editor)
                {
                    CanBeGridColumn = false,
                    CanBeGridFilter = false,
                    MultiCulture = true,
                },
                new FieldDefinition<ProductArea>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.ProductType, SystemFieldTypeConstants.TextOption)
                {
                    CanBeGridColumn = false,
                    CanBeGridFilter = false,
                    MultiCulture = false,
                },
                new FieldDefinition<ProductArea>(PandoNexis.Accelerator.Extensions.Constants.ProductFieldNameConstants.InventoryStatus, SystemFieldTypeConstants.TextOption)
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
