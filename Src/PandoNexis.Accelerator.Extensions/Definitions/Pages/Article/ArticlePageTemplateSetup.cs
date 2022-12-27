using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using PandoNexis.Accelerator.Extensions.Definitions.FieldTemplateHelpers;

namespace PandoNexis.Accelerator.Extensions.Definitions.Pages.Article
{
    internal class ArticlePageTemplateSetup : FieldTemplateHelper
    {
        public override IEnumerable<FieldTemplateChanges> GetFieldTemplateFieldChanges()
        {
            var templateChanges = new List<FieldTemplateChanges>
            {
                GetPageField(PageTemplateNameConstants.Article, "General",SystemFieldDefinitionConstants.Name),
                GetPageField(PageTemplateNameConstants.Article, "General", SystemFieldDefinitionConstants.Url),
                GetPageField(PageTemplateNameConstants.Article, "Contents", PageFieldNameConstants.Title),
                GetPageField(PageTemplateNameConstants.Article, "Contents", PageFieldNameConstants.Introduction),
                GetPageField(PageTemplateNameConstants.Article, "Contents", PageFieldNameConstants.Text),
                GetPageField(PageTemplateNameConstants.Article, "List", PageFieldNameConstants.Links),
                GetPageField(PageTemplateNameConstants.Article, "Image", PageFieldNameConstants.Image),
                GetPageField(PageTemplateNameConstants.Article, "Image", PageFieldNameConstants.AlternativeImageDescription),
                GetPageField(PageTemplateNameConstants.Article, "FileList", PageFieldNameConstants.Files),
             };

            return templateChanges;
        }
            
        public override FieldTemplate GetFieldTemplateNewTemplate()
        {
            var template = new PageFieldTemplate(PageTemplateNameConstants.Article)
                {
                    IndexThePage = true,
                    TemplatePath = "",
                    FieldGroups = new []
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
                        },
                          
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
