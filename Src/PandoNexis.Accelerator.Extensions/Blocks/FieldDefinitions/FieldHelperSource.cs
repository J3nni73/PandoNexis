using Litium.Blocks;
using Litium.FieldFramework;
using Litium.Globalization;
using Litium.Security;
using Litium.Websites;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldHelper;
namespace PandoNexis.Accelerator.Extensions.Blocks.FieldDefinitions
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
                GetFieldOption(nameof(BlockArea), BlockFieldNameConstants.ExtendedClass, BlockFieldNameConstants.primaryColor),
                GetFieldOption(nameof(BlockArea), BlockFieldNameConstants.ExtendedClass, BlockFieldNameConstants.secondaryColor),
                GetFieldOption(nameof(BlockArea), BlockFieldNameConstants.ExtendedClass, BlockFieldNameConstants.thirdColor),
            };

            UpdateFieldOptions(changes);


        }
        public override void HandleMultiFieldFields()
        {
            var changes = new List<FieldMultiFieldChanges>()
            {
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.BlockTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.BlockSubTitle),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.BlockText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.Background),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkText),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToPage),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToCategory),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToProduct),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToProductList),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToFile),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToYouTube),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedLinkToExternalUrl),
               GetMultiFieldChange(nameof(BlockArea), BlockFieldNameConstants.TextBlockItem, BlockFieldNameConstants.ExtendedClass),
            };

            UpdateMultiFieldField(changes);
        }
    }
}
