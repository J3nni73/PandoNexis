using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class ColumnBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public ColumnBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {
           GetBlockField(BlockTemplateNameConstants.ColumnBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockSubTitle),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock,BlockFieldGroupNameConstants.General,BlockFieldNameConstants.BlockText),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock,BlockFieldGroupNameConstants.General,BlockFieldNameConstants.BlockText2),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock, BlockFieldGroupNameConstants.Summary, BlockFieldNameConstants.Summary),
           GetBlockField(BlockTemplateNameConstants.ColumnBlock, BlockFieldGroupNameConstants.Items, BlockFieldNameConstants.ColumnBlockItem),
        };

        templateChanges.AddRange(GetChangesForBlockExtendedCTA(BlockTemplateNameConstants.ColumnBlock));

        return templateChanges;
    }

    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.ColumnBlock)
        {
            CategorySystemId = blockCategoryId,
            Icon = "fas fa-images",
            FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = BlockFieldGroupNameConstants.General,
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = BlockFieldGroupNameConstants.Summary,
                            Collapsed = false,
                          
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.CTA,
                            Collapsed = false,
                          
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                        }
                    }
        };
        return template;
    }
}

