using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class InspirationalBlockTemplateSetup : FieldTemplateSetup
{
    private readonly CategoryService _categoryService;
    public InspirationalBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    public override IEnumerable<FieldTemplate> GetTemplates()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var templates = new List<FieldTemplate>
            {
                new BlockFieldTemplate(BlockTemplateNameConstants.InspirationalBlock)
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
                                BlockFieldNameConstants.Background,
                                BlockFieldNameConstants.FullWidth
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {
                                BlockFieldNameConstants.InspirationalBlockItem,
                            }
                        }
                    }
                }
            };
        return templates;
    }
}

