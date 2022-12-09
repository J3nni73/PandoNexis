using Litium.Accelerator.Definitions;
using Litium.Blocks;
using Litium.FieldFramework;
using PandoNexis.Accelerator.Extensions.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.Accelerator.Extensions.Blocks.TextBlock
{

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
                new BlockFieldTemplate(BlockTemplateNameConstants.TextBlock)
                {
                    CategorySystemId = blockCategoryId,
                    Icon = "fas fa-images",
                    FieldGroups = new []
                    {
                       new FieldTemplateFieldGroup()
                        {
                            Id=BlockFieldGroupNameConstants.Items,
                            Collapsed = false,
                            Fields =
                            {
                                BlockFieldNameConstants.TextBlockItem,
                            }
                        }
                    }
                }
            };
            return templates;
        }
    }
}