using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;
using PNFieldTemplateConstants = PandoNexis.Accelerator.Extensions.Constants.PageFieldTemplateConstants;

namespace Solution.Extensions.Definitions
{
    internal class ArticlePageTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "General",SystemFieldDefinitionConstants.Name),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "General", SystemFieldDefinitionConstants.Url),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "Contents", PageFieldNameConstants.Title),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "Contents", PageFieldNameConstants.Introduction),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "Contents", PageFieldNameConstants.Text),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "List", PageFieldNameConstants.Links),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "Image", PageFieldNameConstants.Image),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "Image", PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(PNFieldTemplateConstants.ArticleWithoutLeftMenu, "FileList", PageFieldNameConstants.Files),
              };

            return templateChanges;
        }

        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(PNFieldTemplateConstants.ArticleWithoutLeftMenu)
            {
                IndexThePage = true,
                TemplatePath = "",
                FieldGroups = new[]
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Contents",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "List",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Image",
                            Collapsed = false
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "FileList",
                            Collapsed = false
                        }
                    },
                Containers = new List<BlockContainerDefinition>
                    {
                        new BlockContainerDefinition()
                        {
                            Id = BlockContainerNameConstant.Header,
                            Name =
                            {
                                ["sv-SE"] = "Huvud",
                                ["en-US"] = "Header",
                            }
                        }
                    }
            };
            return template;
        }
    }
}
