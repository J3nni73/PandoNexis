using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class InspirationalBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public InspirationalBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {
           GetBlockField(BlockTemplateNameConstants.InspirationalBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
           GetBlockField(BlockTemplateNameConstants.InspirationalBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.Background),
           GetBlockField(BlockTemplateNameConstants.InspirationalBlock,BlockFieldGroupNameConstants.Items, BlockFieldNameConstants.InspirationalBlockItem),

        };


        return templateChanges;
    }

    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.InspirationalBlock)
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
                            Collapsed = false,
                        }
                    }
                
            };
        return template;
    }
}

