using System;
using Litium.Blocks;
using Litium.FieldFramework;
using System.Collections.Generic;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PandoNexis.AddOns.Extensions.Block.Constants;

namespace Litium.Accelerator.Definitions.Blocks.BlockBanner
{
    internal class BlockBannerTemplateSetup : FieldTemplateHelper
    {
        private readonly CategoryService _categoryService;

        public BlockBannerTemplateSetup(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
        {
           GetBlockField(BlockTemplateNameConstants.BannerBlock,BlockFieldGroupNameConstants.General, SystemFieldDefinitionConstants.Name),
           GetBlockField(BlockTemplateNameConstants.BannerBlock,BlockFieldGroupNameConstants.Items, BlockFieldNameConstants.BannerBlockItems),
        };


            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var pageCategoryId = _categoryService.Get(BlockCategoryNameConstants.Pages)?.SystemId ?? Guid.Empty;

            var template =
                new BlockFieldTemplate(BlockTemplateNameConstants.BannerBlock)
                {
                    CategorySystemId = pageCategoryId,
                    Icon = "fas fa-image",
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Banners",
                            Collapsed = false,
                        }
                    }
            };
            return template;
        }
      
    }
}
