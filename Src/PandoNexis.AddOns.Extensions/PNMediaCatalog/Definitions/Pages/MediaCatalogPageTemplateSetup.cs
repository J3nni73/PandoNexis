using Litium.Websites;
using Litium.FieldFramework;
using System.Collections.Generic;
using Litium.Accelerator.Constants;
using Litium.Accelerator.Definitions;

namespace PandoNexis.AddOns.Extensions.PNMediaCatalog.Definitions.Pages
{
    internal class MediaCatalogPageTemplateSetup : FieldTemplateSetup
    {
        public override IEnumerable<FieldTemplate> GetTemplates()
        {
            var templates = new List<FieldTemplate>
            {
                new PageFieldTemplate(Constants.PageTemplateNameConstants.MediaCatalog)
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
                            Id = "MediaFolder",
                            Collapsed = false,
                            Fields =
                            {
                                Constants.PageFieldNameConstants.MediaCatalogPointer,
                                Constants.PageFieldNameConstants.AlternativeFirstCatalogName,
                                Constants.PageFieldNameConstants.AlternativeFolderView,
                            }
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
                    },
                },
            };
            return templates;
        }
    }
}
