using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using PandoNexis.AddOns.Extensions.PNCollectionPage;

namespace Litium.Accelerator.Definitions.Pages
{
    internal class ArticlePageTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var templates = new List<FieldTemplate>
            {
                new PageFieldTemplate(PageTemplateNameConstants.Article)
                {
                    IndexThePage = true,
                    TemplatePath = "",
                    FieldGroups = new []
                    {
                        new FieldTemplateFieldGroup()
                        {
                            Id = "General",
                            Collapsed = false,
                            Fields =
                            {
                                SystemFieldDefinitionConstants.Name,
                                SystemFieldDefinitionConstants.Url
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Contents",
                            Collapsed = false,
                            Fields =
                            {
                               PageFieldNameConstants.Title,
                               PageFieldNameConstants.Introduction,
                               PageFieldNameConstants.Text
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "List",
                            Collapsed = false,
                            Fields =
                            {
                                PageFieldNameConstants.Links
                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "Image",
                            Collapsed = false,
                            Fields =
                            {
                                PageFieldNameConstants.Image,
                                PageFieldNameConstants.AlternativeImageDescription,

                            }
                        },
                        new FieldTemplateFieldGroup()
                        {
                            Id = "FileList",
                            Collapsed = false,
                            Fields =
                            {
                                PageFieldNameConstants.Files
                            }
                        },
                          new FieldTemplateFieldGroup()
                        {
                            Id = CollectionPageFieldTemplateConstants.CollectionPageChildFieldGroup,
                            Collapsed = false,
                            Fields =
                            {
                                CollectionPageFieldNameConstants.CollectionPageTitle,
                                CollectionPageFieldNameConstants.CollectionPageDescription,
                                CollectionPageFieldNameConstants.CollectionPageImage,
                                CollectionPageFieldNameConstants.CollectionPageChildPageButtonText,
                                CollectionPageFieldNameConstants.CollectionFilterField1Value,
                                CollectionPageFieldNameConstants.CollectionFilterField2Value,
                                CollectionPageFieldNameConstants.CollectionFilterField3Value,

                            }
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
                },
            };
            return templates;
        }
    }
}
