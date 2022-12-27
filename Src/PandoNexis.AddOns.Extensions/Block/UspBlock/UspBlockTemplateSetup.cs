using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class UspBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public UspBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {

            GetBlockField(BlockTemplateNameConstants.UspBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
           
            GetBlockField(BlockTemplateNameConstants.UspBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle),

            GetBlockField(BlockTemplateNameConstants.UspBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockSubTitle),

            GetBlockField(BlockTemplateNameConstants.UspBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockText),

            GetBlockField(BlockTemplateNameConstants.UspBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.Background),

            GetBlockField(BlockTemplateNameConstants.UspBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.UspBlockItem),
            

        };

        templateChanges.AddRange(GetChangesForBlockExtendedCTA(BlockTemplateNameConstants.UspBlock));

        return templateChanges;
    }
    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.UspBlock)
                {
                    CategorySystemId = blockCategoryId,
                    Icon = "fas fa-images",
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = BlockFieldGroupNameConstants.General,
                            Collapsed = false,
                            Fields =
                            {
                                SystemFieldDefinitionConstants.Name,
                                BlockFieldNameConstants.BlockTitle,
                                BlockFieldNameConstants.BlockSubTitle,
                                BlockFieldNameConstants.BlockText,
                                BlockFieldNameConstants.Background,
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.CTA,
                            Collapsed = false,
                            Fields =
                            {
                                 BlockFieldNameConstants.ExtendedLinkText,
                                 BlockFieldNameConstants.ExtendedLinkToPage,
                                 BlockFieldNameConstants.ExtendedLinkToCategory,
                                 BlockFieldNameConstants.ExtendedLinkToProduct,
                                 BlockFieldNameConstants.ExtendedLinkToFile,
                                 BlockFieldNameConstants.ExtendedLinkToExternalUrl,
                                 BlockFieldNameConstants.ExtendedClass,
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {
                                BlockFieldNameConstants.UspBlockItem,
                            }
                        }
                    }
              
            };
        return template;
    }
}

