using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class UspBlockTemplateSetup : FieldTemplateSetup
{
    private readonly CategoryService _categoryService;
    public UspBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public override IEnumerable<FieldTemplate> GetTemplates()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var templates = new List<FieldTemplate>
            {
                new BlockFieldTemplate(BlockTemplateNameConstants.UspBlock)
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
                }
            };
        return templates;
    }
}

