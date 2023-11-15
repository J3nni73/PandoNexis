using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using Litium.FieldFramework.FieldTypes;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
using PandoNexis.AddOns.Extensions.Block.Constants;

namespace PandoNexis.AddOns.Extensions.PNBlock.Definitions
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
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.Summary, SystemFieldTypeConstants.Editor, true),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockImage, SystemFieldTypeConstants.MediaPointerImage),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockMobileImage, SystemFieldTypeConstants.MediaPointerImage),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockOverlayImage, SystemFieldTypeConstants.MediaPointerImage),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockOverlayLeft, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.QuoteStyle, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.GradiantOverlay, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.CenteredText, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.NegativeMargin, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockTitle2, SystemFieldTypeConstants.Text, true),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockSubTitle2, SystemFieldTypeConstants.Text, true),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.BlockText2, SystemFieldTypeConstants.Text, true),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.YoutubeCode, SystemFieldTypeConstants.Text, true),

               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.Reverse, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.FullWidth, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockFieldDefinition(BlockFieldNameConstants.UseFullWidthBackgroundColor, SystemFieldTypeConstants.Boolean),
               _fieldHelper.GetBlockTextOptionField(BlockFieldNameConstants.Background, SystemFieldTypeConstants.TextOption),
               
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.InspirationalBlockItem, SystemFieldTypeConstants.MultiField, true),
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.ColumnBlockItem, SystemFieldTypeConstants.MultiField, true),
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.YoutubeBlockItem, SystemFieldTypeConstants.MultiField, true),
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.InfoTileBlockItem, SystemFieldTypeConstants.MultiField, true),
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.UspBlockItem, SystemFieldTypeConstants.MultiField, true),
                _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.ImageStatsBlockItem, SystemFieldTypeConstants.MultiField, true),
                //removed this, isn't used _fieldHelper.GetBlockMultiField(BlockFieldNameConstants.HeroBlockItem, SystemFieldTypeConstants.MultiField),

               
            };
            return fields;
        }
    }
}
