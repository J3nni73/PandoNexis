using Litium.Blocks;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;

using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;

using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.AddOns.Extensions.Block.Constants;
using PNABlockFieldNameConstants = PandoNexis.Accelerator.Extensions.Constants.BlockFieldNameConstants;
using BlockFieldNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockFieldNameConstants;

namespace PandoNexis.AddOns.Extensions.PNBlock.Definitions
{
    public class FieldHelperSource : FieldHelper
    {
        public FieldHelperSource(FieldDefinitionService fieldDefinitionService, SecurityContextService securityContextService, LanguageService languageService) : base(fieldDefinitionService, securityContextService, languageService)
        {
        }

        public override void HandleFieldOptions()
        {
            var changes = new List<FieldOptionChanges>()
            {
                GetFieldOption(nameof(BlockArea), PNABlockFieldNameConstants.Background, PNABlockFieldNameConstants.primaryColor),
                GetFieldOption(nameof(BlockArea), PNABlockFieldNameConstants.Background, PNABlockFieldNameConstants.secondaryColor),
                GetFieldOption(nameof(BlockArea), PNABlockFieldNameConstants.Background, PNABlockFieldNameConstants.thirdColor),
            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockSubTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockImage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockVideo),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockSubTitle2),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.BlockText2),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.NegativeMargin),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.FullWidth),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.Reverse),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.Background),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToPage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToCategory),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToProduct),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToProductList),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToFile),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToYouTube),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedLinkToExternalUrl),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InspirationalBlockItem, BlockFieldNameConstants.ExtendedClass),

               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ColumnBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ColumnBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ColumnBlockItem, BlockFieldNameConstants.BlockImage),

               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.YoutubeBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.YoutubeBlockItem, BlockFieldNameConstants.YoutubeCode),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.YoutubeBlockItem, BlockFieldNameConstants.BlockText),




               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.BlockImage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.BlockText2),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToPage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToCategory),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToProduct),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToProductList),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToFile),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToYouTube),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedLinkToExternalUrl),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.InfoTileBlockItem, BlockFieldNameConstants.ExtendedClass),

               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.BlockImage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.Background),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToPage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToCategory),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToProduct),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToProductList),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToFile),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToYouTube),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedLinkToExternalUrl),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.UspBlockItem, BlockFieldNameConstants.ExtendedClass),

               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ImageStatsBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ImageStatsBlockItem, BlockFieldNameConstants.BlockText2),


               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.ImageStatsBlockItem, BlockFieldNameConstants.BlockText2),


            };

            UpdateMultiFieldField(changes);
        }
    }
}
