using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;
using BlockTemplateNameConstants = PandoNexis.AddOns.Extensions.Block.Constants.BlockTemplateNameConstants;

internal class InfoTileBlockTemplateSetup : FieldTemplateHelper
{
    private readonly CategoryService _categoryService;
    public InfoTileBlockTemplateSetup(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
    {
        var templateChanges = new List<FieldTemplateChanges>
        {
            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.General,
                Field = SystemFieldDefinitionConstants.Name
            },
            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.General,
                Field = BlockFieldNameConstants.BlockTitle
            },
            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.General,
                Field = BlockFieldNameConstants.BlockSubTitle
            },

            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.General,
                Field = BlockFieldNameConstants.BlockText
            },

            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.General,
                Field = BlockFieldNameConstants.Background
            },





            new FieldTemplateChanges()
            {
                TemplateType = FieldTemplateHelperConstants.BlockFieldTemplate,
                DisplayTemplate = "",
                TemplateName = BlockTemplateNameConstants.InfoTileBlock,
                FieldGroupType = FieldTemplateHelperConstants.FieldTemplateFieldGroup,
                FieldGroupName = BlockFieldGroupNameConstants.Items,
                Field = BlockFieldNameConstants.InfoTileBlockItem
            }
        };

        templateChanges.AddRange(GetChangesForBlockExtendedCTA(BlockTemplateNameConstants.InfoTileBlock));
        return templateChanges;
    }

    public override FieldTemplate GetFieldTemplateNewTemplate()
    {
        var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

        var template = new BlockFieldTemplate(BlockTemplateNameConstants.InfoTileBlock)
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
                            Id=BlockFieldGroupNameConstants.CTA,
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {
                                BlockFieldNameConstants.InfoTileBlockItem,
                            }
                        }
                    }
        };
        return template;
    }
}

