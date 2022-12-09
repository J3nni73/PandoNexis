using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;
using Litium.FieldFramework;
using Litium.Websites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandoNexis.AddOns.Extensions.PNCollectionPage
{
    internal class CollectionPageFieldTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var templates = new List<FieldTemplate>
            {
                new PageFieldTemplate(CollectionPageFieldTemplateConstants.CollectionPage)
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
                            Id = CollectionPageFieldTemplateConstants.CollectionPageParentFieldGroup,
                            Collapsed = false,
                            Fields =
                            {
                                CollectionPageFieldNameConstants.CollectionPageUseFilter,
                                CollectionPageFieldNameConstants.CollectionFilterField1Name,
                                CollectionPageFieldNameConstants.CollectionFilterField2Name,
                                CollectionPageFieldNameConstants.CollectionFilterField3Name,
                                CollectionPageFieldNameConstants.CollectionPageParentPageButtonText,

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
                        },
                         new BlockContainerDefinition()
                        {
                            Id = BlockContainerNameConstant.Main,
                            Name =
                            {
                                ["sv-SE"] = "Fot",
                                ["en-US"] = "Footer",
                            }
                        }
                    }
                },
            };
            return templates;
        }
    }
}
