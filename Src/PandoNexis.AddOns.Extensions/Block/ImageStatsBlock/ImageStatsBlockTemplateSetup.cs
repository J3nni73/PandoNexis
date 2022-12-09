using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class ImageStatsBlockTemplateSetup : FieldTemplateSetup
{
    private readonly CategoryService _categoryService;
    public ImageStatsBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public override IEnumerable<FieldTemplate> GetTemplates()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var templates = new List<FieldTemplate>
            {
                new BlockFieldTemplate(BlockTemplateNameConstants.ImageStatsBlock)
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
                                BlockFieldNameConstants.BlockTitle2,
                                BlockFieldNameConstants.BlockImage,
                            }
                        },

                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {
                                BlockFieldNameConstants.ImageStatsBlockItem,
                            }
                        }
                    }
                }
            };
        return templates;
    }
}

