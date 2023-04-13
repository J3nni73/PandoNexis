using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class ImageStatsBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public ImageStatsBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {
            GetBlockField(BlockTemplateNameConstants.ImageStatsBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
            GetBlockField(BlockTemplateNameConstants.ImageStatsBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle),
            GetBlockField(BlockTemplateNameConstants.ImageStatsBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle2),
            GetBlockField(BlockTemplateNameConstants.ImageStatsBlock, BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockImage),
            GetBlockField(BlockTemplateNameConstants.ImageStatsBlock,BlockFieldGroupNameConstants.Items, BlockFieldNameConstants.ImageStatsBlockItem),
        };

        return templateChanges;
    }


    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.ImageStatsBlock)
                {
                    CategorySystemId = blockCategoryId,
                    Icon = "fas fa-images",
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = BlockFieldGroupNameConstants.General,
                            Collapsed = false
                        },

                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false
                        }
                    }
              };
        return template;
    }
}

