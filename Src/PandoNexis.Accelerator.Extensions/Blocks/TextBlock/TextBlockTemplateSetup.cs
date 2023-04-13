using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Constants;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{

    internal class TextBlockBlockTemplateSetup : FieldTemplateHelper
    {
        private readonly CategoryService _categoryService;
        public TextBlockBlockTemplateSetup(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetBlockField(BlockTemplateNameConstants.TextBlock,BlockFieldGroupNameConstants.General,SystemFieldDefinitionConstants.Name ),
                GetBlockField(BlockTemplateNameConstants.TextBlock,BlockFieldGroupNameConstants.Items,BlockFieldNameConstants.TextBlockItem ),
            };
            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var blockCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

            var template = new BlockFieldTemplate(BlockTemplateNameConstants.TextBlock)
            {
                CategorySystemId = blockCategoryId,
                Icon = "fas fa-images",
                FieldGroups = new[]
                    {
                       new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {

                            }
                        }
                    }
            };
            return template;
        }

    }
}