using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;

namespace PandoNexis.Accelerator.Extensions.Block.FieldDefinitions
{
    internal class BlockFieldDefinitions : FieldDefinitionSetup
    {
        public readonly FieldHelper _fieldHelper;

        public BlockFieldDefinitions(FieldHelper fieldHelper)
        {
            _fieldHelper = fieldHelper;
        }

        public override IEnumerable<FieldDefinition> GetFieldDefinitions()
        {
            var fields = new[]
            {
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockTitle, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockSubTitle, SystemFieldTypeConstants.Text, true),            
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockText, SystemFieldTypeConstants.Editor, true),
                _fieldHelper.GetBlockTextOptionField(BlockFieldNameConstants.Background, SystemFieldTypeConstants.TextOption),
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.ExtendedLinkText, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetBlockPointerField(BlockFieldNameConstants.ExtendedLinkToPage, SystemFieldTypeConstants.Pointer, PointerTypeConstants.WebsitesPage),
                _fieldHelper.GetBlockPointerField(BlockFieldNameConstants.ExtendedLinkToCategory, SystemFieldTypeConstants.Pointer, PointerTypeConstants.ProductsCategory),
                _fieldHelper.GetBlockPointerField(BlockFieldNameConstants.ExtendedLinkToProduct, SystemFieldTypeConstants.Pointer, PointerTypeConstants.ProductsProduct),
                _fieldHelper.GetBlockPointerField(BlockFieldNameConstants.ExtendedLinkToProductList, SystemFieldTypeConstants.Pointer, PointerTypeConstants.ProductsProductList),
                _fieldHelper.GetBlockPointerField(BlockFieldNameConstants.ExtendedLinkToFile, SystemFieldTypeConstants.Pointer, PointerTypeConstants.MediaFile),
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.ExtendedLinkToYouTube, SystemFieldTypeConstants.Text),
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.ExtendedLinkToExternalUrl, SystemFieldTypeConstants.Text, true),            
                _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.ExtendedButtonSubText, SystemFieldTypeConstants.Text, true),
                _fieldHelper.GetBlockTextOptionField(BlockFieldNameConstants.ExtendedClass, SystemFieldTypeConstants.TextOption),                
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.TextBlockItem, SystemFieldTypeConstants.MultiField),
            };
            return fields;
        }
    }
}
