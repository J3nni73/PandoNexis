using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class HeroBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public HeroBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockTitle2),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockSubTitle),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockText),

            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockImage),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockMobileImage),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockOverlayImage),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.BlockOverlayLeft),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.FullWidth),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.GradiantOverlay),
            GetBlockField(BlockTemplateNameConstants.HeroBlock,BlockFieldGroupNameConstants.General, BlockFieldNameConstants.Background),
        };

        templateChanges.AddRange(GetChangesForBlockExtendedCTA(BlockTemplateNameConstants.HeroBlock));

        return templateChanges;
    }


    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.HeroBlock)
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
                            Id=BlockFieldGroupNameConstants.CTA,
                            Collapsed = false
                        },
                    }
                
            };
        return template;
    }
}

