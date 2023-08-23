using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Products;

namespace PandoNexis.Accelerator.Extensions.Definitions.Products
{
    internal class ProductsFieldDefinitionSetup : FieldDefinitionSetup
    {
        public readonly FieldHelper.FieldHelper _fieldHelper;

        public ProductsFieldDefinitionSetup(FieldHelper.FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new List<FieldDefinition>
            {
                _fieldHelper.GetProductFieldDefinition(Constants.ProductFieldNameConstants.DescriptionExtended,SystemFieldTypeConstants.Editor, true ),
                _fieldHelper.GetProductFieldDefinition(Constants.ProductFieldNameConstants.ProductType,SystemFieldTypeConstants.TextOption ),
                _fieldHelper.GetProductFieldDefinition(Constants.ProductFieldNameConstants.InventoryStatus,SystemFieldTypeConstants.TextOption ),
            };

            return fields;
        }
    }

}
